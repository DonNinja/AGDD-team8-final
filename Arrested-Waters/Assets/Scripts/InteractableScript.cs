using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Arrested_Waters {
    public class InteractableScript : MonoBehaviour {
        protected bool can_interact;
        [SerializeField] protected GameObject interaction_box;

        // Update is called once per frame
        void Update() {
            if (can_interact) {
                if (Input.GetKeyDown(KeyCode.E)) {
                    Interact();
                }
            }
        }

        protected virtual void Interact() {
            Debug.Log("Is interacting");
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.name == "InteractionCollider") {
                interaction_box.SetActive(true); // Show the interaction box
                can_interact = true;
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D collision) {
            if (collision.name == "InteractionCollider") {
                interaction_box.SetActive(false); // Hide the interaction box
                can_interact = false;
            }
        }
    }
}
