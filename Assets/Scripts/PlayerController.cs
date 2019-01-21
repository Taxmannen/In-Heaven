using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Components
    [SerializeField] private Rigidbody rigi;
    [SerializeField] private GameObject bulletPrefab;

    //Design
    [SerializeField] [Range(0, 1000)] private int maxHP = 10; //Max Hit Points

    [SerializeField] [Range(0, 100)] private float baseMovementSpeed = 15f; //Base Movement Speed

    [SerializeField] [Range(-1000, 1000)] private float gravity = 75f; //Gravity (Works agaisnt Jump Power)
    [SerializeField] [Range(0, 100)] private float jumpPower = 25f; //Jump Power (Works against Gravity)
    [SerializeField] [Range(0, 10)] private float groundCheckRaycastDistance = 0.75f; //Distance to ground from players pivot point
    [SerializeField] [Range(0, 100)] private int baseDoubleJumps = 1; //Number of jumps possible in air

    [SerializeField] [Range(0, 100000)] private int bulletDamage = 7;
    [SerializeField] [Range(1, 100)] private float bulletsPerSecond = 10f; //Bullets per second during left mouse down
    [SerializeField] [Range(0, 1000)] private float bulletSpeed = 25f; //The speed of the bullets
    [SerializeField] [Range(0, 10)] private float bulletDuration = 3f; //The duration the bullets last until they are destroyed, low number reduces potential lag
    [SerializeField] [Range(0, 1000)] private float bulletTrajectoryDistance = 50f; //The max end point for the bullets trajectory, should be about the same as the distance between the player face and the boss face.

    [SerializeField] [Range(0, 1000)] private float dashPower = 50f; //The speed of the dash (affects dash distance)
    [SerializeField] [Range(0, 10)] private float dashDuration = 0.1f; //The duration of the dash (affects dash distance)
    [SerializeField] [Range(0, 100)] private float baseDashes = 1; //Number of dashes possible in air

    [SerializeField] [Range(0, 10)] private float dashInvincibleDuration = 0.25f; //Duration of invincibility state after the start of a dash
    [SerializeField] [Range(0, 10)] private float hitInvincibleDuration = 0.1f; //Duration of invincibility state after being hit, necessary to avoid getting hit rapidly multiple times.

    //Debug
    [SerializeField] [ReadOnly] private int hP;
    [SerializeField] [ReadOnly] private float movementSpeed = 0f;
    [SerializeField] [ReadOnly] private int doubleJumps = 0;
    [SerializeField] [ReadOnly] private float dashes = 0;
    [SerializeField] [ReadOnly] private bool grounded;
    [SerializeField] [ReadOnly] private bool jumping;
    [SerializeField] [ReadOnly] private bool dashing;
    [SerializeField] [ReadOnly] private Global.PlayerState playerState = Global.PlayerState.Default;

    //Private
    private float verticalVelocity = 0f;
    private int layerMask = 1 << 9;
    private Coroutine shootCoroutine;
    private Coroutine dashCoroutine;
    private float dashVelocity = 0f;
    private Coroutine invincibleCoroutine;
    private float direction;

    //Design
    [ExecuteInEditMode]
    void OnValidate()
    {

        movementSpeed = baseMovementSpeed;
        doubleJumps = baseDoubleJumps;
        dashes = baseDashes;
        hP = maxHP;

    }

    private void Start()
    {

        movementSpeed = baseMovementSpeed;
        doubleJumps = baseDoubleJumps;
        dashes = baseDashes;
        hP = maxHP;

        InterfaceController.instance.UpdatePlayerHP(hP, maxHP);
        InterfaceController.instance.UpdatePlayerState(playerState);

    }

    /// <summary>
    /// Updates general player values.
    /// </summary>
    public void Upd8(float direction)
    {

        this.direction = direction;

        //Dash Controller
        if (direction == 0)
        {
            if (dashCoroutine != null)
            {
                StopCoroutine(dashCoroutine);
            }
            dashVelocity = 0f;
            dashCoroutine = null;
        }

        //Grounded & Jumping Controller
        if (verticalVelocity > 0)
        {
            grounded = false;
        }

        else
        {

            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + GetComponentInChildren<Collider>().bounds.extents.y, transform.position.z), Vector3.down, groundCheckRaycastDistance, layerMask))
            {
                doubleJumps = baseDoubleJumps;
                dashes = baseDashes;
                grounded = true;
                jumping = false;
            }

        }

    }

    /// <summary>
    /// Applies velocity to the player.
    /// </summary>
    public void Move()
    {
        rigi.velocity = new Vector3((direction * movementSpeed) + dashVelocity, verticalVelocity, 0f);
    }

    /// <summary>
    /// Applies a custom-coded gravity on the object.
    /// </summary>
    public void Gravity()
    {

        if (grounded && !jumping)
        {
            verticalVelocity = -gravity * Time.deltaTime;
        }

        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

    }

    /// <summary>
    /// Applies a vertical velocity to the player.
    /// </summary>
    public void Jump()
    {

        if (grounded)
        {
            jumping = true;
            verticalVelocity = jumpPower;
            AudioController.instance.PlayerJump();

        }

        else
        {

            if (doubleJumps > 0)
            {
                doubleJumps--;
                verticalVelocity = jumpPower;
                AudioController.instance.PlayerDoubleJump();
            }

        }

    }

    /// <summary>
    /// Applies a horizontal velocity to the player.
    /// </summary>
    public void Dash()
    {

        if (grounded)
        {
            if (dashCoroutine == null)
            {
                dashCoroutine = StartCoroutine(DashCoroutine());
            }
        }

        if (!grounded)
        {
            if (dashes > 0)
            {
                if (dashCoroutine == null)
                {
                    dashes--;
                    dashCoroutine = StartCoroutine(DashCoroutine());
                }
            }
        }

    }

    private IEnumerator DashCoroutine()
    {
        Invincible(dashInvincibleDuration);
        dashing = true;
        dashVelocity = direction * dashPower;
        AudioController.instance.Dash();
        yield return new WaitForSeconds(dashDuration);
        dashing = false;
        dashVelocity = 0f;
        dashCoroutine = null;
        yield break;
    }

    RaycastHit hit;
    Vector3 point;

    public void Aim()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {

            if (hit.transform.gameObject.tag != "Player")
            {
                point = hit.point;
            }

        }

        else
        {
            point = ray.GetPoint(bulletTrajectoryDistance);
        }

    }


    /// <summary>
    /// (WIP) Shoots forward.
    /// </summary>
    public void Shoot()
    {

        if (shootCoroutine == null)
        {
            shootCoroutine = StartCoroutine(ShootCoroutine(point));
        }

    }

    private IEnumerator ShootCoroutine(Vector3 point)
    {

        float xangle = Mathf.Atan2(point.z - rigi.position.z, point.y - rigi.position.y) * 180 / Mathf.PI;
        float yangle = Mathf.Atan2(point.x - rigi.position.x, point.z - rigi.position.z) * 180 / Mathf.PI;

        GameObject bullet = Instantiate(bulletPrefab, rigi.position, Quaternion.Euler(xangle, yangle, 0), null);
        Destroy(bullet, bulletDuration);

        Vector3 dir = point - rigi.position;

        dir.Normalize();

        bullet.GetComponent<Rigidbody>().velocity = dir * bulletSpeed;

        bullet.GetComponent<Bullet>().SetDamage(bulletDamage);

        AudioController.instance.PlayerShoot();

        yield return new WaitForSeconds(1 / bulletsPerSecond);
        shootCoroutine = null;
        yield break;
    }







    /// <summary>
    /// Applies the Invincible PlayerState to the object for the duration sent as a parameter.
    /// </summary>
    /// <param name="duration"></param>
    private void Invincible(float duration)
    {

        if (invincibleCoroutine != null)
        {
            StopCoroutine(invincibleCoroutine);
        }

        invincibleCoroutine = StartCoroutine(InvincibleCoroutine(duration));

    }

    private IEnumerator InvincibleCoroutine(float duration)
    {
        playerState = Global.PlayerState.Invincible;
        InterfaceController.instance.UpdatePlayerState(playerState); //Debug
        yield return new WaitForSeconds(duration);
        playerState = Global.PlayerState.Default;
        InterfaceController.instance.UpdatePlayerState(playerState); //Debug
        yield break;
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "BossBullet")
        {

            if (playerState == Global.PlayerState.Default)
            {
                Receive(other.GetComponent<Bullet>().GetDamage());
            }

        }

    }

    public void Receive(int amt)
    {
        
        if (playerState != Global.PlayerState.Invincible)
        {
            if (hP - amt <= 0)
            {
                Die();
            }

            else
            {
                Hit(amt);
            }
        }

    }

    private void Die()
    {
        hP = 0;
        InterfaceController.instance.UpdatePlayerHP(hP, maxHP);
        playerState = Global.PlayerState.Dead;
        GameController.instance.SetGameState(Global.GameState.Fail);
        InterfaceController.instance.Fail();
    }

    private void Hit(int amt)
    {
        hP -= amt;
        Invincible(hitInvincibleDuration);
        InterfaceController.instance.UpdatePlayerHP(hP, maxHP);
    }

}
