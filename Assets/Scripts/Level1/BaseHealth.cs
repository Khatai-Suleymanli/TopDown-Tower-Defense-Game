using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public float Health = 100;

    public void TakeDamage(float damage)
    {
        Health -= damage;
        Debug.Log("Base took damage!");

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
