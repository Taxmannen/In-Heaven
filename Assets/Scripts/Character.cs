using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made By: Jesper Uddefors and Filip Nilsson
/// </summary>
public class Character : MonoBehaviour
{

    [SerializeField] [ReadOnly] internal float maxHP = 10000; //Max Hit Points
    [SerializeField] [ReadOnly] internal float hP;

    // Start is called before the first frame update
    protected void Start()
    {

        hP = maxHP;

    }

    /// <summary>
    /// Checks whether the character should die or get hit by the source depending on the amount sent as a parameter.
    /// </summary>
    /// <param name="amt"></param>
    internal virtual void Receive(float amt)
    {

        if (hP - amt <= 0)
        {
            Die();
        }

        else
        {
            Hit(amt);
        }

    }


    internal virtual void Die()
    {
        hP = 0;
    }

    /// <summary>
    /// Hits the character for the amount sent as a parameter.
    /// </summary>
    /// <param name="amt"></param>
    internal virtual void Hit(float amt)
    {
        hP -= amt;
    }

}
