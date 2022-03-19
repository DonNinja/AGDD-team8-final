using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySight : MonoBehaviour
{

    public GameObject watchingTarget;
    public EnemyAI ai;
    public LayerMask IgnoreMask;
    public Transform visionDirection;
    public float hearingDistance;
    public float sightRadius;

    private bool inRange;
    private float angle;

    public void Start()
    {
        ai = gameObject.GetComponent<EnemyAI>();
        if (!watchingTarget)
            watchingTarget = GameManager.instance.player.gameObject;
    }

    public void FixedUpdate()
    {
        if (inRange)
        {
            if ((transform.position - watchingTarget.transform.position).magnitude < hearingDistance)
            {
                ai.SpottPlayer(watchingTarget);
            }
            RaycastHit2D hit = Physics2D.Raycast(transform.position, watchingTarget.transform.position - transform.position, 1000f, ~IgnoreMask);
            Debug.DrawRay(transform.position, watchingTarget.transform.position - transform.position, Color.green);
            if (hit)
            {
                angle = Vector2.Angle((watchingTarget.transform.position - transform.position), (visionDirection.position - transform.position));
                if (hit.collider.gameObject == watchingTarget && angle <= sightRadius)
                {
                    ai.SpottPlayer(watchingTarget);
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == watchingTarget)
        {
            inRange = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == watchingTarget)
        {
            inRange = false;
        }
    }
}
