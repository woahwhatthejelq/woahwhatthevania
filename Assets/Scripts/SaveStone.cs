using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveStone : MonoBehaviour
{
    private Animator animator;
    bool estaDentro;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            animator.SetBool("SaveBlow", true);
            estaDentro = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.SetBool("SaveBlow", false);
            estaDentro = false;
        }
    }

    private void Update()
    {
        if (estaDentro == true)
        {
            if (Input.GetAxis("Vertical") >= 0.5f)
            {
                GameManager.instance.SaveData(GameManager.instance.gameData.Slot);
                animator.SetBool("SaveBlow", false);
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}
