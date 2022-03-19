using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject weaponSwingFX;
    public GameObject weaponSwingFX2;
    public Transform spawnLocation;
    private Animator anim;
    private GameObject hitBox;

    public void Start()
    {
        anim = GetComponent<Animator>();
        hitBox = transform.GetChild(0).gameObject;
    }

    public void ActiveTrigger()
    {
        hitBox.SetActive(true);
    }
    public void DeactiavteTrigger()
    {
        hitBox.SetActive(false);
    }
    public void SpawnEffect1()
    {
        GameObject swing = Instantiate(weaponSwingFX, spawnLocation.position, spawnLocation.parent.rotation);
    }
    public void SpawnEffect2()
    {
        GameObject swing = Instantiate(weaponSwingFX, spawnLocation.position, spawnLocation.parent.rotation);
    }
}
