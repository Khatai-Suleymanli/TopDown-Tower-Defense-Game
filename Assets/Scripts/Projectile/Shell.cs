using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public GameObject explosionEffect; // explosin effect 


    private CameraShake cameraShake; // reference to camera shake script

    public float explosionRadius = 40.0f;

    public float shakeDuration = 0.5f; // variable values tor replace the ones in camera shake script
    public float shakePower = 0.3f;

    private void Start()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) <= explosionRadius)
                {
                    Destroy(enemy);
                }
            }

            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            cameraShake.StartCameraShake(shakeDuration, shakePower);
            Destroy(gameObject);
        }

    }




}
