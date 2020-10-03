using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

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

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined; //lock dat cursor
        cameraStartPosition = Camera.localPosition;
    }

    void Update() //Noticed a stutter I think was due to using Fixed Update So I changed this to Update. 
                  //Should be a fine like this since you're using Dt though.
    {
        var direction = GetInputTranslationDirection();
        Vector3 move = Camera.forward * direction.z + Camera.right * direction.x;
        move.y = lastmove.y;
        lastmove = move * acceleration + lastmove * (1f - acceleration);
        if (CharacterController.isGrounded)
            lastmove.y = 0f;
        else
            lastmove.y -= 9.98f * Time.deltaTime;
        CharacterController.Move(lastmove * CalculatedSpeed * Time.deltaTime);
        HandleCrouch();
    }

    void HandleCrouch()
    {
        if (Input.GetKey(KeyCode.LeftControl))
            crouch = Mathf.Lerp(crouch, 1f, Time.deltaTime * 5f);
        else
            crouch = Mathf.Lerp(crouch, 0f, Time.deltaTime * 5f);
        Camera.localPosition = cameraStartPosition + Vector3.down * crouchDelta * crouch;
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
