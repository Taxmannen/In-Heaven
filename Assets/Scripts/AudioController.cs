﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    [Header("")]
    [Header("PLAYER SOUNDS")]

    [FMODUnity.EventRef]
    public string playerShoot;
    FMOD.Studio.EventInstance playerShootEv;
    [FMODUnity.EventRef]
    public string playerDash;
    FMOD.Studio.EventInstance playerDashEv;
    [FMODUnity.EventRef]
    public string playerJump;
    FMOD.Studio.EventInstance playerJumpEv;
    [FMODUnity.EventRef]
    public string playerDoubleJump;
    FMOD.Studio.EventInstance playerDoubleJumpEv;
    [FMODUnity.EventRef]
    public string playerGunReverb;
    FMOD.Studio.EventInstance playerGunReverbEv;
    [FMODUnity.EventRef]
    public string playerCommenceShooting;
    FMOD.Studio.EventInstance playerCommenceShootingEv;
    [FMODUnity.EventRef]
    public string playerParryEvent;
    FMOD.Studio.EventInstance playerParryEventEv;
    [FMODUnity.EventRef]
    public string playerSuccessfulParry;
    FMOD.Studio.EventInstance playerSuccessfulParryEv;

    [Header("")]
    [Header("BOSS SOUNDS")]

    [FMODUnity.EventRef]
    public string bossHitRecieveDamage;
    FMOD.Studio.EventInstance bossHitRecieveDamageEv;
    [FMODUnity.EventRef]
    public string bossHitRecieveNoDamage;
    FMOD.Studio.EventInstance bossHitRecieveNoDamageEv;
    [FMODUnity.EventRef]
    public string bossDeath;
    FMOD.Studio.EventInstance bossDeathEv;
    [FMODUnity.EventRef]
    public string bossShoot;
    FMOD.Studio.EventInstance bossShootEv;
    private void Start()
    {
        playerShootEv = FMODUnity.RuntimeManager.CreateInstance(playerShoot);
        playerDashEv = FMODUnity.RuntimeManager.CreateInstance(playerDash);
        playerJumpEv = FMODUnity.RuntimeManager.CreateInstance(playerJump);
        playerDoubleJumpEv = FMODUnity.RuntimeManager.CreateInstance(playerDoubleJump);
        playerGunReverbEv = FMODUnity.RuntimeManager.CreateInstance(playerGunReverb);
        playerCommenceShootingEv = FMODUnity.RuntimeManager.CreateInstance(playerCommenceShooting);
        playerParryEventEv = FMODUnity.RuntimeManager.CreateInstance(playerParryEvent);
        playerSuccessfulParryEv = FMODUnity.RuntimeManager.CreateInstance(playerSuccessfulParry);
        bossHitRecieveDamageEv = FMODUnity.RuntimeManager.CreateInstance(bossHitRecieveDamage);
        bossHitRecieveNoDamageEv = FMODUnity.RuntimeManager.CreateInstance(bossHitRecieveNoDamage);
        bossDeathEv = FMODUnity.RuntimeManager.CreateInstance(bossDeath);
        bossShootEv = FMODUnity.RuntimeManager.CreateInstance(bossShoot);

        //shootingEvent.getParameter("Stop", out stopShooting);
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
    }

    //_______PLAYER________

    public void Walk()
    {
        
    }
    public void PlayerJump()
    {
        playerJumpEv.start();
    }
    public void PlayerDoubleJump()
    {
        playerDoubleJumpEv.start();
    }
    public void PlayerDash()
    {
        playerDashEv.start();
    }
    public void PlayerShoot()
    {
        playerShootEv.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        playerShootEv.start();       
    }

    public void PlayerCommenceShooting()
    {
        playerCommenceShootingEv.start();
    }
    public void PlayerGunReverb()
    {
        playerGunReverbEv.start();
    }
    public void PlayerSuccessfulParry()
    {
        playerSuccessfulParryEv.start();
    }
    public void PlayerUnsuccessfulParry()
    {

    }
    public void PlayerParryEvent()
    {
        playerParryEventEv.start();
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

    public void BossHitRecieveDamage()
    {
        bossHitRecieveDamageEv.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        bossHitRecieveDamageEv.start(); 
    }
    public void BossHitRecieveNoDamage()
    {
        bossHitRecieveDamageEv.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        bossHitRecieveDamageEv.start();
    }
    public void BossDeath()
    {
        bossDeathEv.start();
    }
    public void BossShoot()
    {
        bossShootEv.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        bossShootEv.start();
    }

}
