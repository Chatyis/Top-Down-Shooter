using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    public class Gun
    {
        public int fire_rate = 0;
        public float damage = 0f;
        public int ammo = 0;
        public int curr_ammo = 0;
        public int clip_size = 0;
        public int max_ammo = 0;
        public string name = "";
        public Gun(int fr, float dmg, int _ammo, int c_ammo, int c_size, int m_ammo,string _name)
        {
            fire_rate = fr;
            damage = dmg;
            ammo = _ammo;
            curr_ammo = c_ammo;
            name = _name;
            clip_size = c_size;
            max_ammo = m_ammo;
        }
        public Gun(Gun gun)
        {
            fire_rate = gun.fire_rate;
            damage = gun.damage;
            ammo = gun.ammo;
            curr_ammo = gun.curr_ammo;
            clip_size = gun.clip_size;
            max_ammo = gun.max_ammo;
            name = gun.name;
        }
    }

    [SerializeField] private AnimatorOverrideController[] overrideControllers;

    static Gun Pistol = new Gun(200, 30f,20,20,20,200, "Pistol");
    static Gun Rifle = new Gun(500, 30f,90,30,30,90, "Rifle");
    static Gun Grenade = new Gun(80, 0f, 0, 10, 10, 0, "Grenade");
    public Gun CurrentWeapon = Pistol;

    public GameManagerController GameManager;
    public Animator animator;
    public GameObject Bullet;
    public GameObject GrenadeProjectile;
    private Coroutine reloadCoroutine;
    public float speed = 10f;
    public float score = 0f;
    public bool canShoot = true;
    public bool canDash = true;
    public bool canMove = true;
    public bool isReloading = false;
    public float health = 100f;
    public float dashForce = 30f;
    Rigidbody2D rb;
    public void resetAmmo()
    {
        Grenade.curr_ammo = Grenade.clip_size;
        Rifle.curr_ammo = Rifle.clip_size;
        Rifle.ammo = Rifle.max_ammo;
        Pistol.curr_ammo = Pistol.clip_size;
        Pistol.ammo = Pistol.max_ammo;
    }
    public void setScore(float _score)
    {
        score += _score;
    }
    public void SetWeapon(int value,Coroutine reloadCoroutine)
    {
        if (isReloading) StopCoroutine(reloadCoroutine); 
        isReloading = false;
        animator.runtimeAnimatorController = overrideControllers[value];
    }
    IEnumerator ShootDelay(float waitTime)
    {
        yield return new WaitForSeconds(60/waitTime);
        canShoot = true;
    }
    IEnumerator ReloadDelay(float reloadTime)
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        if (CurrentWeapon.ammo >= (CurrentWeapon.clip_size - CurrentWeapon.curr_ammo))
        {
            CurrentWeapon.ammo += CurrentWeapon.curr_ammo - CurrentWeapon.clip_size;
            CurrentWeapon.curr_ammo = CurrentWeapon.clip_size;
        }
        else
        {
            CurrentWeapon.curr_ammo += CurrentWeapon.ammo;
            CurrentWeapon.ammo = 0;
        }
        isReloading = false;
    }
    IEnumerator DashDelay()
    {
        yield return new WaitForSeconds(0.3f);
        canMove = true;
        animator.SetBool("isDashed", false);
        yield return new WaitForSeconds(1.0f);
        canDash = true;
    }
    IEnumerator ThrowGrenade(float waitTime,Transform emitter)
    {
        animator.SetBool("isThrowing", true);
        yield return new WaitForSeconds(60/waitTime);
        animator.SetBool("isThrowing", false);
        Instantiate(GrenadeProjectile, emitter.position, emitter.rotation);
        canShoot = true;
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        GameManager.DisplayHitScreen();
        if (health <= 0)
        {
            GameManager.PlayerDeath();
            Destroy(gameObject);
        }
    }
    void Shoot()
    {
        canShoot = false;
        Transform emitter = this.gameObject.transform.Find(CurrentWeapon.name).GetChild(0);
        if (CurrentWeapon.damage > 0)
        {
            GameObject bullet = Instantiate(Bullet, emitter.position, emitter.rotation);
            bullet.GetComponent<BulletBehaviour>().damage = CurrentWeapon.damage;
            StartCoroutine(ShootDelay(CurrentWeapon.fire_rate));
        }
        else
        {
            StartCoroutine(ThrowGrenade(CurrentWeapon.fire_rate,emitter));
        }
        CurrentWeapon.curr_ammo -= 1;
    }
    public void PickUp(string itemType)
    {
        switch (itemType)
        {
            case "healthkit":
                {
                    health = 100f;
                    break;
                }
            case "ammo":
                {
                    CurrentWeapon.ammo = CurrentWeapon.max_ammo;
                    if (CurrentWeapon.damage <= 0f) CurrentWeapon.curr_ammo = CurrentWeapon.clip_size; // For grenades
                    break;
                }
        }
    }
    void Start()
    {
        resetAmmo();
        rb = GetComponent<Rigidbody2D>();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManagerController>();
        //Debug.Log(this.GetComponentInParent<Component>());
    }

    void FixedUpdate()
    {
        // Player rotation
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        // Player movement
        Vector2 translation = new Vector2(Input.GetAxis("Horizontal")*speed, Input.GetAxis("Vertical") * speed);
        if (canMove) rb.velocity = translation;
        // Enable running animation
        if (translation.x != 0 || translation.y !=0) animator.SetBool("isRunning", true);
        else animator.SetBool("isRunning", false);
        //Shooting
        if (Input.GetButton("Fire1") && canShoot && !isReloading)
        {
            if(CurrentWeapon.curr_ammo>0) Shoot();
            else reloadCoroutine = StartCoroutine(ReloadDelay(2f));
        }
        //Dashing
        if (Input.GetButton("Jump") && canDash)
        {
           // Debug.Log("Dashed");
            animator.SetBool("isDashed", true);
            canDash = false;
            canMove = false;
            rb.AddForce(new Vector2(Input.GetAxis("Horizontal") * 30 *dashForce, Input.GetAxis("Vertical") * 30 * dashForce));
            
            StartCoroutine(DashDelay());
        }
        //Reloading
        if (Input.GetKey("r") && !isReloading && CurrentWeapon.damage>0)
        {
            reloadCoroutine = StartCoroutine(ReloadDelay(2f));
        }
        //Changing weapons
        if (Input.GetKey("1"))
        {
            CurrentWeapon = Pistol;
            SetWeapon(0, reloadCoroutine);
        }
        if (Input.GetKey("2") )
        {
            CurrentWeapon = Rifle;
            SetWeapon(1, reloadCoroutine);
        }
        if (Input.GetKey("3"))
        {
            CurrentWeapon = Grenade;
            SetWeapon(2, reloadCoroutine);
        }
    }
}
