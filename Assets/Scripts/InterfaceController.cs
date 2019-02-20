using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

/// <summary>
/// Made by: Filip Nilsson
/// </summary>
public class InterfaceController : MonoBehaviour
{

    public static InterfaceController instance;

    [SerializeField] private Text playerHpText;
    [SerializeField] private Text playerStateText;

    [SerializeField] private Text bossHpText;
    [SerializeField] private Text bossStateText;

    [SerializeField] private GameObject failPanel;
    [SerializeField] private GameObject successPanel;

    [SerializeField] public GameObject targetOverlay;

    [SerializeField] private GameObject sliderPanel;
    [SerializeField] private Slider slider;
    [SerializeField] private Text sliderText;



    private void Awake()
    {

        if (instance)
        {
            Destroy(gameObject);
        }

        else
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
            enabled = true;
        }

    }

    private void Start()
    {
        PlayerController player =  GameController.instance.GetPlayerController();

        //slider.maxValue = player.GetSuperChargeMax();
        slider.value = slider.value / slider.maxValue;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Pause();
        }
    }

    public void UpdatePlayerHP(float hP, float maxHP)
    {
        playerHpText.text = "HP: " + hP + "/" + maxHP;
    }

    public void UpdatePlayerState(Global.PlayerState playerState)
    {
        //playerStateText.text = "PlayerState: " + playerState;
    }

    public void UpdateBossHP(float hP, float maxHP)
    {
        //bossHpText.text = "HP: " + hP + "/" + maxHP;
    }

    public void UpdateBossState(Global.BossState bossState)
    {
        //bossStateText.text = "BossState: " + bossState;
    }

    public void Fail()
    {
        failPanel.SetActive(true);
    }

    public void Success()
    {
        successPanel.SetActive(true);
    }
    public void Pause()
    { 
        successPanel.SetActive(!successPanel.active);

        if (successPanel.active)
        {
            GameController.instance.SetGameState(Global.GameState.Pause);
            Time.timeScale = 0;
        }
        else
        {
            GameController.instance.SetGameState(Global.GameState.Game);
            Time.timeScale = 1;
            Debug.Log("Resume: " + Time.timeScale);
        }
    }

    public void BossBulletOverlay(Vector3 point)
    {

        Vector3 cameraPoint = point;
        GameObject testClone = Instantiate(targetOverlay, cameraPoint, Quaternion.identity, transform);
        Destroy(testClone, 1f);

    }

    public void UpdateBossHPBar(float hP, float maxHP)
    {
        slider.maxValue = maxHP;
        slider.value = hP;
        sliderText.text = hP + "/" + maxHP;
    }

    public void HideBossHPBar()
    {
        sliderPanel.SetActive(false);
    }

    public void ShowBossHPBar()
    {
        sliderPanel.SetActive(true);
    }


}
