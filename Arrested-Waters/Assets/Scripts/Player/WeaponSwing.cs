using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwing : MonoBehaviour
{
    public GameObject weaponSwingFX;
    public Transform spawnLocation;

    public void SpawnEffect()
    {
        Instantiate(weaponSwingFX, spawnLocation.position, spawnLocation.rotation);
    }
}
