using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {
    InventoryController inv_cont;
    public enum Material { Wood, Metal, Gems };
    public string player_name;

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

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.name == player_name && !is_picked_up) {
            GameObject other = collision.gameObject;
            Vector3 dir_vec = (other.transform.position - gameObject.transform.position).normalized;

            float dist = Vector2.Distance(other.transform.position, gameObject.transform.position);

            gameObject.transform.position += dir_vec * (trigger_rad - dist) * Time.deltaTime;
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
