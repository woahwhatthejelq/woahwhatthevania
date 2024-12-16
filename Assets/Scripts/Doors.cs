using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// the doors jim morrison no way
public class Doors : MonoBehaviour {

    [SerializeField]
    private int sceneNumber;
    [SerializeField]
    private Vector3 pos;

    private Animator animator;

    private bool isPlayerIn = false;

    void Start() {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            isPlayerIn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") {
            isPlayerIn = false;
        }
    }

    private void Update() {
        if (isPlayerIn && Input.GetKeyDown(KeyCode.E)) {
            StartCoroutine(GoThrough());
        }
    }


    IEnumerator GoThrough() {
        animator.SetTrigger("Open");

        yield return new WaitForSeconds(0.6f);

        GameObject.Find("Player").transform.position = pos;

        SceneManager.LoadScene(sceneNumber);

        Debug.Log("cock");
    }
}