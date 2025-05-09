using UnityEngine;
using UnityEngine.UI;

public class BaseHealth : MonoBehaviour
{
    private int maxHealth = 100;
    public float Health = 100;
    public Slider healthBar;
    public Image fillImage;


    private void Start()
    {
        InitializePowerBar();
    }

    void InitializePowerBar()
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = 100;
    }

    /*void UpdateHealthBar()
    {
        healthBar.value = Health;
    }*/



    public void TakeDamage(float damage)
    {
        Health -= damage;
        //UpdateHealthBar();
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
