using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace Arrested_Waters {

    public class PickUpItem : InteractableScript {
        [TextArea]
        public string message;
        public TextMeshProUGUI messageBox;
        public float timer;
        public GameObject enablesOnPickUp;
        SpriteRenderer sprite;

        private void Start() {
            sprite = GetComponent<SpriteRenderer>();
        }


        protected override void Interact() {
            messageBox.gameObject.SetActive(true);
            messageBox.text = message;
            sprite.enabled = false;
            if (enablesOnPickUp != null)
                enablesOnPickUp.SetActive(true);
            StartCoroutine(DisableText());
        }

        IEnumerator DisableText() {
            yield return new WaitForSeconds(timer);
            messageBox.gameObject.SetActive(false);
            GameObject.Destroy(gameObject);
        }
    }
}
