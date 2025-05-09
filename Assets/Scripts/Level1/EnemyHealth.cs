using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{

    public float Health = 100;



    public void TakeDamage(float damage)
    {
        Health -= damage;
        Debug.Log("Enemy took damage!");

        if (Health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        Debug.Log(Health);
    }
}
