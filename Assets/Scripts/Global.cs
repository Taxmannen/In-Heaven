using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by: Filip Nilsson
/// </summary>
public class Global : MonoBehaviour
{

    public static bool shiftDashing = true;
    public static bool doubleTapDashing = false;

    //Use These
    public static LayerMask groundLayerMask = 1 << 9;
    public static LayerMask playerBulletLayerMask = 0;

    public enum GameState
    {
        Idle,
        Menu,
        Game,
        Pause,
        Fail,
        Success,
    }

    public enum PlayerState
    {
        Default,
        Invincible,
        Dead,
    }

    public enum BossState
    {
        Default,
        Dead,
    }

}
