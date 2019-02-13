using UnityEngine;

/* Script made by Daniel */
public class GunManager : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform gun;
    [SerializeField] private Transform LeftParent;
    [SerializeField] private Transform rightParent;

    bool shifted = true;
    bool leftHand = false;
    bool rightHand = false;
    bool standingStill = true;

    private void Update()
    {
        HandleGun();
    }

    void SetGunHand(bool rightHand)
    {
        if (rightHand) gun.SetParent(rightParent);
        else           gun.SetParent(LeftParent);
        gun.localPosition = new Vector3(0, 0, 0);
        gun.rotation = new Quaternion(0, 0, 0, 0);
        gun.localScale = new Vector3(1, 1, 1);
        shifted = true;
    }

    private void HandleGun()
    {
        if (rb.velocity.x > 0 && !rightHand || rb.velocity.x < 0 && !leftHand || rb.velocity.x == 0 && !standingStill) shifted = false;
        if (!shifted)
        {
            if (rb.velocity.x > 0)
            {
                SetGunHand(false);
                rightHand = true;
                leftHand = false;
                standingStill = false;
            }
            else if (rb.velocity.x < 0)
            {
                SetGunHand(true);
                leftHand = true;
                rightHand = false;
                standingStill = false;
            }
            else if (rb.velocity.x == 0)
            {
                SetGunHand(true);
                rightHand = false;
                leftHand = false;
                standingStill = true;
            }
        }
    }
}