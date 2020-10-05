using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour
{
    public float sweepProgress = 0f;
    public ParticleSystem ParticleSystem;

    // Start is called before the first frame update
    void OnEnable()
    {
        transform.localScale = Vector3.one;
        sweepProgress = 0f;
        GetComponent<Collider>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
    }

    public void Sweep()
    {
        Debug.Log("Sweept");
        sweepProgress += .1f;
        transform.localScale = Vector3.one * (1f - sweepProgress);
        ParticleSystem.Play();

        if (sweepProgress > .5f)
            StartCoroutine(Fin());
            
    }

    public IEnumerator Fin()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;

        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }

}
