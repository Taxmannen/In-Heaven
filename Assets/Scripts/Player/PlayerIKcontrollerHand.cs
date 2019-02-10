using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIKcontrollerHand : MonoBehaviour
{
    protected Animator animator;

    public bool ikActive = false;
    public Transform rightHandObj;// = null;
    public Transform lookObj = null;
    private Quaternion handRotation;
    
    void Start()
    {
        animator = GetComponent<Animator>();

        handRotation = Quaternion.LookRotation(rightHandObj.position - transform.position);

    }

    void OnAnimatorIK()
    {

        if(Input.GetMouseButton(0))
        {
            //if (lookObj != null)
            //{
            //    animator.SetLookAtWeight(1);
            //    animator.SetLookAtPosition(lookObj.position);
            //}

            //if (rightHandObj != null)
            //{
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                animator.SetIKRotation(AvatarIKGoal.RightHand, handRotation);





          //  }
            //else
            //{
            //    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
            //    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
            //    animator.SetLookAtWeight(0);
            //}
        }

    }

}
