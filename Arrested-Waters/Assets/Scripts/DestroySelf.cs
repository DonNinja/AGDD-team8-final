using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public float timer = 5;
    public bool justDisable;

    private float counter;

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        if (timer <= counter)
            if (justDisable == true)
            {
                counter = 0;
                gameObject.SetActive(false);
            }
            else
                Destroy(gameObject);
    }
}
