using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broom : MonoBehaviour
{
    public Transform SweepPoint;
    public LayerMask DirtMask;

    public void Sweep()
    {
        var hits = Physics.OverlapSphere(SweepPoint.transform.position, .5f, DirtMask);

        if(hits.Length > 0)
        { 
            var dust = hits[0].GetComponent<Dust>();
            if(dust)
            {
                dust.Sweep();
            }
        }
    }
}
