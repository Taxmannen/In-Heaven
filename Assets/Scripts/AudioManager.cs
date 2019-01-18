using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;

    //___________________________________________________








    private void Awake()
    {
        if (audioManager)
        {
            Destroy(gameObject);
        }
        else
        {
            audioManager = this;
            DontDestroyOnLoad(gameObject);
            enabled = true;
        }
    }

    //___________________________________________________
    //_______PLAYER________

    void Walk()
    {
       
    }
    void Jump()
    {

    }
    void DoubleJump()
    {

    }
    void Dash()
    {

    }
    void PlayerShoot()
    {

    }
    void Shield()
    {

    }

    //_________ENEMY_________

    void EnemyShoot()
    {

    }
    void Attack1()
    {

    }
    void Attack2()
    {

    }
    void Attack3()
    {

    }
    void Move()
    {

    }
}
