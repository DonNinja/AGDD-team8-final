using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public float cooldown = 1;
    public int currentAmmo = 2;
    public int maxAmmo = 2;
    public GameObject shootFX;
    private PlayerController player;
    public Transform gunPoint;
    public GameObject[] ammoUIslot;
    public bool canShoot = true;

    private void Start()
    {
        currentAmmo = maxAmmo;
        player = GameManager.instance.player;
    }

    void Update()
    {
        if (canShoot && Input.GetMouseButtonDown(0) && player.isAiming)
            ShootNow();
    }

    void ShootNow()
    {
        GameObject fx = Instantiate(shootFX, gunPoint.transform.position, transform.rotation);
        fx.transform.localScale = player.transform.localScale;
        currentAmmo -= 1;
        for (int i = 0; i < maxAmmo; i++)
        {
            ammoUIslot[i].SetActive(false);
            if (i < currentAmmo)
                ammoUIslot[i].SetActive(true);
        }
        canShoot = false;
        StartCoroutine(cooldownCorotine());
    }

    IEnumerator cooldownCorotine()
    {
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }
}
