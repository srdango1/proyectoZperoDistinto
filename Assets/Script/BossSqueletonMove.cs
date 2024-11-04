using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSqueletonMove : MonoBehaviour
{
    [SerializeField] GameObject[] puntos;
    [SerializeField] float velocidad;
    private int puntoActual = 0;

    private SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Vector2.Distance(puntos[puntoActual].transform.position, transform.position) < 0.1f) {

            puntoActual++;
            
            if (puntoActual > puntos.Length -1) {
                puntoActual = 0;
            }
            Girar();
        }
        transform.position = Vector2.MoveTowards(transform.position, puntos[puntoActual].transform.position, velocidad * Time.deltaTime);
    }

    private void Girar(){
        if(transform.position.x < puntos[puntoActual].transform.position.x){
            spriteRenderer.flipX = true;
        }else{
            spriteRenderer.flipX = false;
        }
    }
}
