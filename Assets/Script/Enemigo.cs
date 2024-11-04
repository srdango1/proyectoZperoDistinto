using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private float vida;
    private Animator animator;
    AudioManager audioManager;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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
    }
}