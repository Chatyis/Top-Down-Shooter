using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] pickUps;
    public void SpawnItem()
    {
        if(transform.childCount==0f)
        {
            if (Random.Range(0f, 1) > 0.5) Instantiate(pickUps[0], transform.position, transform.rotation, this.transform);
            else Instantiate(pickUps[1], transform.position, transform.rotation,this.transform);
        }
    }
}
