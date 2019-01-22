using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    public static InputController instance;

    private KeyCode jump = KeyCode.Space;
    private KeyCode left = KeyCode.A;
    private KeyCode right = KeyCode.D;
    private KeyCode down = KeyCode.S;

    private KeyCode test = KeyCode.T;

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

    public bool GetMouseButtonLeft()
    {
        return Input.GetMouseButton(0);
    }

    public bool GetKeyDownJump()
    {
        return Input.GetKeyDown(jump);
    }

    public bool GetKeyDownLeft()
    {
        return Input.GetKeyDown(left);
    }

    public bool GetKeyDownRight()
    {
        return Input.GetKeyDown(right);
    }

    public bool GetKeyDownDown()
    {
        return Input.GetKeyDown(down);
    }

    public bool GetKeyLeft()
    {
        return Input.GetKey(left);
    }

    public bool GetKeyRight()
    {
        return Input.GetKey(right);
    }

    public bool GetMouseButtonUpLeft()
    {
        return Input.GetMouseButtonUp(0);
    }

    public bool GetKeyDownLeftShift()
    {
        return Input.GetKeyDown(KeyCode.LeftShift);
    }

    //==================================================

    public bool GetKeyDownTest()
    {
        return Input.GetKeyDown(test);
    }

    //==================================================

}
