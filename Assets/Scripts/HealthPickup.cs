using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] public int healthRestore = 15; // Iloœæ zdrowia do przywrócenia
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0); // Prêdkoœæ obrotu w stopniach na sekundê

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Sprawdzenie, czy obiekt posiada komponent Damageable (mo¿e byæ ulepszony)
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable)
        {
            // Próba przywrócenia zdrowia
            bool wasHealed = damageable.Heal(healthRestore);

            // Jeœli zdrowie zosta³o przywrócone, niszczy pickupa
            if (wasHealed)
            {
                Destroy(gameObject);
            }
        }
    }

    // Aktualizacja obrotu pickupa co klatkê
    private void Update()
    {
        // Obrót obiektu wokó³ osi Y
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
}
