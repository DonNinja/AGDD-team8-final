using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnableWhenClose : MonoBehaviour
{

    public GameObject thingToEnable;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameManager.instance.player.gameObject)
        {
            thingToEnable.SetActive(true);
        }
        Debug.Log(collision.gameObject);
        AstarPath.active.Scan();
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == GameManager.instance.player.gameObject)
        {
            thingToEnable.SetActive(false);
        }
    }


}
