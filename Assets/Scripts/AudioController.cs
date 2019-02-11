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
    //[FMODUnity.EventRef]
    //[SerializeField] private string playerGunReverb;
    //FMOD.Studio.EventInstance playerGunReverbEv;
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
    [FMODUnity.EventRef]
    [SerializeField] private string bossPatternShot;
    FMOD.Studio.EventInstance bossPatternshotEv;

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
    [FMODUnity.EventRef]
    [SerializeField] private string muteSfxDynamic;
    FMOD.Studio.EventInstance muteSfxDynamicEv;
    FMOD.Studio.ParameterInstance muteSfxParameter;
    [FMODUnity.EventRef]
    [SerializeField] private string muteSfxSnap;
    FMOD.Studio.EventInstance muteSfxSnapEv;

    [Header("")]
    [Header("NON Diegetic")]

    [FMODUnity.EventRef]
    [SerializeField] private string menuClick;
    FMOD.Studio.EventInstance menuClickEv;
    [FMODUnity.EventRef]
    [SerializeField] private string menuClickBack;
    FMOD.Studio.EventInstance menuClickBackEv;
    [FMODUnity.EventRef]
    [SerializeField] private string menuHover;
    FMOD.Studio.EventInstance menuHoverEv;
    [FMODUnity.EventRef]
    [SerializeField] private string menuPopup;
    FMOD.Studio.EventInstance menuPopupEv;

    [SerializeField] private Queue<FMOD.Studio.EventInstance> bossDestructionQueue = new Queue<FMOD.Studio.EventInstance>();
    [SerializeField] private Queue<FMOD.Studio.EventInstance> playerShootQueue = new Queue<FMOD.Studio.EventInstance>();
    [SerializeField] private Queue<FMOD.Studio.EventInstance> menuClickQueue = new Queue<FMOD.Studio.EventInstance>();
    [SerializeField] private Queue<FMOD.Studio.EventInstance> menuClickBackQueue = new Queue<FMOD.Studio.EventInstance>();
    [SerializeField] private Queue<FMOD.Studio.EventInstance> menuHoverQueue = new Queue<FMOD.Studio.EventInstance>();
    [SerializeField] private Queue<FMOD.Studio.EventInstance> menuPopupQueue = new Queue<FMOD.Studio.EventInstance>();
    [SerializeField] private Queue<FMOD.Studio.EventInstance> bossPatternShotQueue = new Queue<FMOD.Studio.EventInstance>();

    private void Start()
    {
        
        playerDashEv = FMODUnity.RuntimeManager.CreateInstance(playerDash);
        playerJumpEv = FMODUnity.RuntimeManager.CreateInstance(playerJump);
        playerDoubleJumpEv = FMODUnity.RuntimeManager.CreateInstance(playerDoubleJump);
        //playerGunReverbEv = FMODUnity.RuntimeManager.CreateInstance(playerGunReverb);
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
        muteSfxDynamicEv = FMODUnity.RuntimeManager.CreateInstance(muteSfxDynamic);
        muteSfxSnapEv = FMODUnity.RuntimeManager.CreateInstance(muteSfxDynamic);
        
        muteAllDynamicEv.getParameter("MuteAllParameter", out muteAllParameter);
        muteMusicDynamicEv.getParameter("MuteMusicParameter", out muteMusicParameter);
        muteSfxDynamicEv.getParameter("MuteSfxParameter", out muteSfxParameter);

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
    //public void PlayerGunReverb()
    //{
    //    playerGunReverbEv.start();
    //}
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
    public void BossPatternShot()
    {
        FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(bossPatternShot);
        eventInstance.start();
        bossPatternShotQueue.Enqueue(eventInstance);
        StartCoroutine(BossPatternShotRoutine());

    }
    private IEnumerator BossPatternShotRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        FMOD.Studio.EventInstance eventInstance = bossPatternShotQueue.Dequeue();
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        yield break;
    }

    //__________Overrides_______

    public void SetMaster(Slider slider)
    {
        muteAllDynamicEv.start();
        muteAllParameter.setValue (slider.value * 0.01f);
    }
    public void ToggleMaster(Toggle toggle)
    {
        if (toggle.isOn)
        {
            muteAllSnapEv.start();
        }
        else
        {
            muteAllSnapEv.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }
    public void SetMusic(Slider slider)
    {
        muteMusicDynamicEv.start();
        muteMusicParameter.setValue(slider.value * 0.01f);
    }
    public void ToggleMusic(Toggle toggle)
    {
        if (toggle.isOn)
        {
            muteMusicSnapEv.start();
        }
        else
        {
            muteMusicSnapEv.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }
    public void SetSfx(Slider slider)
    {
        muteSfxDynamicEv.start();
        muteSfxParameter.setValue(slider.value * 0.01f);
    }
    public void ToggleSfx(Toggle toggle)
    {
        if (toggle.isOn)
        {
            muteSfxSnapEv.start();
        }
        else
        {
            muteSfxSnapEv.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }

    //_________NON DIEGETIC_______

    public void MenuHover()
    {
        FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(menuHover);
        eventInstance.start();
        menuHoverQueue.Enqueue(eventInstance);
        StartCoroutine(MenuHoverRoutine());
    }
    private IEnumerator MenuHoverRoutine()
    {
        yield return new WaitForSeconds(1f);
        FMOD.Studio.EventInstance eventinstance = menuHoverQueue.Dequeue();
        eventinstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        yield break;
    }
    public void MenuClick()
    {
        FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(menuClick);
        eventInstance.start();
        menuClickQueue.Enqueue(eventInstance);
        StartCoroutine(MenuClickRoutine());
    }
    private IEnumerator MenuClickRoutine()
    {
        yield return new WaitForSeconds(1f);
        FMOD.Studio.EventInstance eventinstance = menuClickQueue.Dequeue();
        eventinstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        yield break;
    }
    public void MenuPopup()
    {
        FMOD.Studio.EventInstance eventinstance = FMODUnity.RuntimeManager.CreateInstance(menuPopup);
        eventinstance.start();
        menuPopupQueue.Enqueue(eventinstance);
        StartCoroutine(MenuPopupRoutine());
    }
    private IEnumerator MenuPopupRoutine()
    {
        yield return new WaitForSeconds(1f);
        FMOD.Studio.EventInstance eventInstance = menuPopupQueue.Dequeue();
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        yield break;
    }
    public void MenuClickBack()
    {
        FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(menuClickBack);
        eventInstance.start();
        menuClickBackQueue.Enqueue(eventInstance);
        StartCoroutine(MenuClickBackRoutine());
    }
    private IEnumerator MenuClickBackRoutine()
    {
        yield return new WaitForSeconds(1f);
        FMOD.Studio.EventInstance eventInstance = menuClickBackQueue.Dequeue();
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        yield break;
    }




}
