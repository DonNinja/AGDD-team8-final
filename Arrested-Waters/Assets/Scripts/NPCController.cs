using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using TMPro;

public class NPCController : MonoBehaviour {
    [Header("Dialogue")]
    public List<string> main_dialogue;
    public List<string> repeating_dialogue;

    [SerializeField] TextMeshProUGUI dialogue_text;
    [SerializeField] GameObject interaction_box;

    public float letter_interval_ms;

    float time_counter;
    int repeat_iterator = 0;
    int letter_iterator = 0;
    int next_string_length = 0;
    bool can_talk = false;
    string next_text = "";

    private void Start() {
        dialogue_text.text = "";
    }

    // Update is called once per frame
    void Update() {
        if (can_talk) {
            if (Input.GetKeyDown(KeyCode.E)) {
                Talk();
            }
        }
    }

    private void FixedUpdate() {
        // Check if we're written the entire string
        if (letter_iterator < next_string_length) {
            // Start the counter
            time_counter += Time.deltaTime;
            if (time_counter > letter_interval_ms / 1000) { // If we've reached the desired time
                dialogue_text.text += next_text[letter_iterator]; // Add the next letter
                time_counter = 0; // Reset counter
                letter_iterator++;
            }
        }
    }

    void Talk() {
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

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name == "InteractionCollider") {
            interaction_box.SetActive(true); // Show the interaction box
            can_talk = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.name == "InteractionCollider") {
            interaction_box.SetActive(false); // Hide the interaction box
            can_talk = false;
            dialogue_text.text = ""; // Empty dialogue text
            next_text = ""; // Reset next text
        }
    }
}
