using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Odno�nik do suwaka paska zdrowia i tekstu z ilo�ci� zdrowia
    public Slider healthSlider;
    public TMP_Text healthBarText;
    private Damageable playerDamagable; // Komponent Damageable gracza

    // Inicjalizacja odniesienia do komponentu Damageable gracza
    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerDamagable = player.GetComponent<Damageable>();
        }
        else
        {
            Debug.Log("No player found");
        }
    }

    // Ustawienie pocz�tkowego paska zdrowia i tekstu na podstawie aktualnego zdrowia gracza
    private void Start()
    {
        UpdateHealthUI(playerDamagable.Health, playerDamagable.Maxhealth);
    }

    /*
    Subskrypcja oznacza, �e klasa HealthBar "zapisuje si�" na zmiany zdrowia gracza i reaguje na nie przez aktualizacj� UI. 
    Kiedy zdrowie gracza si� zmienia, 
    HealthBar automatycznie odbiera to zdarzenie i aktualizuje pasek zdrowia oraz wy�wietlany tekst.
     */

    // Subskrybowanie zmiany zdrowia gracza po w��czeniu komponentu
    private void OnEnable()
    {
        playerDamagable.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    // Usuwanie subskrypcji zmiany zdrowia po wy��czeniu komponentu
    private void OnDisable()
    {
        playerDamagable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    // Metoda obs�uguj�ca zmiany zdrowia gracza; aktualizuje pasek zdrowia i tekst
    private void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        UpdateHealthUI(newHealth, maxHealth);
    }

    // Oblicza procentowy stan suwaka zdrowia na podstawie aktualnego i maksymalnego zdrowia
    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }

    // Aktualizuje UI zdrowia - warto�� suwaka i tekst
    private void UpdateHealthUI(int currentHealth, int maxHealth)
    {
        healthSlider.value = CalculateSliderPercentage(currentHealth, maxHealth);
        healthBarText.text = $"HP {currentHealth} / {maxHealth}";
    }
}
