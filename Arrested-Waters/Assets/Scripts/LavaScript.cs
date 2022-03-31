using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaScript : MonoBehaviour
{
    public PlayerStats PC;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PC.StartBurn(10);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PC.StopBurn();
    }
}
