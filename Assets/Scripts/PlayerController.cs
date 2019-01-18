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
    /// Updates general player variables such as bool's keeping track of whether the player is grounded/jumping or not but also in charge of resetting bool's like doubleJump and dashes.
    /// </summary>
    public void CustomUpdate()
    {

        if (verticalVelocity < 0)
        {
            jumping = false;
        }

        if (grounded && jumping)
        {
            grounded = false;
        }

        else
        {

            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + GetComponentInChildren<Collider>().bounds.extents.y, transform.position.z) ,Vector3.down, groundCheckRaycastDistance, layerMask))
            {
                doubleJumps = baseDoubleJumps;
                dashes = baseDashes;
                grounded = true;
            }

            else
            {
                grounded = false;
            }

        }

    }

    /// <summary>
    /// Moves the player in the direction sent as a parameter.
    /// </summary>
    /// <param name="direction"></param>
    public void Move(float direction)
    {

        if (direction == 0)
        {
            if (dashCoroutine != null)
            {
                StopCoroutine(dashCoroutine);
            }
            dashVelocity = 0f;
            dashCoroutine = null;
        }

        else
        {

            if (grounded && !dashing)
            {
                AudioController.instance.Walk();
            }

        }

        rigi.velocity = new Vector3((direction * movementSpeed) + dashVelocity, verticalVelocity, 0f);
    }

    /// <summary>
    /// Applies custom coded gravity on the player.
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
    /// Tells the player to jump/doubleJump if allowed to do so.
    /// </summary>
    public void Jump()
    {

        if (grounded)
        {
            jumping = true;
            AudioController.instance.Jump();
            verticalVelocity = jumpPower;
        }

        else
        {
            if (doubleJumps > 0)
            {
                doubleJumps--;
                AudioController.instance.DoubleJump();
                verticalVelocity = jumpPower;
            }
        }

    }

    /// <summary>
    /// (WIP) Shoots forward.
    /// </summary>
    public void Shoot()
    {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {

            if (hit.transform.gameObject.tag != "Player")
            {
                if (hit.point.z > 0)
                {
                    if (shootCoroutine == null)
                    {
                        shootCoroutine = StartCoroutine(ShootCoroutine(hit.point));
                    }
                }
            }

        }

        else
        {
            if (shootCoroutine == null)
            {
                shootCoroutine = StartCoroutine(ShootCoroutine(ray.GetPoint(bulletTrajectoryDistance)));
            }
        }

    }

    private IEnumerator ShootCoroutine(Vector3 point)
    {
        GameObject bullet = Instantiate(bulletPrefab, rigi.position, bulletPrefab.transform.rotation, null);
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
    /// Tells the player to dash in the direction sent as a parameter.
    /// </summary>
    /// <param name="direction"></param>
    public void Dash(float direction)
    {
        
        if (grounded)
        {
            if (dashCoroutine == null)
            {
                dashCoroutine = StartCoroutine(DashCoroutine(direction));
            }
        }

        if (!grounded)
        {
            if (dashes > 0)
            {
                if (dashCoroutine == null)
                {
                    dashes--;
                    dashCoroutine = StartCoroutine(DashCoroutine(direction));
                }
            }
        }

    }

    private IEnumerator DashCoroutine(float direction)
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

    /// <summary>
    /// Sets the state of the player to invincible for the duration sent as a parameter.
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
        InterfaceController.instance.UpdatePlayerState(playerState);
        yield return new WaitForSeconds(duration);
        playerState = Global.PlayerState.Default;
        InterfaceController.instance.UpdatePlayerState(playerState);
        yield break;
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
                hP -= amt;
                InterfaceController.instance.UpdatePlayerHP(hP, maxHP);
                Invincible(0.1f);
            }
        }

        else
        {
            InvincibleFeedback();
        }

    }

    private void Die()
    {
        hP = 0;
        InterfaceController.instance.UpdatePlayerHP(hP, maxHP);
        GameController.instance.SetGameState(Global.GameState.Idle);
        InterfaceController.instance.GameOver();
    }

    private void InvincibleFeedback()
    {
        //Invincible Visual Effects?
    }

    //Inspector
    [ExecuteInEditMode]
    void OnValidate()
    {
        hP = maxHP;
    }

}
