using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_3_Shoot : MonoBehaviour
{
    public float AttackRange = 10f;
    public float FireRate = 1f;
    private int damage = 5;
    private float NextFireTime;

    [Header("------MUZZLE-------")]
    public ParticleSystem muzzleEffect;
    public ParticleSystem muzzleEffect2;

    //for ilustrating range
    public float radious = 5f;
    public Color color = Color.blue; // color of the range circle of the tower cannons

    [Header("------Animation-------")]
    public Animator animator;

    [Header("------Sound-------")]
    public AudioSource audioSource;


    private void OnDrawGizmos()
    {
        Gizmos.color = color;

        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }


    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
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
            else
            {
                animator.SetBool("shoot", false);
            }
        }
    }

    void ShootEnemy(GameObject enemy)
    {
        // animation transition here:
        animator.SetBool("shoot", true);
        // sound effect starts here:
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.Log("Audiosource not found!!!!");
        }
        Debug.Log(animator.GetBool("shoot"));
        Debug.Log("3 shot the enemy");


        

        if (muzzleEffect != null) // error handling
        {
            muzzleEffect.Play();
            muzzleEffect2.Play();
            Debug.Log("Fire effect called");
            
        }

        // error handling
        enemy.GetComponent<BaseHealth>().TakeDamage(damage); // replace with enemyHealth script.
        enemy.GetComponent<EnemyHitEffect>().TriggerHit();
        Debug.Log("Turret shot the enemy");

    }
}
