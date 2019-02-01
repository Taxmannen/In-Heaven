using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float doubleTapInterval = 0.25f;

    float horizontalDirection;
    float verticalDirection;
    private bool canDashRight = false;
    private bool canDashLeft = false;


    [Header("Freature/Changes in Testing")]
    [SerializeField] private bool changeDashKeyToMouse;
    [SerializeField] private bool testKeyConfiguration;
    [SerializeField] private bool onlyShootOnGround;
    //Timer/TimeCounter
    [SerializeField] private float timeBeforeStartShooting = 0.2f;
    [SerializeField][ReadOnly] private float timer = 0;

    private void Update()
    {
        if(GameController.instance.gameState == Global.GameState.Game)
        {
            UpdatePlayer();
        }
    }

    private void UpdatePlayer()
    {

        horizontalDirection = GetHorizontal();
        verticalDirection = GetVerticalDirection();

        playerController.Upd8(horizontalDirection, verticalDirection);

        playerController.Move();

        playerController.Gravity();

        if (InputController.instance.GetKeyDownJump() || InputController.instance.GetKeyDownUp())
        {
            playerController.Jump();
        }

        playerController.Aim();

        if (AbleToShoot() && (onlyShootOnGround?playerController.grounded:true))
        {
            playerController.Shoot();
        }

        //Dash: Double Tap
        if (Global.doubleTapDashing && !InputController.instance.isGamePad)
        {

            if (InputController.instance.GetKeyDownLeft())
            {

                if (canDashLeft)
                {
                    playerController.Dash();
                }

                else
                {
                    StartCoroutine(DoubleTapDashCoroutine(-1f));
                }

            }

            if (InputController.instance.GetKeyDownRight())
            {

                if (canDashRight)
                {
                    playerController.Dash();
                }

                else
                {
                    StartCoroutine(DoubleTapDashCoroutine(1f));
                }

            }

        }

        //Dash: Shift
        if (Global.shiftDashing)
        {
            if (GetDiffrentDashKey() && horizontalDirection != 0)
            {
                playerController.Dash();
            }
        }

        if (InputController.instance.GetMouseButtonUpLeft())
        {
            playerController.shootAction.ShootReverb();
        }

        if (InputController.instance.GetMouseButtonDownLeft())
        {
            // AudioController.instance.PlayerShootStart();
            AudioController.instance.PlayerCommenceShooting();
        }

        if (ParryButtonOnMouse() && !AbleToShoot())
        {
            playerController.Parry();
            AudioController.instance.PlayerParryEvent();
        }

        if (InputController.instance.GetKeyDownSupercharge())
        {
            playerController.SuperCharge();
        }

    }
    float GetHorizontal()
    {
        float direction = 0;
        if(InputController.instance.GetKeyLeft())
        {
            direction = -1;
        }
        if (InputController.instance.GetKeyRight())
        {
            direction = 1;
        }
        return direction;
    }
    float GetVerticalDirection()
    {
        float direction = 0f;

        if (InputController.instance.GetKeyDownDown())
        {
            direction -= 1f;
        }
        return direction;
    }

    private IEnumerator DoubleTapDashCoroutine(float direction)
    {

        switch (direction)
        {

            case 1:
                canDashLeft = false;
                canDashRight = true;
                yield return new WaitForSeconds(doubleTapInterval);
                canDashRight = false;
                yield break;

            case -1:
                canDashRight = false;
                canDashLeft = true;
                yield return new WaitForSeconds(doubleTapInterval);
                canDashLeft = false;
                yield break;

        }

    }

    private bool GetDiffrentDashKey()
    {
        if (changeDashKeyToMouse)
        {
            return InputController.instance.GetMouseButtonDownRight();

        } else
        {
            return InputController.instance.GetKeyDownLeftShift();
        }

    }

    //Might be done better but works as intended 
    private bool ParryButtonOnMouse()
    {


        if(testKeyConfiguration)
        {
            timer += Time.deltaTime;

            if (InputController.instance.GetMouseButtonUpLeft() && timer < timeBeforeStartShooting)
            {
                Debug.Log("Parry");
                return true;
            }

            if (!InputController.instance.GetMouseButtonLeft())
            {
                timer = 0;
            }

            return false;
        }
        return InputController.instance.GetMouseButtonDownRight();
    }

    private bool AbleToShoot()
    {
        if (testKeyConfiguration)
        {
            if(timer > timeBeforeStartShooting && InputController.instance.GetMouseButtonLeft())
            {
                Debug.Log("Shooting");
                return true;
             }
            return false;
        }

        return InputController.instance.GetMouseButtonLeft();
    }
}
