using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Turret2Shoot : MonoBehaviour
{
    public float AttackRange = 5f;
    public float FireRate = 3f;
    public float soundRate = 0.1f;
    private int damage = 2;
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





        foreach (GameObject enemy in enemies)
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) <= AttackRange && Time.time > NextSoundTime)
            {
                // sound effect starts here:
                if (audioSource != null && audioSource.clip != null)
                {
                    audioSource.Play();
                }
                else
                {
                    Debug.Log("Audiosource not found!!!!");
                }
                NextSoundTime = soundRate + Time.time;
            }

            if (Vector3.Distance(transform.position, enemy.transform.position) <= AttackRange && Time.time > NextFireTime)
            {
                //shoot enemy
                ShootEnemy(enemy);
                NextFireTime = FireRate + Time.time;

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
        animator.SetBool("shooting", true);
        
        Debug.Log(animator.GetBool("shooting"));

        if (muzzleEffect != null) // error handling
        {
            muzzleEffect.Play();
            //muzzleEffect2.Play();
        }

        // error handling
        enemy.GetComponent<BaseHealth>().TakeDamage(damage); // replace with enemyHealth script.
        Debug.Log("Turret shot the enemy");

    }
}
