using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuUI : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private TextMeshProUGUI _factText;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(StartGame);
        _quitButton.onClick.AddListener(QuitGame);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(StartGame);
        _quitButton.onClick.RemoveListener(QuitGame);
    }

    private void Start()
    {
        _factText.text = GameConstants.START_MENU_FACT;
    }

    private void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
