using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAround : MonoBehaviour
{
    public float yaw;
    float pitch;

    bool invertY = false;

    public float mouseSensitivityFactor = 1f;

    // Start is called before the first frame update
    void Start()
    {
        pitch = transform.eulerAngles.x;
        yaw = transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        var mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * (invertY ? 1 : -1));

        // var mouseSensitivityFactor = 1f; // mouseSensitivityCurve.Evaluate(mouseMovement.magnitude);

        yaw += mouseMovement.x * mouseSensitivityFactor;
        pitch += mouseMovement.y * mouseSensitivityFactor;
        pitch = Mathf.Clamp(pitch, -80f, 80f);


        transform.eulerAngles = new Vector3(pitch, yaw, 0f);
    }


}
