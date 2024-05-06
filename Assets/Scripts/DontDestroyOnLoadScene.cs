using UnityEngine;

public class DontDestroyOnLoadScene : MonoBehaviour
{
    private static DontDestroyOnLoadScene _instance;

    // Assurez-vous que l'instance est unique dans la scène
    public static DontDestroyOnLoadScene instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DontDestroyOnLoadScene>();
                if (_instance == null)
                {
                    GameObject singleton = new GameObject(typeof(DontDestroyOnLoadScene).Name);
                    _instance = singleton.AddComponent<DontDestroyOnLoadScene>();
                    DontDestroyOnLoad(singleton);
                }
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RemoveFromDontDestroyOnLoad()
    {
        // Ajoutez ici le code pour retirer l'objet de la liste DontDestroyOnLoad
        // Par exemple :
        // DontDestroyOnLoad(this.gameObject);
        // ou
        // Destroy(this.gameObject);
    }
}
