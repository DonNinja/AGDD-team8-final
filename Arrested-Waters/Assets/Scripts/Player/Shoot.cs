using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public float cooldown = 1;
    public float ammo = 4;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            ShootNow();
    }

    void ShootNow()
    {
        Debug.Log("Bang");
    }
}
