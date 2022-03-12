using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCController : MonoBehaviour {
    [Header("Dialogue")]
    public List<string> main_dialogue;
    public List<string> repeating_dialogue;

    [SerializeField] TextMeshProUGUI dialogue_text;
    [SerializeField] GameObject interaction_box;

    int repeat_iterator = 0;
    bool can_talk = false;

    private void Start() {
        dialogue_text.text = "";
    }

    // Update is called once per frame
    void Update() {
        if (can_talk) {
            interaction_box.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E)) {
                Talk();
            }
        }
        else {
            interaction_box.SetActive(false);
        }
    }

    void Talk() {
        string next_text;
        if (main_dialogue.Count > 0) {
            next_text = main_dialogue[0];
            main_dialogue.RemoveAt(0);
        }
        else {
            if (repeat_iterator == repeating_dialogue.Count) {
                repeat_iterator = 0;
            }
            next_text = repeating_dialogue[repeat_iterator];
            repeat_iterator++;
        }

        dialogue_text.text = next_text;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name == "InteractionCollider") {
            can_talk = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.name == "InteractionCollider") {
            can_talk = false;
        }
    }
}
