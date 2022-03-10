using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour {
    public InventoryController inv_cont;

    public enum Material { Wood, Metal, Gems };

    public Material mat_type;

    // Start is called before the first frame update
    void Start() {
        inv_cont = InventoryController.instance;
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        inv_cont.IncreaseInventory(mat_type);
        Destroy(gameObject);
        Destroy(this);
    }
}
