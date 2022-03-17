using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Arrested_Waters {

    public class BoatManager : InteractableScript
    {
        BoatController boat;
        PlayerController player;
        BoatCameraController boatCamera;

        public float minZoom = 1f;
        public float maxZoom = 5f;
        public float zoomStep = 0.1f;
        public float smoothness = 0.1f;

        bool onBoat = false;
        bool inBoat = false;

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

        private void LateUpdate()
        {
            if (onBoat)
                StartCoroutine(ZoomOutSmoothly());
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
            player.enabled = false;
            player.transform.position = boat.playerSeat.transform.position;
            onBoat = true;

        }
        public void ExitBoatFunc()
        {
            Debug.Log("Exit");
            GameManager.instance.mainCamera.target = player.transform;
            boat.enabled = false;
            boatCamera.enabled = false;
            player.enabled = true;
            onBoat = false;

        }

        private IEnumerator ZoomOutSmoothly()
        {
            WaitForSeconds myWait = new WaitForSeconds(smoothness);
            Camera.main.orthographicSize = minZoom; //you could also comment this line out and just use your current zoomValue... It's just for having a start position for the zoomanimation
            while (Camera.main.orthographicSize < maxZoom)
            {
                Camera.main.orthographicSize += zoomStep * boat.speedKPH; //take your players one for the velocity  
                yield return myWait;
            }
            Camera.main.orthographicSize = maxZoom; //ensure it is exactly set to the maxZoom value after the animation
        }
    }
}
