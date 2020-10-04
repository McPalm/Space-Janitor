using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectNoise : MonoBehaviour
{


    private void OnCollisionEnter(Collision collision)
    {
        var impulse = collision.impulse.magnitude;
        var au = GetComponent<AudioSource>();
        au.volume = impulse * Mathf.Clamp01(.17f);
        au.Play();
    }
}
