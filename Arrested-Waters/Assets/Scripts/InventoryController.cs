using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryController : MonoBehaviour {
    public static InventoryController instance;

    [SerializeField] TextMeshProUGUI wood_text;
    [SerializeField] TextMeshProUGUI metal_text;
    [SerializeField] TextMeshProUGUI gem_text;
    int wood_amt = 0;
    int metal_amt = 0;
    int gem_amt = 0;

    private void Awake() {
        instance = this;
    }

    // Update is called once per frame
    void Update() {
        wood_text.text = wood_amt.ToString();
        metal_text.text = metal_amt.ToString();
        gem_text.text = gem_amt.ToString();
    }

    public void IncreaseInventory(ItemController.Material mat_type) {
        switch (mat_type) {
            case ItemController.Material.Wood:
                wood_amt++;
                break;
            case ItemController.Material.Metal:
                metal_amt++;
                break;
            case ItemController.Material.Gems:
                gem_amt++;
                break;
            default:
                break;
        }
    }

}
