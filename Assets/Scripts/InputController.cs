using UnityEngine;

public class InputController : MonoBehaviour {
    public bool isController;

    public static InputController instance;

    private KeyCode jump = KeyCode.Space;
    private KeyCode left = KeyCode.A;
    private KeyCode right = KeyCode.D;
    private KeyCode down = KeyCode.S;
    private KeyCode supercharge = KeyCode.Q;

    private KeyCode jumpController = KeyCode.JoystickButton0;
    private KeyCode parryController = KeyCode.JoystickButton1;
    private KeyCode dashController = KeyCode.JoystickButton2;
    private KeyCode superchargeController = KeyCode.JoystickButton3;

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
        if (isController) return shootController;
        else return Input.GetMouseButtonDown(0);
    }

    public bool GetMouseButtonDownRight()
    {
        if (isController) return Input.GetKeyDown(parryController);
        else return Input.GetMouseButtonDown(1);
    }

    public bool GetMouseButtonLeft()
    {
        if (isController) return shootController;
        return Input.GetMouseButton(0);
    }

    public bool GetKeyDownJump()
    {
        if (isController) return Input.GetKeyDown(jumpController);
        else return Input.GetKeyDown(jump);
    }

    public bool GetKeyDownLeft()
    {
        if (isController) return leftController;
        else return Input.GetKeyDown(left);
    }

    public bool GetKeyDownRight()
    {
        if (isController) return rightController;
        else return Input.GetKeyDown(right);
    }

    public bool GetKeyDownDown()
    {
        if (isController) return downController;
        else return Input.GetKeyDown(down);
    }

    public bool GetKeyLeft()
    {
        if (isController) return leftController;
        else return Input.GetKey(left);
    }

    public bool GetKeyRight()
    {
        if (isController) return rightController;
        else return Input.GetKey(right);
    }

    public bool GetMouseButtonUpLeft()
    {
        if (isController) return !leftController;
        else return Input.GetMouseButtonUp(0);
    }

    public bool GetKeyDownLeftShift()
    {
        if (isController) return Input.GetKeyDown(dashController);
        else return Input.GetKeyDown(KeyCode.LeftShift);
    }

    public bool GetKeyDownSupercharge()
    {
        if (isController) return Input.GetKeyDown(superchargeController);
        else return Input.GetKeyDown(supercharge);
    }

    //==================================================

    public bool GetKeyDownTest()
    {
        return Input.GetKeyDown(test);
    }

    //==================================================
}
