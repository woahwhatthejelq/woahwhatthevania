using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

    public bool grounded = false;

    [SerializeField]
    private int health;
    private int _health;

    public bool isHit = false;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        
        animator = GetComponentInChildren<Animator>();

        animationManager = GetComponentInChildren<AnimationManager>();

        _health = health;
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

            if (Input.GetButtonDown("Fire1") && grounded) {
                animator.SetTrigger("Attack");
                isAttacking = true;
            }
        } else {
            jumping = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "EnemyAttack") {
            TakeDamage(1);
        }
    }

    private void FixedUpdate() {
        if (!isAttacking) {
            rb.velocity = new Vector2(speed * horizontal, rb.velocity.y);

            if (jumping && grounded) {
                rb.AddForce(Vector2.up * jumpForce);
                animator.SetBool("Jump", true);
            }

        } else if (grounded) {
            animator.SetBool("Jump", false);
        }
        if (isAttacking) {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        animator.SetFloat("YVelocity", rb.velocity.y);
    }

    public void TakeDamage(int dmg) {
        if (!isHit) {
            Debug.Log("bro took " + dmg + " damage. :skull: Those who know:");

            _health -= dmg;
            rb.AddRelativeForce(Vector2.right);
            isHit = true;
            animator.SetTrigger("Hurt");

            if (_health < 1) {
                Death();
            }
        }
    }

    private void Death() {
        Debug.Log("Compiler Error.");
        animator.SetTrigger("Die");
    }
}
