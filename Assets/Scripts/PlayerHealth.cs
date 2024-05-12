using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Animator animator;
    public HealthBar HealthBar;
    public SpriteRenderer sr;

    public PlayerInvulnerable playerInvulnerable;

    [Tooltip("Please uncheck it on production")]
    public bool needResetHP = true;

    [Header("ScriptableObjects")]
    public PlayerData playerData;

    [Header("Debug")]
    public VoidEventChannel onDebugDeathEvent;

    [Header("Broadcast event channels")]
    public VoidEventChannel onPlayerDeath;
    


    private void Awake()
    {
        if (needResetHP || playerData.currentHealth <= 0)
        {
            playerData.currentHealth = playerData.maxHealth;
        }
    }

    private void OnEnable()
    {
        onDebugDeathEvent.OnEventRaised += Die;
    }

    public void TakeDamage(float damage)
    {
        // Réduire la santé du joueur
        playerData.currentHealth -= damage;

        // Mettre à jour la barre de santé
        HealthBar.Fill(playerData.currentHealth / playerData.maxHealth);

        // Vérifier si le joueur est mort
           {
        if (playerInvulnerable.isInvulnerable && damage < float.MaxValue) return;

        playerData.currentHealth -= damage;
        if (playerData.currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(playerInvulnerable.Invulnerable());
        }
    }
    }

    private void Die()
    {
        onPlayerDeath?.Raise();
        GetComponent<Rigidbody2D>().simulated = false;
        transform.Rotate(0f, 0f, 45f);
        animator.SetTrigger("Death");
    }

    public void OnPlayerDeathAnimationCallback()
    {
        sr.enabled = false;
    }

    private void OnDisable()
    {
        onDebugDeathEvent.OnEventRaised -= Die;
    }
}
