using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remove : StateMachineBehaviour
{
    public float fadetime = 0.5f;           // Czas zanikania obiektu w sekundach
    private float timeElapsed = 0f;         // Czas, kt�ry up�yn�� od rozpocz�cia zanikania
    private SpriteRenderer spriteRenderer;  // Komponent SpriteRenderer, aby zmienia� przezroczysto��
    private GameObject objToRemove;         // Obiekt, kt�ry ma zosta� zniszczony
    private Color startColor;               // Pocz�tkowy kolor (z przezroczysto�ci�) obiektu
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {        
        timeElapsed = 0f;                   
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
        objToRemove = animator.gameObject;
    }

    // Ta metoda jest wywo�ywana w ka�dej klatce animacji
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Zwi�kszanie czasu, kt�ry up�yn�� od rozpocz�cia animacji
        timeElapsed += Time.deltaTime;

        // Obliczanie nowej przezroczysto�ci (alpha) na podstawie up�ywaj�cego czasu
        float newAlpha = startColor.a * (1 - timeElapsed / fadetime);

        // Ustawianie nowego koloru z obliczon� przezroczysto�ci�
        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

        // Je�li czas zanikania min��, zniszcz obiekt
        if (timeElapsed > fadetime)
        {
            Destroy(objToRemove);
        }
    }
}
