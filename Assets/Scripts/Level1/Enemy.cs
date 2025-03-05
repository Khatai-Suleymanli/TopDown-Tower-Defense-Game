using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float AttackRange = 15f;
    public float FireRate = 1f;
    public int damage = 10;
    private float NextFireTime;
    private GameObject BaseTarget;

   

    void Start()
    {
        BaseTarget = GameObject.FindWithTag("Base");
        

       
    }
    
    void Update()
    {

        if (BaseTarget != null && Time.time > NextFireTime)
        {

            float distance = Vector3.Distance(transform.position, BaseTarget.transform.position);
            if (distance <= AttackRange)
            {
                ShootBase(); //For shooting
                NextFireTime = FireRate + Time.time;
            }
            else {
                //animator.SetBool("Shooting", false);
            }
        }
    }
    void ShootBase()
    {
        BaseTarget.GetComponent<BaseHealth>().TakeDamage(damage);

       
        Debug.Log("Enemy shot the base");

        //particle effect here
        //particle efect dusmenin silahinin ucundan play olsun
    }
}
