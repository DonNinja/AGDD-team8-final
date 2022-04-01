using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


namespace Arrested_Waters
{

    public class DeathController : MonoBehaviour
    {
        public Collider2D MapBounds;
        public PlayerController Player;

        public GameObject RespawnPos;
        public GameObject BoatRespawnPos;
        public Collider2D StarterBoat;
        public EnterBoat EscapeBoat;
        private Collider2D EscapeBoatCollider;

        public TextMeshProUGUI CannotEscapeNotif;
        public TextMeshProUGUI BiggerBoatNotif;

        private Vector3 BoatInitPos;
        private Vector3 PlayerInitPos;

        private void Start()
        {
            CannotEscapeNotif.gameObject.SetActive(false);
            BiggerBoatNotif.gameObject.SetActive(false);
            EscapeBoatCollider = EscapeBoat.gameObject.GetComponent<Collider2D>();
        }

        private void Update()
        {
            if (GameManager.instance.isDead)
            {
                GameManager.instance.OnDeath();
            }
        }

        private void Respawn()
        {
            GameManager.instance.boat.transform.position = BoatRespawnPos.transform.position;
            Player.transform.position = RespawnPos.transform.position;
            //GameManager.instance.boat.gameObject.GetComponent<EnterBoat>().ExitBoatFunc();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision == EscapeBoatCollider && EscapeBoat.stage == 2)
            {
                GameManager.instance.EndGame();
            }
            else if(collision == StarterBoat)
            {
                Respawn();
                StartCoroutine(Notification(true));
            }
            else
            {
                Respawn();
                StartCoroutine(Notification(false));
            }
        }

        private IEnumerator Notification(bool needsBiggerBoat)
        {
            CannotEscapeNotif.gameObject.SetActive(true);
            if (needsBiggerBoat)
            {
                BiggerBoatNotif.gameObject.SetActive(true);
            }

            yield return new WaitForSeconds(8);

            CannotEscapeNotif.gameObject.SetActive(false);
            BiggerBoatNotif.gameObject.SetActive(false);

        }
    }
}
