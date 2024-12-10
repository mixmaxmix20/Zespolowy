using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    public UnityEvent noCollidersRemain;
    public List<Collider2D> detectedColliders = new List<Collider2D>();
    Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!detectedColliders.Contains(collision)) // Sprawdzenie, czy dany collider nie znajduje si� ju� na li�cie
        {
            detectedColliders.Add(collision); // Dodanie collidera do listy
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedColliders.Remove(collision); // Usuni�cie collidera, kt�ry opu�ci� stref�, z listy

        if (detectedColliders.Count <= 0)  // Je�li po usuni�ciu nie ma ju� �adnych collider�w na li�cie
        {
            
            noCollidersRemain.Invoke(); // Gdy lista jest pusta , Aktywuje zdarzenie noCollidersRemain.
        }
    }
}
