using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopButton : MonoBehaviour
{
    public GameObject player;
    public int coinPrice = 10;
    public void OnClick()
    {
        player = GameObject.FindWithTag("Player");
        IInventory inventory = player.GetComponent<IInventory>();
        if (inventory != null)
        {
            bool result = inventory.Buy(coinPrice);
            if (result) {
                print("zakupiono przedmiot");
                inventory.Keys += 1;
            }
        }
    }
}
