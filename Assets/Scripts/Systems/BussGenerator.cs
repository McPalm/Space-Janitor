using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BussGenerator : MonoBehaviour
{
    public GameObject[] TrashPrefabs;
    public BoxCollider SpawnZone;

    Transform MockPoint;

    public int trashNumber;
    public int dirtNumber = 3;

    List<GameObject> spawnedTrash;
    Dust[] DirtPatches;

    private void Awake()
    {
        MockPoint = new GameObject().transform;
        MockPoint.SetParent(SpawnZone.transform);
        spawnedTrash = new List<GameObject>();
    }

    private void Start()
    {
        DirtPatches = FindObjectsOfType<Dust>();
        foreach(var dirt in DirtPatches)
        {
            dirt.gameObject.SetActive(false);
        }
    }

    public void Generate()
    {
        for (int i = 0; i < trashNumber; i++)
        {
            var point = GetRandomPoint();
            var fab = TrashPrefabs[Random.Range(0, TrashPrefabs.Length)];
            var obj = Instantiate(fab, GetRandomPoint(), Quaternion.identity); // TODO randomize the rotation probably.
            obj.transform.rotation = new Quaternion(Random.value * 180f, Random.value * 180f, Random.value * 180f, Random.value * 180f);
            spawnedTrash.Add(obj);
        }
        for(int i = 0; i < dirtNumber; i++)
        {
            DirtPatches[Random.Range(0, DirtPatches.Length)].gameObject.SetActive(true);
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
        foreach(var dirt in DirtPatches)
        {
            if (dirt.isActiveAndEnabled)
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
        foreach (var dirt in DirtPatches)
        {
            dirt.gameObject.SetActive(false);
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
