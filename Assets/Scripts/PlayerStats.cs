using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int attack, defense, agility, intelligence;

    [SerializeField] private TMP_Text attackText, defenseText, agilityText, intelligenceText;

    [SerializeField] private TMP_Text attackPreText, defensePreText, agilityPreText, intelligencePreText;
    [SerializeField] private Image previewImage;
    [SerializeField] private GameObject selectedItemStats, selectedItemImage;

    private void Start()
    {
        UpdateEquipmentStats();
    }

    public void UpdateEquipmentStats()
    {
        attackText.text = attack.ToString();
        defenseText.text = defense.ToString();
        agilityText.text = agility.ToString();
        intelligenceText.text = intelligence.ToString();
    }

    public void PreviewEquipmentStats(int attack, int defense, int agility, int intelligence, Sprite itemSprite)
    {
        attackPreText.text = attack.ToString();
        defensePreText.text = defense.ToString();
        agilityPreText.text = agility.ToString();
        intelligencePreText.text = intelligence.ToString();
        previewImage.sprite = itemSprite;

        selectedItemImage.SetActive(true);
        selectedItemStats.SetActive(true);

    }
    public void TurnOffPreviewStats()
    {
        selectedItemImage.SetActive(false);
        selectedItemStats.SetActive(false);
    }
}
