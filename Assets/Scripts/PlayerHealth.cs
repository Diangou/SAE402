using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Animator animator;
    public HealthBar HealthBar;
    public SpriteRenderer sr;

    public PlayerInvulnerable playerInvulnerable;

    public int maxHealth = 100;
    public int currentHealth;
    

    [Tooltip("Please uncheck it on production")]
    public bool needResetHP = true;

    [Header("ScriptableObjects")]
    public PlayerData playerData;

    [Header("Debug")]
    public VoidEventChannel onDebugDeathEvent;

    [Header("Broadcast event channels")]
    public VoidEventChannel onPlayerDeath;
    
    void Start()
    {
        currentHealth = maxHealth;
        HealthBar.SetMaxHealth(maxHealth);
    }

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
    // Vérifie si le joueur est invincible et si les dégâts sont infinis, dans ce cas, ne pas appliquer de dégâts
    if (playerInvulnerable.isInvulnerable && damage == float.MaxValue)
    {
        return;
    }

    // Calcul des dégâts
    float totalDamage = damage;

    // Si les dégâts sont en nombres entiers, convertissez-les en int et soustrayez-les de la santé actuelle
    if (Mathf.Approximately(damage, Mathf.Round(damage)))
    {
        int intDamage = Mathf.RoundToInt(damage);
        currentHealth -= intDamage;
    }
    else // Sinon, utilisez les dégâts float normaux
    {
        playerData.currentHealth -= damage;
        totalDamage = playerData.currentHealth <= 0 ? damage + playerData.currentHealth : damage;
        currentHealth -= Mathf.RoundToInt(totalDamage);
    }

    // Mettre à jour la barre de vie
    HealthBar.SetHealth(currentHealth);

    // Vérifie si la santé actuelle est inférieure ou égale à zéro, si oui, appelle Die()
    if (currentHealth <= 0)
    {
        Die();
    }
    else // Sinon, commence la coroutine d'invulnérabilité si nécessaire
    {
        StartCoroutine(playerInvulnerable.Invulnerable());
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
