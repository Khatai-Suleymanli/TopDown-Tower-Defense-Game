using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseShooting : MonoBehaviour
{
    public float AttackRange = 10f;
    public float FireRate = 1f;
    public int damage = 30;
    private float NextFireTime;


    public Transform shootPoint;

    [Header("------MUZZLE-------")]
    public ParticleSystem muzzleEffect;

    //for ilustrating range
    public float radious = 5f;
    public Color color = Color.green; // color of the range circle of the tower cannons


    [Header("------Visualizing-------")]
    public Color lineColor = Color.red;
    public float thickness = 0.05f;

    [Header("------Animation-------")]
    public Animator animator;

    [Header("------Sound-------")]
    public AudioSource audioSource;




    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        
        Gizmos.DrawWireSphere( transform.position, AttackRange );



        ///////
        Gizmos.color = lineColor;

        Vector3 start =  shootPoint.transform.position;
        Vector3 end = shootPoint.transform.position + shootPoint.transform.forward;


        Gizmos.DrawLine(start, end);
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


        
       

        foreach (GameObject enemy in enemies) {

            Vector3 forwardTurret = shootPoint.transform.forward; // our turret's forward vector


            Vector3 toEnemyVector =enemy.transform.position - transform.position;  // bizden dusmene vector


            float dotValue = Vector3.Dot(forwardTurret, toEnemyVector);

            if (Vector3.Distance(transform.position, enemy.transform.position) <= AttackRange && Time.time > NextFireTime && dotValue >= 0.98)
            {
                //shoot enemy
                ShootEnemy(enemy);
                NextFireTime = FireRate + Time.time;
                break; //shooting one enemy at a time
            }
            else {
                animator.SetBool("shoot", false);
            }

        }
    }

    void ShootEnemy(GameObject enemy) {

        animator.SetBool("shoot", true);

        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.Log("Audiosource not found!!!!");
        }

        if (muzzleEffect != null) // error handling
        {
            muzzleEffect.Play();
        }

        // error handling
        enemy.GetComponent<BaseHealth>().TakeDamage(damage); // replace with enemyHealth script.
        Debug.Log("Base shot the enmy");

    }
}
