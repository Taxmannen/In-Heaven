using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class KeybindData : MonoBehaviour
{

    public static KeybindData instance;

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

    public enum Keyfunc
    {
        Left,
        Right,
        Jump,
        Tempname,
        Dash,
        ShootParry,
        Shoot,
        Parry,
    }

    private Text text;

    public void OCText(Text text)
    {
        this.text = text;
    }

    private Keyfunc keyFunc;

    public void SelectedKey(Keyfunc func)
    {
        keyFunc = func;
    }

    public void SavePrimary()
    {

    }

    public void SaveSecondary()
    {

    }

}
