using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuUI : MonoBehaviour
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _endlessButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private TextMeshProUGUI _factText;
    [SerializeField] private float _startTime = 2f;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(StartGame);
        _endlessButton.onClick.AddListener(StartEndlessGame);
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
        StartCoroutine(StartGameRoutine());
    }

    private void StartEndlessGame()
    {
        StartCoroutine(StartEndlessGameRoutine());
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator StartGameRoutine()
    {
        yield return new WaitForSeconds(_startTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator StartEndlessGameRoutine()
    {
        yield return new WaitForSeconds(_startTime);
        SceneManager.LoadScene("Endless");
    }
}
