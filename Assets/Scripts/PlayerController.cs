using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Serialized
    [SerializeField] private Rigidbody rigi;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bullets;

    //Private
    private float verticalVelocity = 0f;
    private int layerMask = 1 << 9;
    private Coroutine shootCoroutine;
    private Coroutine dashCoroutine;
    private float dashVelocity = 0f;
    private Coroutine invincibleCoroutine;
    private float inputDirection;
    private RaycastHit aimHit;
    private Vector3 aimPoint;
    private Vector3 spawn;
    private bool spawned;
    private Coroutine dashCooldownCoroutine;

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
    [SerializeField] [Range(0, 10)] private float dashCooldown = 1f;
    [SerializeField] [Range(0, 2)] private float verticalReductionDuringDash = 0.5f;

    //Debug
    [SerializeField] [ReadOnly] private int hP;
    [SerializeField] [ReadOnly] private float movementSpeed = 0f;
    [SerializeField] [ReadOnly] private int doubleJumps = 0;
    [SerializeField] [ReadOnly] private float dashes = 0;
    [SerializeField] [ReadOnly] private bool grounded;
    [SerializeField] [ReadOnly] private bool jumping;
    [SerializeField] [ReadOnly] private float actualVerticalReductionDuringDash = 1;
    [SerializeField] [ReadOnly] private bool dashOnCooldown;
    [SerializeField] [ReadOnly] private Global.PlayerState playerState = Global.PlayerState.Default;



    [ExecuteInEditMode]
    private void OnValidate()
    {

        movementSpeed = baseMovementSpeed;
        doubleJumps = baseDoubleJumps;
        dashes = baseDashes;
        hP = maxHP;

    }



    public void Start()
    {

        if (!spawned)
        {
            spawn = rigi.position;
            spawned = true;
        }

        else
        {
            rigi.position = spawn;
        }

        movementSpeed = baseMovementSpeed;
        doubleJumps = baseDoubleJumps;
        dashes = baseDashes;

        hP = maxHP;
        InterfaceController.instance.UpdatePlayerHP(hP, maxHP);

        playerState = Global.PlayerState.Default;
        InterfaceController.instance.UpdatePlayerState(playerState);

    }



    public void Freeze()
    {

        rigi.velocity = Vector3.zero;

        if (shootCoroutine != null)
        {
            StopCoroutine(shootCoroutine);
            shootCoroutine = null;
        }
        
        if (dashCoroutine != null)
        {
            StopCoroutine(dashCoroutine);
            dashCoroutine = null;
        }
        
        if (invincibleCoroutine != null)
        {
            StopCoroutine(invincibleCoroutine);
            invincibleCoroutine = null;
        }

        foreach (Transform bullet in bullets)
        {
            Destroy(bullet.gameObject);
        }

    }



    /// <summary>
    /// Updates general player values.
    /// </summary>
    public void Upd8(float direction)
    {

        this.inputDirection = direction;

        //Dash Controller
        if (direction == 0)
        {
            if (dashCoroutine != null)
            {
                StopCoroutine(dashCoroutine);
            }
            dashVelocity = 0f;
            dashCoroutine = null;
            actualVerticalReductionDuringDash = 1f;
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
        rigi.velocity = new Vector3((inputDirection * movementSpeed) + dashVelocity, verticalVelocity * actualVerticalReductionDuringDash, 0f);
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

        if (dashOnCooldown)
        {
            return;
        }

        if (grounded)
        {
            if (dashCoroutine == null)
            {
                dashCoroutine = StartCoroutine(DashCoroutine());
                dashCooldownCoroutine = StartCoroutine(DashCooldownCoroutine());
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
                    dashCooldownCoroutine = StartCoroutine(DashCooldownCoroutine());
                }
            }
        }

    }

    private IEnumerator DashCoroutine()
    {
        dashOnCooldown = true;
        Invincible(dashInvincibleDuration);
        dashVelocity = inputDirection * dashPower;
        AudioController.instance.Dash();
        actualVerticalReductionDuringDash = verticalReductionDuringDash;
        yield return new WaitForSeconds(dashDuration);
        actualVerticalReductionDuringDash = 1f;
        dashVelocity = 0f;
        dashCoroutine = null;
        yield break;
    }
    
    private IEnumerator DashCooldownCoroutine()
    {
        yield return new WaitUntil(() => dashCoroutine == null);
        yield return new WaitForSeconds(dashCooldown);
        dashOnCooldown = false;
        yield break;
    }

    

    /// <summary>
    /// Updates the direction the player is aiming towards.
    /// </summary>
    public void Aim()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out aimHit))
        {

            if (aimHit.transform.gameObject.tag != "Player")
            {
                aimPoint = aimHit.point;
            }

        }

        else
        {
            aimPoint = ray.GetPoint(bulletTrajectoryDistance);
        }

    }



    /// <summary>
    /// Shoots towards the direction the player is aiming towards.
    /// </summary>
    public void Shoot()
    {

        if (shootCoroutine == null)
        {
            shootCoroutine = StartCoroutine(ShootCoroutine(aimPoint));
        }

    }

    private IEnumerator ShootCoroutine(Vector3 point)
    {

        float xangle = Mathf.Atan2(point.z - rigi.position.z, point.y - rigi.position.y) * 180 / Mathf.PI;
        float yangle = Mathf.Atan2(point.x - rigi.position.x, point.z - rigi.position.z) * 180 / Mathf.PI;

        GameObject bullet = Instantiate(bulletPrefab, rigi.position, Quaternion.Euler(xangle, yangle, 0), bullets);
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



    /// <summary>
    /// Event for entering triggers.
    /// </summary>
    /// <param name="other"></param>
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



    /// <summary>
    /// Checks whether the player should die or get hit by the source depending on the amount sent as a parameter.
    /// </summary>
    /// <param name="amt"></param>
    public void Receive(int amt)
    {
        
        if (playerState == Global.PlayerState.Default)
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



    /// <summary>
    /// Kills the player.
    /// </summary>
    private void Die()
    {

        Freeze();
        GameController.instance.FreezeBoss();

        hP = 0;
        InterfaceController.instance.UpdatePlayerHP(hP, maxHP);
        playerState = Global.PlayerState.Dead;
        GameController.instance.SetGameState(Global.GameState.Fail);
        InterfaceController.instance.Fail();

    }



    /// <summary>
    /// Hits the player for the amount sent as a parameter.
    /// </summary>
    /// <param name="amt"></param>
    private void Hit(int amt)
    {
        hP -= amt;
        Invincible(hitInvincibleDuration);
        InterfaceController.instance.UpdatePlayerHP(hP, maxHP);
    }



    public void PlayerShootReverb()
    {
        AudioController.instance.GunReverb();
    }

}
