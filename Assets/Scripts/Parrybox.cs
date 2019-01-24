using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parrybox : MonoBehaviour
{

    //Serialize
    [SerializeField] public LayerMask lm = 0;

    //Private
    private PlayerController playerController;
    private RaycastHit hit;
    private Vector3 rayPoint;

    private void Start()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Boss Parryable Bullet")
        {

            Destroy(other.gameObject);
            playerController.IncreaseSuperCharge();

        }

    }

}