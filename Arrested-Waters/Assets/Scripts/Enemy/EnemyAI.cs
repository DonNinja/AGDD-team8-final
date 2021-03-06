using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public AIPath path;
    public float health;
    public bool aggro;
    public GameObject player;
    private Rigidbody2D rigi;
    public GameObject attack;
    private SpriteRenderer sprite;
    private Animator anim;
    public AudioClip monsterSound;
    public AudioSource audioSource;
    public float attackWalkSpeed;
    public GameObject drop;

    bool immune = false;

    public bool immuneToStagger = false;
    public float movmentSpeed = 6;
    public float attackSpeed = 2;
    private float attackTimer;
    public float attackAnimationtime;
    public float attackRange = 5;

    private float walkSpeed;

    public float interestTime = 3;
    private float interestTimer = 0;

    public void Start()
    {
        path = gameObject.GetComponent<AIPath>();
        player = GameManager.instance.player.gameObject;
        rigi = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        //attack = transform.GetChild(2).gameObject;
        audioSource = GetComponent<AudioSource>();

        attackTimer = attackSpeed;
        walkSpeed = movmentSpeed;

        path.maxSpeed = walkSpeed;
    }

    public void SpottPlayer(GameObject player)    
    {
        interestTimer = 0;
        aggro = true;
        if (monsterSound != null && !audioSource.isPlaying)
            audioSource.PlayOneShot(monsterSound);
    }
    public void LosePlayer()
    {
        aggro = false;
        interestTimer = 0;
        path.destination = transform.position;
    }

    private void FixedUpdate()
    {
        if (aggro)
        {
            path.destination = player.transform.position;
            interestTimer += Time.deltaTime;
            if (interestTimer >= interestTime)
                LosePlayer();
        }

        path.maxSpeed = walkSpeed;

        if (rigi.velocity.magnitude <= 0.1f)
        {
            path.canMove = true;
        }

        if (attackTimer < attackSpeed)
        {
            attackTimer += Time.deltaTime;
        }
        if ((player.transform.position - transform.position).magnitude <= attackRange && attackTimer >= attackSpeed && path.canMove == true && aggro == true)
        {
            Attack();
        }

    }

    public void TakeDamage(float power)
    {
        SpottPlayer(player);
        if (!immune)
        {
            health -= power;
            if (health <= 0)
            {
                Die();
            }

            if (!immuneToStagger)
            {
                path.canMove = false;
                rigi.velocity = (player.transform.position - transform.position).normalized * -10;
                rigi.AddForce((player.transform.position - transform.position).normalized * -10000);
                StopAttack();
            }
        }
    }

    public void Attack()
    {
        attackTimer = 0;
        attack.SetActive(true);

        walkSpeed = attackWalkSpeed;
        Invoke("StopAttack", attackAnimationtime);
    }
    public void StopAttack()
    {
        attack.SetActive(false);
        walkSpeed = movmentSpeed;
    }
    public void Die()
    {
        Destroy(gameObject);
        Instantiate(drop, transform.position, transform.rotation);
    }

    IEnumerator imuneToDamage()
    {
        yield return new WaitForSeconds(0.2f);
        immune = false;
    }
}
