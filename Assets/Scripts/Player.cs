using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour {

    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField]
    private float speed;
    [SerializeField]
    private float horizontal;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private bool jumping = false;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update() {
        horizontal = 0f;
        if (Input.GetKey(KeyCode.D)) {
            horizontal = 1f;
        }
        if (Input.GetKey(KeyCode.A)) {
            horizontal = -1f;
        }

        if (horizontal > 0) {
            transform.eulerAngles = Vector3.zero;
            animator.SetBool("Run", true);
        } else if (horizontal < 0) {
            transform.eulerAngles = new Vector3(0, 180, 0);
            animator.SetBool("Run", true);
        } else {
            animator.SetBool("Run", false);
        }

        jumping = Input.GetKey(KeyCode.W);
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(speed * horizontal, rb.velocity.y);

        if (jumping && rb.velocity.y == 0) {
            rb.AddForce(Vector2.up * jumpForce);
            animator.SetBool("Jump", true);
        } else if ( rb.velocity.y == 0) {
            animator.SetBool("Jump", false);
        }
    }
}
