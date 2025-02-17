using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurretLookAtEnemy : MonoBehaviour
{
    [Header("Rotation stuff")]
    public float rotationSpeed = 1f;
    public float distance = 10f;
    public string enemyTag = "Enemy";

    private Transform currentTarget;


    // Update is called once per frame
    void Update()
    {
        currentTarget = FindClosestEnemy();

        float distanceToEnemy = Vector3.Distance(transform.position, currentTarget.position);

        if(currentTarget != null && distanceToEnemy <= distance)
        {
            Vector3 direction = currentTarget.position - transform.position;

            Quaternion lookRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }


    private Transform FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        Transform closestEnemy = null;

        float minDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies) {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < minDistance) { 
                minDistance = distance;
                closestEnemy = enemy.transform;
            }
        
        }

        return closestEnemy;


    }
}
