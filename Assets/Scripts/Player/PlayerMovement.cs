﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerController))]
public class PlayerMovement : MonoBehaviour
{
    private PlayerController player;
    private DashAction dash;

    [SerializeField] [Range(0, 100)] private float baseMovementSpeed = 15f; //Base Movement Speed
    [SerializeField] [ReadOnly] private float movementSpeed = 0f;
    [SerializeField] [ReadOnly] private float actualVerticalReductionDuringDash = 1;


    [Header("GRAVITY")]
    [SerializeField] [Range(-1000, 1000)] private float gravity = 75f; //Gravity (Works agaisnt Jump Power)
    [SerializeField] [Range(0, 100)] private float forcedGravitySpeed = 25f; //The additional force affecting the player on pressed down, during aired
    [SerializeField] [Range(0, 100)] internal float forcedPower = 40f; //Forced gravity power when pressing down key.

    

    [Header("JUMP")]
    [SerializeField] [Range(0, 100)] private float jumpPower = 25f; //Jump Power (Works against Gravity)
    [SerializeField] [Range(0, 100)] private int maxDoubleJumps = 1; //Number of jumps possible in air
    [SerializeField] [ReadOnly] private int doubleJumps = 0;

    internal float actualForcedGravity;
    internal float verticalVelocity;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
        doubleJumps = maxDoubleJumps;
        movementSpeed = baseMovementSpeed;

        dash = GetComponent<DashAction>();
    }

    public void Move()
    {
        player.rigi.velocity = new Vector3(GetHorizontalMovement(player.horizontalDirection), GetVertical() * actualVerticalReductionDuringDash, 0f);
    }
    public float GetVertical()
    {
        return verticalVelocity + actualForcedGravity;
    }
    public float GetHorizontalMovement(float horizontalDirection)
    {
        return (horizontalDirection * movementSpeed) + dash.velocity;
    }

    public void Gravity()
    {

        if (player.grounded && !player.jumping)
        {
            verticalVelocity = -gravity * Time.deltaTime;
        }

        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

    }
    public void JumpReset()
    {
        doubleJumps = maxDoubleJumps;
    }
    public void Jump()
    {

        if (player.grounded)
        {

            player.jumping = true;
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

}