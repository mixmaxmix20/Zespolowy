using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public Animator animator;
    public bool isOpen = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isOpen)
        {
            animator.SetTrigger("isOpen");
            print("Otwarto skrzynkê");
            isOpen = true;
        }
    }
}
