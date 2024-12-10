using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodModePickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Damageable damageable = collision.GetComponent<Damageable>();
            if (damageable != null)
            {
                // damageable.StartGodMode();
                print("Gracz jest niesmiertelny");
                gameObject.SetActive(false);
            }

        }
    }
}
