using System.Collections;
using UnityEngine;

/* Script made by Daniel */
public class ShaderManager : MonoBehaviour {

    #region Variables
    [Header("Invincibility")]
    [Tooltip("Color when invincible/boss getting hit")]
    [SerializeField] private Color hitColor = new Color(0.5f, 0.5f, 0.5f);
    [Range(0, 15)] [Tooltip("The speed of lerp between the colors when invincible/boss getting hit")]
    [SerializeField] private float hitSpeed = 5;
    [Range(0, 1)] [Tooltip("The percentage of lerping when invincible/boss getting hit")] //ÄNDRAS
    [SerializeField] private float hitLerpPercentage = 0.5f;

    [Header("Dash")]
    [Tooltip("Color when dash recharged")]
    [SerializeField] private Color dashColor;
    [Range(0, 10)] [Tooltip("The speed of lerp beween the colors when dash is recharged")]
    [SerializeField] private float dashSpeed = 3;
    [Range(0, 1)] [Tooltip("The percentage of lerping when dash is recharged")] //ÄNDRAS
    [SerializeField] private float dashLerpPercentage = 1;

    [Header("Meshes")]
    [Tooltip("Add all the renderers that the mesh contain")]
    [SerializeField] private Renderer[] renderers;

    private Material[] materials;
    private Color[] orgColors;
    private Color[] lerpedColors;
    private bool fading;
    #endregion

    private void Start()
    {
        Setup();
    }

    public void HitEffect(float duration)
    {
        StartCoroutine(ColorEffect(hitColor, hitSpeed, hitLerpPercentage));
        Invoke("StopColorEffect", duration);
    }

    //Under Utveckling
    public void DashEffect()
    {
        /*StartCoroutine(ColorEffect(dashColor, dashSpeed, dashLerpPercentage));
        Invoke("StopColorEffect", 0.27f);*/
    }

    private IEnumerator ColorEffect(Color color, float speed, float lerpPercentage)
    {
        fading = true;
        while (fading)
        {
            ColorLerp(color, speed, lerpPercentage);
            yield return null;
        }
        for (int i = 0; i < materials.Length; i++)  materials[i].SetColor("_Color", orgColors[i]);
    }

    private void ColorLerp(Color color, float speed, float lerpPercent)
    {
        for (int i = 0; i < materials.Length; i++)
        {
            lerpedColors[i] = Color.Lerp(orgColors[i], color, Mathf.PingPong(Time.time * speed, lerpPercent));
            materials[i].SetColor("_Color", lerpedColors[i]);
        }
    }

    private void StopColorEffect()
    {
        fading = false;
        StopCoroutine("ColorEffect");
    }

    private void Setup()
    {
        //This will add the objects renderer if the array is empty
        if (renderers == null || renderers.Length == 0)
        {
            renderers = new Renderer[1];
            renderers[0] = GetComponent<Renderer>();
        }

        materials = new Material[renderers.Length];
        orgColors = new Color[renderers.Length];
        lerpedColors = new Color[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            materials[i] = renderers[i].material;
            orgColors[i] = materials[i].GetColor("_Color");
            lerpedColors[i] = orgColors[i];
        }
    }
}