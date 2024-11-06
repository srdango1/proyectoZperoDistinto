using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private float vida;
    
    private Animator animator;
    AudioManager audioManager;
    private Collider2D collider2D;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        collider2D = GetComponent<Collider2D>();
    }
    public void TomarDaño(float daño)
    {
        vida -= daño;
        audioManager.DañoEnemigo(gameObject.name);

        if (vida <= 0)
        {
            audioManager.MuerteEnemigo(gameObject.name);
            Muerte();
        }
    }
    private void Muerte()
    {
        animator.SetTrigger("Muerte");
        collider2D.enabled = false;

    }
}