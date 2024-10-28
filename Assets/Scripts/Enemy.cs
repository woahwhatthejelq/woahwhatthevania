using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField]
    private float speed;
    [SerializeField]
    private float attackRadius;
    [SerializeField]
    private float detectRadius;
    [SerializeField]
    private float health;

    private bool isHit = false;

    private float _health;

    private Rigidbody2D rb;
    private Transform player;
    private Animator animator;

    private bool playerDetected = false;

    private bool isAttacking = false;


    void Start() {
        player = GameObject.Find("Player").GetComponent<Transform>();

        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();

        _health = health;
    }

    // Update is called once per frame
    void FixedUpdate() {
        Vector3 vectorDistance = player.position - transform.position;
        Vector3 direction = vectorDistance.normalized;

        float moduleDistance = vectorDistance.magnitude;

        if (moduleDistance < detectRadius) {
            playerDetected = true;
            animator.SetBool("Walking", true);
        } else {
            playerDetected = false;
            rb.velocity = new Vector2(0, rb.velocity.y);
            animator.SetBool("Walking", false);
        }
        if (playerDetected && !isHit && !isAttacking) {
            if (vectorDistance.x > 0) {
                transform.eulerAngles = new Vector3(0, 180, 0);
            } else {
                transform.eulerAngles = Vector3.zero;
            }

            if (attackRadius < moduleDistance) {
                rb.velocity = new Vector2(speed * direction.x, rb.velocity.y);
            } else {
                rb.velocity = new Vector2(0, rb.velocity.y);
                animator.SetBool("Walking", false);
                Attack();
            }
        }
    }

    private void Attack() {
        //Bro attacked.
        animator.SetTrigger("Attack");
        isAttacking = true;
    }

    public void StopAttacking() {
        isAttacking = false;
    }

    public void TakeDamage(float dmg) {
        if (!isHit) {
            Debug.Log("bro took " + dmg + " damage. :skull: Those who know:");

            _health -= dmg;
            rb.AddRelativeForce(Vector2.right);
            isHit = true;
            Invoke("NoHit", 0.3f);
            animator.SetTrigger("Hit");

            if (_health < 1) {
                animator.SetTrigger("Die");
            }
        }
    }

    private void NoHit() {
        isHit = false;
    }

    public void Death() {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "PlayerAttack") {
            TakeDamage(1f);
        }
    }
}
