using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetEachDay : MonoBehaviour
{
    Quaternion rotation;
    Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        rotation = transform.rotation;
        position = transform.position;

        FindObjectOfType<Gamestate>().NewDayEvent += ResetEachDay_NewDayEvent;
    }

    private void ResetEachDay_NewDayEvent()
    {
        transform.position = position;
        transform.rotation = rotation;
    }
    
}
