using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] public Transform target;
    private NavMeshAgent agent;
    public Animator animator;
    public Collider2D collider2d;
    public GameObject SpitterProjectile;
    public string enemyClass = "default";
    public float health = 100f;
    //public float speed = 10f;
    public float damageDealt = 30f;
    public float scoreValue = 20f;
    bool canShoot = true;
    public bool canDealDamage = true;
    public void TakeDamage(float damage)
    {
        health -= damage;
       // Debug.Log("Got hit by " + damage + " damage; Health left: " + health);
        if (health <= 0)
        {
            transform.Find("GFX").gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
            Destroy(this.gameObject, 100);
            if (damage >= 100f) animator.SetBool("isExploded", true);
            animator.SetBool("isDead", true);
            agent.enabled = false;
            collider2d.enabled = false;
            GameObject.Find("Player").GetComponent<PlayerController>().setScore(scoreValue);
        }
    }
    public void DealDamage(GameObject target)
    {
        target.GetComponent<PlayerController>().TakeDamage(damageDealt);
    }
    IEnumerator SpitterShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(0.8f);
        Instantiate(SpitterProjectile, transform.position, transform.rotation);
        canShoot = true;
    }
    IEnumerator DamageDelay()
    {
        canDealDamage = false;
        agent.isStopped = true;
        animator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.45f);
        transform.Find("AttackArea").GetComponent<Collider2D>().enabled = true;
        yield return new WaitForSeconds(0.05f);
        animator.SetBool("isAttacking", false);
        transform.Find("AttackArea").GetComponent<Collider2D>().enabled = false;
        canDealDamage = true;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.name == "Player" && canDealDamage && enemyClass!="spitter")
        {
            //collision.collider.GetComponent<PlayerController>().TakeDamage(damageDealt);
            StartCoroutine(DamageDelay());
            //Debug.Log("Damage dealt");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //Debug.Log("test");
        if (collision.collider.name == "Player")
        {
            agent.isStopped = false;
        }
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateUpAxis = false;
        agent.updateRotation = false;
        target = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(health>0)
        {
            float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 1000 * Time.deltaTime);
            agent.SetDestination(target.position);
            if (!agent.isStopped) { animator.SetBool("isActive", true); }
            else animator.SetBool("isActive", false);
            if (enemyClass == "spitter" && canShoot)
            {
                StartCoroutine(SpitterShoot());
            }
        }
    }
}
