using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private float imunity;

    //private HealthBar healthBar;

    private GameObject damageFlash;

    public bool defeated;

    public void Awake()
    {

        //healthBar = FindObjectOfType<HealthBar>();
        // Damage Flash is the second child of the Canvas in the scene.
        //damageFlash = GameObject.Find("Canvas").transform.GetChild(1).gameObject;


    }

    public void Start()
    {

        currentHealth = maxHealth;
        //healthBar.SetMaxHealth(maxHealth);
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
            //StartCoroutine(DamageAnimation());

            currentHealth -= dmg;

            //healthBar.SetHealth(currentHealth);

            if (currentHealth <= 0)
            {
                GameManager.instance.isDead = true;
                defeated = true;
                Debug.Log("You have been defeated");
                //GameManager.instance.Defeat();
            }
        }
    }

   // IEnumerator DamageAnimation()
   // {
        //damageFlash.SetActive(true);
        //yield return new WaitForSeconds(0.4f);
        //damageFlash.SetActive(false);
    //}
}
