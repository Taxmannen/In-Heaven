using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    [Header("")]
    [Header("PLAYER SOUNDS")]

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
    [FMODUnity.EventRef]
    public string playerSuccessfulParry;

    [Header("")]
    [Header("BOSS SOUNDS")]

    [FMODUnity.EventRef]
    public string bossHitRecieveDamage;
    [FMODUnity.EventRef]
    public string bossHitRecieveNoDamage;
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
    public void PlayerSuccessfulParry()
    {
        FMODUnity.RuntimeManager.PlayOneShot(playerSuccessfulParry);
    }
    public void PlayerUnsuccessfulParry()
    {

    }
    public void PlayerParryEvent()
    {

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
        FMODUnity.RuntimeManager.PlayOneShot(bossHitRecieveDamage);
    }
    public void BossHitRecieveNoDamage()
    {
        FMODUnity.RuntimeManager.PlayOneShot(bossHitRecieveNoDamage);
    }
    public void BossDeath()
    {
        FMODUnity.RuntimeManager.PlayOneShot(bossDeath);
    }

}
