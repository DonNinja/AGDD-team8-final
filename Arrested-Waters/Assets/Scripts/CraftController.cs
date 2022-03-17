using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Arrested_Waters {
    public class CraftController : InteractableScript {
        [Header("Crafting")]
        [SerializeField] int wood_req;
        [SerializeField] int metal_req;
        [SerializeField] int gem_req;

        [SerializeField] TextMeshProUGUI wood_txt;
        [SerializeField] TextMeshProUGUI metal_txt;
        [SerializeField] TextMeshProUGUI gem_txt;
        [SerializeField] GameObject requirements;

        InventoryController inventoryController;

        int stage = 0;

        bool has_entered;

        // Start is called before the first frame update
        void Start() {
            inventoryController = InventoryController.instance;
        }

        protected override void Interact() {
            if (!requirements.activeSelf) {
                requirements.SetActive(true);
                switch (stage) {
                    case 0:
                        wood_req = 5;
                        metal_req = 2;
                        gem_req = 1;

                        break;

                    case 1:
                        wood_req = 50;
                        metal_req = 10;
                        gem_req = 5;

                        break;

                    default:
                        wood_req = 99;
                        metal_req = 99;
                        gem_req = 99;

                        break;
                }
                wood_txt.text = wood_req.ToString();
                metal_txt.text = metal_req.ToString();
                gem_txt.text = gem_req.ToString();
            }
            else {
                int available_wood = inventoryController.wood_amt;
                int available_metal = inventoryController.metal_amt;
                int available_gems = inventoryController.gem_amt;
                if (available_wood >= wood_req && available_metal >= metal_req && available_gems >= gem_req) {
                    // TODO: Upgrade ship
                    switch (stage) {
                        case 0:
                            break;

                        default:
                            break;
                    }

                    // Update inventory
                    inventoryController.wood_amt -= wood_req;
                    inventoryController.metal_amt -= metal_req;
                    inventoryController.gem_amt -= gem_req;
                    Debug.Log("UPGRADING SHIP");
                    stage++;
                }
                else {

                }

                requirements.SetActive(false);
            }
        }

        protected override void OnTriggerExit2D(Collider2D collision) {
            base.OnTriggerExit2D(collision);

            if (collision.name == "InteractionCollider") {
                requirements.SetActive(false);
            }
        }
    }
}