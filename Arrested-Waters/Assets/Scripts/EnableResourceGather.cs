using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableResourceGather : MonoBehaviour
{
    public CollectableController collect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameManager.instance.player.gameObject)
            collect.enabled = true;
    }
}
