using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Serialized
    [Header("SERIALIZED")]
    [SerializeField] private Rigidbody playerRigi;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bullets;
    [SerializeField] private BoxCollider parrybox;

    //Design
    [Header("GENERAL")]
    [SerializeField] [Range(0, 1000)] private float maxHP = 10; //Max Hit Points
    [SerializeField] [Range(0, 100)] private float baseMovementSpeed = 15f; //Base Movement Speed
    [SerializeField] [Range(0, 10)] private float groundcheckDistance = 1.15f; //Distance to ground from players pivot point (FIRST VALUE = HALF PLAYER HEIGHT)

    [Header("JUMP")]
    [SerializeField] [Range(0, 100)] private float jumpPower = 25f; //Jump Power (Works against Gravity)
    [SerializeField] [Range(0, 100)] private int maxDoubleJumps = 1; //Number of jumps possible in air

    [Header("BULLETS")]
    [SerializeField] [Range(0, 100000)] private int playerBulletDamage = 7;
    [SerializeField] [Range(1, 100)] private float basePlayerBulletsPerSecond = 10f; //Bullets per second during left mouse down
    [SerializeField] [Range(0, 1000)] private float playerBulletSpeed = 25f; //The speed of the bullets
    [SerializeField] [Range(0, 10)] private float playerBulletLifetime = 3f; //The duration the bullets last until they are destroyed, low number reduces potential lag
    [SerializeField] [Range(0, 1000)] private float playerBulletTrajectoryDistance = 50f; //The max end point for the bullets trajectory, should be about the same as the distance between the player face and the boss face.

    [Header("DASH")]
    [SerializeField] [Range(0, 1000)] private float dashPower = 50f; //The speed of the dash (affects dash distance)
    [SerializeField] [Range(0, 10)] private float dashDuration = 0.1f; //The duration of the dash (affects dash distance)
    [SerializeField] [Range(0, 10)] private float dashInvincibleDuration = 0.25f; //Duration of invincibility state after the start of a dash
    [SerializeField] [Range(0, 2)] private float dashVerticalReduction = 0.5f; //Reduces the vertical velocity affecting the player during the dash
    [SerializeField] [Range(0, 100)] private float maxDashes = 1; //Number of dashes possible in air
    [SerializeField] [Range(0, 10)] private float dashCooldown = 1f; //Cooldown for dash, ground and air

    [Header("GRAVITY")]
    [SerializeField] [Range(-1000, 1000)] private float gravity = 75f; //Gravity (Works agaisnt Jump Power)
    [SerializeField] [Range(0, 100)] private float forcedGravitySpeed = 25f; //The additional force affecting the player on pressed down, during aired
    [SerializeField] [Range(0, 100)] private float forcedPower = 40f; //Forced gravity power when pressing down key.

    [Header("PARRY")]
    [SerializeField] [Range(0, 100000)] private float playerParryBulletDamage = 10f;
    [SerializeField] [Range(0, 1000)] private float playerParryBulletSpeed = 100f;
    [SerializeField] [Range(0, 10)] private float parryDuration = 1f;
    [SerializeField] [Range(0, 10)] private float parryCooldown = 1f;

    [Header("SLEDGEHAMMER")]
    [SerializeField] [Range(0, 10)] private float sledgeDuration = 3f;

    [Header("HIT")]
    [SerializeField] [Range(0, 10)] private float hitInvincibleDuration = 0.1f; //Duration of invincibility state after being hit, necessary to avoid getting hit rapidly multiple times.

    [Header("SUPERCHARGE")]
    [SerializeField] [Range(0, 100)] private float superChargeMax = 1f;
    [SerializeField] [Range(0, 100)] private float superChargeIncrease = 1f;

    //Private
    private Coroutine shootCoroutine = null;
    private Coroutine dashCoroutine = null;
    private Coroutine dashCooldownCoroutine = null;
    private Coroutine invincibleCoroutine = null;
    private Coroutine parryCoroutine = null;
    private Coroutine parryCooldownCoroutine = null;
    private Coroutine superChargeCoroutine = null;
    private float verticalVelocity;
    private float dashVelocity;
    private float horizontalDirection;
    private float verticalDirection;
    private float actualForcedGravity;
    private float parryCoroutineCounter;
    private float playerBulletsPerSecond;
    private RaycastHit aimHit;
    private Vector3 aimPoint;
    private Vector3 spawnPoint;
    private bool spawned;

    //Debug
    [Header("DEBUG")]
    [SerializeField] [ReadOnly] private float hP;
    [SerializeField] [ReadOnly] private float movementSpeed = 0f;
    [SerializeField] [ReadOnly] private int doubleJumps = 0;
    [SerializeField] [ReadOnly] private float dashes = 0;
    [SerializeField] [ReadOnly] private bool grounded;
    [SerializeField] [ReadOnly] private bool jumping;
    [SerializeField] [ReadOnly] private float actualVerticalReductionDuringDash = 1;
    [SerializeField] [ReadOnly] private bool dashOnCooldown;
    [SerializeField] [ReadOnly] private Global.PlayerState playerState = Global.PlayerState.Default;
    [SerializeField] [ReadOnly] private float superCharge;



    /// <summary>
    /// Updates necessary values on direct changes in the hierarchy during both runtime and edit mode.
    /// </summary>
    [ExecuteInEditMode]
    private void OnValidate()
    {

        hP = maxHP;
        movementSpeed = baseMovementSpeed;
        doubleJumps = maxDoubleJumps;
        dashes = maxDashes;
        playerBulletsPerSecond = basePlayerBulletsPerSecond;

    }



    /// <summary>
    /// Player Setup.
    /// </summary>
    public void Start()
    {

        if (!spawned)
        {
            spawnPoint = playerRigi.position;
            spawned = true;
        }

        else
        {
            playerRigi.position = spawnPoint;
        }

        movementSpeed = baseMovementSpeed;
        doubleJumps = maxDoubleJumps;
        dashes = maxDashes;

        hP = maxHP;
        InterfaceController.instance.UpdatePlayerHP(hP, maxHP);

        playerState = Global.PlayerState.Default;
        InterfaceController.instance.UpdatePlayerState(playerState);

        playerBulletsPerSecond = basePlayerBulletsPerSecond;

    }


    /// <summary>
    /// Freezes the player.
    /// </summary>
    public void Freeze()
    {

        playerRigi.velocity = Vector3.zero;

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
    public void Upd8(float horizontalDirection, float verticalDirection)
    {
        float temp = this.horizontalDirection;
        this.horizontalDirection = horizontalDirection;
        this.verticalDirection = verticalDirection;

        if (!grounded)
        {
            if (verticalDirection < 0)
            {
                actualForcedGravity = -forcedPower;
            }
        }

        else
        {
            actualForcedGravity = 0f;
        }

        //Dash Controller
        if (horizontalDirection != temp || verticalDirection != 0)
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

            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + GetComponentInChildren<Collider>().bounds.extents.y, transform.position.z), Vector3.down, groundcheckDistance, 1 << 9))
            {
                doubleJumps = maxDoubleJumps;
                dashes = maxDashes;
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
        playerRigi.velocity = new Vector3((horizontalDirection * movementSpeed) + dashVelocity, (verticalVelocity + actualForcedGravity) * actualVerticalReductionDuringDash, 0f);
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
                actualForcedGravity = 0f;
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
            if (dashCooldownCoroutine == null)
            {
                dashCoroutine = StartCoroutine(DashCoroutine());
                dashCooldownCoroutine = StartCoroutine(DashCooldownCoroutine());
            }
        }

        if (!grounded)
        {
            if (dashes > 0)
            {
                if (dashCooldownCoroutine == null)
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
        Invincible(dashInvincibleDuration);

        if (horizontalDirection < 0)
        {
            dashVelocity = -1 * dashPower;
        }

        else
        {
            dashVelocity = dashPower;
        }
        
        AudioController.instance.PlayerDash();
        actualVerticalReductionDuringDash = dashVerticalReduction;
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
        dashCooldownCoroutine = null;
        yield break;
    }

    

    /// <summary>
    /// Updates the direction the player is aiming towards.
    /// </summary>
    public void Aim()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out aimHit, 0))
        {

            if (aimHit.transform.gameObject.tag != "Player")
            {
                aimPoint = aimHit.point;
            }

        }

        else
        {
            aimPoint = ray.GetPoint(playerBulletTrajectoryDistance);
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

        float xangle = Mathf.Atan2(point.z - playerRigi.position.z, point.y - playerRigi.position.y) * 180 / Mathf.PI;
        float yangle = Mathf.Atan2(point.x - playerRigi.position.x, point.z - playerRigi.position.z) * 180 / Mathf.PI;

        GameObject bullet = Instantiate(bulletPrefab, playerRigi.position, Quaternion.Euler(xangle, yangle, 0), bullets);
        Destroy(bullet, playerBulletLifetime);

        Vector3 dir = point - playerRigi.position;

        dir.Normalize();

        bullet.GetComponent<Rigidbody>().velocity = dir * playerBulletSpeed;

        bullet.GetComponent<PlayerBullet>().SetDamage(playerBulletDamage);

        AudioController.instance.PlayerShoot();

        yield return new WaitForSeconds(1 / playerBulletsPerSecond);
        shootCoroutine = null;
        yield break;

    }

    public void PlayerShootReverb()
    {
        AudioController.instance.PlayerGunReverb();
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
    /// Checks whether the player should die or get hit by the source depending on the amount sent as a parameter.
    /// </summary>
    /// <param name="amt"></param>
    public void Receive(float amt)
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
    private void Hit(float amt)
    {
        hP -= amt;
        Invincible(hitInvincibleDuration);
        InterfaceController.instance.UpdatePlayerHP(hP, maxHP);
    }



    /// <summary>
    /// Parry WIP
    /// </summary>
    public void Parry()
    {

        if (parryCooldownCoroutine == null)
        {
            parryCoroutine = StartCoroutine(ParryCoroutine());
            parryCooldownCoroutine = StartCoroutine(ParryCooldownCoroutine());
        }

    }

    private IEnumerator ParryCoroutine()
    {

        parrybox.enabled = true;
        //playerState = Global.PlayerState.Invincible;    

        for (parryCoroutineCounter = sledgeDuration; parryCoroutineCounter > 0; parryCoroutineCounter -= Time.deltaTime)
        {

            if (parryCoroutineCounter <= (sledgeDuration - parryDuration))
            {
                parrybox.enabled = false;
                //playerState = Global.PlayerState.Default;
                break;
            }

            yield return null;

        }

        yield return new WaitForSeconds(parryCoroutineCounter);
        parryCoroutine = null;
        yield break;

    }

    private IEnumerator ParryCooldownCoroutine()
    {
        yield return new WaitUntil(() => parryCoroutine == null);
        yield return new WaitForSeconds(parryCooldown);
        parryCooldownCoroutine = null;
        yield break;
    }



    /// <summary>
    /// TBI
    /// </summary>
    public void SuperCharge()
    {

        if (superCharge == superChargeMax)
        {

            if (superChargeCoroutine == null)
            {
                superChargeCoroutine = StartCoroutine(SuperChargeCoroutine());
            }

        }

    }

    private IEnumerator SuperChargeCoroutine()
    {
        superCharge = 0;
        InterfaceController.instance.UpdateSuperChargeSlider(0);
        playerBulletsPerSecond *= 10f; //Hardcoded
        yield return new WaitForSeconds(1f); //Hardcoded
        playerBulletsPerSecond = basePlayerBulletsPerSecond;
        superChargeCoroutine = null;
        yield break;
    }

    public void IncreaseSuperCharge()
    {

        if (superCharge + superChargeIncrease >= superChargeMax)
        {
            superCharge = superChargeMax;
        }

        else
        {
            superCharge += superChargeIncrease;
        }

        InterfaceController.instance.UpdateSuperChargeSlider(superCharge);

    }



    //Getters & Setters
    public float GetSuperChargeMax()
    {
        return superChargeMax;
    }

    public Global.PlayerState GetPlayerState()
    {
        return playerState;
    }

}
