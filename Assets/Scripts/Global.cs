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
    }

    public enum PlayerState
    {
        Default,
        Invincible,
    }

    public enum BossState
    {
        Default,
    }

}
