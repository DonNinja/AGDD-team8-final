using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryController : MonoBehaviour {
    public static InventoryController instance;

    [SerializeField] TextMeshProUGUI wood_text;
    [SerializeField] TextMeshProUGUI metal_text;
    [SerializeField] TextMeshProUGUI gem_text;
    [SerializeField] TextMeshProUGUI gold_text;
    [SerializeField] TextMeshProUGUI add_text;
    [SerializeField] Animator anim;

    public int wood_amt = 0;
    public int metal_amt = 0;
    public int gem_amt = 0;
    public int gold_amt = 0;

    private void Awake() {
        if (!instance)
            instance = this;
        add_text.enabled = false;
    }

    // Update is called once per frame
    void Update() {
    }

    public void IncreaseInventory(CollectableController.Material mat_type, int amount=1) {
        add_text.alpha = 1.0f;

        switch (mat_type) {
            case CollectableController.Material.Wood:
                wood_amt += amount;
                add_text.transform.position = wood_text.transform.position;
                wood_text.text = wood_amt.ToString();
                break;
            case CollectableController.Material.Metal:
                metal_amt += amount;
                add_text.transform.position = metal_text.transform.position;
                metal_text.text = metal_amt.ToString();
                break;
            case CollectableController.Material.Gems:
                gem_amt += amount;
                add_text.transform.position = gem_text.transform.position;
                gem_text.text = gem_amt.ToString();
                break;
            case CollectableController.Material.Gold:
                gold_amt += amount;
                add_text.transform.position = gold_text.transform.position;
                gold_text.text = gold_amt.ToString();
                break;
            default:
                break;
        }

        anim.SetTrigger("Pickup");
    }

}
