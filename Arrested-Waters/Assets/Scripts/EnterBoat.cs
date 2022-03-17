using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBoat : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == GameManager.instance.boat.gameObject)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("Enter the ship");
            }
        }
    }
}
