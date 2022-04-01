using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableWhenClose : MonoBehaviour
{

    public GameObject thingToEnable;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameManager.instance.player.gameObject)
        {
            thingToEnable.SetActive(true);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == GameManager.instance.player.gameObject)
        {
            thingToEnable.SetActive(false);
        }
    }


}
