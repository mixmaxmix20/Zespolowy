using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMenu : MonoBehaviour
{
    public Transform container;
    public Transform shopItemTemplate;
    private void Awake()
    {
        shopItemTemplate.gameObject.SetActive(false);
    }

    public void Show()
    {
        shopItemTemplate.gameObject.SetActive(true);
    }

    public void Hide() {
        shopItemTemplate.gameObject.SetActive(false);
    }
}
