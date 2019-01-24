using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocationTracker : MonoBehaviour
{

    public static float playerLocationX;
    public static float playerLocationY;

    
   //tracks the location of the player
    void Update()
    {
        playerLocationX = transform.position.x;
        playerLocationY = transform.position.y;

    }
}
