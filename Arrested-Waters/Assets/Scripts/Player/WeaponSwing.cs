using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwing : MonoBehaviour
{
    public GameObject weaponSwingFX;
    public GameObject weaponSwingFX2;
    public Transform spawnLocation;
    public GameObject gun;
    [SerializeField] AudioSource weaponSwingAudioSource;
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
        if (Input.GetMouseButtonDown(1))
        {
            anim.SetBool("gunmode", true);
        }
        if (Input.GetMouseButtonUp(1))
        {
            anim.SetBool("gunmode", false);
        }

    }

    public void ActiveTrigger()
    {
        hitBox.SetActive(true);
    }
    public void DeactivateTrigger()
    {
        hitBox.SetActive(false);
    }

    public void ActivateGun()
    {
        player.isAiming = true;
        gun.SetActive(true);
    }
    public void DeactivateGun()
    {
        player.isAiming = false;
        gun.SetActive(false);
    }

    public void SpawnEffect()
    {
        GameObject swing = Instantiate(weaponSwingFX, spawnLocation.position, spawnLocation.parent.rotation);
        swing.transform.localScale = (player.transform.localScale)/2;
        swing.transform.GetChild(0).localScale = (player.transform.localScale)/ 2;
    }
    public void SpawnEffect2()
    {
        GameObject swing = Instantiate(weaponSwingFX2, spawnLocation.position, spawnLocation.parent.rotation);
        swing.transform.localScale = (player.transform.localScale) / 2;
        swing.transform.GetChild(0).localScale = (player.transform.localScale) / 2;
        swing.transform.localScale = new Vector3(swing.transform.localScale.x*-1, swing.transform.localScale.y, swing.transform.localScale.z);
        swing.transform.GetChild(0).localScale = new Vector3(swing.transform.localScale.x, swing.transform.localScale.y, swing.transform.localScale.z);
    }

    public void PlaySwingSound() {
        weaponSwingAudioSource.Play();
    }
}
