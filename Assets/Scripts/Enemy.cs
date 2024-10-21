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

    private Rigidbody2D rb;
    private Transform player;


    void Start() {
        player = GameObject.Find("Player").GetComponent<Transform>();

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate() {
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
