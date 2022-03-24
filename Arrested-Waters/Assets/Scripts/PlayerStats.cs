using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private float imunity;

    public HealthBar healthBar;

    public bool defeated;

    public void Start()
    {

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void Update()
    {
        imunity -= Time.deltaTime;
    }

    public void TakeDamage(int dmg)
    {

        if (imunity <= 0)
        {
            imunity = 1;
            

            currentHealth -= dmg;

            healthBar.SetHealth(currentHealth);

            if (currentHealth <= 0)
            {
                GameManager.instance.isDead = true;
                defeated = true;
                Debug.Log("You have been defeated");
                //GameManager.instance.Defeat();
            }
        }
    }
}
