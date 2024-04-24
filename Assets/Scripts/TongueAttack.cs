using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TongueAttack : MonoBehaviour


{
    
    
    void OnTriggerEnter2D (Collider2D collider2D){

         if (collider2D.CompareTag("Player"))
        {
            Debug.Log;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
