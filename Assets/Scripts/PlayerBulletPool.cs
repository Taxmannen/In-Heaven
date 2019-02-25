using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletPool : MonoBehaviour
{
    public static PlayerBulletPool current;
    public int poolAmount = 100;
    public GameObject playerBulletPrefab;
    public bool scale = true;
    List<GameObject> playerBullets;
    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerBullets = new List<GameObject>();
        for (int i = 0; i < poolAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(playerBulletPrefab, transform);
            obj.transform.position = new Vector3(0, -20, 0);
            playerBullets.Add(obj);

        }
        
    }
    public GameObject GetPlayerBullets()
    {
        for (int i = 0; i < poolAmount; i++)
        {
            if (playerBullets[i].transform.position.y <= -20)
            {
                return playerBullets[i];
            }
        }
        if (scale)
        {
            GameObject obj = (GameObject)Instantiate(playerBulletPrefab, transform);
            obj.transform.position = new Vector3(0, -30, 0);
            playerBullets.Add(obj);
            poolAmount = playerBullets.Count;
            return obj;
        }

        return null;
    }


}
