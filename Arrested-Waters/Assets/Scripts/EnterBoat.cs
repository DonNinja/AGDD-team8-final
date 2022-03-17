using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Arrested_Waters {

    public class EnterBoat : InteractableScript
    {
        BoatController boat;
        PlayerController player;
        bool onBoat;

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
            if (onBoat)
                EnterBoatFunc();
            else
                ExitBoatFunc();
        }

        public void EnterBoatFunc()
        {
            GameManager.instance.mainCamera.target = boat.transform;
            GameManager.instance.mainCamera.GetComponent<Animator>().SetTrigger("zoomOut");
            boat.enabled = true;
            player.enabled = false;
            player.transform.position = boat.playerSeat.transform.position;
            onBoat = true;

        }
        public void ExitBoatFunc()
        {
            GameManager.instance.mainCamera.target = boat.transform;
            GameManager.instance.mainCamera.GetComponent<Animator>().SetTrigger("zoomOut");
            boat.enabled = true;
            player.enabled = false;
            player.transform.position = boat.playerSeat.transform.position;
            onBoat = true;

        }

    }
}
