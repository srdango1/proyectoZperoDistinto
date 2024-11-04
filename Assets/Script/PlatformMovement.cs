using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    [SerializeField] GameObject[] puntos;
    [SerializeField] float velocidad;
    private int puntoActual = 0;
    private void Update()
    {

        if (Vector2.Distance(puntos[puntoActual].transform.position, transform.position) < 0.1f) {

            puntoActual++;

            if (puntoActual > puntos.Length -1) {
                puntoActual = 0;
            }
        }


        transform.position = Vector2.MoveTowards(transform.position, puntos[puntoActual].transform.position, velocidad * Time.deltaTime);
    }
    
}
