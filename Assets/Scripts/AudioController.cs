using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    //___________________________________________________

    [FMODUnity.EventRef]
    public string shoot;
    [FMODUnity.EventRef]
    public string bossHit;

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

    //___________________________________________________
    //_______PLAYER________

    public void Walk()
    {
        //FMODUnity.RuntimeManager.PlayOneShot(shoot);
    }
    public void Jump()
    {
        //FMODUnity.RuntimeManager.PlayOneShot(shoot);
    }
    public void DoubleJump()
    {
        //FMODUnity.RuntimeManager.PlayOneShot(shoot);
    }
    public void Dash()
    {
        //FMODUnity.RuntimeManager.PlayOneShot(shoot);
    }
    public void PlayerShoot()
    {
        FMODUnity.RuntimeManager.PlayOneShot(shoot);
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

    public void BossHit()
    {
        FMODUnity.RuntimeManager.PlayOneShot(bossHit);
    }

}
