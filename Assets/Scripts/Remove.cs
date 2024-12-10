using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remove : StateMachineBehaviour
{
    public float fadetime = 0.5f;           // Czas zanikania obiektu w sekundach
    private float timeElapsed = 0f;         // Czas, który up³yn¹³ od rozpoczêcia zanikania
    private SpriteRenderer spriteRenderer;  // Komponent SpriteRenderer, aby zmieniaæ przezroczystoœæ
    private GameObject objToRemove;         // Obiekt, który ma zostaæ zniszczony
    private Color startColor;               // Pocz¹tkowy kolor (z przezroczystoœci¹) obiektu
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {        
        timeElapsed = 0f;                   
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;
        objToRemove = animator.gameObject;
    }

    // Ta metoda jest wywo³ywana w ka¿dej klatce animacji
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Zwiêkszanie czasu, który up³yn¹³ od rozpoczêcia animacji
        timeElapsed += Time.deltaTime;

        // Obliczanie nowej przezroczystoœci (alpha) na podstawie up³ywaj¹cego czasu
        float newAlpha = startColor.a * (1 - timeElapsed / fadetime);

        // Ustawianie nowego koloru z obliczon¹ przezroczystoœci¹
        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

        // Jeœli czas zanikania min¹³, zniszcz obiekt
        if (timeElapsed > fadetime)
        {
            Destroy(objToRemove);
        }
    }
}
