using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIKcontrollerHand : MonoBehaviour
{
    protected Animator animator;

    private PlayerMovement playerMovement;

    public GameObject playerReferenceObj;

    private PlayerController playerController;

    bool movingleft;

    public bool ikActive = false;
    public Transform rightHandObj;// = null;
    public Transform leftHandObj;
    public Transform lookObj = null;
    private Quaternion handRotation;
    private Quaternion leftHandRotation;
    private Vector3 lookObjPosition;
    private Vector3 rightHandObjPosition;
    private Vector3 leftHandObjPosition;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = playerReferenceObj.GetComponent<PlayerController>();

        handRotation = Quaternion.LookRotation(rightHandObj.position - transform.position);
        leftHandRotation = Quaternion.LookRotation(leftHandObj.position - transform.position);

    }
    private void Update()
    {
        if(GameController.instance.gameState == Global.GameState.Game)
        {
            ikActive = true;
        }
        else
        {
            ikActive = false;
        }
    }

    void OnAnimatorIK()
    {
        if (playerController.playerState != Global.PlayerState.Dead)
        {
            if (ikActive)
            {
                lookObjPosition = lookObj.position;
                rightHandObjPosition = rightHandObj.position;
                leftHandObjPosition = leftHandObj.position;
            }


            if (lookObj != null)
            {
                animator.SetLookAtWeight(1);
                animator.SetLookAtPosition(lookObjPosition);
            }

            if (animator.GetBool("MovingLeft") == false)
            {
                

                if (rightHandObj != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObjPosition);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, handRotation);
                }
                else
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                    animator.SetLookAtWeight(0);
                }
            }



            else if (animator.GetBool("MovingLeft") == true)
            {
                if (leftHandObj != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObjPosition);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandRotation);
                }
                else
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
                    animator.SetLookAtWeight(0);
                }
            }

        }
    }

}
