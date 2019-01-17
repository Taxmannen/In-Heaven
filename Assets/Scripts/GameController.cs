using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] private PlayerController playerController;

    private void Update()
    {

        UpdatePlayer();

    }

    private void UpdatePlayer()
    {

        float direction = CalculateDirection();

        playerController.CustomUpdate();

        playerController.Move(direction);

        playerController.Gravity();

        if (InputController.instance.GetKeyDownJump())
        {
            playerController.Jump();
        }

        if (InputController.instance.GetMouseButtonLeft())
        {
            playerController.Shoot();
        }

        if (InputController.instance.GetKeyDownLeftShift() && direction != 0)
        {
            playerController.Dash(direction);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            playerController.TakeDamage(1);
        }

    }

    /// <summary>
    /// Returns the direction the player moves in depending on which inputs are pressed.
    /// </summary>
    /// <returns></returns>
    private float CalculateDirection()
    {

        float direction = 0f;

        if (InputController.instance.GetKeyLeft())
        {
            direction += -1f;
        }

        if (InputController.instance.GetKeyRight())
        {
            direction += 1f;
        }

        return direction;

    }

}
