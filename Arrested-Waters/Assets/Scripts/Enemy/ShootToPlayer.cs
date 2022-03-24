using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootToPlayer : MonoBehaviour
{
    private Rigidbody2D rigi;
    public float force;

    public void Start()
    {
        rigi = GetComponent<Rigidbody2D>();
        rigi.velocity = (GameManager.instance.player.transform.position - transform.position).normalized*force;
    }

}
