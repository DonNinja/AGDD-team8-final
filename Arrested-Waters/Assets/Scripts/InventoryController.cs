using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryController : MonoBehaviour {
    public static InventoryController instance;

    [SerializeField] TextMeshProUGUI wood_text;
    int wood_amt = 0;

    private void Awake() {
        instance = this;
    }

    // Update is called once per frame
    void Update() {
        wood_text.text = wood_amt.ToString();
    }

    public void IncreaseInventory(ItemController.Material mat_type) {
        if (mat_type == ItemController.Material.Wood) {
            wood_amt++;
        }
    }

}
