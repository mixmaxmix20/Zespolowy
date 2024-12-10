using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite;
    public InventoryManager.ItemType itemType;
    [SerializeField] private int maxStack;

    [SerializeField] TMP_Text quantityText;
    [SerializeField] private Image itemImage;

    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;

    public GameObject selectedShader;
    public bool thisItemSelected;
    private InventoryManager inventoryManager;

    private void Awake()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription, InventoryManager.ItemType itemType)
    {
        if (!isFull || this.itemName == itemName)
        {
            this.itemName = itemName;
            this.itemSprite = itemSprite;
            this.itemDescription = itemDescription;
            this.itemType = itemType;

            int canAdd = Mathf.Min(quantity, maxStack - this.quantity);
            this.quantity += canAdd;
            isFull = this.quantity > 0;

            if (this.quantity > 0)
            {
                quantityText.enabled = true;
            }

            UpdateSlotUI();
            return quantity - canAdd;
        }
        return quantity;
    }


    public void UpdateSlotUI()
    {
        if (quantity > 0)
        {
            itemImage.sprite = itemSprite;
            quantityText.enabled = true; 
            quantityText.text = quantity > 1 ? quantity.ToString() : ""; // Poka¿ iloœæ tylko jeœli > 1
        }
        else
        {
            ClearSlot();
        }
    }


    public void ClearSlot()
    {
        itemName = string.Empty;
        itemSprite = emptySprite;
        itemDescription = string.Empty;
        quantity = 0;
        isFull = false;

        itemImage.sprite = emptySprite;
        quantityText.text = "";
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    private void OnLeftClick()
    {
        if (thisItemSelected)
        {
            if (!string.IsNullOrEmpty(itemName))
            {
                bool usable = inventoryManager.UseItem(itemName); 
                if (usable)
                {
                    UpdateSlotUI();
                }
            }
        }
        else
        {
            inventoryManager.DeselectAllSlots(); 
            selectedShader.SetActive(true); 
            thisItemSelected = true;

            itemDescriptionNameText.text = itemName;
            itemDescriptionText.text = itemDescription;
            itemDescriptionImage.sprite = itemSprite ?? emptySprite;
        }
    }



    private void OnRightClick()
    {
        if (quantity > 0)
        {
            GameObject itemToDrop = new GameObject(itemName);
            Item newItem = itemToDrop.AddComponent<Item>();
            newItem.quantity = quantity; 
            newItem.itemName = itemName;
            newItem.sprite = itemSprite;
            newItem.itemDescription = itemDescription;

            SpriteRenderer sr = itemToDrop.AddComponent<SpriteRenderer>();
            sr.sprite = itemSprite;
            sr.sortingOrder = 5;
            sr.sortingLayerName = "Ground";

            itemToDrop.AddComponent<BoxCollider2D>();
            itemToDrop.AddComponent<Rigidbody2D>();
            itemToDrop.transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(2, 0, 0);

            ClearSlot();
        }
    }
}
