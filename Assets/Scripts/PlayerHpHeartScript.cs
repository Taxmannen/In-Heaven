using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpHeartScript : MonoBehaviour
{
    private PlayerController playerController;

    float health;
    float numberOfHearts;

    public Image[] heartImage;
    public Sprite heartSprite;

    private float bossHealthMax;
    public float healthBarBaseSize;

    int healthPoints;

    void Start()
    {
        healthPoints = heartImage.Length;
        //heartImage = GetComponent<Image>();

        playerController = gameObject.GetComponent<PlayerController>();

        numberOfHearts = playerController.maxHP;
        
    }
          


    void Update()
    {
        health = playerController.hP;

        if(health> numberOfHearts)
        {
            health = numberOfHearts;
        }

        for(int i = 0; i<heartImage.Length; i++)
        {
            if (i<numberOfHearts)
            {
                heartImage[i].enabled = true;
            }
            else
            {
                heartImage[i].enabled = false;
            }
        }
        
    }
}
