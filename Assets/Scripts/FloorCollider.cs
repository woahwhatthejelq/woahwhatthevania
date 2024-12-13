using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCollider : MonoBehaviour {

    public bool grounded;

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "Floor") {
            grounded = true;
            Debug.Log("Ohhhh");
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Floor") {
            grounded = false;
        }
    }
}
