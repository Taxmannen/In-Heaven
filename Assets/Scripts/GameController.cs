using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    //Instance
    public static GameController instance;

    //Design
    [SerializeField] private float doubleTapInterval = 0.25f;

    //Debug
    [SerializeField] private Global.GameState gameState = Global.GameState.Idle;
    [SerializeField] private PlayerController playerController;

    //Private
    private bool canDashRight = false;
    private bool canDashLeft = false;

    private void Awake()
    {

        if (instance)
        {
            Destroy(gameObject);
        }

        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            enabled = true;
        }

    }

    private void Start()
    {
        gameState = Global.GameState.Game;
    }

    private void Update()
    {

        if (gameState == Global.GameState.Game)
        {
            UpdatePlayer();
        }

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

        //Dash: Double Tap
        if (Global.doubleTapDashing)
        {

            if (InputController.instance.GetKeyDownLeft())
            {

                if (canDashLeft)
                {
                    playerController.Dash(-1f);
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
                    playerController.Dash(1f);
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

            if (InputController.instance.GetKeyDownLeftShift() && direction != 0)
            {
                playerController.Dash(direction);
            }

        }
        
        //==================================================

        if (InputController.instance.GetKeyDownTest())
        {
            Time.timeScale = 0.025f;
            //playerController.Receive(1);
        }

        //==================================================

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

    public void SetGameState(Global.GameState gameState)
    {
        this.gameState = gameState;
    }

}
