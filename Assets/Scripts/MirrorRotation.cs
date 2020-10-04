using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorRotation : MonoBehaviour
{

    public Transform original;

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0f, original.eulerAngles.y, 0f);
    }
}
