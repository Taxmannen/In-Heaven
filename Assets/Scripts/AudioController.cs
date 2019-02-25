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
    private List<Queue<FMOD.Studio.EventInstance>> queues = new List<Queue<FMOD.Studio.EventInstance>>();

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
    // FMOD.Studio.ParameterInstance bossDeathParameter;
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

    private float playerShootSoundDuration = 0.5f;
    private Queue<FMOD.Studio.EventInstance> bossDestructionQueue = new Queue<FMOD.Studio.EventInstance>();
    private Queue<FMOD.Studio.EventInstance> playerShootQueue = new Queue<FMOD.Studio.EventInstance>();
    private Queue<FMOD.Studio.EventInstance> menuClickQueue = new Queue<FMOD.Studio.EventInstance>();
    private Queue<FMOD.Studio.EventInstance> menuClickBackQueue = new Queue<FMOD.Studio.EventInstance>();
    private Queue<FMOD.Studio.EventInstance> menuHoverQueue = new Queue<FMOD.Studio.EventInstance>();
    private Queue<FMOD.Studio.EventInstance> menuPopupQueue = new Queue<FMOD.Studio.EventInstance>();
    private Queue<FMOD.Studio.EventInstance> bossPatternShotQueue = new Queue<FMOD.Studio.EventInstance>();
    private Queue<FMOD.Studio.EventInstance> playerTakesDamageQueue = new Queue<FMOD.Studio.EventInstance>();
    private Queue<FMOD.Studio.EventInstance> bossTakesNoDamageQueue = new Queue<FMOD.Studio.EventInstance>();
    private Queue<FMOD.Studio.EventInstance> bossTeleportInQueue = new Queue<FMOD.Studio.EventInstance>();
    private Queue<FMOD.Studio.EventInstance> bossTeleportOutQueue = new Queue<FMOD.Studio.EventInstance>();
    private Queue<FMOD.Studio.EventInstance> playerJumpQueue = new Queue<FMOD.Studio.EventInstance>();
    private Queue<FMOD.Studio.EventInstance> playerDoubleJumpQueue = new Queue<FMOD.Studio.EventInstance>();
    private Queue<FMOD.Studio.EventInstance> playerDashQueue = new Queue<FMOD.Studio.EventInstance>();
    private Queue<FMOD.Studio.EventInstance> playerCommenceShootingQueue = new Queue<FMOD.Studio.EventInstance>();
    private Queue<FMOD.Studio.EventInstance> playerSuccessfulParryQueue = new Queue<FMOD.Studio.EventInstance>();
    private Queue<FMOD.Studio.EventInstance> playerParryEventQueue = new Queue<FMOD.Studio.EventInstance>();
    private Queue<FMOD.Studio.EventInstance> bossDeathQueue = new Queue<FMOD.Studio.EventInstance>();
    private Queue<FMOD.Studio.EventInstance> bossLaserLoopQueue = new Queue<FMOD.Studio.EventInstance>();
    private Queue<FMOD.Studio.EventInstance> bossLaserShootQueue = new Queue<FMOD.Studio.EventInstance>();
    private Queue<FMOD.Studio.EventInstance> bossLaserChargeQueue = new Queue<FMOD.Studio.EventInstance>();

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
        //  bossDeathEv.getParameter("StopDeath", out bossDeathParameter);
        #endregion
        queues.Add(bossDestructionQueue);
        queues.Add(playerShootQueue);
        queues.Add(menuClickQueue);
        queues.Add(menuClickBackQueue);
        queues.Add(menuHoverQueue);
        queues.Add(menuPopupQueue);
        queues.Add(bossPatternShotQueue);
        queues.Add(playerTakesDamageQueue);
        queues.Add(bossTakesNoDamageQueue);
        queues.Add(bossTeleportInQueue);
        queues.Add(bossTeleportOutQueue);
        queues.Add(playerJumpQueue);
        queues.Add(playerDoubleJumpQueue);
        queues.Add(playerDashQueue);
        queues.Add(playerCommenceShootingQueue);
        queues.Add(playerSuccessfulParryQueue);
        queues.Add(playerParryEventQueue);
        queues.Add(bossDeathQueue);
        queues.Add(bossLaserLoopQueue);
        queues.Add(bossLaserShootQueue);
        queues.Add(bossLaserChargeQueue);


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
        FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(playerJump);
        eventInstance.start();
        playerJumpQueue.Enqueue(eventInstance);
        StartCoroutine(PlayerJumpRoutine());
    }
    private IEnumerator PlayerJumpRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        FMOD.Studio.EventInstance eventInstance = playerJumpQueue.Dequeue();
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        yield break;
    }
    public void PlayerDoubleJump()
    {
        FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(playerDoubleJump);
        eventInstance.start();
        playerDoubleJumpQueue.Enqueue(eventInstance);
        StartCoroutine(PlayerDoubleJumpRoutine());
    }
    private IEnumerator PlayerDoubleJumpRoutine()
    {
        yield return new WaitForSeconds(0.3f);
        FMOD.Studio.EventInstance eventInstance = playerDoubleJumpQueue.Dequeue();
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        yield break;
    }
    public void PlayerDash()
    {
        FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(playerDash);
        eventInstance.start();
        playerDashQueue.Enqueue(eventInstance);
        StartCoroutine(PlayerDashRoutine());
    }
    private IEnumerator PlayerDashRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        FMOD.Studio.EventInstance eventInstance = playerDashQueue.Dequeue();
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        yield break;
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
        FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(playerCommenceShooting);
        eventInstance.start();
        playerCommenceShootingQueue.Enqueue(eventInstance);
        StartCoroutine(PlayerCommenceShootingRoutine());

    }
    private IEnumerator PlayerCommenceShootingRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        FMOD.Studio.EventInstance eventInstance = playerCommenceShootingQueue.Dequeue();
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        yield break;
    }
    public void PlayerSuccessfulParry()
    {
        FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(playerSuccessfulParry);
        eventInstance.start();
        playerSuccessfulParryQueue.Enqueue(eventInstance);
        StartCoroutine(PlayerSuccessfulParryRoutine());
    }
    private IEnumerator PlayerSuccessfulParryRoutine()
    {
        yield return new WaitForSeconds(5f);
        FMOD.Studio.EventInstance eventInstance = playerSuccessfulParryQueue.Dequeue();
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        yield break;
    }
    public void PlayerParryEvent()
    {
        FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(playerParryEvent);
        eventInstance.start();
        playerParryEventQueue.Enqueue(eventInstance);
        StartCoroutine(PlayerParryEventRoutine());
    }
    private IEnumerator PlayerParryEventRoutine()
    {
        yield return new WaitForSeconds(1f);
        FMOD.Studio.EventInstance eventInstance = playerParryEventQueue.Dequeue();
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        yield break;
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

    #region BossSounds


    public void BossHitRecieveDamage()
    {
        FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(bossHitRecieveDamage);
        eventInstance.start();
        bossDestructionQueue.Enqueue(eventInstance);
        StartCoroutine(BossDestructionRoutine());

    }
    private IEnumerator BossDestructionRoutine()
    {

        yield return new WaitForSeconds(3f);
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
        FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(bossDeath);
        eventInstance.start();
        bossDeathQueue.Enqueue(eventInstance);
    }
    public void StopBossDeath()
    {
        FMOD.Studio.EventInstance eventInstance = bossDeathQueue.Dequeue();
        eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
    public void BossDestruction()
    {
        FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(bossDestruction);
        eventInstance.start();
        bossDestructionQueue.Enqueue(eventInstance);
        StartCoroutine(Global(5f, bossDestructionQueue));
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
        yield return new WaitForSeconds(0.01f);
        play = true;
        yield return new WaitForSeconds(0.2f);
        FMOD.Studio.EventInstance eventInstance = bossPatternShotQueue.Dequeue();
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        yield break;
    }

    public void BossLaserLoop()
    {
        bossLaserLoopParameter.setValue(0);
        //FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(bossLaserLoop);
        bossLaserLoopEv.start();
       // bossLaserLoopQueue.Enqueue(eventInstance);
    }

    public void StopBossLaserLoop()
    {
        bossLaserLoopParameter.setValue(1f);
       // FMOD.Studio.EventInstance eventInstance = bossLaserLoopQueue.Dequeue();
        bossLaserLoopEv.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void BossLaserShoot()
    {
        FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(bossLaserShoot);
        eventInstance.start();
        bossLaserShootQueue.Enqueue(eventInstance);
        StartCoroutine(Global(6f, bossLaserShootQueue));
    }

    public void BossLaserCharge()
    {
        FMOD.Studio.EventInstance eventInstance = FMODUnity.RuntimeManager.CreateInstance(bossLaserCharge);
        eventInstance.start();
        bossLaserChargeQueue.Enqueue(eventInstance);
        StartCoroutine(Global(1.5f, bossLaserChargeQueue));
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

    public IEnumerator Global(float time, Queue<FMOD.Studio.EventInstance> queue)
    {
        yield return new WaitForSeconds(time);
        FMOD.Studio.EventInstance eventInstance = queue.Dequeue();
        eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        yield break;
    }

    public void SoundKiller()
    {

        StopAllCoroutines();
        StopBossLaserLoop();
        foreach(Queue<FMOD.Studio.EventInstance> q in queues)
        {
            for (int i = 0; i < q.Count; i++)
            {
                FMOD.Studio.EventInstance eventInstance = q.Dequeue();
                eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

            }
        }
    }
}