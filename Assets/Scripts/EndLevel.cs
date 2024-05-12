using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public ParticleSystem particles;
    public AudioClip audioClip;

    [Header("Broadcast event channels")]
    public StringEventChannel onLevelEnded;
    public PlaySoundAtEventChannel sfxAudioChannel;

    public string nextLevelName; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        particles.Play();
        sfxAudioChannel.Raise(audioClip, transform.position);

        
        if (!string.IsNullOrEmpty(nextLevelName))
        {
            SceneManager.LoadScene(nextLevelName); 
        }
        else
        {
            // Si le prochain niveau n'est pas défini, cela signifie que c'est la fin du jeu
           
            onLevelEnded.Raise("EndGame");
        }
    }
    
}