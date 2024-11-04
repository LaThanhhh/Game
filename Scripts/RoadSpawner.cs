using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public GameObject[] titlePrefabs;
    public float zSpawn = 0;
    public float titleLength = 30;
    public int numberOfTitles = 5;
    public List<GameObject> activeTitles = new List<GameObject>();

    public Transform playerTransfrom;
    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnTitle(i); 
        }

        for (int i = 3; i < numberOfTitles; i++)
        {
            SpawnTitle(Random.Range(0, titlePrefabs.Length));
        }
    }

    void Update()
    {
        if (playerTransfrom.position.z - 35 > zSpawn - (numberOfTitles * titleLength))
        {
            SpawnTitle(Random.Range(0, titlePrefabs.Length));
            DeleteTitle();
        }
    }

    public void SpawnTitle(int titleIndex)
    {
        GameObject go = Instantiate(titlePrefabs[titleIndex], transform.forward * zSpawn, transform.rotation);
        activeTitles.Add(go);
        zSpawn += titleLength;
    }
    private void DeleteTitle()
    {
        Destroy(activeTitles[0]);
        activeTitles.RemoveAt(0);
    }
}