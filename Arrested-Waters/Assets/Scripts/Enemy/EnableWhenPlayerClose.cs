using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnableWhenPlayerClose : MonoBehaviour
{

    public EnemyAI ai;
    public EnemySight sight;
    public AIPath path;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ai.enabled = true;
        sight.enabled = true;
        path.enabled = true;
    }

}
