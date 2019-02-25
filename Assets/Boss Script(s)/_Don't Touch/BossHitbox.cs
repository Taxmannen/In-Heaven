using UnityEngine;

/// <summary>
/// Made by: Filip Nilsson, planned by: Filip Nilsson + Jesper Uddefors
/// </summary>
public class BossHitbox : MonoBehaviour
{
    //Read Only
    [SerializeField] [ReadOnly] private bool weakpoint;
    [SerializeField] [ReadOnly] private float maxHP;
    [SerializeField] [ReadOnly] private float hP;
    [SerializeField] [ReadOnly] private int activePhase;

    //VFXs
    [SerializeField]
    private GameObject sparks;
    [SerializeField]
    private ParticleSystem explosion;
    public Transform explosionTransform;

    [SerializeField]
    private UIWhiteFadeAndFlash whiteFlash;

    [SerializeField]
    private ShaderManager shaderFlash;

    //Private
    private Boss boss;
    private ShaderManager shaderManager;


    //Main
    private void Start()
    {
        boss = GetComponentInParent<Boss>();
    }

    public bool Damagable()
    {
        if (!weakpoint)
        {
            return false;
        }

        if (activePhase != boss.GetActivePhase())
        {
            return false;
        }

        return true;

    }
    public void Receive(float amt)
    {

        if (hP - amt <= 0)
        {
            Die();
            ActivateSparksVFX();
            ActivateExplosionVFX();
            ActivateScreenflashVFX();
            DeactivateShaderFlash();
        }

        else
        {
            Hit(amt);
            //shaderManager.HitEffect(0.4f, false);
        }
    }

    private void Die()
    {
        boss.Receive(hP);
        hP = 0;
        weakpoint = false;
        CameraController.instance.CameraShake();
        //gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        AudioController.instance.BossDestruction();
    }

    private void ActivateSparksVFX() {
        if (sparks != null)
        {
            sparks = Instantiate(sparks, transform.position, transform.rotation, transform);
            sparks.SetActive(true);
            //sparks.transform.position = transform.position;
        }
        else
        {
            Debug.Log("Add sparks VFXs to the destroyed object!");
        }
    }

    private void ActivateExplosionVFX()
    {
        if (explosion != null)
        {
            ParticleSystem newExplosion = Instantiate(explosion, (explosionTransform? explosionTransform.position:transform.position) + (Vector3.back * 2), Quaternion.AngleAxis(90,Vector3.right));
            newExplosion.transform.localScale = new Vector3(1, 1, 1);
            foreach (var item in newExplosion.transform.GetComponentsInChildren<Transform>())
            {
                item.localScale = new Vector3(1, 1, 1);
            }
            newExplosion.Play();
            Destroy(newExplosion.gameObject, 3);
            GetComponent<Renderer>().enabled = false;
        }
        else
        {
            Debug.Log("Add explosion VFXs to the destroyed object!");
        }
    }

    private void ActivateScreenflashVFX()
    {
        if (whiteFlash != null)
        {
            whiteFlash.StartWhiteFlash();
        }
        else
        {
            Debug.Log("Add screenflash-reference!");
        }
    }

    private void DeactivateShaderFlash() {
        if (shaderFlash != null)
        {
            shaderFlash.StopColorEffect();
        }
        else {
            Debug.Log("Add Shaderflash reference to the weakpoint");
        }
    }

    private void Hit(float amt)
    {
        hP -= amt;
        boss.Receive(amt);
    }


    //Getters & Setters
    public bool GetWeakpoint()
    {
        return weakpoint;
    }

    public void SetWeakpoint(bool weakpoint)
    {
        this.weakpoint = weakpoint;
        if (this.weakpoint)
        {
            if(shaderManager = gameObject.GetComponent<ShaderManager>())
            {

            }
            else
            {
                shaderManager = gameObject.AddComponent<ShaderManager>();
            }
            
        }
    }

    public float GetMaxHP()
    {
        return maxHP;
    }

    public void SetMaxHP(float maxHP)
    {
        this.maxHP = maxHP;
    }

    public float GetHP()
    {
        return hP;
    }

    public void SetHP(float hP)
    {
        this.hP = hP;
    }

    public int GetActivePhase()
    {
        return activePhase;
    }

    public void SetActivePhase(int activePhase)
    {
        this.activePhase = activePhase;
    }
}