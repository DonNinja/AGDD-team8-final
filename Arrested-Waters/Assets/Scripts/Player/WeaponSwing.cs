using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwing : MonoBehaviour
{
    public GameObject weaponSwingFX;
    public Transform spawnLocation;
    private Animator anim;
    private GameObject hitBox;
    private PlayerController player;

    public void Start()
    {
        anim = GetComponent<Animator>();
        hitBox = transform.GetChild(0).gameObject;
        player = GameManager.instance.player;
    }


    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && !player.isAiming)
        {
            anim.SetTrigger("Attack");
        }
    }

    public void ActiveTrigger()
    {
        hitBox.SetActive(true);
    }
    public void DeactiavteTrigger()
    {
        hitBox.SetActive(false);
    }

    public void SpawnEffect()
    {
        Instantiate(weaponSwingFX, spawnLocation.position, spawnLocation.parent.rotation);
    }
    public void SpawnEffect2()
    {
        GameObject swing = Instantiate(weaponSwingFX, spawnLocation.position, spawnLocation.parent.rotation);
        swing.transform.rotation = Quaternion.Euler(swing.transform.rotation.x, swing.transform.rotation.y, swing.transform.rotation.z-180);
        swing.transform.localScale = new Vector3(swing.transform.localScale.x * -1, swing.transform.localScale.y, swing.transform.localScale.z);
        swing.transform.GetChild(0).localScale = new Vector3(swing.transform.localScale.x, swing.transform.localScale.y, swing.transform.localScale.z);
    }
}
