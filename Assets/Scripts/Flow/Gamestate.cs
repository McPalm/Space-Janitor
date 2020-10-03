using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamestate : MonoBehaviour
{
    public BussGenerator BussGenerator;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        int count = 0;
    start:
        Debug.Log("Start!");
        yield return new WaitForSeconds(1f);
        Debug.Log("1");
        BussGenerator.trashNumber = 5 + count;
        SpawnTrash();
        count++;
        Debug.Log("2");
        yield return new WaitForSeconds(30f);
    evaluate:
        Debug.Log("TIME!!!");
        Debug.Log($"you missed {BussGenerator.MissedTrash()} objects!");
        yield return new WaitForSeconds(1f);
        BussGenerator.Reset();
        goto start;
    }

    void SpawnTrash()
    {
        BussGenerator.Generate();
    }
}
