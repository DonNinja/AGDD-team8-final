using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Arrested_Waters {
    //[System.Serializable]
    //public class Upgrade {
    //    public GameObject g_obj;
    //    public bool activate;
    //}

    [System.Serializable]
    public class GameObjList {
        public List<GameObject> game_objects;
    }

    [System.Serializable]
    public class GameObjListList {
        public List<GameObjList> game_obj_lists;
    }

    [System.Serializable]
    public class UpgradeCosts {
        public int wood_costs;
        public int metal_costs;
        public int gem_costs;
        public int gold_costs;
    }

    public class CraftController : InteractableScript {
        [Header("Crafting")]

        [SerializeField] TextMeshProUGUI wood_txt;
        [SerializeField] TextMeshProUGUI metal_txt;
        [SerializeField] TextMeshProUGUI gem_txt;
        [SerializeField] TextMeshProUGUI gold_txt;
        [SerializeField] GameObject requirements;
        [SerializeField] GameObject boat_interaction_box;
        [SerializeField] GameObject sail;
        [SerializeField] GameObject boat;
        [SerializeField] Sprite final_boat;
        [SerializeField] AudioSource craft_sound;
        [SerializeField] GameObjListList upgrade_array;
        [SerializeField] List<UpgradeCosts> upgrade_costs;

        public InventoryController inventoryController;

        EnterBoat eb;
        int wood_req;
        int metal_req;
        int gem_req;
        int gold_req;

        bool is_upgrading;

        void Start() {
            //if (GameObject.Find("Boat"))
            eb = boat.GetComponent<EnterBoat>();

            if (GameObject.Find("UI Canvas"))
                inventoryController = InventoryController.instance;
        }

        protected override void Update() {
            if (interaction_box.activeSelf && is_upgrading) {
                if (Input.GetKeyDown(KeyCode.F)) {
                    Interact();
                }
            }
        }

        protected override void Interact() {
            if (!requirements.activeSelf) {
                requirements.SetActive(true);
                //switch (eb.stage) {
                //    case 0:
                //        wood_req = 5;
                //        metal_req = 2;
                //        gem_req = 0;

                //        break;

                //    case 1:
                //        wood_req = 50;
                //        metal_req = 10;
                //        gem_req = 0;

                //        break;

                //    default:
                //        wood_req = 999;
                //        metal_req = 999;
                //        gem_req = 999;

                //        break;
                //}
                Debug.Log(upgrade_costs.Count);

                wood_req = upgrade_costs[eb.stage].wood_costs;
                metal_req = upgrade_costs[eb.stage].metal_costs;
                gem_req = upgrade_costs[eb.stage].gem_costs;
                gold_req = upgrade_costs[eb.stage].gold_costs;

                wood_txt.text = wood_req.ToString();
                metal_txt.text = metal_req.ToString();
                gem_txt.text = gem_req.ToString();
                gold_txt.text = gold_req.ToString();
            }
            else {
                int available_wood = inventoryController.wood_amt;
                int available_metal = inventoryController.metal_amt;
                int available_gems = inventoryController.gem_amt;
                int available_gold = inventoryController.gold_amt;

                if (available_wood >= wood_req && available_metal >= metal_req && available_gems >= gem_req && available_gold >= gold_req) {
                    if (craft_sound) {
                        craft_sound.Play();
                    }
                    // TODO: Upgrade ship
                    //switch (eb.stage) {
                    //    case 0:
                    //        sail.SetActive(true);

                    //        //upgrade_array.game_obj_lists[0];

                    //        break;

                    //    case 1:
                    //        boat.GetComponent<SpriteRenderer>().sprite = final_boat;

                    //        break;

                    //    default:
                    //        break;
                    //}
                    if (upgrade_array.game_obj_lists.Count != 0) {
                        foreach (GameObject obj in upgrade_array.game_obj_lists[eb.stage].game_objects) {
                            obj.SetActive(true);
                        }
                    }

                    // Update inventory
                    inventoryController.wood_amt -= wood_req;
                    inventoryController.metal_amt -= metal_req;
                    inventoryController.gem_amt -= gem_req;
                    inventoryController.gold_amt -= gold_req;

                    eb.IncreaseStage();
                }

                requirements.SetActive(false);
            }
        }

        protected override void OnTriggerEnter2D(Collider2D collision) {
            base.OnTriggerEnter2D(collision);
            // TODO: Check if has enter_boat script
            //if ()
            if (collision.name == "InteractionCollider") {
                is_upgrading = true;
            }
        }

        protected override void OnTriggerExit2D(Collider2D collision) {
            base.OnTriggerExit2D(collision);

            if (collision.name == "InteractionCollider") {
                requirements.SetActive(false);
                is_upgrading = false;
            }
        }
    }
}