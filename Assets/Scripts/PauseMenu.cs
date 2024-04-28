using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject Container;

    public void Continue(){
        Container.SetActive(false);
        Time.timeScale = 1;
    }
}
