using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

namespace Arrested_Waters {
    public class InteractableScript : MonoBehaviour {
        protected bool can_interact;
        public string what_do;
        [SerializeField] protected GameObject interaction_box;
        [SerializeField] protected TextMeshProUGUI interaction_text;
        protected bool is_interacting;

        // Update is called once per frame
        protected virtual void Update() {
            if (interaction_box.activeSelf && is_interacting) {
                if (Input.GetKeyDown(KeyCode.E)) {
                    Interact();
                }
            }
        }

        protected virtual void Interact() {
            Debug.Log("Is interacting");
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision) {
            if (!interaction_box.activeSelf && collision.name == "InteractionCollider") {
                interaction_box.SetActive(true); // Show the interaction box
                is_interacting = true;
                interaction_text.text = what_do;
            }
        }

        protected virtual void OnTriggerExit2D(Collider2D collision) {
            if (collision.name == "InteractionCollider") {
                interaction_box.SetActive(false); // Hide the interaction box
                is_interacting = false;
            }
        }
    }
}
