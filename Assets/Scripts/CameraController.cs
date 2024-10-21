using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField]
    private Transform follow;

    [SerializeField]
    private Vector3 camOffset;

    [SerializeField]
    private Vector2 limitX;

    [SerializeField]
    private Vector2 limitY;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        float x = Mathf.Clamp(follow.position.x + camOffset.x, limitX.x, limitX.y);
        float y = Mathf.Clamp(follow.position.y + camOffset.y, limitY.x, limitY.y);
        float z = follow.position.z + camOffset.z;
        transform.position = new Vector3(x, y, z);
    }
}
