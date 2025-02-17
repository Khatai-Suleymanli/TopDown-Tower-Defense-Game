using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret1Shoot : MonoBehaviour
{
    public float AttackRange = 10f;
    public float FireRate = 1f;
    public int damage = 5;
    private float NextFireTime;

    [Header("------MUZZLE-------")]
    public ParticleSystem muzzleEffect;
    public ParticleSystem muzzleEffect2;

    //for ilustrating range
    public float radious = 5f;
    public Color color = Color.green; // color of the range circle of the tower cannons

    private void OnDrawGizmos()
    {
        Gizmos.color = color;

        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");





        foreach (GameObject enemy in enemies)
        {

            if (Vector3.Distance(transform.position, enemy.transform.position) <= AttackRange && Time.time > NextFireTime)
            {
                //shoot enemy
                ShootEnemy(enemy);
                NextFireTime = FireRate + Time.time;
                break; //shooting one enemy at a time
            }
        }
    }

    void ShootEnemy(GameObject enemy)
    {

        if (muzzleEffect != null) // error handling
        {
            muzzleEffect.Play();
            muzzleEffect2.Play();
        }

        // error handling
        enemy.GetComponent<BaseHealth>().TakeDamage(damage); // replace with enemyHealth script.
        Debug.Log("Base shot the enmy");

    }
}
