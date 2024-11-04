using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class coleccionables : MonoBehaviour
{
    [SerializeField] Text textItems;
    private int contador = 0;
    AudioManager audioManager;
    private void Awake(){
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collider){
    if (collider.gameObject.tag == "coin"){
        audioManager.PlayEfectos(audioManager.coleccionable);
        Destroy(collider.gameObject);
        contador++;
        textItems.text = $"X {contador}";
    }
   }
}
