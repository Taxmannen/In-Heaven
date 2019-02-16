using UnityEngine;

/* Script made by Daniel */
public class GunManager : MonoBehaviour
{
    [SerializeField] private Transform gun;
    [SerializeField] private Transform LeftParent;
    [SerializeField] private Transform rightParent;

    private Animator anim;
    private bool shifted = true;

    private void Start()
    {
        anim = GetComponent<Animator>();
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

    void OnAnimatorIK()
    {
        bool movLeft = anim.GetBool("MovingLeft");
        if (movLeft && gun.parent != LeftParent || !movLeft && gun.parent != rightParent) shifted = false;
        if (!shifted)
        {
            if (movLeft) SetGunHand(false);
            else         SetGunHand(true);
        }
    }
}