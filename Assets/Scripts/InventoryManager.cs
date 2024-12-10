using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    public GameObject EquipmentMenu;
    public GameObject EquipmentButton;
    public GameObject InventoryButton;

    private bool isInventoryOpen = false;
    internal bool isEquipmentOpen = false;

    public ItemSlot[] itemSlot;
    public EquipmentSlot[] equipmentSlot;
    public EquippedSlot[] equippedSlot;
    public ItemSo[] itemSOs;

    public bool IsEquipmentOpen => isEquipmentOpen;
    public bool IsInventoryOpen => isInventoryOpen;

    public void SwitchToInventory()
    {
        if (isEquipmentOpen)
        {
            CloseEquipment();
        }

        if (!isInventoryOpen)
        {
            isInventoryOpen = true;
            InventoryMenu.SetActive(true);
        }

        UpdateButtonVisibility();
        Debug.Log("Switched to Inventory");
    }

    public void SwitchToEquipment()
    {
        if (isInventoryOpen)
        {
            CloseInventory();
        }

        if (!isEquipmentOpen)
        {
            isEquipmentOpen = true;
            EquipmentMenu.SetActive(true);
        }

        UpdateButtonVisibility();
        Debug.Log("Switched to Equipment");
    }

    public void ToggleInventory()
    {
        if (isEquipmentOpen)
        {
            CloseEquipment();
        }

        isInventoryOpen = !isInventoryOpen;
        InventoryMenu.SetActive(isInventoryOpen);
        UpdateButtonVisibility();
        Debug.Log("Inventory state changed: " + (isInventoryOpen ? "Opened" : "Closed"));
    }

    public void ToggleEquipment()
    {
        if (isInventoryOpen)
        {
            CloseInventory();
        }

        isEquipmentOpen = !isEquipmentOpen;
        EquipmentMenu.SetActive(isEquipmentOpen);
        UpdateButtonVisibility();
        Debug.Log("Equipment state changed: " + (isEquipmentOpen ? "Opened" : "Closed"));
    }

    public void CloseInventory()
    {
        isInventoryOpen = false;
        InventoryMenu.SetActive(false);
        UpdateButtonVisibility();
        Debug.Log("Inventory closed");
    }

    public void CloseEquipment()
    {
        isEquipmentOpen = false;
        EquipmentMenu.SetActive(false);
        UpdateButtonVisibility();
        Debug.Log("Equipment closed");
    }

    private void UpdateButtonVisibility()
    {
        bool shouldShowButtons = isInventoryOpen || isEquipmentOpen;
        EquipmentButton.SetActive(shouldShowButtons);
        InventoryButton.SetActive(shouldShowButtons);
    }

    public bool UseItem(string itemName)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull && itemSlot[i].itemName == itemName)
            {
                bool usable = false;
                for (int j = 0; j < itemSOs.Length; j++)
                {
                    if (itemSOs[j].name == itemName)
                    {
                        usable = itemSOs[j].UseItem();
                        break;
                    }
                }

                if (usable)
                {
                    itemSlot[i].quantity--;

                    if (itemSlot[i].quantity <= 0)
                    {
                        itemSlot[i].ClearSlot();
                    }

                    return true;
                }
            }
        }
        return false;
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription, ItemType itemType)
    {
        if (itemType == ItemType.consumable || itemType == ItemType.crafting || itemType == ItemType.collectible)
        {
            for (int i = 0; i < itemSlot.Length; i++)
            {
                if (!itemSlot[i].isFull || itemSlot[i].quantity == 0)
                {
                    int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription, itemType);
                    return leftOverItems;
                }
            }
        }
        else
        {
            for (int i = 0; i < equipmentSlot.Length; i++)
            {
                if (!equipmentSlot[i].isFull || equipmentSlot[i].quantity == 0)
                {
                    int leftOverItems = equipmentSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription, itemType);
                    return leftOverItems;
                }
            }
        }

        return quantity;
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            equipmentSlot[i].selectedShader.SetActive(false);
            equipmentSlot[i].thisItemSelected = false;
        }
        for (int i = 0; i < equippedSlot.Length; i++)
        {
            equippedSlot[i].selectedShader.SetActive(false);
            equippedSlot[i].thisItemSelected = false;
        }
    }

    public enum ItemType
    {
        consumable,
        crafting,
        collectible,
        head,
        body,
        legs,
        feet,
        hand,
        amulet,
        ring,
        none
    };
}
