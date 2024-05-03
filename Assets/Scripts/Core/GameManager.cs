using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool IsPlaying { get; private set; } = true;

    [SerializeField] private float _sceneTransitionTime = 3f;

    private PlayerMover _playerMover;
    private CollectorManager _collectorManager;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("More than one Game Manager in scene.");
            Destroy(Instance);
        }

        DontDestroyOnLoad(this.gameObject);
        Instance = this;
    }

    private void OnEnable()
    {
        _playerMover = GameObject.FindFirstObjectByType<PlayerMover>();
        _collectorManager = GameObject.FindFirstObjectByType<CollectorManager>();
        _collectorManager.OnLevelCompleted += HandleOnLevelCompleted;
        SceneManager.sceneLoaded += HandleSceneLoaded;
    }

    private void OnDisable()
    {
        _collectorManager.OnLevelCompleted -= HandleOnLevelCompleted;
        SceneManager.sceneLoaded -= HandleSceneLoaded;
    }

    private void HandleSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if(!(arg0.buildIndex % 2 ==0 || SceneManager.GetActiveScene().buildIndex != SceneManager.sceneCountInBuildSettings - 1)) // if the current scene does NOT have an odd build index nor is the final scene aka the end game scene
        {
            IsPlaying = true;
            _playerMover = GameObject.FindFirstObjectByType<PlayerMover>();
            _collectorManager = GameObject.FindFirstObjectByType<CollectorManager>();
            _collectorManager.OnLevelCompleted += HandleOnLevelCompleted;
        }
    }

    private void HandleOnLevelCompleted(object sender, EventArgs e)
    {
        IsPlaying = false;
        StartCoroutine(TransitionToNextSceneRoutine());
        Debug.Log("Moving top next scene");
    }

    private IEnumerator TransitionToNextSceneRoutine()
    {
        yield return new WaitForSeconds(_sceneTransitionTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //loads the next scene by build index
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
