using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSound : MonoBehaviour
{

    public AudioClip Clip;
    public AudioSource AudioSource;
    
    public void Play()
    {
        AudioSource.clip = Clip;
        AudioSource.Play();
    }
}
