using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerDamage : MonoBehaviour
{
    [SerializeField] private float vidaInicial;
    public float VidaActual { get; private set; }
    [SerializeField] private Rigidbody2D fisicas;

    private Animator animator;
    [SerializeField] private Transform[] respawnPoints;
    private Transform respawnPointActual;
    public float fuerzaRebote = 10f;
    private AudioManager audioManager;

    // Variables para la inmunidad
    private bool esInmune = false;
    [SerializeField] private float tiempoInmunidad = 1.5f; // Duración de la inmunidad en segundos

    private void Awake()
    {
        VidaActual = vidaInicial;
        animator = GetComponent<Animator>();
        fisicas = GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        if (respawnPoints.Length > 0)
            respawnPointActual = respawnPoints[0];
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("spikes") && VidaActual > 0 && !esInmune)
        {
            Daño(10);
            Vector2 direccion = new Vector2(transform.position.x, 0);
            choque(direccion);
        }
    }

    public void Daño(float _damage)
    {
        if (esInmune) return; // Evita recibir daño si está en inmunidad

        VidaActual = Mathf.Clamp(VidaActual - _damage, 0, vidaInicial);

        if (VidaActual > 0)
        {
            animator.SetTrigger("dañoPlayer");
            audioManager.PlayEfectos(audioManager.golpePlayer);
            StartCoroutine(ActivarInmunidad()); // Inicia la inmunidad
        }
        else if (VidaActual <= 0)
        {
            animator.SetTrigger("death");
            audioManager.PlayEfectos(audioManager.muertePlayer);
            fisicas.bodyType = RigidbodyType2D.Static;
            Invoke("ReiniciarScene", 2f);
        }
    }

    // Coroutine para activar y desactivar la inmunidad
    private IEnumerator ActivarInmunidad()
    {
        esInmune = true;
        yield return new WaitForSeconds(tiempoInmunidad);
        esInmune = false;
    }

    public void choque(Vector2 direccion)
    {
        Vector2 rebote = new Vector2(transform.position.x - direccion.x, 1).normalized;
        fisicas.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Respawn"))
        {
            for (int i = 0; i < respawnPoints.Length; i++)
            {
                if (other.transform == respawnPoints[i])
                {
                    respawnPointActual = respawnPoints[i];
                    break;
                }
            }
        }
        else if (other.CompareTag("Caida"))
        {
            audioManager.PlayEfectos(audioManager.caidaPlayer);
            Invoke("ReiniciarPosicion", 0.1f);
        }
        else if (other.gameObject.CompareTag("Enemigo") && VidaActual > 0 && !esInmune)
        {
            Daño(10);
            Vector2 direccion = new Vector2(transform.position.x, 0);
            choque(direccion);
        }
    }

    public void ReiniciarScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void ReiniciarPosicion()
    {
        transform.position = respawnPointActual.position;
        VidaActual = vidaInicial;
        fisicas.bodyType = RigidbodyType2D.Dynamic;
    }

    public void masVida(float _value)
    {
        VidaActual = Mathf.Clamp(VidaActual + _value, 0, vidaInicial);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Daño(10);
        }
    }
}
    