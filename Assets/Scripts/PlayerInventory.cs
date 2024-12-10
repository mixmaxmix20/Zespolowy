using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, IInventory
{
    public int money = 0;
    public int keys = 0;
    public int Money { get => money; set => money = value; }
    public int Keys { get => keys; set => keys = value; }
    
    public bool Buy(int coinPrice)
    {
        if (Money >= coinPrice)
        {
            Money -= coinPrice;
            return true;
        }
        else 
        { 
            return false;
        }
    }
}
