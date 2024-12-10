using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purchasable : MonoBehaviour
{
    public int coinPrice = 15;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            IInventory inventory = collision.GetComponent<IInventory>();
            if (inventory != null)
            {
                inventory.Buy(coinPrice);
            }

        }
    }
}
