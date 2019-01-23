using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    [Header("")]
    [Header ("PLAYER SOUNDS")]

    [FMODUnity.EventRef]
    public string playerShoot;
    [FMODUnity.EventRef]
    public string playerDash;
    [FMODUnity.EventRef]
    public string playerJump;
    [FMODUnity.EventRef]
    public string playerDoubleJump;
    [FMODUnity.EventRef]
    public string playerGunReverb;
    [FMODUnity.EventRef]
    public string playerCommenceShooting;

    [Header("")]
    [Header ("BOSS SOUNDS")]

    [FMODUnity.EventRef]
    public string bossRecieveDamage;
    [FMODUnity.EventRef]
    public string bossDeath;

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


    //==========PLAYER==========

    public void PlayerWalk()
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
    public void PlayerDash()
    {
        FMODUnity.RuntimeManager.PlayOneShot(playerDash);
    }
    public void PlayerShoot()
    {
        FMODUnity.RuntimeManager.PlayOneShot(playerShoot);
    }
    public void PlayerCommenceShooting()
    {       
        FMODUnity.RuntimeManager.PlayOneShot(playerCommenceShooting);
    }
    public void PlayerGunReverb()
    {
        FMODUnity.RuntimeManager.PlayOneShot(playerGunReverb);
    }

    //==========ENEMY==========

    public void BossShoot()
    {

    }
    public void BossScatterShot()
    {

    }
    public void BossLaserRay()
    {

    }
    public void BossMove()
    {

    }

    public void BossHitRecieveDamage()
    {
        FMODUnity.RuntimeManager.PlayOneShot(bossRecieveDamage);
    }
    public void BossHitRecieveNoDamage()
    {

    }
    public void BossDeath()
    {
        FMODUnity.RuntimeManager.PlayOneShot(bossDeath);
    }

}
