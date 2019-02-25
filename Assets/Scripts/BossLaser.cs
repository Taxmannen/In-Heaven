using UnityEngine;

public class BossLaser : MonoBehaviour
{
    float damage = 1;

    [SerializeField]
    internal ParticleSystem vfx;

    private void Start()
    {
        if (vfx == null)
        {
            vfx = GetComponentInChildren<ParticleSystem>();
        }

        if (vfx != null)
        {
            vfx.Play();
        }
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(AudioController.instance.GetEventInstance(), GetComponent<Transform>(), GetComponent<Rigidbody>());
        AudioController.instance.BossLaserLoop();
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player Hitbox")
        {
            other.GetComponentInParent<PlayerController>().Receive(damage);
        }
    }
}