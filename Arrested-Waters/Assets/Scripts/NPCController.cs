using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using TMPro;

namespace Arrested_Waters {
    public class NPCController : InteractableScript {
        [Header("Dialogue")]
        public List<string> main_dialogue;
        public List<string> repeating_dialogue;

        [SerializeField] TextMeshProUGUI dialogue_text;

        public float letter_interval_ms;

        float time_counter;
        int repeat_iterator = 0;
        int letter_iterator = 0;
        int next_string_length = 0;
        string next_text = "";

        protected void Start() {
            dialogue_text.text = "";
        }

        private void FixedUpdate() {
            // Check if we're written the entire string
            if (letter_iterator < next_string_length) {
                // Start the counter
                time_counter += Time.deltaTime;
                if (letter_interval_ms >= 0) {
                    if (time_counter > letter_interval_ms / 1000) { // If we've reached the desired time
                        dialogue_text.text += next_text[letter_iterator]; // Add the next letter
                        time_counter = 0; // Reset counter
                        letter_iterator++;
                    }
                } else {
                    // If letter interval is less than 0, display whole text instantly
                    dialogue_text.text = next_text;
                    letter_iterator = next_string_length;
                }
            }
        }

        protected override void Interact() {
            // Start by iterating through main dialogue
            if (main_dialogue.Count > 0) {
                next_text = main_dialogue[0];
                main_dialogue.RemoveAt(0);
            }
            // Then iterate and repeat repeating dialogue
            else {
                if (repeat_iterator == repeating_dialogue.Count) {
                    repeat_iterator = 0;
                }
                next_text = repeating_dialogue[repeat_iterator];
                repeat_iterator++;
            }

            // Set everything to be ready to display
            dialogue_text.text = "";
            next_string_length = next_text.Length;
            letter_iterator = 0;
        }

        protected override void OnTriggerExit2D(Collider2D collision) {
            if (collision.name == "InteractionCollider") {
                base.OnTriggerExit2D(collision);
                dialogue_text.text = ""; // Empty dialogue text
                next_text = ""; // Reset next text
            }
        }
    }
}
