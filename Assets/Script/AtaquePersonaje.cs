using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaquePersonaje : MonoBehaviour
{
    [SerializeField] private Transform ControladorAtaque;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float dañoGolpe;
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private float tiempoSiguienteAtaque;
    private Animator animator;
    AudioManager audioManager;


    [SerializeField] private float distanciaGolpe; // Distancia en la que se posicionará el golpe desde el personaje
    private bool mirandoDerecha = true; // Suponemos que el personaje comienza mirando a la derecha

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }

    private void Update()
    {
        if (tiempoSiguienteAtaque > 0)
        {
            tiempoSiguienteAtaque -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Fire1") && tiempoSiguienteAtaque <= 0)
        {

            Golpe();
            tiempoSiguienteAtaque = tiempoEntreAtaques;
        }

     // Aquí puedes actualizar la dirección del personaje
        // Suponemos que tienes un método para detectar hacia dónde mira
        // Esto es solo un ejemplo básico:




        float movimientoHorizontal = Input.GetAxis("Horizontal");
        if (movimientoHorizontal > 0)
            mirandoDerecha = true;
        else if (movimientoHorizontal < 0)
            mirandoDerecha = false;
    }

    private void Golpe()
    {
        // Mueve el ControladorAtaque según la dirección en la que mira el personaje

        if (mirandoDerecha)
        {
            ControladorAtaque.localPosition = new Vector2(distanciaGolpe, ControladorAtaque.localPosition.y);
        }
        else
        {
            ControladorAtaque.localPosition = new Vector2(-distanciaGolpe, ControladorAtaque.localPosition.y);
        }

        animator.SetTrigger("Golpe");
        audioManager.PlayEfectos(audioManager.playerAtaque);
        Collider2D[] objetos = Physics2D.OverlapCircleAll(ControladorAtaque.position, radioGolpe);

        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemigo"))
            {
                colisionador.transform.GetComponent<Enemigo>().TomarDaño(dañoGolpe);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ControladorAtaque.position, radioGolpe);
    }
}