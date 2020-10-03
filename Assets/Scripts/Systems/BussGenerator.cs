using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BussGenerator : MonoBehaviour
{
    public GameObject[] TrashPrefabs;
    public BoxCollider SpawnZone;

    Transform MockPoint;

    public int trashNumber;

    List<GameObject> spawnedTrash;

    private void Awake()
    {
        MockPoint = new GameObject().transform;
        MockPoint.SetParent(SpawnZone.transform);
        spawnedTrash = new List<GameObject>();
    }

    public void Generate()
    {
        Debug.Log($"Generating {trashNumber} trash!");
        for (int i = 0; i < trashNumber; i++)
        {
            var point = GetRandomPoint();
            var fab = TrashPrefabs[Random.Range(0, TrashPrefabs.Length)];
            var obj = Instantiate(fab, GetRandomPoint(), Quaternion.identity); // TODO randomize the rotation probably.
            spawnedTrash.Add(obj);
        }
    }

    public int MissedTrash()
    {
        int miss = 0;
        foreach(var trash in spawnedTrash)
        {
            if (trash != null)
                miss++;
        }
        return miss;
    }

    public void Reset()
    {
        foreach(var trash in spawnedTrash)
        {
            if (trash != null)
                Destroy(trash);
        }
        spawnedTrash.Clear();
    }

    Vector3 GetRandomPoint()
    {
        var local = new Vector3(
            (.5f - Random.value) * SpawnZone.size.x,
            (.5f - Random.value) * SpawnZone.size.y,
            (.5f - Random.value) * SpawnZone.size.z);
        MockPoint.localPosition = local;
        return MockPoint.position;
    }
}
