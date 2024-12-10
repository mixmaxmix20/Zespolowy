using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    /* 
    Parallax effect to technika, która sprawia, 
    ¿e obiekty w tle poruszaj¹ siê z ró¿nymi prêdkoœciami w zale¿noœci od ich odleg³oœci od kamery. 
    Obiekty bli¿ej kamery poruszaj¹ siê szybciej ni¿ te, które s¹ dalej, co tworzy wra¿enie g³êbi w scenie. 
    W tym przypadku efekt parallaxu jest u¿ywany, aby t³o porusza³o siê w zale¿noœci od ruchu kamery, tworz¹c efekt g³êbi.
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
