using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitterProjectileBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    public float bullet_speed = 10f;
    public float damage = 10f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddRelativeForce(Vector2.right * bullet_speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            collision.GetComponent<PlayerController>().TakeDamage(damage);
        }
        if (collision.tag !="Enemy" && collision.tag != "Projectile")
        {
            Destroy(this.gameObject);
        }
    }   
    // Update is called once per frame
    void Update()
    {
        
    }
}
