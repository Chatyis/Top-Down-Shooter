using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    Collider2D coll2d;
    // Start is called before the first frame update
    void Start()
    {
        coll2d = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            transform.parent.GetComponent<EnemyBehaviour>().DealDamage(collision.gameObject);
            coll2d.enabled = false;
        }
    }
    void Update()
    {
        
    }
}
