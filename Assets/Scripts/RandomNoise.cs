using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomNoise : MonoBehaviour
{
    public AudioClip[] noises;

    public AudioSource AudioSource;
    // Start is called before the first frame update
    public void Play()
    {
        AudioSource.clip = noises[Random.Range(0, noises.Length)];
        AudioSource.Play();
    }
}
