using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] public int healthRestore = 15; // Ilo�� zdrowia do przywr�cenia
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0); // Pr�dko�� obrotu w stopniach na sekund�

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Sprawdzenie, czy obiekt posiada komponent Damageable (mo�e by� ulepszony)
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable)
        {
            // Pr�ba przywr�cenia zdrowia
            bool wasHealed = damageable.Heal(healthRestore);

            // Je�li zdrowie zosta�o przywr�cone, niszczy pickupa
            if (wasHealed)
            {
                Destroy(gameObject);
            }
        }
    }

    // Aktualizacja obrotu pickupa co klatk�
    private void Update()
    {
        // Obr�t obiektu wok� osi Y
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }
}
