using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{

    [SerializeField]
    internal Material material;

    [SerializeField]
    internal ParticleSystem vfx;

    [SerializeField]
    internal float horizontalScrollSpeedMultiplier = 0;

    [SerializeField]
    internal float verticalScrollSpeedMultiplier = 1;

    internal Vector2 textureScrollingOffset = new Vector2(0, 1);

    private void Start()
    {
        material = GetComponent<Renderer>().material;
        vfx = GetComponentInChildren<ParticleSystem>();
        vfx.Play();
    }

    private void Update()
    {
        textureScrollingOffset.x += Time.deltaTime * horizontalScrollSpeedMultiplier;
        textureScrollingOffset.y += Time.deltaTime * verticalScrollSpeedMultiplier;
        material.SetTextureOffset("_MainTex", textureScrollingOffset);
    }
}
