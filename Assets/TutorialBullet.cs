using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBullet : MonoBehaviour
{
    Rigidbody rb;
    public Transform tutorialBulletOrigin;
    public Transform tutorialBulletTarget;
    public float bulletSpeed;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("HEJEHJ");
        rb = GetComponent<Rigidbody>();
        Vector3 direction = tutorialBulletTarget.position - tutorialBulletOrigin.position;
        rb.velocity = direction.normalized * bulletSpeed;

    }

    // Update is called once per frame
    void Update()
    {
     
    }
}
