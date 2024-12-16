using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour {

    private Player player;

    private void Start() {
        player = GetComponentInParent<Player>();
    }

    public void FinishAttack() {
        player.isAttacking = false;
        Debug.Log("Finish Attack");
    }

    public void BroIsntHitAnymore() {
        player.isHit = false;
        player.isAttacking = false;
        Debug.Log("Bro Isnt Hit Anymore");
    }
}
