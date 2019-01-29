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

    private Coroutine restartCoroutine;

    //Design

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
            
            UpdateBoss();
        }

    }


    float direction;

    /// <summary>
    /// Updates everything in player.
    /// </summary>
    

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

        Vector3 SpreadShotBulletSpawnPosition = bossController.RandomSpawnPoint();
        float spreadShotStartingTarget = spreadShot.generateSpreadShootTarget();

        yield return new WaitForSeconds(1f);
       // Debug.Log("Testing");
        //int i = 0;
        for (counter = 3; counter > 0; counter -= Time.deltaTime)
        {
          //  i++;
            //Debug.Log(i);

            switch (random)
            {
                case 1:
                    bossController.RandomSpawnPointShoot();
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
