using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by: Filip Nilsson, Edited By: Jesper Uddefors
/// </summary>
public class GameController : MonoBehaviour
{

    //Instance
    public static GameController instance;

    //Serialized
    [SerializeField] internal PlayerController playerController;
    [SerializeField] private BossController bossController;
    [SerializeField] private SpreadShot spreadShot;

    //Private
    private Coroutine restartCoroutine;

    //Design
    [SerializeField] private Texture2D crosshairTexture;

    //Debug
    [SerializeField] internal Global.GameState gameState = Global.GameState.Idle;

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
    /// <summary>
    /// Restarts the game in game state.
    /// </summary>
    public void Restart()
    {

        playerController.Start();
        //bossController.Start();
        gameState = Global.GameState.Game;
        AudioController.instance.StopFailMenuDuck();

    }
    /// <summary>
    /// Calls on the boss to freeze.
    /// </summary>
    public void FreezeBoss()
    {
        //bossController.Freeze();
    }

    /// <summary>
    /// Calls on the player to freeze.
    /// </summary>
    public void FreezePlayer()
    {
        playerController.Freeze();
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