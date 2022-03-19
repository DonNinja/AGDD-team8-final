using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brakable : MonoBehaviour
{
    public GameObject particles;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Attack")
        {
            Instantiate(particles, collision.transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
