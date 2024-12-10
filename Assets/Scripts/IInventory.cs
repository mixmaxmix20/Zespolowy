using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInventory
{
    int Money { get; set; }
    int Keys { get; set; }
    bool Buy(int coinPrice);
}