using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : MonoBehaviour
{
    public Rigidbody binRB;


    public AudioClip[] noises;

    private void OnTriggerEnter(Collider other)
    {
        var ohs = other.GetComponent<InteractiveObject>();
        if(ohs)
        {
            if (ohs.objectType == ObjectType.Trash)
            {
                var rb = ohs.GetComponent<Rigidbody>();
                binRB.AddForce(rb.velocity * rb.mass / binRB.mass, ForceMode.Impulse);
                rb.velocity *= .1f;
                
                Destroy(ohs.gameObject, .2f);

                var au = GetComponent<AudioSource>();
                au.clip = noises[Random.Range(0, noises.Length)];
                au.Play();
            }
        }
    }
}
