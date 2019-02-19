using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxCar : MonoBehaviour
{
    Rigidbody rigidbody;
    public Transform target;
    public float speed;
    private Vector3 direction;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        target = GetComponentInParent<ParallaxCode>().targetLocation;
        GetComponentInChildren<Renderer>().material.SetColor("_Color",new Color(Random.value, Random.value, Random.value));

    }

    public void SetDirection(Transform target)
    {
        this.target = target;
        direction = this.target.position - transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            if (Vector3.Distance(transform.position, target.position) < 10)
            {
                Destroy(gameObject);
            }
            Vector3 velocity = direction * (speed * Time.fixedDeltaTime);
            rigidbody.velocity = velocity;
            //transform.rotation.SetFromToRotation(transform.position,target.position);

        }
        
    }
}
