using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] Button restartButton;
    [SerializeField] Button jumpButton;
    [SerializeField] Button sprintButton;

    public void startGamePlay()
    {
        SceneManager.LoadScene(1); 
    }

    public void ReloadLevel()
    {
        restartButton.gameObject.SetActive(false);
        jumpButton.gameObject.SetActive(true);
        sprintButton.gameObject.SetActive(true);    
        SceneManager.LoadScene(1);
    }
}
