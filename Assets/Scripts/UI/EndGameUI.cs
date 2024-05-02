using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGameUI : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _quitButton;
    [SerializeField] private TextMeshProUGUI _factText;

    void Start()
    {
        _restartButton.onClick.AddListener(RestartGame);
        _quitButton.onClick.AddListener(QuitGame);
        _factText.text = GameConstants.END_GAME_FACT;
    }

    private void RestartGame()
    {
        GameManager.Instance.RestartGame();
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
