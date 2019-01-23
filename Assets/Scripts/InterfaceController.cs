using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class InterfaceController : MonoBehaviour
{

    public static InterfaceController instance;

    [SerializeField] private Text playerHpText;
    [SerializeField] private Text playerStateText;

    [SerializeField] private Text bossHpText;
    [SerializeField] private Text bossStateText;

    [SerializeField] private GameObject failPanel;
    [SerializeField] private GameObject successPanel;

    [SerializeField] private GameObject test;

    [SerializeField] private Slider slider;



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

    private void Start()
    {
        slider.maxValue = GameController.instance.GetPlayerController().GetSuperChargeMax();
        slider.value = slider.value / slider.maxValue;
    }

    public void UpdatePlayerHP(float hP, float maxHP)
    {
        playerHpText.text = "HP: " + hP + "/" + maxHP;
    }

    public void UpdatePlayerState(Global.PlayerState playerState)
    {
        playerStateText.text = "PlayerState: " + playerState;
    }

    public void UpdateBossHP(float hP, float maxHP)
    {
        bossHpText.text = "HP: " + hP + "/" + maxHP;
    }

    public void UpdateBossState(Global.BossState bossState)
    {
        bossStateText.text = "BossState: " + bossState;
    }

    public void Fail()
    {
        failPanel.SetActive(true);
    }

    public void Success()
    {
        successPanel.SetActive(true);
    }

    public void BossBulletOverlay(Vector3 point)
    {

        Vector3 cameraPoint = Camera.main.WorldToScreenPoint(point);

        GameObject testClone = Instantiate(test, cameraPoint, Quaternion.identity, FindObjectOfType<Canvas>().gameObject.transform);
        Destroy(testClone, 1f);

    }

    public void UpdateSuperChargeSlider(float value)
    {
        slider.value = value;
    }

}
