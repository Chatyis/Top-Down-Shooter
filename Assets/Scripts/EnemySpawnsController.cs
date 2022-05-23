using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnsController : MonoBehaviour
{
    // Start is called before the first frame update
    public float spawnRate = 0.5f;
    void Start()
    {
        StartCoroutine(SpawnDelay());
    }
    IEnumerator SpawnDelay()
    {
        transform.GetChild(Random.Range(0, transform.childCount)).GetComponent<EnemySpawnBehaviour>().SpawnEnemy();
        yield return new WaitForSeconds(spawnRate);
        StartCoroutine(SpawnDelay());
    }
}
