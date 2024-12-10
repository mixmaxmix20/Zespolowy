using UnityEngine;
using static InventoryManager;
using UnityEngine.EventSystems;


public class Item : MonoBehaviour
{
    [SerializeField] internal int quantity;
    [SerializeField] internal string itemName;
    [SerializeField] internal Sprite sprite;
    [SerializeField] internal string itemDescription;
    public InventoryManager.ItemType itemType;
    private InventoryManager inventoryManager;

    private void Awake()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            int leftOverItems = inventoryManager.AddItem(itemName, quantity, sprite, itemDescription, itemType);
            if (leftOverItems <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                quantity = leftOverItems;
            }
            
        }
    }



}



