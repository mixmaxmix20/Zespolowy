using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            IInventory inventory = collision.GetComponent<IInventory>();
            if (inventory != null)
            {
                inventory.Keys = inventory.Keys + 1;
                print("Gracz ma " + inventory.Keys + " kluczy w ekwipunku.");
                gameObject.SetActive(false);
            }

        }
    }
}
