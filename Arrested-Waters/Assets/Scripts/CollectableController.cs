using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour {
    InventoryController inv_cont;
    public enum Material { Wood, Metal, Gems };
    public string player_name;

    [SerializeField] LayerMask wall;
    public Material mat_type;
    bool is_picked_up = false;
    float trigger_rad;

    // Start is called before the first frame update
    void Start() {
        inv_cont = InventoryController.instance;
        trigger_rad = gameObject.GetComponent<CircleCollider2D>().radius;
    }

    // Update is called once per frame
    void Update() {

    }

    private bool HasDirectLine(Transform from, Transform to) {
        Vector3 ray_start = from.position;
        Vector3 ray_end = to.position;

        Vector3 direction = ray_end - ray_start;
        float dist = Vector3.Distance(ray_start, ray_end);

        // Check a raycast between the 2 places
        RaycastHit2D raycast_hit = Physics2D.Raycast(ray_start, direction, dist, wall.value);

        // Check if raycast hits something
        bool did_hit = raycast_hit.collider == null;

        Color ray_c = did_hit ? new Color(0, 1, 0) : new Color(1, 0, 0);

        Debug.DrawRay(ray_start, direction, ray_c);

        return did_hit;
    }

    private void OnTriggerStay2D(Collider2D collision) {
        GameObject other = collision.gameObject;

        // Check if:
        //      1. the player is within range,
        //      2. it hasn't been picked up yes (to prevent it getting picked up twice)
        //      3. that the item has a direct line to the player
        if (collision.gameObject.name == player_name && !is_picked_up && HasDirectLine(gameObject.transform, other.transform)) {
            // Float toward the player
            Vector3 dir_vec = (other.transform.position - gameObject.transform.position).normalized;

            float dist = Vector2.Distance(other.transform.position, gameObject.transform.position);

            gameObject.transform.position += dir_vec * (trigger_rad - dist) * Time.deltaTime; // Move towards playr at speed
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == player_name && !is_picked_up) {
            is_picked_up = true;
            inv_cont.IncreaseInventory(mat_type);
            Destroy(gameObject);
            Destroy(this);
        }
    }
}
