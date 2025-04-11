using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyHitEffect : MonoBehaviour
{
    private Renderer[] enemyRenderers;
    private Color[] originalColors;  

    [Header("Hit Effect settings")]
    [SerializeField] private Color hitColor = Color.green;
    [SerializeField] private float flashDuration = 0.15f;
    public bool Emission = true;
    public float emissionIntensity = 2f;


    // Start is called before the first frame update
    void Start()
    {
        // Use GetComponentsInChildren<Renderer>() to get all Renderer components in child objects
        enemyRenderers = GetComponentsInChildren<Renderer>();
        originalColors = new Color[enemyRenderers.Length];

        for (int i = 0; i < enemyRenderers.Length; i++)
        {
            originalColors[i] = enemyRenderers[i].material.color;

            if (Emission)
            {
                enemyRenderers[i].material.EnableKeyword("_EMISSION"); // syntax
            }
        }
    }

    public void TriggerHit()
    {
        StartCoroutine(HitEffectRoutine());
    }

    private IEnumerator HitEffectRoutine()
    {
        foreach (Renderer renderer in enemyRenderers)
        {
            renderer.material.color = hitColor;
            if (Emission)
            { // -- syntax
                renderer.material.SetColor("_EmissionColor", hitColor * emissionIntensity);
                renderer.material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
                DynamicGI.UpdateEnvironment();
            }

        }


        yield return new WaitForSeconds(flashDuration);

        for (int i = 0; i < enemyRenderers.Length; i++)
        {
            enemyRenderers[i].material.color = originalColors[i];
            if (Emission)
            {
                enemyRenderers[i].material.SetColor("_EmissionColor", Color.black);
                DynamicGI.UpdateEnvironment();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
