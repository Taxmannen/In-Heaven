using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIKcontrollerElbow : MonoBehaviour
{

    private ShootAction shootAction;
    protected Animator animator;

    [Range(9f, 15f)]
    public float armMovement;

    Vector3 elbowMovment;
   
    void Start()
    {

        elbowMovment = transform.position;
        animator = GetComponent<Animator>();
    }


     void Update()
    {

        if (Input.GetMouseButton(0))
        {
            elbowMovment = new Vector3(transform.position.x, transform.position.y, armMovement);

            transform.position = elbowMovment;
        }

    }

    //void OnAnimatorIK()
    //{
    //    if (Input.GetMouseButton(0))
    //    {
    //        animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);

    //    }

    //}
}
