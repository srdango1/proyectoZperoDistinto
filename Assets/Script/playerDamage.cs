using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerDamage : MonoBehaviour
{
    [SerializeField]private float vidaInicial;
    public float VidaActual {get;private set;}
    [SerializeField] Rigidbody2D fisicas;

    private Animator animator;

    [SerializeField] private Transform[] respawnPoints;
    private Transform respawnPointActual;
    public float fuerzaRebote = 10f;
        AudioManager audioManager;

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
        if (other.gameObject.CompareTag("spikes") && VidaActual > 0)
        {
            Daño(10); 
            Vector2 direccion = new Vector2 (transform.position.x,0);
            choque (direccion);
        }
    }
    public void Daño(float _damage){
        VidaActual =  Mathf.Clamp(VidaActual - _damage,0, vidaInicial);
        
        if (VidaActual>0)
        {
            animator.SetTrigger("dañoPlayer");
            audioManager.PlayEfectos(audioManager.golpePlayer);

            
        }

        else if (VidaActual<=0){
            animator.SetTrigger("death");
            audioManager.PlayEfectos(audioManager.muertePlayer);
            fisicas.bodyType = RigidbodyType2D.Static;
            Invoke("ReiniciarScene", 2f);
            
        }

    }
    //Para que lance al jugador al aire el daño
    public void choque (Vector2 direccion){
        Vector2 rebote = new Vector2(transform.position.x - direccion.x ,1 ).normalized;
        fisicas.AddForce(rebote * fuerzaRebote,ForceMode2D.Impulse);
    }
  
    private void OnTriggerEnter2D (Collider2D other){
        //Se necesitan poner los respawn tanto en mapa como en la lista
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
        //Funciona con poner el gameobject con el tag caida
        else if (other.CompareTag("Caida"))
        {
           Invoke("ReiniciarPosicion",0.1f);  //Se supone que demora 0.1s en activarse
        }
        //ambos objetos en prefab
    }
    public void ReiniciarScene() {
        SceneManager.LoadScene("SampleScene");
    }
    private void ReiniciarPosicion()
    {
        transform.position = respawnPointActual.position;
        VidaActual = vidaInicial;
        fisicas.bodyType = RigidbodyType2D.Dynamic;
    }
    private void Update()
        {
           if(Input.GetKeyDown(KeyCode.E)){
            Daño(10);
           } 
        }
}
    