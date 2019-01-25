using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parrybox : MonoBehaviour
{

    //Serialize
    [SerializeField] public LayerMask lm = 0;

    //Private
    private PlayerController playerController;
    internal BoxCollider collider;

    private void Start()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
        collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Boss Parryable Bullet")
        {
            AudioController.instance.PlayerSuccessfulParry();
            Destroy(other.gameObject);
            playerController.IncreaseSuperCharge();
            GetComponent<Collider>().enabled = false;
        }
    }
}