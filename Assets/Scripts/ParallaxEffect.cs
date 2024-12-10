using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    /* 
    Parallax effect to technika, kt�ra sprawia, 
    �e obiekty w tle poruszaj� si� z r�nymi pr�dko�ciami w zale�no�ci od ich odleg�o�ci od kamery. 
    Obiekty bli�ej kamery poruszaj� si� szybciej ni� te, kt�re s� dalej, co tworzy wra�enie g��bi w scenie. 
    W tym przypadku efekt parallaxu jest u�ywany, aby t�o porusza�o si� w zale�no�ci od ruchu kamery, tworz�c efekt g��bi.
    */
    public Camera cam;
    public Transform followTarget;
    float startingZ;
    Vector2 startingPosition;
    Vector2 camMoveSinceStart => (Vector2) cam.transform.position - startingPosition;

    float zDistanceFromTarget => transform.position.z - followTarget.position.z;

    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }
   
    void Update()
    {
        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;

        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
