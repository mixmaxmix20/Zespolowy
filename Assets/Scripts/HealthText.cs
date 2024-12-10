using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public Vector3 moveSpeed = new Vector3(0, 75, 0);   // Pr�dko�� poruszania si� tekstu
    public float timeToFade = 1f;                       // Czas, po kt�rym tekst zniknie
    RectTransform textTransform;                        // Odno�nik do RectTransform (pozycjonowanie UI)
    TextMeshProUGUI textMeshPro;                        // Odno�nik do komponentu TextMeshProUGUI (tekst)

    private float timeElapsed = 0f;                     // Zmienna do �ledzenia czasu
    private Color startColor;                           // Pocz�tkowy kolor tekstu

    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        startColor = textMeshPro.color; // Zapami�tanie pocz�tkowego koloru
    }

    private void Update()
    {
        textTransform.position += moveSpeed * Time.deltaTime; // Przemieszczanie tekstu o zadan� pr�dko�� co klatk�
        timeElapsed += Time.deltaTime;                        // Aktualizacja czasu

        // Je�eli tekst nie osi�gn�� jeszcze czasu zanikania
        if (timeElapsed < timeToFade)
        {
            float fadeAlpha = startColor.a * (1 - timeElapsed / timeToFade); // Obliczenie alfa na podstawie up�ywu czasu
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, fadeAlpha); // Ustawienie koloru z obliczon� przezroczysto�ci�
        }
        else
        {
            // Zniszczenie obiektu, gdy tekst si� sko�czy�
            Destroy(gameObject);
        }
    }
}
