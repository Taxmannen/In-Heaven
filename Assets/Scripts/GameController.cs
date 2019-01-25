using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    //Instance
    public static GameController instance;

    //Serialized
    [SerializeField] private PlayerController playerController;
    [SerializeField] private BossController bossController;
    [SerializeField] private SpreadShot spreadShot;

    //Private
    private bool canDashRight = false;
    private bool canDashLeft = false;
    private Coroutine restartCoroutine;

    //Design
    [SerializeField] private float doubleTapInterval = 0.25f;
    [SerializeField] private Texture2D crosshairTexture;

    [SerializeField] private float playerAccelerationControl = 1f;
    [SerializeField] private float playerDeaccelerationControl = 1f;

    //Debug
    [SerializeField] private Global.GameState gameState = Global.GameState.Idle;



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

    /// <summary>
    /// Sets the cursor texture to the crosshair texture.
    /// </summary>
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

    float horizontalDirection;
    float verticalDirection;
    float direction;

    /// <summary>
    /// Updates everything in player.
    /// </summary>
    private void UpdatePlayer()
    {

        horizontalDirection = CalculateHorizontalDirection();
        verticalDirection = CalculateVerticalDirection();

        playerController.Upd8(horizontalDirection, verticalDirection);

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

            if (InputController.instance.GetKeyDownLeftShift() && horizontalDirection != 0)
            {
                playerController.Dash();
            }

        }

        if (InputController.instance.GetMouseButtonUpLeft())
        {
            playerController.PlayerShootReverb();
        }

        if (InputController.instance.GetMouseButtonDownLeft())
        {
           // AudioController.instance.PlayerShootStart();
            AudioController.instance.PlayerCommenceShooting();
        }

        if (InputController.instance.GetMouseButtonDownRight())
        {
            playerController.Parry();
        }

        if (InputController.instance.GetKeyDownSupercharge())
        {
            playerController.SuperCharge();
        }

    }

    bool ready = true;

    /// <summary>
    /// Updates everything in boss.
    /// </summary>
    private void UpdateBoss()
    {
        bossController.NewMove();

        if (patternCoroutine == null)
        {
            patternCoroutine = StartCoroutine(Pattern());
        }

    }

    private Coroutine patternCoroutine;

    private float counter;

    

    private IEnumerator Pattern()
    {
        

        int random = Random.Range((int)1, (int)4); // Change to 4 for spread

        Vector3 SpreadShotBulletSpawnPosition= spreadShot.generateSpreadShotSpawn();
        float spreadShotStartingTarget = spreadShot.generateSpreadShootTarget();

        yield return new WaitForSeconds(1f);
        Debug.Log("Testing");
        for (counter = 3; counter > 0; counter -= Time.deltaTime)
        {

            switch (random)
            {
                case 1:
                    bossController.Shoot();
                    break;

                case 2:
                    bossController.Laser();
                    break;

                case 3:                   
                    spreadShot.SpreadShotShoot(SpreadShotBulletSpawnPosition, spreadShotStartingTarget);
                    break;
            }

            yield return null;
        }

        patternCoroutine = null;
        yield break;

    }

    private IEnumerator ShootPattern()
    {

        ready = false;

        for (counter = 3; counter > 0; counter -= Time.deltaTime)
        {
            bossController.Shoot();
            yield return null;
        }

        ready = true;
        patternCoroutine = null;
        yield break;
    }

    /// <summary>
    /// Restarts the game in game state.
    /// </summary>
    public void Restart()
    {

        playerController.Start();
        bossController.Start();
        gameState = Global.GameState.Game;

    }



    /// <summary>
    /// Calls on the boss to freeze.
    /// </summary>
    public void FreezeBoss()
    {
        bossController.Freeze();
    }

    /// <summary>
    /// Calls on the player to freeze.
    /// </summary>
    public void FreezePlayer()
    {
        playerController.Freeze();
    }



    /// <summary>
    /// Returns the horizontal direction of the player depending on input.
    /// </summary>
    /// <returns></returns>
    private float CalculateHorizontalDirection()
    {

        if (!InputController.instance.GetKeyLeft() && !InputController.instance.GetKeyRight())
        {
            
            if ((direction < 0 && direction > playerDeaccelerationControl * Time.deltaTime) || (direction > 0 && direction < playerDeaccelerationControl * Time.deltaTime))
            {
                direction = 0;
            }

            else
            {

                if (direction < 0)
                {
                    direction += playerDeaccelerationControl * Time.deltaTime;
                }

                if (direction > 0)
                {
                    direction -= playerDeaccelerationControl * Time.deltaTime;
                }

            }

        }

        else
        {

            if (InputController.instance.GetKeyLeft())
            {

                if (direction > 0)
                {
                    direction = -playerAccelerationControl * Time.deltaTime;
                }

                else
                {

                    if (direction >= -(1f + playerAccelerationControl * Time.deltaTime))
                    {
                        direction -= playerAccelerationControl * Time.deltaTime;
                    }

                    else
                    {
                        direction = -1f;
                    }

                }

            }

            if (InputController.instance.GetKeyRight())
            {

                if (direction < 0)
                {
                    direction = playerAccelerationControl * Time.deltaTime;
                }

                else
                {

                    if (direction <= (1f - playerAccelerationControl * Time.deltaTime))
                    {
                        direction += playerAccelerationControl * Time.deltaTime;
                    }

                    else
                    {
                        direction = 1f;
                    }

                }

            }

        }

        return direction;

    }

    /// <summary>
/// Returns the vertical direction of the player depending on input.
/// </summary>
/// <returns></returns>
    private float CalculateVerticalDirection()
    {

        float direction = 0f;

        if (InputController.instance.GetKeyDownDown())
        {
            direction -= 1f;
        }

        return direction;

    }



    /// <summary>
    /// Method which makes double tapping a direction to dash possible.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
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



    /// <summary>
    /// Sets the game state of the game controller to desired state.
    /// </summary>
    /// <param name="gameState"></param>
    public void SetGameState(Global.GameState gameState)
    {
        this.gameState = gameState;
    }

    public PlayerController GetPlayerController()
    {
        return playerController;
    }

}
