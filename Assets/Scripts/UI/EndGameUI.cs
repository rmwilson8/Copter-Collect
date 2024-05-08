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
    [SerializeField] private float _restartTime = 2f;

    void Start()
    {
        _restartButton.onClick.AddListener(RestartGame);
        _quitButton.onClick.AddListener(QuitGame);
        _factText.text = GameConstants.END_GAME_FACT;
    }

    private void RestartGame()
    {
        StartCoroutine(RestartGameRoutine());
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator RestartGameRoutine()
    {
        yield return new WaitForSeconds(_restartTime);
        GameManager.Instance.RestartGame();
    }
}
