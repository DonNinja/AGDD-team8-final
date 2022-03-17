using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public float power;
    public bool isEnemy;
    public GameObject blood;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isEnemy)
        {
            if (collision.gameObject.tag == "MonsterHitBox")
            {
                collision.gameObject.transform.parent.GetComponent<EnemyAI>().TakeDamage(power);
                Instantiate(blood, collision.transform.position, transform.rotation);
            }
        }
        else
        {
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerStats>().TakeDamage((int)power);
                Instantiate(blood, collision.transform.position, transform.rotation);
            }
        }
    }
}
