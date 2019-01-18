using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Components
    [SerializeField] private Rigidbody rigi;
    [SerializeField] private GameObject bulletPrefab;

    //Design
    [SerializeField] private int maxHP = 10; //Max Hit Points
    [SerializeField] private float baseMovementSpeed = 15f; //Base Movement Speed
    [SerializeField] private float gravity = 75f; //Gravity (Works agaisnt Jump Power)
    [SerializeField] private float jumpPower = 25f; //Jump Power (Works against Gravity)
    [SerializeField] private int baseDoubleJumps = 1; //Number of jumps possible in air
    [SerializeField] private float groundCheckRaycastDistance = 0.75f; //Distance to ground from players pivot point
    [SerializeField] private float bulletsPerSecond = 10f; //Bullets per second during left mouse hold
    [SerializeField] private float bulletSpeed = 25f; //The speed of the bullets
    [SerializeField] private float bulletDuration = 3f; //The duration the bullets last until they are destroyed
    [SerializeField] private float dashPower = 50f; //The speed of the dash (affects dash distance)
    [SerializeField] private float dashDuration = 0.1f; //The duration of the dash (affects dash distance)
    [SerializeField] private float baseDashes = 1; //Number of dashes possible in air
    [SerializeField] private float dashInvincibleDuration = 0.25f; //Duration of invincibility state after the start of a dash
    [SerializeField] private float hitInvincibleDuration = 0.1f; //Duration of invincibility state after being hit, necessary to avoid getting hit rapidly multiple times.

    //Debug
    [SerializeField] private int hP;
    [SerializeField] private float movementSpeed = 0f;
    [SerializeField] private int doubleJumps = 0;
    [SerializeField] private float dashes = 0;
    [SerializeField] private bool grounded;
    [SerializeField] private bool jumping;
    [SerializeField] private Global.PlayerState playerState = Global.PlayerState.Default;

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
    }

    /// <summary>
    /// Updates general player variables such as bool's keeping track of whether the player is grounded/jumping or not but also resets doubleJumps etc.
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

            if (Physics.Raycast(transform.position, Vector3.down, groundCheckRaycastDistance, layerMask))
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
            verticalVelocity = jumpPower;
        }

        else
        {
            if (doubleJumps > 0)
            {
                doubleJumps--;
                verticalVelocity = jumpPower;
            }
        }

    }

    public void Shoot()
    {

        if (shootCoroutine == null)
        {
            shootCoroutine = StartCoroutine(ShootCoroutine());
        }

    }

    private IEnumerator ShootCoroutine()
    {
        GameObject bullet = Instantiate(bulletPrefab, rigi.position, bulletPrefab.transform.rotation, null);
        Destroy(bullet, bulletDuration);
        bullet.GetComponent<Rigidbody>().velocity = Vector3.forward * bulletSpeed;
        yield return new WaitForSeconds(1 / bulletsPerSecond);
        shootCoroutine = null;
        yield break;
    }

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
                dashes--;
                if (dashCoroutine == null)
                {
                    dashCoroutine = StartCoroutine(DashCoroutine(direction));
                }
            }
        }

    }

    private IEnumerator DashCoroutine(float direction)
    {
        Invincible(dashInvincibleDuration);
        InterfaceController.instance.UpdatePlayerState(playerState);
        dashVelocity = direction * dashPower;
        yield return new WaitForSeconds(dashDuration);
        dashVelocity = 0f;
        dashCoroutine = null;
        yield break;
    }

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
        Debug.Log("Cancelled");
        playerState = Global.PlayerState.Default;
        InterfaceController.instance.UpdatePlayerState(playerState);
        yield break;
    }

    public void TakeDamage(int amt)
    {
        
        if (playerState != Global.PlayerState.Invincible)
        {
            if (hP - amt <= 0)
            {
                Die();
            }

            else
            {
                Receive(amt);
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
    }

    private void Receive(int amt)
    {
        hP -= amt;
        InterfaceController.instance.UpdatePlayerHP(hP, maxHP);
        Invincible(0.1f);
    }

    private void InvincibleFeedback()
    {
        //Invincible Visual Effects?
    }

}
