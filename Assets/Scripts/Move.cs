using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    public Transform Camera;
    public Rigidbody Rigidbody;

    // Update is called once per frame
    void Update()
    {
        var direction = GetInputTranslationDirection();
        if(direction.z != 0f)
        {
            Rigidbody.AddForce(Camera.forward * direction.z);
        }
        if(direction.x != 0f)
        {
            Rigidbody.AddForce(Camera.right * direction.x);
        }
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
        return direction;
    }
}
