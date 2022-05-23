using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnsController : MonoBehaviour
{
    public float spawnRate = 5f;
    void Start()
    {
        StartCoroutine(SpawnDelay());
    }
    IEnumerator SpawnDelay()
    {
        transform.GetChild(Random.Range(0, transform.childCount )).GetComponent<ItemSpawnBehaviour>().SpawnItem();
        yield return new WaitForSeconds(spawnRate);
        StartCoroutine(SpawnDelay());
    }
}
