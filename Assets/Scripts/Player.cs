using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour {

    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float speed;
    private float horizontal;
    private bool jumping = false;
    public bool isAttacking = false;
    private AnimationManager animationManager;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        
        animator = GetComponentInChildren<Animator>();

        animationManager = GetComponentInChildren<AnimationManager>();
    }

    // Update is called once per frame
    void Update() {
        horizontal = 0f;
        if (!isAttacking) {
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

            jumping = Input.GetButton("Jump");

            if (Input.GetButtonDown("Fire1")) {
                animator.SetTrigger("Attack");
                isAttacking = true;
                Invoke("Continue", animator.playbackTime);
            }
        } else {
            horizontal = 0f;
            jumping = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        
    }

    private void Continue() {
        animationManager.FinishAttack();
    }

    private void FixedUpdate() {
        if (!isAttacking) {
            rb.velocity = new Vector2(speed * horizontal, rb.velocity.y);

            if (jumping && rb.velocity.y == 0) {
                rb.AddForce(Vector2.up * jumpForce);
                animator.SetBool("Jump", true);
            }

        } else if (rb.velocity.y == 0) {
            animator.SetBool("Jump", false);
        }
    }
}
