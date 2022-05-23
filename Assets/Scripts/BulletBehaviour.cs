using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    Rigidbody2D rb;
    public float bullet_speed = 10f;
    public float damage = 10f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddRelativeForce(Vector2.right * bullet_speed);
    }
    // Checking collisions, dealing damage
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy") collision.collider.GetComponent<EnemyBehaviour>().TakeDamage(damage);
        if (collision.collider.name != "Player") Destroy(this.gameObject);
    }
}
