using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPotionPickup : MonoBehaviour
{
    public float jumpMultiplier = 1.2f;
    //public float duration = 3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.StartJumpBoost(jumpMultiplier);
                print("Potka na jump'a dziala");
                gameObject.SetActive(false);
            }
        }
    }
}
