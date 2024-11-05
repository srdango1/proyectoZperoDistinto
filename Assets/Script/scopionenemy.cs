using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class scopionenemy : MonoBehaviour
{   public GameObject Player;
      void Update()
    {
        Vector3 direction = Player.transform.position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f,1.0f);
        else transform.localScale = new Vector3 (-1.0f,1.0f,1.0f);

        
    }
}
