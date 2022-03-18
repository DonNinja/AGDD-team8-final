using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Arrested_Waters {

    public class EnterBoat : InteractableScript
    {
        BoatController boat;
        PlayerController player;
        public CameraController boatCamera;

        bool onBoat = false;

        private void Start()
        {
            boat = GameManager.instance.boat;
            player = GameManager.instance.player;
        }

        void FixedUpdate()
        {
            if (onBoat)
            {
                player.transform.position = boat.playerSeat.transform.position;
            }
        }

        protected override void Interact()
        {
            if (!onBoat)
                EnterBoatFunc();
            else
                ExitBoatFunc();
        }

        public void EnterBoatFunc()
        {
            Debug.Log("Enter");
            GameManager.instance.mainCamera.target = boat.transform;
            boat.enabled = true;
            boatCamera.enabled = true;
            player.onBoat = true;
            player.enabled = false;
            player.transform.position = boat.playerSeat.transform.position;
            onBoat = true;        }
        public void ExitBoatFunc()
        {
            Debug.Log("Exit");
            GameManager.instance.mainCamera.target = player.transform;
            boat.enabled = false;
            boatCamera.enabled = false;
            player.enabled = true;
            player.transform.position = boat.playerSeat.transform.position;
            onBoat = false;
        }


        protected override void OnTriggerExit2D(Collider2D col)
        {
            base.OnTriggerExit2D(col);
            player.onBoat = false;
            Debug.Log("Left Boat");
        }
    }
}
