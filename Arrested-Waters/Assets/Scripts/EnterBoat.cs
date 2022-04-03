using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Arrested_Waters {

    public class EnterBoat : InteractableScript {
        public static EnterBoat instance;

        public BoatController boat;
        PlayerController player;

        [Header("Boat")]
        public CameraController boatCamera;
        public int stage = 0;

        bool onBoat = false;
        bool entering_boat = false;
        private void Awake() {
            if (!instance) {
                instance = this;
            }
        }

        private void Start() {
            player = GameManager.instance.player;
        }

        void FixedUpdate() {
            if (onBoat) {
                player.transform.position = boat.playerSeat.transform.position;
            }
        }

        protected override void Interact() {
            if (entering_boat) {
                if (!onBoat)
                    EnterBoatFunc();
                else
                    ExitBoatFunc();

                interaction_text.text = what_do;
            }
        }

        public void EnterBoatFunc() {
            Debug.Log("Enter");
            what_do = "Exit";
            GameManager.instance.mainCamera.target = boat.transform;
            player.boat = boat;
            GameManager.instance.SetBoat(boat);
            boat.enabled = true;
            boatCamera.enabled = true;
            player.onBoat = true;
            player.enabled = false;
            player.transform.position = boat.playerSeat.transform.position;
            onBoat = true;
        }
        public void ExitBoatFunc() {
            Debug.Log("Exit");
            what_do = "Enter";
            GameManager.instance.mainCamera.target = player.transform;
            boat.enabled = false;
            boatCamera.enabled = false;
            player.enabled = true;
            player.transform.position = boat.playerSeat.transform.position;
            onBoat = false;
        }

        public void IncreaseStage() {
            Debug.Log("UPGRADING SHIP");
            stage++;
            if (stage > 0) {
                interaction_box.SetActive(true);
            }
        }

        protected override void OnTriggerEnter2D(Collider2D collision) {
            base.OnTriggerEnter2D(collision);
            if (stage == 0) {
                interaction_box.SetActive(false);
            }
            if (collision.name == "InteractionCollider")
                entering_boat = true;
            if (collision.name == "Player") {
                player.onBoat = true;
            }
        }


        protected override void OnTriggerExit2D(Collider2D col) {
            base.OnTriggerExit2D(col);
            if (col.name == "InteractionCollider")
                entering_boat = false;
            if (col.name == "Player") {
                player.onBoat = false;
                Debug.Log("Left Boat");
            }
        }
    }
}
