using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    public IInventory playerInventory;
    public TMP_Text coinsText;
    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerInventory = player.GetComponent<IInventory>();
        }
        else
        {
            print("No inventory found");
        }
    }
    void Update()
    {
        coinsText.text = "Coins: " + playerInventory.Money.ToString();
    }
}
