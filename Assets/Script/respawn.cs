using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class respawn : MonoBehaviour
{
    public GameObject Player;
    public GameObject respawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D (Collision2D other){
        if (other.gameObject.CompareTag("Player")){
            Player.transform.position = respawnPoint.transform.position;
        }
    }
}
