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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
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

        if (InputController.instance.GetMouseButtonLeft())
        {
            playerController.Shoot();
        }

        //Dash: Double Tap
        if (Global.doubleTapDashing)
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

            if (InputController.instance.GetKeyDownLeftShift() && horizontalDirection != 0)
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

        if (InputController.instance.GetMouseButtonDownRight())
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
}
