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
        GameObject player_obj;

        float time_counter;
        public int repeat_iterator = 0;
        int letter_iterator = 0;
        int next_string_length = 0;
        string next_text = "";
        bool done_talking = false;

        protected void Start() {
            dialogue_text.text = "";
            player_obj = GameObject.Find("Player");
        }

        private void FixedUpdate() {
            // Check if we're written the entire string
            if (letter_iterator < next_string_length) {
                // Start the counter
                time_counter += Time.deltaTime;
                if (letter_interval_ms >= 0) {
                    if (time_counter > letter_interval_ms / 1000) { // If we've reached the desired time
                        dialogue_text.text += next_text[letter_iterator]; // Add the next letter
                        time_counter = 0; // Reset timer
                        letter_iterator++;
                    }
                }
                else {
                    // If letter interval is less than 0, display whole text instantly
                    dialogue_text.text = next_text;
                    letter_iterator = next_string_length;
                }
            }
            else if (next_string_length != 0) {
                done_talking = true;
            }
            if (done_talking) {
                if (main_dialogue.Count > 0) {
                    main_dialogue.RemoveAt(0);
                }
                else {
                    if (repeat_iterator == repeating_dialogue.Count - 1) {
                        repeat_iterator = 0;
                    }
                    else {
                        repeat_iterator++;
                    }
                }
                done_talking = false;
                next_string_length = 0;
            }
        }

        protected override void Interact() {
            time_counter = 0; // Reset timer

            // Start by iterating through main dialogue
            if (main_dialogue.Count > 0) {
                next_text = main_dialogue[0];
            }
            // Then iterate and repeat repeating dialogue
            else {
                next_text = repeating_dialogue[repeat_iterator];
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
                letter_iterator = 0;
                next_string_length = 0;
            }
        }

        protected override void OnTriggerEnter2D(Collider2D collision) {
            base.OnTriggerEnter2D(collision);
            //if (collision.name == "InteractionCollider" && !is_talking) {
            //    is_talking = true;
            //}
        }

        private void OnTriggerStay2D(Collider2D collision) {
            if (collision.name == "InteractionCollider" && is_interacting) {
                Vector3 _direction = (player_obj.transform.position - transform.position).normalized;
                _direction.z = 0; // Reset z depth

                Quaternion _rotation = Quaternion.FromToRotation(Vector3.up, _direction);

                transform.rotation = Quaternion.Slerp(transform.rotation, _rotation, Time.deltaTime * 5); // Rotate towards player slowly
            }
        }
    }
}
