using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public float cooldown = 1;
    public float ammo = 4;
    public GameObject shootFX;
    private PlayerController player;
    public Transform gunPoint;

    private void Start()
    {
        player = GameManager.instance.player;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && player.isAiming)
            ShootNow();
    }

    void ShootNow()
    {
        Instantiate(shootFX, gunPoint.transform.position, transform.rotation);
    }
}
