using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwing : MonoBehaviour
{
    public GameObject weaponSwingFX;
    public Transform spawnLocation;

    public void SpawnEffect()
    {
        GameObject particle = Instantiate(weaponSwingFX, spawnLocation.position, spawnLocation.parent.rotation);
    }
}
