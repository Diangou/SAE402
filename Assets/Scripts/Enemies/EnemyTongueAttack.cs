using System.Collections;
using UnityEngine;

public class EnemyTongueAttack : MonoBehaviour
{
    public Animator animator;

    // Delay between each attack
    public float timeDelayBetweenAttacks = 10;

    // Position of the tongue
    public Transform tonguePosition;
    public float tongueLength = 3;

    // Layer of GameObject considered as touchable
    public LayerMask layerMask;

    private string currentAnimationName;

    // Detect if the tongue touched something
    private bool hasTouchedSomething = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Shoot());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
        }
    }

    private void FixedUpdate()
    {
        Vector2 startCast = new Vector2(
            tonguePosition.position.x,
            tonguePosition.position.y
        );

        // The raycast is called if it doesn't touched something
        // and the current animation is the attack. NOTE : "ChamelonAttack" is the same of the animation in the animator
        if (!hasTouchedSomething && currentAnimationName == "ChamelonAttack")
        {
            RaycastHit2D hit = Physics2D.Linecast(
                startCast, 
                // The length of the raycast is based on the percentage of progression of the attack animation
                new Vector3(startCast.x + (transform.right.normalized.x * tongueLength * animator.GetCurrentAnimatorStateInfo(0).normalizedTime), startCast.y), 
                layerMask
            );
            if (hit.collider != null)
            {
                hasTouchedSomething = true;
                // Hit player
                print("Gameobject touched " + hit.collider.name);
            }
        }
    }

    private void Update()
    {
        // Retrieve the name of the current animation
        currentAnimationName = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
    }

    IEnumerator Shoot()
    {
        animator.SetTrigger("Shoot");
        // Wait for the end of the animation
        yield return new WaitForSeconds(
            animator.GetCurrentAnimatorStateInfo(0).length
        );
        hasTouchedSomething = false;

        // Wait for the end of the delay between attack
        yield return new WaitForSeconds(
            timeDelayBetweenAttacks - animator.GetCurrentAnimatorStateInfo(0).length
        );

        // Restart the shoot sequence
        StartCoroutine(Shoot());
    }

    void OnDrawGizmos()
    {
        if (tonguePosition != null)
        {
            Gizmos.color = Color.yellow;

            Vector2 startCast = new Vector2(
                tonguePosition.position.x,
                tonguePosition.position.y
            );

            if (!hasTouchedSomething && currentAnimationName == "ChamelonAttack")
            {
                Gizmos.DrawLine(
                    startCast,
                    new Vector3(startCast.x + (transform.right.normalized.x * tongueLength * animator.GetCurrentAnimatorStateInfo(0).normalizedTime), startCast.y)
                );
            }
        }
    }
}
