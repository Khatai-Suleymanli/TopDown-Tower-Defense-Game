using UnityEngine;

public class BaseHealth : MonoBehaviour
{
    public int Health = 100;

    public void TakeDamage(int damage)
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
    }
}
