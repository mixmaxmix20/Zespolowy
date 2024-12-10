using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPotionPickup : MonoBehaviour
{
    public float speedMultiplier = 2f;
    //public float duration = 3f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.StartSpeedBoost(speedMultiplier);
                print("Potka na speed'a dziala");
                gameObject.SetActive(false);
            }
        }
    }

}
