using System.Collections;
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


    [Header("Acceleration")]
    [SerializeField] [Range(0.1f, 2.0f)][Tooltip("The value is in Seconds.")] private float timeToMaxMovmentSpeed = 0.1f;
    [SerializeField] [Range(0.1f, 2.0f)][Tooltip("The value is in Seconds.")] private float timeToMinMovementSpeed = 0.1f;



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

    [Header("DEBUG")]
    internal float velocityDirection;
    internal float deaccelerationValue;
    internal float accelerationValue;

    private void OnValidate()
    {
        accelerationValue = baseMovementSpeed / timeToMaxMovmentSpeed;
        deaccelerationValue = baseMovementSpeed / timeToMinMovementSpeed;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
        doubleJumps = maxDoubleJumps;
        movementSpeed = baseMovementSpeed;
        accelerationValue = baseMovementSpeed / timeToMaxMovmentSpeed;
        deaccelerationValue = baseMovementSpeed / timeToMinMovementSpeed;
        dash = GetComponent<DashAction>();
    }


    public float HorizontalAcceleration(float direction)
    {
        if (direction == 0 )
        {

            if ((velocityDirection < 0 && velocityDirection > deaccelerationValue * Time.deltaTime) || (velocityDirection > 0 && velocityDirection < deaccelerationValue * Time.deltaTime))
            {
                velocityDirection = 0;

            }
            else
            {
                if (velocityDirection > 0)
                {
                    velocityDirection -= deaccelerationValue * Time.deltaTime;
                }
                if (velocityDirection < 0)
                {
                    velocityDirection += deaccelerationValue * Time.deltaTime;
                }
            }





        } else
        {
            if(direction > 0)
            {
                if(velocityDirection > 0)
                {
                    velocityDirection += accelerationValue * Time.deltaTime;
                } else
                {
                    velocityDirection += deaccelerationValue * Time.deltaTime;
                }
                
            } else if(direction < 0)
            {

                if(velocityDirection < 0)
                {
                    velocityDirection -= accelerationValue * Time.deltaTime;
                } else
                {
                    velocityDirection -= deaccelerationValue * Time.deltaTime;
                }
               
            }

        }
        velocityDirection = Mathf.Clamp(velocityDirection, -baseMovementSpeed, baseMovementSpeed);
        player.animator.SetFloat("Movement", velocityDirection);
        player.animator.transform.rotation = Quaternion.AngleAxis(direction * 90, Vector3.up);
        return velocityDirection;
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
        return HorizontalAcceleration(horizontalDirection) + dash.velocity;
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
            Statistics.instance.numberOfJumps++;

        }

        else
        {

            if (doubleJumps > 0)
            {
                actualForcedGravity = 0f;
                doubleJumps--;
                verticalVelocity = jumpPower;
                AudioController.instance.PlayerDoubleJump();
                Statistics.instance.numberOfDoubleJumps++;
            }

        }

    }

}
/// Horizontal Movment Acceleration needs to be implemented back/better
/// Old Code down Below 

//[SerializeField] private float playerAccelerationControl = 1f;
//[SerializeField] private float playerDeaccelerationControl = 1f;
//private float CalculateHorizontalDirection()
//{

//    if (!InputController.instance.GetKeyLeft() && !InputController.instance.GetKeyRight())
//    {

//        if ((direction < 0 && direction > playerDeaccelerationControl * Time.deltaTime) || (direction > 0 && direction < playerDeaccelerationControl * Time.deltaTime))
//        {
//            direction = 0;
//        }

//        else
//        {

//            if (direction < 0)
//            {
//                direction += playerDeaccelerationControl * Time.deltaTime;
//            }

//            if (direction > 0)
//            {
//                direction -= playerDeaccelerationControl * Time.deltaTime;
//            }

//        }

//    }

//    else
//    {

//        if (InputController.instance.GetKeyLeft())
//        {

//            if (direction > 0)
//            {
//                direction = -playerAccelerationControl * Time.deltaTime;
//            }

//            else
//            {

//                if (direction >= -(1f + playerAccelerationControl * Time.deltaTime))
//                {
//                    direction -= playerAccelerationControl * Time.deltaTime;
//                }

//                else
//                {
//                    direction = -1f;
//                }

//            }

//        }

//        if (InputController.instance.GetKeyRight())
//        {

//            if (direction < 0)
//            {
//                direction = playerAccelerationControl * Time.deltaTime;
//            }

//            else
//            {

//                if (direction <= (1f - playerAccelerationControl * Time.deltaTime))
//                {
//                    direction += playerAccelerationControl * Time.deltaTime;
//                }

//                else
//                {
//                    direction = 1f;
//                }

//            }

//        }

//    }

//    return direction;

//}