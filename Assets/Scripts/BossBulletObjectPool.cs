using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletObjectPool : MonoBehaviour
{
    public static BossBulletObjectPool current;
    public GameObject plasmaBulletPrefab;
    public GameObject plasmaBulletParrablePrefab;

    public int poolPlasmaAmount = 200;
    public int poolParryableAmount = 100;
    public bool scale = true;
    List<GameObject> plasmaBullets;

    List<GameObject> plasmaBulletParrables;

    private void Awake()
    {
        if(current == null)
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
        plasmaBullets = new List<GameObject>();
        plasmaBulletParrables = new List<GameObject>();
        for (int i = 0; i < poolPlasmaAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(plasmaBulletPrefab, transform);
            obj.transform.position = new Vector3(0, -30, 0);
            plasmaBullets.Add(obj);

        }
        for (int i = 0; i < poolParryableAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(plasmaBulletParrablePrefab, transform);
            obj.transform.position = new Vector3(0, -30, 0);
            plasmaBulletParrables.Add(obj);
        }
    }
    public GameObject GetPooledPlasmaBullet()
    {
        for (int i = 0; i < poolPlasmaAmount; i++)
        {
            if(plasmaBullets[i].transform.position.y <= -20)
            {
                return plasmaBullets[i];
            }
        }
        if (scale)
        {
            GameObject obj = (GameObject)Instantiate(plasmaBulletPrefab, transform);
            obj.transform.position = new Vector3(0, -30, 0);
            plasmaBullets.Add(obj);
            poolPlasmaAmount = plasmaBullets.Count;
            return obj;
        }

        return null;
    }
    public GameObject GetPooledPlasmaBulletParrable()
    {
        for (int i = 0; i < poolParryableAmount; i++)
        {
            if(plasmaBulletParrables[i] != null)
            {
                if (plasmaBulletParrables[i].transform.position.y <= -20)
                {
                    return plasmaBulletParrables[i];
                }
            }
            
        }
        if(scale)
        {
            GameObject obj = (GameObject)Instantiate(plasmaBulletParrablePrefab, transform);
            obj.transform.position = new Vector3(0, -30, 0);
            plasmaBulletParrables.Add(obj);
            poolParryableAmount = plasmaBulletParrables.Count;
            return obj;
        }
        
        return null;
    }

}
