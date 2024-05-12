using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverManager : MonoBehaviour
{
    [Header("Listen to event channels")]
    public VoidEventChannel onPlayerDeath;

    private void OnEnable()
    {
        onPlayerDeath.OnEventRaised += OnGameOver;
    }

    public void OnGameOver()
    {
        Debug.Log("GameOver !");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDisable()
    {
        onPlayerDeath.OnEventRaised -= OnGameOver;
      
    }
}
