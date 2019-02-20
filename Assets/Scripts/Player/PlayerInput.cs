using System.Collections;
using UnityEngine;

/// <summary>
/// Made By: Jesper Uddefors and Filip Nilsson
/// </summary>
public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private float doubleTapInterval = 0.25f;

    float horizontalDirection;
    float verticalDirection;
    private bool canDashRight = false;
    private bool canDashLeft = false;


    [Header("Testing Settings")]
    [SerializeField] private bool changeDashKeyToMouse;
    [SerializeField] private bool testKeyConfiguration;

    //Timer/TimeCounter
    [SerializeField] private float timeBeforeStartShooting = 0.2f;
    [SerializeField][ReadOnly] private float timer = 0;

    private void Update()
    {
        if (GameController.instance.gameState == Global.GameState.Game)
        {
            UpdatePlayer();
        }

        if (Input.GetKeyUp(KeyCode.U))
        {
            playerController.shootAction.onlyShootOnGround = !playerController.shootAction.onlyShootOnGround;
            Debug.Log("Only shoot while on Ground: " + playerController.shootAction.onlyShootOnGround);
        }

        if (Input.GetKeyUp(KeyCode.I))
        {
            playerController.shootAction.onlyShootWhenStandingStill = !playerController.shootAction.onlyShootWhenStandingStill;
            Debug.Log("Only shoot while not Moving: " + playerController.shootAction.onlyShootWhenStandingStill);
        }
    }

    private void UpdatePlayer()
    {
        horizontalDirection = GetHorizontal();
        verticalDirection = GetVerticalDirection();

        playerController.Upd8(horizontalDirection, verticalDirection);

        playerController.Move();

        playerController.Gravity();

        if (InputController.instance.GetKeyDownJump())
        {
            playerController.Jump();
        }

        playerController.Aim();

        if (AbleToShoot() && !playerController.parryAction.checkParryActive())
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
            if (GetDiffrentDashKey())
            {
                playerController.Dash();
            }
        }

        if (InputController.instance.GetKeyUpShoot())
        {
            playerController.shootAction.ShootReverb();
        }

        if (InputController.instance.GetKeyDownShoot())
        {
            // AudioController.instance.PlayerShootStart();
           // AudioController.instance.PlayerCommenceShooting();
        }

        if (ParryButtonOnMouse())
        {
            playerController.Parry();    
        }
        if(playerController.superChargeResource.superCharge >= playerController.superChargeResource.superChargeMax)
        {
            playerController.SuperCharge();
        }

    }

    float GetHorizontal()
    {
        float direction = 0;
        if (InputController.instance.GetKeyLeft())
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
            return InputController.instance.GetKeyDownParry();
        }

        else
        {
            return InputController.instance.GetKeyDownDash();
        }

    }

    //Might be done better but works as intended 
    private bool ParryButtonOnMouse()
    {
        if (testKeyConfiguration)
        {
            timer += Time.deltaTime;

            if (InputController.instance.GetKeyUpShoot() && timer < timeBeforeStartShooting)
            {
                Debug.Log("Parry");
                return true;
            }

            if (!InputController.instance.GetKeyShoot())
            {
                timer = 0;
            }
            return false;
        }
        return InputController.instance.GetKeyDownParry();
    }

    private bool AbleToShoot()
    {
        if (testKeyConfiguration)
        {
            if (timer > timeBeforeStartShooting && InputController.instance.GetKeyShoot())
            {
                //Debug.Log("Shooting");
                return true;
             }
            return false;
        }
        return InputController.instance.GetKeyShoot();
    }
}
