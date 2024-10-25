using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCollider : MonoBehaviour {

    private Player player;

    // Start is called before the first frame update
    void Start() {
        player = GetComponentInParent<Player>();
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "Floor") {
            player.grounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Floor") {
            player.grounded = false;
        }
    }
}
