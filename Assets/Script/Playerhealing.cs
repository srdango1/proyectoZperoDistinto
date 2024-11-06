using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerhealing : MonoBehaviour
{
   [SerializeField] private float valorVida;
   private void OnTriggerEnter2D(Collider2D other){
    if (other.tag == "Player"){
        other.GetComponent<playerDamage>().masVida(valorVida);
         Destroy(gameObject);
    }
   }
}
