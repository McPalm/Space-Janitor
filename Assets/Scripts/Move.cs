using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Move : MonoBehaviour
{

    public UnityEvent OnStep;

    public Transform Camera;
    public CharacterController CharacterController;
    public float speed = 5f;
    public float crouchSpeed = 2f;
    public float acceleration = .2f;
    Vector3 lastmove;

    public float crouch;

    public float crouchDelta = .5f;

    Vector3 cameraStartPosition;

    float CalculatedSpeed => Mathf.Lerp(speed, crouchSpeed, crouch);

    // Update is called once per frame

    public float bobProgression = 0f;
    public float bobMagnitude = 0f;

    float walkDuration = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //lock dat cursor
        // Application.
        cameraStartPosition = Camera.localPosition;
    }

    void Update() //Noticed a stutter I think was due to using Fixed Update So I changed this to Update. 
                  //Should be a fine like this since you're using Dt though.
    {
        var direction = GetInputTranslationDirection();
        direction.y = 0f;
        direction = Vector3.ClampMagnitude(direction, 1f);
        if (direction.sqrMagnitude > .1f)
            walkDuration += Time.deltaTime;
        else
            walkDuration -= Time.deltaTime * 3f;
        walkDuration = Mathf.Clamp01(walkDuration);
        if (walkDuration < 1f)
            direction *= walkDuration;
        Vector3 move = Camera.forward * direction.z + Camera.right * direction.x * .85f;
        move.y = lastmove.y;
        lastmove = move * acceleration + lastmove * (1f - acceleration);
        if (CharacterController.isGrounded)
            lastmove.y = 0f;
        else
            lastmove.y -= 9.98f * Time.deltaTime;

        

        
        CharacterController.Move(lastmove * CalculatedSpeed * Time.deltaTime);
        HandleBob();
        HandleCrouch();
    }

    bool stepped = false;
    void HandleBob()
    {
        var f = lastmove.magnitude;
        if (f <= .1f)
        {
            bobMagnitude -= Time.deltaTime * 2f;
            if(bobMagnitude < .01f)
            {
                bobProgression = 0f;
                stepped = false;
            }
        }
        else
        {
            if(stepped == false)
            {
                if (Mathf.Sin(bobProgression * 15f) > 0f && walkDuration > .01f)
                {
                    OnStep.Invoke();
                    stepped = true;
                    Debug.Log("Step!");
                }
            }
            else if(Mathf.Sin(bobProgression * 15f) < 0f)
            {
                stepped = false;
            }
            bobProgression += Time.deltaTime * f;
            bobMagnitude += Time.deltaTime * f * 5f;
        }
        bobMagnitude = Mathf.Clamp01(bobMagnitude);
    }

    void HandleCrouch()
    {
        if (Input.GetKey(KeyCode.LeftControl))
            crouch = Mathf.Lerp(crouch, 1f, Time.deltaTime * 5f);
        else
        {
            crouch = Mathf.Lerp(crouch, 0f, Time.deltaTime * 5f);
        }
        Camera.localPosition = cameraStartPosition + Vector3.down * crouchDelta * crouch + (bobMagnitude * Mathf.Sin(bobProgression * 15f) * Vector3.down * .08f);
    }

    Vector3 GetInputTranslationDirection()
    {
        Vector3 direction = new Vector3();


        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
        }
        //if(Input.GetKey(KeyCode.Escape))
        //{
        //    Application.Quit();
        //}

        return direction;
    }
}
