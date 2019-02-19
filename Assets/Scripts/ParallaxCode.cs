using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxCode : MonoBehaviour
{

    public GameObject car1Prefab;
    public GameObject car2Prefab;
    public GameObject car3Prefab;

    public Transform targetLocation;

    public GameObject lastCar;
    // Update is called once per frame
    void Update()
    {
        if(lastCar == null)
        {
            StartCoroutine(SpawnCar());

        }
        else if(Vector3.Distance(transform.position,lastCar.transform.position) > 20)
        {
            StartCoroutine(SpawnCar());
        }
    }

    private IEnumerator SpawnCar()
    {
        int randomNumber = Random.Range(0,2);

        switch (randomNumber)
        {
            case 0:
                lastCar = Instantiate(car1Prefab, transform);
                break;
            case 1:
                lastCar = Instantiate(car2Prefab, transform);
                break;
            case 2:
                lastCar = Instantiate(car3Prefab, transform);
                break;

            default:
                Debug.Log("No Car Spawned");
                break;
        }
        Destroy(lastCar, 20f);
        yield return new WaitForEndOfFrame();
        lastCar.GetComponent<ParallaxCar>().SetDirection(targetLocation);
       
        yield return null;
    }
}
