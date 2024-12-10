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
        if (!detectedColliders.Contains(collision)) // Sprawdzenie, czy dany collider nie znajduje siê ju¿ na liœcie
        {
            detectedColliders.Add(collision); // Dodanie collidera do listy
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedColliders.Remove(collision); // Usuniêcie collidera, który opuœci³ strefê, z listy

        if (detectedColliders.Count <= 0)  // Jeœli po usuniêciu nie ma ju¿ ¿adnych colliderów na liœcie
        {
            
            noCollidersRemain.Invoke(); // Gdy lista jest pusta , Aktywuje zdarzenie noCollidersRemain.
        }
    }
}
