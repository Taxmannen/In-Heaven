using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by: Filip Nilsson, Edited By: Daniel Nordahl and Jesper Uddefors
/// </summary>
public class InputController : MonoBehaviour {
    [HideInInspector] public bool isGamePad;

    public static InputController instance;

    public List<KeyCode> all = new List<KeyCode>();

    private KeyCode left = KeyCode.A;
    private KeyCode right = KeyCode.D;
    private KeyCode jump = KeyCode.Space;
    private KeyCode down = KeyCode.S;
    private KeyCode dash = KeyCode.LeftShift;
    private KeyCode shoot = KeyCode.Mouse0;
    private KeyCode parry = KeyCode.Mouse1;

    private KeyCode left1;
    private KeyCode right1;
    private KeyCode jump1 = KeyCode.W;
    private KeyCode down1;
    private KeyCode dash1;
    private KeyCode shoot1;
    private KeyCode parry1;

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

    public void SetIndexInAll(int inx, KeyCode key)
    {
        all[inx] = key;
        UpdateAll();
    }

    private void UpdateAll()
    {
        left = all[0];
        right = all[1];
        jump = all[2];
        down = all[3];
        dash = all[4];
        shoot = all[5];
        parry = all[6];
        parry = all[7];

        left1 = all[8];
        right1 = all[9];
        jump1 = all[10];
        down1 = all[11];
        dash1 = all[12];
        shoot1 = all[13];
        parry1 = all[12];
        parry1 = all[15];

    }

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
        if (gamepads.Length > 0 && gamepads[0].Length > 0) isGamePad = true;
        else isGamePad = false;
    }

    private void Start()
    {
        all.Clear();
        all.Add(left);
        all.Add(right);
        all.Add(jump);
        all.Add(down);
        all.Add(dash);
        all.Add(shoot);
        all.Add(parry);
        all.Add(parry);
        all.Add(left1);
        all.Add(right1);
        all.Add(jump1);
        all.Add(down1);
        all.Add(dash1);
        all.Add(shoot1);
        all.Add(parry1);
        all.Add(parry1);
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

    public bool GetKeyDownShoot()
    {
        if (isGamePad)
        {
            return shootController;
        }

        else
        {

            if (Input.GetKeyDown(shoot) || Input.GetKeyDown(shoot1))
            {
                return true;
            }

            else
            {
                return false;
            }

        }

    }

    public bool GetKeyDownParry()
    {
        if (isGamePad)
        {
             return Input.GetKeyDown(parryController);
        }

        else
        {

            if (Input.GetKeyDown(parry) || Input.GetKeyDown(parry1))
            {
                return true;
            }

            else
            {
                return false;
            }

        }

    }

    public bool GetKeyShoot()
    {

        if (isGamePad)
        {
            return shootController;
        }

        else
        {
            if (Input.GetKey(shoot) || Input.GetKey(shoot1))
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        
    }

    public bool GetKeyDownJump()
    {

        if (isGamePad)
        {
            return Input.GetKeyDown(jumpController);
        }

        else
        {

            if (Input.GetKeyDown(jump) || Input.GetKeyDown(jump1))
            {
                return true;
            }

            else
            {
                return false;
            }

        }

    }

    public bool GetKeyDownLeft()
    {

        if (isGamePad)
        {
            return leftController;
        }

        else
        {

            if (Input.GetKeyDown(left) || Input.GetKeyDown(left1))
            {
                return true;
            }

            else
            {
                return false;
            }

        }

    }

    public bool GetKeyDownRight()
    {

        if (isGamePad)
        {
            return rightController;
        }

        else
        {

            if (Input.GetKeyDown(right) || Input.GetKeyDown(right1))
            {
                return true;
            }

            else
            {
                return false;
            }

        }

    }

    public bool GetKeyDownDown()
    {

        if (isGamePad)
        {
            return downController;
        }

        else
        {

            if (Input.GetKeyDown(down) || Input.GetKeyDown(down1))
            {
                return true;
            }

            else
            {
                return false;
            }

        }

    }

    public bool GetKeyLeft()
    {

        if (isGamePad)
        {
            return leftController;
        }

        else
        {

            if (Input.GetKey(left) || Input.GetKey(left1))
            {
                return true;
            }

            else
            {
                return false;
            }

        }

    }

    public bool GetKeyRight()
    {

        if (isGamePad)
        {
            return rightController;
        }

        else
        {

            if (Input.GetKey(right) || Input.GetKey(right1))
            {
                return true;
            }

            else
            {
                return false;
            }

        }

    }

    public bool GetKeyUpShoot()
    {

        if (isGamePad)
        {
            return !leftController;
        }

        else
        {

            if (Input.GetKeyUp(shoot) || Input.GetKeyUp(shoot1))
            {
                return true;
            }

            else
            {
                return false;
            }

        }

    }

    public bool GetKeyDownDash()
    {

        if (isGamePad)
        {
            return Input.GetKeyDown(dashController);
        }

        else
        {

            if (Input.GetKeyDown(dash) || Input.GetKeyDown(dash1))
            {
                return true;
            }

            else
            {
                return false;
            }

        }

    }



    public bool GetKeyDownTest()
    {
        return Input.GetKeyDown(test);
    }

}