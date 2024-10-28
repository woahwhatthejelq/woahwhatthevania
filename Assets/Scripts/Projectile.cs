using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField]
    public Enemy parent;
    [SerializeField]
    private float damage;
    [SerializeField]
    private Vector2 startVel;

    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = startVel;
    }

    void Update() {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x);

        angle *= 180/Mathf.PI;

        transform.eulerAngles = new Vector3(0, 0, angle+180);
    }
}
