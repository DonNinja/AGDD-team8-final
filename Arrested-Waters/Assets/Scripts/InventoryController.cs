using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryController : MonoBehaviour {
    public static InventoryController instance;

    [SerializeField] TextMeshProUGUI wood_text;
    [SerializeField] TextMeshProUGUI metal_text;
    [SerializeField] TextMeshProUGUI gem_text;
    [SerializeField] TextMeshProUGUI add_text;
    [SerializeField] Animator anim;

    int wood_amt = 0;
    int metal_amt = 0;
    int gem_amt = 0;

    private void Awake() {
        instance = this;
        add_text.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        wood_text.text = wood_amt.ToString();
        metal_text.text = metal_amt.ToString();
        gem_text.text = gem_amt.ToString();
    }

    public void IncreaseInventory(ItemController.Material mat_type) {
        add_text.alpha = 1.0f;

        switch (mat_type) {
            case ItemController.Material.Wood:
                wood_amt += 1;
                add_text.transform.position = wood_text.transform.position;
                break;
            case ItemController.Material.Metal:
                metal_amt += 1;
                add_text.transform.position = metal_text.transform.position;
                break;
            case ItemController.Material.Gems:
                gem_amt += 1;
                add_text.transform.position = gem_text.transform.position;
                break;
            default:
                break;
        }

        anim.SetTrigger("Pickup");
    }

}