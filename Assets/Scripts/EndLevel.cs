using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{
    public ParticleSystem particles;
    public string nextLevelName;
    public AudioClip audioClip;

    [Header("Broadcast event channels")]
    public StringEventChannel onLevelEnded;
    public PlaySoundAtEventChannel sfxAudioChannel;

    public string NomDeScene;



    private void OnTriggerEnter2D(Collider2D other)
    {
        particles.Play();
        sfxAudioChannel.Raise(audioClip, transform.position);
        SceneManager.LoadScene("Level2");
    }


}
