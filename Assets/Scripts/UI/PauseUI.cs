using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] Button resumeButton;
    [SerializeField] Button restartButton;
    [SerializeField] Button exitButton;

    void Start()
    {
        resumeButton.onClick.AddListener(() =>
        {
            GameRound.instance.Unpause();
        });
        exitButton.onClick.AddListener(() => {
            GameRound.instance.SaveBestScore();
            Application.Quit(); 
        });
        restartButton.onClick.AddListener(() => {
            GameRound.instance.SaveBestScore();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        });
    }
}
