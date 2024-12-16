using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField]
    private FloorCollider floorCollider;

    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float speed;
    private float horizontal;
    private bool jumping = false;
    public bool isAttacking = false;
    private AnimationManager animationManager;

    public bool grounded = false;
    private bool isJumping = false;
    private bool isSliding = false;

    [SerializeField]
    private int health;
    private int _health;

    [SerializeField]
    private GameObject fireball;

    [SerializeField]
    private Transform spawnPointFireball;

    [SerializeField]
    private float firerate;
    private float fireballTimePass;

    [SerializeField]
    private float manaCost;

    [SerializeField]
    private GameObject deathScreen;

    public bool isHit = false;

    private GameObject fireBreath;

    // Start is called before the first frame update

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        
        animator = GetComponentInChildren<Animator>();

        animationManager = GetComponentInChildren<AnimationManager>();

        fireBreath = transform.GetChild(2).gameObject;

        _health = health;
    }

    // Update is called once per frame
    void Update() {
        grounded = floorCollider.grounded;
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

            if (Input.GetButtonDown("Jump") && grounded) {
                floorCollider.grounded = false;
                grounded = false;
                jumping = false;
                rb.AddForce(Vector2.up * jumpForce);
                animator.SetBool("Jump", true);
            }


            if (Input.GetButtonDown("Fire1") && grounded) {
                animator.SetTrigger("Attack");
                isAttacking = true;
            }

            if (Input.GetButtonDown("Fire2") && grounded) {
                ShootFireball();
            }
        } else {
            jumping = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "EnemyAttack") {
            TakeDamage(1);
        }

        if (collision.tag == "Death") {
            TakeDamage(1000);
        }
    }

    private void FixedUpdate() {
        if (!isAttacking) {
            float groundedModifier = 1;
            if (!grounded) {
                groundedModifier = 0.7f;
            }
            rb.velocity = new Vector2(speed * horizontal * groundedModifier, rb.velocity.y);

            /*if (jumping && grounded) {
                floorCollider.grounded = false;
                grounded = false;
                jumping = false;
                rb.AddForce(Vector2.up * jumpForce);
                animator.SetBool("Jump", true);
            }*/

        } else if (grounded) {
            animator.SetBool("Jump", false);
        }
        if (isAttacking) {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        animator.SetFloat("YVelocity", Mathf.Round(rb.velocity.y*100)/100);
        animator.SetBool("Jump", !grounded);
    }

    public void TakeDamage(float dmg) {
        if (!isHit) {
            Debug.Log("bro took " + dmg + " damage. :skull: Those who know:");

            GameManager.instance.gameData.Life -= dmg;
            rb.AddRelativeForce(Vector2.right);
            isHit = true;
            animator.SetTrigger("Hurt");

            if (GameManager.instance.gameData.Life <= 0f) {
                Death();
            }
        }
    }

    private void Death() {
        Debug.Log("Compiler Error.");
        animator.SetTrigger("Die");
        StartCoroutine(DeathScreen());
    }

    IEnumerator DeathScreen() {
        yield return new WaitForSeconds(0.6f);

        deathScreen.SetActive(true);

        Time.timeScale = 0f;
    }

    private void ShootFireball() {
        if (firerate <= fireballTimePass) {
            GameManager.instance.gameData.Mana -= manaCost;
            GameObject fireballClone = Instantiate(fireball, spawnPointFireball.position, spawnPointFireball.rotation);
            fireballTimePass = 0;
            fireBreath.SetActive(true);
            fireBreath.GetComponent<Animator>().SetTrigger("Fire");
        }
    }
}
