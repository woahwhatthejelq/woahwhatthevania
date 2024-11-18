using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : MonoBehaviour {
    private enum bossStates {Idle, Walk, Roar};

    private bossStates state;
    private Animator animator;

    private bool waiting;

    private Transform player;

    private Rigidbody2D rb;

    [SerializeField]
    private float stoppingDistance;
    [SerializeField]
    private float speed;
    [SerializeField]
    private Transform roarSpawnPoint;
    [SerializeField]
    private GameObject roarPrefab;
    [SerializeField]
    private float roarProjectileSpeed;
    [SerializeField]
    private float roarCooldown;

    private void Start() {
        ChangeState(bossStates.Idle);

        state = bossStates.Idle;
        animator = GetComponent<Animator>();
        waiting = true;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Start is called before the first frame update
    void ChangeState(bossStates _state) {
        switch (_state) { 
            case bossStates.Idle:
                StartCoroutine(Idle());
                break;

            case bossStates.Walk:
                StartCoroutine(Walk());
                break;

            case bossStates.Roar:
                StartCoroutine(Roar());
                break;
        }
    }

    IEnumerator Idle() {
        while (waiting) {
            yield return null;
        }

        ChangeState(bossStates.Walk);
    }

    IEnumerator Walk() {
        Vector2 distanceVector = player.position - transform.position;
        Vector2 direction = distanceVector.normalized;
        float distance = distanceVector.magnitude;
        while (distance > stoppingDistance) {
            rb.velocity = new Vector2(speed * direction.x, 0);
            distanceVector = player.position - transform.position;
            distanceVector = distanceVector.normalized;
            distance = distanceVector.magnitude;
            yield return null;
        }

        ChangeState(bossStates.Idle);
    }

    IEnumerator Roar() {
        animator.SetTrigger("Roar");
        yield return WaitForSeconds(roarCooldown);
    }

    public void ShootRoarProjectile() {
        GameObject clone = Instantiate(roarPrefab, roarSpawnPoint.position, roarSpawnPoint.rotation);
        clone.GetComponent<Rigidbody2D>().AddForce(clone.transform.right * -1 * roarProjectileSpeed);
    }
}
