﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Serialized
    [Header("SERIALIZED")]
    [SerializeField] internal Rigidbody rigi;

    //Design
    [Header("GENERAL")]
    [SerializeField] [Range(0, 1000)] private float maxHP = 10; //Max Hit Points

    [SerializeField] [Range(0, 10)] private float groundcheckDistance = 1.15f; //Distance to ground from players pivot point (FIRST VALUE = HALF PLAYER HEIGHT)

    [Header("HIT")]
    [SerializeField] [Range(0, 10)] private float hitInvincibleDuration = 0.1f; //Duration of invincibility state after being hit, necessary to avoid getting hit rapidly multiple times.    

    //Private
    internal float horizontalDirection;
    internal float verticalDirection;

    private Vector3 spawnPoint;
    private bool spawned;

    //Debug
    [Header("DEBUG")]
    [SerializeField] [ReadOnly] private float hP;

    [SerializeField] [ReadOnly] internal bool grounded;
    [SerializeField] [ReadOnly] internal bool jumping;

    [SerializeField] [ReadOnly] private bool dashOnCooldown;
    [SerializeField] [ReadOnly] internal Global.PlayerState playerState = Global.PlayerState.Default;

    internal DashAction dashAction;
    internal ParryAction parryAction;
    internal ShootAction shootAction;
    internal InvincibleEffect invincible;
    internal PlayerMovement movement;
    internal SuperChargeResource superChargeResource;
    internal AimMechanic aim;
    /// <summary>
    /// Updates necessary values on direct changes in the hierarchy during both runtime and edit mode.
    /// </summary>
    [ExecuteInEditMode]
    private void OnValidate()
    {
        hP = maxHP;
        dashAction = dashAction == null ? GetComponent<DashAction>() : dashAction;
        parryAction = parryAction == null ? GetComponent<ParryAction>() : parryAction;
        shootAction = shootAction == null ? GetComponent<ShootAction>() : shootAction;
        invincible = invincible == null ? GetComponent<InvincibleEffect>() : invincible;
        movement = movement == null ? GetComponent<PlayerMovement>() : movement;
        superChargeResource = superChargeResource == null ? GetComponent<SuperChargeResource>() : superChargeResource;
    }
    /// <summary>
    /// Player Setup.
    /// </summary>
    public void Start()
    {

        if (!spawned)
        {
            spawnPoint = rigi.position;
            spawned = true;
        }

        else
        {
            rigi.position = spawnPoint;
        }

        hP = maxHP;
        InterfaceController.instance.UpdatePlayerHP(hP, maxHP);

        playerState = Global.PlayerState.Default;
        InterfaceController.instance.UpdatePlayerState(playerState);

        aim = aim == null ? GetComponent<AimMechanic>() : aim;

        dashAction = dashAction == null ? GetComponent<DashAction>() : dashAction;
        parryAction = parryAction == null ? GetComponent<ParryAction>() : parryAction;
        shootAction = shootAction == null ? GetComponent<ShootAction>() : shootAction;
        invincible = invincible == null ? GetComponent<InvincibleEffect>() : invincible;
        movement = movement == null ? GetComponent<PlayerMovement>() : movement;
        superChargeResource = superChargeResource == null ? GetComponent<SuperChargeResource>() : superChargeResource;

    }
    /// <summary>
    /// Freezes the player.
    /// </summary>
    public void Freeze()
    {
        rigi.velocity = Vector3.zero;

        dashAction.MyStopCorutine();

        invincible.MyStopCoroutine();

        shootAction.DestroyAllBullets();
        shootAction.MyStopCorutine();

    }

    /// <summary>
    /// Updates general player values.
    /// </summary>
    public void Upd8(float horizontalDirection, float verticalDirection)
    {
        float temp = this.horizontalDirection;
        this.horizontalDirection = horizontalDirection;
        this.verticalDirection = verticalDirection;

        // If player wants to go faster down
        if (!grounded)
        {
            if (verticalDirection < 0)
            {
                movement.actualForcedGravity = -movement.forcedPower;
            }
        }

        else
        {
            movement.actualForcedGravity = 0f;
        }

        //Ground/Air check
        if (movement.verticalVelocity > 0)
        {
            grounded = false;
        }

        else
        {

            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y + GetComponentInChildren<Collider>().bounds.extents.y, transform.position.z), Vector3.down, groundcheckDistance, 1 << 9))
            {
                movement.JumpReset();
                dashAction.Reset();
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
        movement.Move();
    }
    public void Jump()
    {
        movement.Jump();
    }
    public void Gravity()
    {
        movement.Gravity();
    }
    public void Parry()
    {
        parryAction.Parry();
    }
    public void Dash()
    {
        dashAction.Dash();
    }
    public void Shoot()
    {
        shootAction.Shoot();
    }
    public void SuperCharge()
    {
        superChargeResource.SuperCharge();
    }
    public void Invincible(float duration)
    {
        invincible.Invincible(duration);
    }
    public void Aim()
    {
        aim.Aim();
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

    //Getters
    public float GetSuperChargeMax()
    {
        return superChargeResource.superChargeMax;
    }

    public Global.PlayerState GetPlayerState()
    {
        return playerState;
    }

    public float GetHorizontalDirection()
    {
        return horizontalDirection;
    }

    public float GetDashVelocity()
    {
        return dashAction.velocity;
    }

    public bool GetShootDuringDash()
    {
        return dashAction.shootDuringDash;
    }
}
