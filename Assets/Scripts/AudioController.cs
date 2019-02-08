using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Made by: Vidar M
/// </summary>
public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    [Header("")]
    [Header("PLAYER SOUNDS")]

    [FMODUnity.EventRef]
    [SerializeField] private string playerShoot;
    FMOD.Studio.EventInstance playerShootEv;
    [FMODUnity.EventRef]
    [SerializeField] private string playerDash;
    FMOD.Studio.EventInstance playerDashEv;
    [FMODUnity.EventRef]
    [SerializeField] private string playerJump;
    FMOD.Studio.EventInstance playerJumpEv;
    [FMODUnity.EventRef]
    [SerializeField] private string playerDoubleJump;
    FMOD.Studio.EventInstance playerDoubleJumpEv;
    [FMODUnity.EventRef]
    [SerializeField] private string playerGunReverb;
    FMOD.Studio.EventInstance playerGunReverbEv;
    [FMODUnity.EventRef]
    [SerializeField] private string playerCommenceShooting;
    FMOD.Studio.EventInstance playerCommenceShootingEv;
    [FMODUnity.EventRef]
    [SerializeField] private string playerParryEvent;
    FMOD.Studio.EventInstance playerParryEventEv;
    [FMODUnity.EventRef]
    [SerializeField] private string playerSuccessfulParry;
    FMOD.Studio.EventInstance playerSuccessfulParryEv;

    [Header("")]
    [Header("BOSS SOUNDS")]

    [FMODUnity.EventRef]
    [SerializeField] private string bossHitRecieveDamage;
    FMOD.Studio.EventInstance bossHitRecieveDamageEv;
    [FMODUnity.EventRef]
    [SerializeField] private string bossHitRecieveNoDamage;
    FMOD.Studio.EventInstance bossHitRecieveNoDamageEv;
    [FMODUnity.EventRef]
    [SerializeField] private string bossDeath;
    FMOD.Studio.EventInstance bossDeathEv;
    [FMODUnity.EventRef]
    [SerializeField] private string bossShoot;
    FMOD.Studio.EventInstance bossShootEv;
    [FMODUnity.EventRef]
    [SerializeField] private string bossDestruction;
    FMOD.Studio.EventInstance bossDestructionEv;

    [SerializeField] private Queue<FMOD.Studio.EventInstance> bossDestructionQueue = new Queue<FMOD.Studio.EventInstance>();
    [SerializeField] private Queue<FMOD.Studio.EventInstance> playerShootQueue = new Queue<FMOD.Studio.EventInstance>();

    [Header("")]
    [Header("SOUND OVERRIDES")]

    [FMODUnity.EventRef]
    [SerializeField] private string muteAllDynamic;
    FMOD.Studio.EventInstance muteAllDynamicEv;
    FMOD.Studio.ParameterInstance muteAllParameter;

    [FMODUnity.EventRef]
    [SerializeField] private string muteAllSnap;
    FMOD.Studio.EventInstance muteAllSnapEv;

    [FMODUnity.EventRef]
    [SerializeField] private string muteMusicDynamic;
    FMOD.Studio.EventInstance muteMusicDynamicEv;
    FMOD.Studio.ParameterInstance muteMusicParameter;

    [FMODUnity.EventRef]
    [SerializeField] private string muteMusicSnap;
    FMOD.Studio.EventInstance muteMusicSnapEv;


    private void Start()
    {
        
        playerDashEv = FMODUnity.RuntimeManager.CreateInstance(playerDash);
        playerJumpEv = FMODUnity.RuntimeManager.CreateInstance(playerJump);
        playerDoubleJumpEv = FMODUnity.RuntimeManager.CreateInstance(playerDoubleJump);
        playerGunReverbEv = FMODUnity.RuntimeManager.CreateInstance(playerGunReverb);
        playerCommenceShootingEv = FMODUnity.RuntimeManager.CreateInstance(playerCommenceShooting);
        playerParryEventEv = FMODUnity.RuntimeManager.CreateInstance(playerParryEvent);
        playerSuccessfulParryEv = FMODUnity.RuntimeManager.CreateInstance(playerSuccessfulParry);
        
        bossHitRecieveNoDamageEv = FMODUnity.RuntimeManager.CreateInstance(bossHitRecieveNoDamage);
        bossDeathEv = FMODUnity.RuntimeManager.CreateInstance(bossDeath);
        bossShootEv = FMODUnity.RuntimeManager.CreateInstance(bossShoot);
        bossDestructionEv = FMODUnity.RuntimeManager.CreateInstance(bossDestruction);

        muteAllDynamicEv = FMODUnity.RuntimeManager.CreateInstance(muteAllDynamic);
        muteAllSnapEv = FMODUnity.RuntimeManager.CreateInstance(muteAllSnap);
        muteMusicDynamicEv = FMODUnity.RuntimeManager.CreateInstance(muteMusicDynamic);
        muteMusicSnapEv = FMODUnity.RuntimeManager.CreateInstance(muteMusicSnap);
        //shootingEvent.getParameter("Stop", out stopShooting);
        muteAllDynamicEv.getParameter("MuteAllParameter", out muteAllParameter);
        muteMusicDynamicEv.getParameter("MuteMusicParameter", out muteMusicParameter);
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
        FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(playerShoot);
        eventInstance.start();
        playerShootQueue.Enqueue(eventInstance);
        StartCoroutine(PlayerShootRoutine());
    }

    private IEnumerator PlayerShootRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        FMOD.Studio.EventInstance eventInstance = playerShootQueue.Dequeue();
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        yield break;
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
        FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(bossHitRecieveDamage);
        eventInstance.start();
        bossDestructionQueue.Enqueue(eventInstance);
        StartCoroutine(BossDestructionRoutine());

    }

    private IEnumerator BossDestructionRoutine()
    {
        
        yield return new WaitForSeconds(1.5f);
        FMOD.Studio.EventInstance eventInstance = bossDestructionQueue.Dequeue();
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        yield break;

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
    public void BossDestruction()
    {
        bossDestructionEv.start();
    }

    //__________Overrides_______

    public void SetMaster(float value)
    {
        muteAllDynamicEv.start();
        muteAllParameter.setValue (value);
    }
    public void ToggleMaster(Toggle toggle)
    {
        if (!toggle.isOn)
        {
            muteAllSnapEv.start();
        }
        else
        {
            muteAllSnapEv.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }

    public void SetMusic(float value)
    {
        muteMusicDynamicEv.start();
        muteMusicParameter.setValue(value);
    }
    public void MuteMusic()
    {
        muteMusicSnapEv.start();
    }
    public void UnmuteMusic()
    {
        muteMusicDynamicEv.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }



}
