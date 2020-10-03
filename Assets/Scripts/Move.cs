using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    public Transform Camera;
    public CharacterController CharacterController;
    public float speed = 5f;
    public float acceleration;
    Vector3 lastmove;



    // Update is called once per frame

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined; //lock dat cursor
    }

    void Update() //Noticed a stutter I think was due to using Fixed Update So I changed this to Update. 
                  //Should be a fine like this since you're using Dt though.
    {
        var direction = GetInputTranslationDirection();
        Vector3 move = Camera.forward * direction.z + Camera.right * direction.x;
        move.y = 0f;
        lastmove = move * .2f + lastmove * .8f;
        if(CharacterController.isGrounded)
            lastmove.y -= 0f;
        else
            lastmove.y -= 9.98f * Time.fixedDeltaTime;
        CharacterController.Move(lastmove * speed * Time.fixedDeltaTime);

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
