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
            Destroy(Instance.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        Instance = this;
    }

    private void OnEnable()
    {
        _playerMover = GameObject.FindFirstObjectByType<PlayerMover>();
        _collectorManager = GameObject.FindFirstObjectByType<CollectorManager>();
        _collectorManager.OnLevelCompleted += HandleOnLevelCompleted;
        _playerMover.OnFuelOutEvent += HandleLevelLost;
        SceneManager.sceneLoaded += HandleSceneLoaded;
    }

    private void OnDisable()
    {
        if(_collectorManager != null)
        {
            _collectorManager.OnLevelCompleted -= HandleOnLevelCompleted;

        }

        SceneManager.sceneLoaded -= HandleSceneLoaded;
    }

    private void HandleSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if(!(arg0.buildIndex % 2 ==0)) // if the current scene has an odd build index nor is the final scene aka the end game scene
        {
            IsPlaying = true;
            _playerMover = GameObject.FindFirstObjectByType<PlayerMover>();
            _collectorManager = GameObject.FindFirstObjectByType<CollectorManager>();

            if(_collectorManager != null) // end menu scene does not have a collector manager
            {
                _collectorManager.OnLevelCompleted += HandleOnLevelCompleted;
            }
        }
    }

    private void HandleLevelLost(object sender, EventArgs e)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
