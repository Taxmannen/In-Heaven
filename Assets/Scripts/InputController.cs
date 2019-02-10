using UnityEngine;

/// <summary>
/// Made by: Filip Nilsson, Edited By: Daniel Nordahl and Jesper Uddefors
/// </summary>
public class InputController : MonoBehaviour {
    public bool isGamePad;

    public static InputController instance;

    private KeyCode jump = KeyCode.Space;
    private KeyCode up = KeyCode.W;
    private KeyCode left = KeyCode.A;
    private KeyCode right = KeyCode.D;
    private KeyCode down = KeyCode.S;
    private KeyCode supercharge = KeyCode.Q;

    private KeyCode jumpController = KeyCode.JoystickButton0;
    private KeyCode parryController = KeyCode.JoystickButton1;
    private KeyCode dashController = KeyCode.JoystickButton2;
    private KeyCode superchargeController = KeyCode.JoystickButton3;

    //For AXIS
    private bool shootController;
    private bool leftController;
    private bool rightController;
    private bool upController;
    private bool downController;

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

        string[] gamepads = Input.GetJoystickNames();
        if (gamepads.Length > 0) isGamePad = true;
        else isGamePad = false;
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Shoot") != 0) shootController = true;
        else                                shootController = false;

        if      (Input.GetAxisRaw("Horizontal") < 0) leftController = true;
        else if (Input.GetAxisRaw("Horizontal") > 0) rightController = true;
        else
        {
            leftController = false;
            rightController = false;
        }

        if (Input.GetAxisRaw("Vertical") < 0) upController = true;
        if (Input.GetAxisRaw("Vertical") > 0) downController = true;
        else
        {
            upController = false;
            downController = false;
        }
    }

    public bool GetMouseButtonDownLeft()
    {
        if (isGamePad) return shootController;
        else return Input.GetMouseButtonDown(0);
    }

    public bool GetMouseButtonDownRight()
    {
        if (isGamePad) return Input.GetKeyDown(parryController);
        else return Input.GetMouseButtonDown(1);
    }

    public bool GetMouseButtonLeft()
    {
        if (isGamePad) return shootController;
        return Input.GetMouseButton(0);
    }

    public bool GetKeyDownJump()
    {
        if (isGamePad) return Input.GetKeyDown(jumpController);
        else return Input.GetKeyDown(jump);
    }

    public bool GetKeyDownLeft()
    {
        if (isGamePad) return leftController;
        else return Input.GetKeyDown(left);
    }

    public bool GetKeyDownRight()
    {
        if (isGamePad) return rightController;
        else return Input.GetKeyDown(right);
    }

    public bool GetKeyDownDown()
    {
        if (isGamePad) return downController;
        else return Input.GetKeyDown(down);
    }

    public bool GetKeyLeft()
    {
        if (isGamePad) return leftController;
        else return Input.GetKey(left);
    }

    public bool GetKeyRight()
    {
        if (isGamePad) return rightController;
        else return Input.GetKey(right);
    }

    public bool GetMouseButtonUpLeft()
    {
        if (isGamePad) return !leftController; //??
        else return Input.GetMouseButtonUp(0);
    }

    public bool GetKeyDownLeftShift()
    {
        if (isGamePad) return Input.GetKeyDown(dashController);
        else return Input.GetKeyDown(KeyCode.LeftShift);
    }

    public bool GetKeyDownSupercharge()
    {
        if (isGamePad) return Input.GetKeyDown(superchargeController);
        else return Input.GetKeyDown(supercharge);
    }

    public bool GetKeyDownUp()
    {
        if (isGamePad) return Input.GetKeyDown(up);
        else return Input.GetKeyDown(up);
    }

    public bool GetKeyDownTest()
    {
        return Input.GetKeyDown(test);
    }
}