using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{

    public static bool shiftDashing = true;
    public static bool doubleTapDashing = true;

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
