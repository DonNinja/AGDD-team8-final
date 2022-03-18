using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arrested_Waters
{

    public class DeathController : MonoBehaviour
    {
        public Collider2D MapBounds;
        public EnterBoat boat;
        public PlayerController player;

        private Vector3 boatInitPos;
        private Vector3 playerInitPos;

        private void Start()
        {
            boatInitPos = boat.transform.position;
            playerInitPos = player.transform.position;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                boat.transform.position = boatInitPos;
                player.transform.position = playerInitPos;
                boat.ExitBoatFunc();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision = MapBounds)
            {
                if (boat.stage == 2)
                {
                    Application.Quit();
                    //GameManager.instance.EndGame();
                }
                else
                {
                    boat.transform.position = boatInitPos;
                    player.transform.position = playerInitPos;
                    boat.ExitBoatFunc();
                }
            }
        }
    }
}
