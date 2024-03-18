using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class blockHit : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool isAnimating = false; 
    public int maxHits = -1;

    private void OnCollisionEnter2D (Collision2D collision)
    {
    if (collision.gameObject.CompareTag("Player")&& !isAnimating && maxHits != 0){
        Vector3 upDirection = transform.TransformDirection(Vector3.up);
        Vector3 compareDirection = (collision.transform.position - transform.position).normalized;

        if (Vector3.Dot(upDirection, compareDirection) < 0){
            StartCoroutine(Hit());
        }
    }
    }

    IEnumerator Hit(){
        isAnimating = true;
        Vector3 endPosition = transform.position + Vector3.up * 0.5f;
        yield return transform.MoveBackAndForth(endPosition);
        isAnimating = false; 
    }
}
