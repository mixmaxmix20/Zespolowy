using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static InventoryManager;

public class EquipmentSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    // INFO PRZEDMIOT //
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite;
    public InventoryManager.ItemType itemType;

    [SerializeField] private GameObject itemInfoPanel;      
    [SerializeField] private TMP_Text itemNameText;        
    [SerializeField] private TMP_Text itemDescriptionText;  
    [SerializeField] private Image itemIconImage;          

    [SerializeField] private Image itemImage;

    // Equipped Slots //
    [SerializeField] private EquippedSlot headSlot, bodySlot, legsSlot, feetSlot, handsSlot, neckSlot, fingerSlot1, fingerSlot2;

    public GameObject selectedShader;
    public bool thisItemSelected;
    private InventoryManager inventoryManager;
    private EquipmentSOLibrary equipmentSOLibrary;

    private bool isMouseInside = false;  

    private void Awake()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        equipmentSOLibrary = GameObject.Find("InventoryCanvas").GetComponent<EquipmentSOLibrary>();
    }

    private void OnDisable()
    {
        // Jeœli obiekt jest wy³¹czany, traktujemy to jakby kursor opuœci³ obiekt
        if (isMouseInside)
        {
            OnPointerExit(null);
            isMouseInside = false;
        }
    }

    private void OnEnable()
    {
        // Po w³¹czeniu obiektu, sprawdzamy, czy kursor nadal jest nad obiektem
        if (isMouseInside && isFull)
        {
            OnPointerEnter(null);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isFull && inventoryManager.isEquipmentOpen)
        {
            itemNameText.text = itemName;            

            itemInfoPanel.SetActive(true);

            for (int i = 0; i < equipmentSOLibrary.equipmentSO.Length; i++)
            {
                if (equipmentSOLibrary.equipmentSO[i].itemName == itemName)
                {
                    equipmentSOLibrary.equipmentSO[i].PreviewEquipment();
                    break;
                }
            }

            RectTransform rectTransform = itemInfoPanel.GetComponent<RectTransform>();
            RectTransform slotRectTransform = GetComponent<RectTransform>();
            Vector3 newPosition = slotRectTransform.position + new Vector3(-slotRectTransform.rect.width - 150, -110, 0); 
            rectTransform.position = newPosition;

            isMouseInside = true;  
        }
        else
        {
            itemInfoPanel.SetActive(false);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemInfoPanel.SetActive(false);
        isMouseInside = false;  
    }


    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription, ItemType itemType)
    {
        if (isFull)
        {
            return quantity; // Je¿eli slot jest pe³ny, zwróæ pozosta³¹ iloœæ
        }

        this.itemType = itemType;
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;
        this.itemDescription = itemDescription;

        this.quantity = 1;
        isFull = true;

        return 0; // Nie ma pozosta³ej iloœci do zwrócenia
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
        if (isFull)
        {
            if (thisItemSelected)
            {
                EquipGear();
            }
            else
            {
                inventoryManager.DeselectAllSlots();
                selectedShader.SetActive(true);
                thisItemSelected = true;
                for (int i = 0; i < equipmentSOLibrary.equipmentSO.Length; i++)
                {
                    if (equipmentSOLibrary.equipmentSO[i].itemName == this.itemName)
                    {
                        equipmentSOLibrary.equipmentSO[i].PreviewEquipment();
                    }
                }
            }
        }
        else
        {
            
            inventoryManager.DeselectAllSlots();
            selectedShader.SetActive(true);
            thisItemSelected = true;
        }
    }

    private void EquipGear()
    {
        // Wyposa¿ przedmiot w odpowiedni slot
        if (itemType == ItemType.head) { headSlot.EquipGear(itemSprite, itemName, itemDescription); }
        if (itemType == ItemType.body) { bodySlot.EquipGear(itemSprite, itemName, itemDescription); }
        if (itemType == ItemType.legs) { legsSlot.EquipGear(itemSprite, itemName, itemDescription); }
        if (itemType == ItemType.feet) { feetSlot.EquipGear(itemSprite, itemName, itemDescription); }
        if (itemType == ItemType.hand) { handsSlot.EquipGear(itemSprite, itemName, itemDescription); }
        if (itemType == ItemType.amulet) { neckSlot.EquipGear(itemSprite, itemName, itemDescription); }
        if (itemType == ItemType.ring)
        {
            if (!fingerSlot1.slotInUse)
            {
                fingerSlot1.EquipGear(itemSprite, itemName, itemDescription);
            }
            else if (!fingerSlot2.slotInUse)
            {
                fingerSlot2.EquipGear(itemSprite, itemName, itemDescription);
            }
        }

        EmptySlot();
    }

    private void EmptySlot()
    {
        itemName = string.Empty;
        itemSprite = null;
        itemDescription = string.Empty;
        itemType = ItemType.none;

        itemImage.sprite = emptySprite;
        quantity = 0;

        isFull = false;
    }

    private void OnRightClick()
    {
        if (quantity <= 0 || string.IsNullOrEmpty(itemName) || itemSprite == null)
        {
            Debug.LogWarning("Slot is empty. No item to drop.");
            return; 
        }

        GameObject itemToDrop = new GameObject(itemName);
        Item newItem = itemToDrop.AddComponent<Item>();
        newItem.quantity = 1; 
        newItem.itemName = itemName;
        newItem.sprite = itemSprite;
        newItem.itemDescription = itemDescription;
        newItem.itemType = itemType;

        // Dodanie SpriteRenderer do wizualizacji przedmiotu
        SpriteRenderer sr = itemToDrop.AddComponent<SpriteRenderer>();
        sr.sprite = itemSprite;
        sr.sortingOrder = 5;
        sr.sortingLayerName = "Ground";

        // Dodanie komponentów fizycznych
        itemToDrop.AddComponent<BoxCollider2D>();
        itemToDrop.AddComponent<Rigidbody2D>();

        // Pozycjonowanie obiektu na scenie
        itemToDrop.transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(2, 0, 0);

        quantity -= 1;

        if (quantity == 0)
        {
            EmptySlot();
        }
    }

}
