using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    //Instance
    public static GameController instance;

    //Design
    [SerializeField] private float doubleTapInterval = 0.25f;
    [SerializeField] private Texture2D crosshairTexture;

    //Debug
    [SerializeField] private Global.GameState gameState = Global.GameState.Idle;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private BossController bossController;

    //Private
    private bool canDashRight = false;
    private bool canDashLeft = false;
    private Coroutine restartCoroutine;

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

        SetCursorToCrosshair();

        gameState = Global.GameState.Game;

    }

    private void SetCursorToCrosshair()
    {
        Cursor.SetCursor(crosshairTexture, new Vector2(8, 8), CursorMode.Auto);
    }

    private void Update()
    {

        if (gameState == Global.GameState.Game)
        {
            UpdatePlayer();
            UpdateBoss();
        }

    }



    public void Restart()
    {

        playerController.Start();
        bossController.Start();
        gameState = Global.GameState.Game;

    }



    private void UpdatePlayer()
    {

        float direction = CalculateDirection();

        playerController.Upd8(direction, 0f);

        playerController.Move();

        playerController.Gravity();

        if (InputController.instance.GetKeyDownJump())
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

            if (InputController.instance.GetKeyDownLeftShift() && direction != 0)
            {
                playerController.Dash();
            }

        }

        if (InputController.instance.GetMouseButtonUpLeft())
        {
            playerController.PlayerShootReverb();
        }

    }

    private void UpdateBoss()
    {
        bossController.NewMove();
        bossController.Shoot();

        if (InputController.instance.GetKeyDownTest())
        {
            bossController.Laser();
        }

    }

    public void FreezeBoss()
    {
        bossController.Freeze();
    }

    public void FreezePlayer()
    {
        playerController.Freeze();
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
