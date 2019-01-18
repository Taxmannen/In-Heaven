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

    [SerializeField] private GameObject gameOverPanel;

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

    public void UpdatePlayerHP(int hP, int maxHP)
    {
        playerHpText.text = "HP: " + hP + "/" + maxHP;
    }

    public void UpdatePlayerState(Global.PlayerState playerState)
    {
        playerStateText.text = "PlayerState: " + playerState;
    }

    public void UpdateBossHP(int hP, int maxHP)
    {
        bossHpText.text = "HP: " + hP + "/" + maxHP;
    }

    public void UpdateBossState(Global.BossState bossState)
    {
        bossStateText.text = "BossState: " + bossState;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

}
