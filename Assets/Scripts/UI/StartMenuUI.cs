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

    private void Start()
    {
        _factText.text = GameConstants.START_MENU_FACT;
    }

    public void StartGame()
    {
        StartCoroutine(StartGameRoutine());
    }

    public void StartEndlessGame()
    {
        StartCoroutine(StartEndlessGameRoutine());
    }

    public void QuitGame()
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
