using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float velocidad;
    [SerializeField] float fuerzaSalto;
    private Rigidbody2D fisicas;
    private Animator animator;
    private SpriteRenderer sprite;
    private bool enSuelo = true;
    private int cantSaltos = 0;
    AudioManager audioManager;
    private bool sonidoCaminando = false;

    void Start()
    {
        fisicas = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        // Está tomando la A o la D, o bien el análogo del joystick
        var ejeX = Input.GetAxis("Horizontal");
        // Si ejeX es 0 -> no hay intención de parte del jugador para mover el personaje
        // Si ejeX es mayor a 0 -> el jugador quiere que el personaje se mueva a la derecha
        // Si ejeX es menor a 0 -> el jugador quiere que el personaje se mueva a la izquierda
        if (ejeX > 0)
        {
            // El animator NO PUEDE activar o desactivar animaciones directamente
            // PERO SÍ PUEDE modificar los valores de los parámetros que condicionas las animaciones
            animator.SetBool("corriendo", true);
            sprite.flipX = false;
            if(!sonidoCaminando){
                audioManager.Pasos();
                sonidoCaminando = true;
            }
        }
        else if (ejeX < 0)
        {
            animator.SetBool("corriendo", true);
            sprite.flipX = true;
            if(!sonidoCaminando){
                audioManager.Pasos();
                sonidoCaminando = true;
            }
        }
        else
        {
            animator.SetBool("corriendo", false);
            if (sonidoCaminando)
            {
                audioManager.paraPasos();
                sonidoCaminando=false;
            }
        }

        // var vectorMovimiento = new Vector2(ejeX, 0);
        // transform.Translate(vectorMovimiento * Time.deltaTime * velocidad);
        // Otra forma de mover al personaje es usando solamente las físicas...
        // Para esto tendremos que tocar la propiedad velocity del Rigidbody2D y asignarle (como siempre)
        // un vector2 con los valores de velocidad en x e y.
        fisicas.velocity = new Vector2(ejeX * velocidad, fisicas.velocity.y);

        Saltar();
        // Activamos la animación de salto de acuerdo al estado inverso de enSuelo.
        // enSuelo está en true cuando estamos tocando el TileMap suelo. Por lo tanto, en ese caso
        // NO tenemos que activar la animación de salto (o sea, lo inverso al estado enSuelo)
        // OTRA posibilidad es asociar la variable cantSaltos.. así se pueden activar/desactivar
        // las animaciones de salto y doble salto, o pasar a idle de acuerdo a los números 1, 2 y 0
        animator.SetInteger("saltos", cantSaltos);
    }

    public void Saltar()
    {
        if (Input.GetKeyDown(KeyCode.Space) && cantSaltos < 2)
        {
            cantSaltos++;
            var vectorSalto = Vector2.up;
            audioManager.PlayEfectos(audioManager.salto);
            fisicas.AddForce(vectorSalto * fuerzaSalto, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("spikes"))
        {
            enSuelo = true;
            audioManager.PlayEfectos(audioManager.aterrizar);
            cantSaltos = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            enSuelo = false;
        }
    }
}
