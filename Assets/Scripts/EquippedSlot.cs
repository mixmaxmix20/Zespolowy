using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using static InventoryManager;

public class EquippedSlot : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{

    [SerializeField] private Image slotImage;
    [SerializeField] private TMP_Text slotName;

    [SerializeField] private ItemType itemType;
    private Sprite itemSprite;
    private string itemName;
    private string itemDescription;
    public bool isFull;
    private InventoryManager inventoryManager;
    private EquipmentSOLibrary equipmentSOLibrary;
    private bool isMouseInside = false;
    public bool slotInUse;

    [SerializeField] public GameObject selectedShader;
    [SerializeField] public bool thisItemSelected;
    [SerializeField] Sprite emptySprite;

    [SerializeField] private GameObject itemInfoPanel; 
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private Image itemIconImage;

    private void Awake()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        equipmentSOLibrary = GameObject.Find("InventoryCanvas").GetComponent<EquipmentSOLibrary>();
    }
    private void OnDisable()
    {
        if (isMouseInside)
        {
            OnPointerExit(null);
            isMouseInside = false;
        }
    }

    private void OnEnable()
    {
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
        if (slotInUse)
        {
            if (thisItemSelected)
            {
                UnEquipGear();
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
    }

    private void OnRightClick()
    {
        UnEquipGear();
    }

    public void EquipGear(Sprite itemSprite, string itemName, string itemDescription)
    {
        if (slotInUse)
        {
            UnEquipGear();
        }

        this.itemSprite = itemSprite;
        slotImage.sprite = itemSprite;
        slotName.enabled = false;

        this.itemName = itemName;
        this.itemDescription = itemDescription;

        for (int i = 0; i < equipmentSOLibrary.equipmentSO.Length; i++)
        {
            if (equipmentSOLibrary.equipmentSO[i].itemName == this.itemName)
            {
                equipmentSOLibrary.equipmentSO[i].EquipItem();
            }
        }

        slotInUse = true;
        isFull = true;
    }

    private void UnEquipGear()
    {
        if (!slotInUse || string.IsNullOrEmpty(itemName))
            return;

        inventoryManager.DeselectAllSlots();

        int remainingItems = inventoryManager.AddItem(itemName, 1, itemSprite, itemDescription, itemType);

        if (remainingItems == 0)
        {
            for (int i = 0; i < equipmentSOLibrary.equipmentSO.Length; i++)
            {
                if (equipmentSOLibrary.equipmentSO[i].itemName == this.itemName)
                {
                    equipmentSOLibrary.equipmentSO[i].UnEquipItem();
                    break;
                }
            }

            if (itemType.ToString() == "ring")
            {
                slotName.text = "Finger";
            }
            else if (itemType.ToString() == "amulet")
            {
                slotName.text = "Neck";
            }
            else
            {
                slotName.text = char.ToUpper(itemType.ToString()[0]) + itemType.ToString().Substring(1);
            }

            ClearSlot();
        }
    }




    private void ClearSlot()
    {
        this.itemSprite = null;
        this.itemName = string.Empty;
        this.itemDescription = string.Empty;

        slotImage.sprite = emptySprite;
        slotName.enabled = true; 
        selectedShader.SetActive(false); 

        slotInUse = false;
        isFull = false;
        thisItemSelected = false;
    }
}
