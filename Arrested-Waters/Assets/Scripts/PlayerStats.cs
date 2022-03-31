using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    public float currentHealth;
    private float imunity;
    public float regenerate = 0.1f;
    public HealthBar healthBar;

    public bool defeated;

    [SerializeField] AudioSource hurt_audio;

    public void Start()
    {

        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void Update()
    {
        imunity -= Time.deltaTime;
        if (currentHealth < maxHealth)
            currentHealth += (regenerate * Time.deltaTime);
        healthBar.SetHealth(currentHealth);
    }

    public void TakeDamage(int dmg)
    {
        if (imunity <= 0)
        {
            imunity = 1;

            hurt_audio.Play();

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
