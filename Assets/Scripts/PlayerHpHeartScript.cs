using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpHeartScript : MonoBehaviour
{
    private PlayerController playerController;

    public int health;
    public int numberofhearts;

    public Image[] heartImage;

    public Sprite heartSprite;

    private float bossHealthMax;
    public float healthBarBaseSize;


    void Start()
    {
        //heartImage = GetComponent<Image>();

        playerController = gameObject.GetComponent<PlayerController>();
        bossHealthMax = playerController.maxHP;
        
    }

    void UpdateHealthbar(float dmgTaken)
    {

    }

    
    void Update()
    {
        Debug.Log("PlayerHp shite "+bossHealthMax);
    }
}
