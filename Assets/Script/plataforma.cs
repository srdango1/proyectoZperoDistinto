using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plataforma : MonoBehaviour
{
    [SerializeField] GameObject[] puntos;
   
    private int currentWaypointIndex = 0;
    private float speed = 2f;

    private void Update() {
    if (Vector2.Distance(puntos[currentWaypointIndex].transform.position, transform.position) < .1f) {
        currentWaypointIndex++;
        if (currentWaypointIndex >= puntos.Length) {currentWaypointIndex = 0;}

        transform.position = Vector2.MoveTowards(transform.position, puntos[currentWaypointIndex].transform.position, Time.deltaTime * speed);
    }
}

    
}
