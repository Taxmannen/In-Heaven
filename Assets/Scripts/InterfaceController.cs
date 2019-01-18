using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class InterfaceController : MonoBehaviour
{

    public static InterfaceController instance;

    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private Text playerHpText;
    [SerializeField] private Text bossHpText;

    [SerializeField] private GameObject playerHpPanel;

    [SerializeField] private Text playerStateText;

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

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }

}
