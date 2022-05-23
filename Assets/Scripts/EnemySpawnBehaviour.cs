using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] Enemies;
    public void SpawnEnemy()
    {
        if (Random.Range(0f, 1f) > 0.8) Instantiate(Enemies[1], new Vector3(transform.position.x, transform.position.y, -0.5333323f), transform.rotation);
        else
        {
            for (int i = 0; i < Random.Range(0, 5); i++)
            { 
                Instantiate(Enemies[0], new Vector3(transform.position.x+i, transform.position.y, -0.5333323f), transform.rotation);
                Debug.Log("Spawning enemy");
            }

        }
        
    }
}
