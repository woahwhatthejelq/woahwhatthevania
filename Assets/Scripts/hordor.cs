using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hordor : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log("colider");
        if (collision.gameObject.tag == "Player") {
            GameManager.instance.gameData.Life += 10;
            Destroy(gameObject);
        }
    }
}
