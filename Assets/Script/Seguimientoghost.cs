using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguirjugadorArea : MonoBehaviour
{
    public float radioBusqueda = 5f;
    public LayerMask capaPlayer;
    public Transform transformplayer;
    public EstadoMovimiento estadoActual = EstadoMovimiento.Esperando;
    public float distanciaMaxima = 10f;
    public float VelocidadMovimiento = 4f;
    public Vector3 puntoInicial;

    public enum EstadoMovimiento
    {
        Esperando,
        Siguiendo,
        Volviendo
    }

    private void Start()
    {
        puntoInicial = transform.position;
    }

    private void Update()
    {
        switch (estadoActual)
        {
            case EstadoMovimiento.Esperando:
                EstadoEsperando();
                break;
            case EstadoMovimiento.Siguiendo:
                EstadoSiguiendo();
                break;
            case EstadoMovimiento.Volviendo:
                EstadoVolviendo();
                break;
        }

        MirarJugador();
    }

    private void EstadoEsperando()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, radioBusqueda, capaPlayer);

        if (playerCollider)
        {
            transformplayer = playerCollider.transform;
            estadoActual = EstadoMovimiento.Siguiendo;
        }
    }

    private void EstadoSiguiendo()
    {
        if (transformplayer == null || Vector3.Distance(transform.position, transformplayer.position) > distanciaMaxima)
        {
            estadoActual = EstadoMovimiento.Volviendo;
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, transformplayer.position, VelocidadMovimiento * Time.deltaTime);
    }

    private void EstadoVolviendo()
    {
        float distanciaAlPuntoInicial = Vector3.Distance(transform.position, puntoInicial);

        if (distanciaAlPuntoInicial > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, puntoInicial, VelocidadMovimiento * Time.deltaTime);
        }
        else
        {
            estadoActual = EstadoMovimiento.Esperando;
        }
    }

    private void MirarJugador()
    {
        if (transformplayer != null)
        {
            if (transformplayer.position.x > transform.position.x)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioBusqueda);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(puntoInicial, distanciaMaxima);
    }
}
