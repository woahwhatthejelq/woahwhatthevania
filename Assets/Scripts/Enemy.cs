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

    private bool playerDetected = false;


    void Start() {
        player = GameObject.Find("Player").GetComponent<Transform>();

        rb = GetComponent<Rigidbody2D>();

        _health = health;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (playerDetected && !isHit) {
            Vector3 vectorDistance = player.position - transform.position;
            Vector3 direction = vectorDistance.normalized;

            float moduleDistance = vectorDistance.magnitude;

            if (vectorDistance.x > 0) {
                transform.eulerAngles = new Vector3(0, 180, 0);
            } else {
                transform.eulerAngles = Vector3.zero;
            }

            if (attackRadius < moduleDistance) {
                rb.velocity = new Vector2(speed * direction.x, rb.velocity.y);
            } else {
                rb.velocity = new Vector2(0, rb.velocity.y);
                //attack
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            playerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") {
            playerDetected = false;

            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    public void TakeDamage(float dmg) {
        if (!isHit) {
            Debug.Log("bro took " + dmg + " damage. :skull: Those who know:");

            _health -= dmg;
            rb.AddRelativeForce(Vector2.right);
            isHit = true;
            Invoke("NoHit", 0.3f);

            if (_health < 0) {
                Death();
            }
        }
    }

    private void NoHit() {
        isHit = false;
    }

    public void Death() {
        Destroy(gameObject);
    }
}
