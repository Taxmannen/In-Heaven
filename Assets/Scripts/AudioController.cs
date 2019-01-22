using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    //___________________________________________________

    [FMODUnity.EventRef]
    public string playerShoot;
    [FMODUnity.EventRef]
    public string bossRecieveDamage;
    [FMODUnity.EventRef]
    public string bossDeath;
    [FMODUnity.EventRef]
    public string playerDash;
    [FMODUnity.EventRef]
    public string playerJump;
    [FMODUnity.EventRef]
    public string playerDoubleJump;
    [FMODUnity.EventRef]
    public string gunReverb;

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
        
    }
    public void PlayerJump()
    {
        FMODUnity.RuntimeManager.PlayOneShot(playerJump);
    }
    public void PlayerDoubleJump()
    {
        FMODUnity.RuntimeManager.PlayOneShot(playerDoubleJump);
    }
    public void Dash()
    {
        FMODUnity.RuntimeManager.PlayOneShot(playerDash);
    }
    public void PlayerShoot()
    {
        FMODUnity.RuntimeManager.PlayOneShot(playerShoot);
    }
    void Shield()
    {

    }
    public void GunReverb()
    {
        FMODUnity.RuntimeManager.PlayOneShot(gunReverb);
    }

    //_________ENEMY_________

    public void EnemyShoot()
    {

    }
    public void Attack1()
    {

    }
    public void Attack2()
    {

    }
    public void Attack3()
    {

    }
    public void Move()
    {

    }

    public void BossHit()
    {
        FMODUnity.RuntimeManager.PlayOneShot(bossRecieveDamage);
    }
    public void BossDeath()
    {
        FMODUnity.RuntimeManager.PlayOneShot(bossDeath);
    }

}
