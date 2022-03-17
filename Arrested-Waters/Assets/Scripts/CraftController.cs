using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arrested_Waters {
    public class CraftController : InteractableScript {
        [Header("Crafting")]
        public InventoryController inventoryController;

        // Start is called before the first frame update
        void Start() {
            inventoryController = InventoryController.instance;
        }

        protected override void Interact() {
            
        }
    }
}