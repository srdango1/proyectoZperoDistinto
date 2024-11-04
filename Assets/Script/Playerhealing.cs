using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerhealing : MonoBehaviour
{
    // Variables para la vida del jugador
    public int maxHealth = 3;
    public int currentHealth;


    void Start()
    {
        currentHealth = maxHealth;
    }


    void Update()
    {
    
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
    
        if (collision.CompareTag("corazon"))
        {
            Heal(1);
            Destroy(collision.gameObject);
        }
    }

    // Método para curar al jugador
    void Heal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Debug.Log("Vida actual: " + currentHealth);
    }
}
