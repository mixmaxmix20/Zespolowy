using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coinValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            IInventory inventory = collision.GetComponent<IInventory>();
            if (inventory != null) { 
                inventory.Money = inventory.Money + coinValue;
                //print("Gracz ma " + inventory.Money + " monet w ekwipunku.");
                gameObject.SetActive(false);
            }
            
        }
    }
}