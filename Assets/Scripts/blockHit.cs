using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class blockHit : MonoBehaviour
{
    public SpriteRenderer sr;
    public bool isHidden = false;

    private bool isAnimating = false; 
    public int maxHits = -1;

    public PlatformEffector2D platformEffector2D;
    public GameObject collectiblePrefab;
  

    private void Awake(){
        
        platformEffector2D.enabled= isHidden;
        if (isHidden){
            sr.color = Color.clear;
        }
    }

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
        platformEffector2D.enabled= false;
        sr.color = Color.white;
        maxHits--;
        Vector3 endPosition = transform.position + Vector3.up * 0.5f;
        yield return transform.MoveBackAndForth(endPosition);

        if(collectiblePrefab != null){
            GameObject collectible = Instantiate(collectiblePrefab, transform.position, Quaternion.identity);

            Collectible collec = collectible.GetComponent<Collectible>();
            collec.canBeDestroyedOnContact = false;
            Vector3 collectibleEndPosition = collectible.transform.localPosition + Vector3.up *1.5f;
            
            yield return collectible.transform.MoveBackAndForth(collectibleEndPosition);
            collec.Picked();
        }

        isAnimating = false; 
    }
}
