using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class KeybindData : MonoBehaviour
{

    public static KeybindData instance;

    [SerializeField] private Canvas canvas;

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }

        else
        {
            instance = this;
        }
    }

    private string savedString;
    Text selectedText;
    KeyCode savedKey;
    Coroutine checkForNew;

    public void ButtonPressed(Text buttonText)
    {
        savedString = buttonText.text;
        buttonText.text = "Rebinding...";
        selectedText = buttonText;
        checkForNew = StartCoroutine(CheckForNew());

    }

    private IEnumerator CheckForNew()
    {

        savedKey = new KeyCode();

        do
        {

            if (savedKey.ToString() != "None")
            {
                savedString = savedKey.ToString();
                selectedText.text = savedString;
                canvas.enabled = false;

                for (int i = 0; i < InputController.instance.all.Count; i++)
                {

                    if (InputController.instance.all[i] == savedKey)
                    {
                        InputController.instance.all[i] = KeyCode.None;

                        foreach (IGiveUpxD xD in FindObjectsOfType<IGiveUpxD>())
                        {
                            if (xD.enumToInt == (i + 1))
                            {
                                xD.GetComponentInChildren<Text>().text = "Unbound";
                            }
                        }

                    }

                }

                CheckWhich(selectedText.GetComponentInParent<IGiveUpxD>().enumToInt);
                CancelPressed();
                checkForNew = null;
                yield break;
            }

            yield return null;

        } while (true);

    }

    private void CheckWhich(int which)
    {

        switch (which)
        {
            case 1:
                InputController.instance.SetIndexInAll(0, savedKey);
                break;
            case 2:
                InputController.instance.SetIndexInAll(1, savedKey);
                break;
            case 3:
                InputController.instance.SetIndexInAll(2, savedKey);
                break;
            case 4:
                InputController.instance.SetIndexInAll(3, savedKey);
                break;
            case 5:
                InputController.instance.SetIndexInAll(4, savedKey);
                break;
            case 6:
                InputController.instance.SetIndexInAll(5, savedKey);
                break;
            case 7:
                InputController.instance.SetIndexInAll(6, savedKey);
                break;
            case 8:
                InputController.instance.SetIndexInAll(7, savedKey);
                break;
            case 9:
                InputController.instance.SetIndexInAll(8, savedKey);
                break;
            case 10:
                InputController.instance.SetIndexInAll(9, savedKey);
                break;
            case 11:
                InputController.instance.SetIndexInAll(10, savedKey);
                break;
            case 12:
                InputController.instance.SetIndexInAll(11, savedKey);
                break;
            case 13:
                InputController.instance.SetIndexInAll(12, savedKey);
                break;
            case 14:
                InputController.instance.SetIndexInAll(13, savedKey);
                break;
            case 15:
                InputController.instance.SetIndexInAll(14, savedKey);
                break;
            case 16:
                InputController.instance.SetIndexInAll(15, savedKey);
                break;


        }

    }

    private void OnGUI()
    {

        Event e = Event.current;

        if (e.isKey)
        {

            if (Input.GetKey(KeyCode.Tab))
            {
                savedKey = KeyCode.Tab;
            }

            else if (Input.GetKey(KeyCode.Backslash))
            {
                savedKey = KeyCode.Backslash;
            }

            else
            {
                savedKey = e.keyCode;
            }

        }

        else if (e.isMouse)
        {

            if (Input.GetKey(KeyCode.Mouse0))
            {
                savedKey = KeyCode.Mouse0;
            }

            else if (Input.GetKey(KeyCode.Mouse1))
            {
                savedKey = KeyCode.Mouse1;
            }

            else if (Input.GetKey(KeyCode.Mouse2))
            {
                savedKey = KeyCode.Mouse2;
            }

            else if (Input.GetKey(KeyCode.Mouse3))
            {
                savedKey = KeyCode.Mouse3;
            }

            else if (Input.GetKey(KeyCode.Mouse4))
            {
                savedKey = KeyCode.Mouse4;
            }

            else if (Input.GetKey(KeyCode.Mouse5))
            {
                savedKey = KeyCode.Mouse5;
            }

            else if (Input.GetKey(KeyCode.Mouse6))
            {
                savedKey = KeyCode.Mouse6;
            }


        }

        else if (e.shift)
        {

            if (Input.GetKey(KeyCode.LeftShift))
            {
                savedKey = KeyCode.LeftShift;
            }
            else
            {
                savedKey = KeyCode.RightShift;
            }

        }

    }

    public void CancelPressed()
    {
        selectedText.text = savedString;
        selectedText = null;
    }

}
