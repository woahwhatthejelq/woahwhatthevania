using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    [SerializeField]
    private Transform cam;

    [SerializeField]
    private float percent;

    private Vector3 previous;

    // Start is called before the first frame update
    void Start() {
        previous = cam.position;
    }

    // Update is called once per frame
    void LateUpdate() {
        Vector3 diff = cam.position - previous;
        transform.Translate(diff * percent);

        previous = cam.position;
    }
}