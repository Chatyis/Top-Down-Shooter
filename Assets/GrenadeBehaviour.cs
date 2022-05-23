using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBehaviour : MonoBehaviour
{
    Rigidbody2D rb;
    Collider2D coll2d;
    public Animator animator;
    public float bullet_speed = 10f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll2d = GetComponent<Collider2D>();
        StartCoroutine(GrenadeTimer());
        rb.AddRelativeForce(Vector2.right * bullet_speed);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyBehaviour>().TakeDamage(150f);
        }
    }
    IEnumerator GrenadeTimer()
    {
        yield return new WaitForSeconds(1f);
        rb.velocity = Vector2.zero;
        coll2d.enabled = true;
        animator.SetBool("isExploded", true);
        yield return new WaitForSeconds(.5f);
        Destroy(gameObject);
    }

}
