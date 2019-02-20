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

    #region Declarations
    [Header("")]
    [Header("PLAYER SOUNDS")]
    #region PlayerSounds

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
    [SerializeField] private string playerCommenceShooting;
    FMOD.Studio.EventInstance playerCommenceShootingEv;
    [FMODUnity.EventRef]
    [SerializeField] private string playerParryEvent;
    FMOD.Studio.EventInstance playerParryEventEv;
    [FMODUnity.EventRef]
    [SerializeField] private string playerSuccessfulParry;
    FMOD.Studio.EventInstance playerSuccessfulParryEv;
    [FMODUnity.EventRef]
    [SerializeField] private string playerTakesDamage;
    FMOD.Studio.EventInstance playerTakesDamageEv;
    [FMODUnity.EventRef]
    [SerializeField] private string playerDeath;
    FMOD.Studio.EventInstance playerDeathEv;
    #endregion
    [Header("")]
    [Header("BOSS SOUNDS")]
    #region BossSounds
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
    [FMODUnity.EventRef]
    [SerializeField] private string bossLaserLoop;
    FMOD.Studio.EventInstance bossLaserLoopEv;
    FMOD.Studio.ParameterInstance bossLaserLoopParameter;
    [FMODUnity.EventRef]
    [SerializeField] private string bossLaserShoot;
    FMOD.Studio.EventInstance bossLaserShootEv;
    [FMODUnity.EventRef]
    [SerializeField] private string bossLaserCharge;
    FMOD.Studio.EventInstance bossLaserChargeEv;
    [FMODUnity.EventRef]
    [SerializeField] private string bossTeleportIn;
    [FMODUnity.EventRef]
    [SerializeField] private string bossTeleportOut;

    #endregion
    [Header("")]
    [Header("SOUND OVERRIDES")]
    #region SoundOverrides
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
    [FMODUnity.EventRef]
    [SerializeField] public string failMenuDuck;
    FMOD.Studio.EventInstance failMenuDuckEv;
    FMOD.Studio.ParameterInstance fadeOutParameter;
    #endregion
    [Header("")]
    [Header("NON Diegetic")]
    #region NonDiegetic
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
    #endregion

    [SerializeField] private float playerShootSoundDuration = 0.5f;
    [SerializeField] private Queue<FMOD.Studio.EventInstance> bossDestructionQueue = new Queue<FMOD.Studio.EventInstance>();
    [SerializeField] private Queue<FMOD.Studio.EventInstance> playerShootQueue = new Queue<FMOD.Studio.EventInstance>();
    [SerializeField] private Queue<FMOD.Studio.EventInstance> menuClickQueue = new Queue<FMOD.Studio.EventInstance>();
    [SerializeField] private Queue<FMOD.Studio.EventInstance> menuClickBackQueue = new Queue<FMOD.Studio.EventInstance>();
    [SerializeField] private Queue<FMOD.Studio.EventInstance> menuHoverQueue = new Queue<FMOD.Studio.EventInstance>();
    [SerializeField] private Queue<FMOD.Studio.EventInstance> menuPopupQueue = new Queue<FMOD.Studio.EventInstance>();
    [SerializeField] private Queue<FMOD.Studio.EventInstance> bossPatternShotQueue = new Queue<FMOD.Studio.EventInstance>();
    [SerializeField] private Queue<FMOD.Studio.EventInstance> playerTakesDamageQueue = new Queue<FMOD.Studio.EventInstance>();
    [SerializeField] private Queue<FMOD.Studio.EventInstance> bossTakesNoDamageQueue = new Queue<FMOD.Studio.EventInstance>();
    [SerializeField] private Queue<FMOD.Studio.EventInstance> bossTeleportInQueue = new Queue<FMOD.Studio.EventInstance>();
    [SerializeField] private Queue<FMOD.Studio.EventInstance> bossTeleportOutQueue = new Queue<FMOD.Studio.EventInstance>();
    #endregion

    private void Start()
    {
        #region Instance Creations
        playerDashEv = FMODUnity.RuntimeManager.CreateInstance(playerDash);
        playerJumpEv = FMODUnity.RuntimeManager.CreateInstance(playerJump);
        playerDoubleJumpEv = FMODUnity.RuntimeManager.CreateInstance(playerDoubleJump);
        playerCommenceShootingEv = FMODUnity.RuntimeManager.CreateInstance(playerCommenceShooting);
        playerParryEventEv = FMODUnity.RuntimeManager.CreateInstance(playerParryEvent);
        playerSuccessfulParryEv = FMODUnity.RuntimeManager.CreateInstance(playerSuccessfulParry);
        playerDeathEv = FMODUnity.RuntimeManager.CreateInstance(playerDeath);

        bossHitRecieveNoDamageEv = FMODUnity.RuntimeManager.CreateInstance(bossHitRecieveNoDamage);
        bossDeathEv = FMODUnity.RuntimeManager.CreateInstance(bossDeath);
        bossShootEv = FMODUnity.RuntimeManager.CreateInstance(bossShoot);
        bossDestructionEv = FMODUnity.RuntimeManager.CreateInstance(bossDestruction);
        bossLaserLoopEv = FMODUnity.RuntimeManager.CreateInstance(bossLaserLoop);
        bossLaserShootEv = FMODUnity.RuntimeManager.CreateInstance(bossLaserShoot);
        bossLaserChargeEv = FMODUnity.RuntimeManager.CreateInstance(bossLaserCharge);

        muteAllDynamicEv = FMODUnity.RuntimeManager.CreateInstance(muteAllDynamic);
        muteAllSnapEv = FMODUnity.RuntimeManager.CreateInstance(muteAllSnap);
        muteMusicDynamicEv = FMODUnity.RuntimeManager.CreateInstance(muteMusicDynamic);
        muteMusicSnapEv = FMODUnity.RuntimeManager.CreateInstance(muteMusicSnap);
        muteSfxDynamicEv = FMODUnity.RuntimeManager.CreateInstance(muteSfxDynamic);
        muteSfxSnapEv = FMODUnity.RuntimeManager.CreateInstance(muteSfxDynamic);
        failMenuDuckEv = FMODUnity.RuntimeManager.CreateInstance(failMenuDuck);

        failMenuDuckEv.getParameter("FadeOut", out fadeOutParameter);
        bossLaserLoopEv.getParameter("StopLoop", out bossLaserLoopParameter);
        muteAllDynamicEv.getParameter("MuteAllParameter", out muteAllParameter);
        muteMusicDynamicEv.getParameter("MuteMusicParameter", out muteMusicParameter);
        muteSfxDynamicEv.getParameter("MuteSfxParameter", out muteSfxParameter);
        #endregion
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


    #region PlayerSounds

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
        yield return new WaitForSeconds(playerShootSoundDuration);
        FMOD.Studio.EventInstance eventInstance = playerShootQueue.Dequeue();
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        yield break;
    }
    public void PlayerCommenceShooting()
    {  
        playerCommenceShootingEv.start();
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
    public void PlayerTakingDamage()
    {
        FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(playerTakesDamage);
        eventInstance.start();
        playerTakesDamageQueue.Enqueue(eventInstance);
        StartCoroutine(PlayerTakingDamageRoutine());
    }
    private IEnumerator PlayerTakingDamageRoutine()
    {
        yield return new WaitForSeconds(2f);
        FMOD.Studio.EventInstance eventInstance = playerTakesDamageQueue.Dequeue();
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
      yield break;
    }
    public void PlayerDeath()
    {
        playerDeathEv.start();
        StartCoroutine(StopDeathRoutine());
    }
    private IEnumerator StopDeathRoutine()
    {
        yield return new WaitForSeconds(6f);
        playerDeathEv.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        yield break;
    }
    #endregion

    #region EnemySounds
    public void BossLaserLoop()
    {
        bossLaserLoopParameter.setValue(0);
        bossLaserLoopEv.start();
    }
    public void StopBossLaserLoop()
    {
        bossLaserLoopParameter.setValue(1f);
        bossLaserLoopEv.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
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
        FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(bossHitRecieveNoDamage);
        eventInstance.start();
        bossTakesNoDamageQueue.Enqueue(eventInstance);
        StartCoroutine(BossHitRecieveNoDamageRoutine());
    }
    private IEnumerator BossHitRecieveNoDamageRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        FMOD.Studio.EventInstance eventInstance = bossTakesNoDamageQueue.Dequeue();
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        yield break;
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
    private bool play = true;
    public void BossPatternShot()
        
    {
        if (play)
        {
            FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(bossPatternShot);
            eventInstance.start();
            bossPatternShotQueue.Enqueue(eventInstance);
            play = false;
            StartCoroutine(BossPatternShotRoutine());
        }
    }
    private IEnumerator BossPatternShotRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        play = true;
        yield return new WaitForSeconds(10f);
        FMOD.Studio.EventInstance eventInstance = bossPatternShotQueue.Dequeue();
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        yield break;
    }
    public void BossLaserShoot()
    {
        bossLaserShootEv.start();
    }
    public void BossLaserCharge()
    {
        bossLaserChargeEv.start();
    }
    public void BossTeleportIn()
    {
        FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(bossTeleportIn);
        eventInstance.start();
        bossTeleportInQueue.Enqueue(eventInstance);
        StartCoroutine(BossTeleportInRoutine());
    }
    private IEnumerator BossTeleportInRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        FMOD.Studio.EventInstance eventInstance = bossTeleportInQueue.Dequeue();
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        yield break;
    }
    public void BossTeleportOut()
    {
        FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(bossTeleportOut);
        eventInstance.start();
        bossTeleportOutQueue.Enqueue(eventInstance);
        StartCoroutine(BossTeleportOutRoutine());
    }
    private IEnumerator BossTeleportOutRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        FMOD.Studio.EventInstance eventInstance = bossTeleportOutQueue.Dequeue();
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        yield break;
    }


    #endregion

    #region Overrides
    public void SetMaster(Slider slider)
    {
        muteAllDynamicEv.start();
        muteAllParameter.setValue(slider.value * 0.01f);
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
    public void FailMenuDuck()
    {
        fadeOutParameter.setValue(0);
        failMenuDuckEv.start();
    }
    public void StopFailMenuDuck()
    {
        fadeOutParameter.setValue(1);
        StartCoroutine(BeginFadeOut());
    }
    private IEnumerator BeginFadeOut()
    {
        yield return new WaitForSeconds(1f);
        failMenuDuckEv.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        yield break;
    }
    #endregion

    #region Non Diegetic
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
    #endregion

    public FMOD.Studio.EventInstance GetEventInstance()
    {
        return bossLaserLoopEv;
    }
}