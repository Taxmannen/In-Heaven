using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletObjectPool : MonoBehaviour
{
    public static BossBulletObjectPool current;
    public GameObject plasmaBulletPrefab;
    public GameObject plasmaBulletParrablePrefab;

    public int poolAmount = 100;

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
        for (int i = 0; i < poolAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(plasmaBulletPrefab, transform);
            obj.transform.position = new Vector3(0, 0, 1000);
            obj.GetComponent<ParticleSystem>().Pause();
            plasmaBullets.Add(obj);

        }
        for (int i = 0; i < poolAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(plasmaBulletParrablePrefab, transform);
            obj.transform.position = new Vector3(0, 0, 1000);
            plasmaBulletParrables.Add(obj);
        }
    }
    public GameObject GetPooledPlasmaBullet()
    {
        for (int i = 0; i < poolAmount; i++)
        {
            if(plasmaBullets[i].transform.position.z >= 900)
            {
                return plasmaBullets[i];
            }
        }

        return null;
    }
    public GameObject GetPooledPlasmaBulletParrable()
    {
        for (int i = 0; i < poolAmount; i++)
        {
            if(plasmaBulletParrables[i] != null)
            {
                if (plasmaBulletParrables[i].transform.position.z >= 900)
                {
                    return plasmaBulletParrables[i];
                }
            }
            
        }
        return null;
    }

}
