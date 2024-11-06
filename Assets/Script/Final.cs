using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : MonoBehaviour
{
    [SerializeField] private GameObject WinScreen;
    private  void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if(WinScreen != null)
            {
                WinScreen.SetActive(true);
            }
        }
    }
}
