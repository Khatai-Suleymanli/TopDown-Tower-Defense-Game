using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Turret2Shoot : MonoBehaviour
{
    public float AttackRange = 5f;
    public float FireRate = 0.1f;
    public float damage = 2.0f;
    private float NextFireTime;
    private float NextSoundTime;

    [Header("------MUZZLE-------")]
    public ParticleSystem muzzleEffect;
    public ParticleSystem smokeEffct;
    //public ParticleSystem muzzleEffect2;

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





        foreach (GameObject enemy in enemies)  // ------ all good
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) <= AttackRange && Time.time > NextFireTime)
            {
                //shoot enemy
                ShootEnemy(enemy);
                NextFireTime = FireRate + Time.time;
                if (audioSource != null && audioSource.clip != null)
                {
                    audioSource.Play();
                }
                else
                {
                    Debug.Log("Audiosource not found!!!!");
                }

                break; //shooting one enemy at a time
            }
            else
            {
                animator.SetBool("shooting", false);
                smokeEffct.Play();
            }
        }
    }

    void ShootEnemy(GameObject enemy)
    {
        // animation transition here:
        animator.SetBool("shooting", true);  // shooting anim needs to change ----------------------------------
        
        Debug.Log(animator.GetBool("shooting"));

        if (muzzleEffect != null) // error handling   // change the muzzle effect ------------------------------
        {
            muzzleEffect.Play();
            //muzzleEffect2.Play();
        }

        // error handling
        enemy.GetComponent<BaseHealth>().TakeDamage(damage); // replace with enemyHealth script.  ------------------------------------
        enemy.GetComponent<EnemyHitEffect>().TriggerHit();
        Debug.Log("Turret shot the enemy");
    }
}
