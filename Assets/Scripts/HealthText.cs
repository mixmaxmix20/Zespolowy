using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public Vector3 moveSpeed = new Vector3(0, 75, 0);   // Prêdkoœæ poruszania siê tekstu
    public float timeToFade = 1f;                       // Czas, po którym tekst zniknie
    RectTransform textTransform;                        // Odnoœnik do RectTransform (pozycjonowanie UI)
    TextMeshProUGUI textMeshPro;                        // Odnoœnik do komponentu TextMeshProUGUI (tekst)

    private float timeElapsed = 0f;                     // Zmienna do œledzenia czasu
    private Color startColor;                           // Pocz¹tkowy kolor tekstu

    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        startColor = textMeshPro.color; // Zapamiêtanie pocz¹tkowego koloru
    }

    private void Update()
    {
        textTransform.position += moveSpeed * Time.deltaTime; // Przemieszczanie tekstu o zadan¹ prêdkoœæ co klatkê
        timeElapsed += Time.deltaTime;                        // Aktualizacja czasu

        // Je¿eli tekst nie osi¹gn¹³ jeszcze czasu zanikania
        if (timeElapsed < timeToFade)
        {
            float fadeAlpha = startColor.a * (1 - timeElapsed / timeToFade); // Obliczenie alfa na podstawie up³ywu czasu
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, fadeAlpha); // Ustawienie koloru z obliczon¹ przezroczystoœci¹
        }
        else
        {
            // Zniszczenie obiektu, gdy tekst siê skoñczy³
            Destroy(gameObject);
        }
    }
}
