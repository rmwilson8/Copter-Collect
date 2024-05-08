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

    private PlayerStateMachine _playerStateMachine;
    private CollectorManager _collectorManager;
    private InputReader _inputReader;

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
        _playerStateMachine = GameObject.FindFirstObjectByType<PlayerStateMachine>();
        _collectorManager = GameObject.FindFirstObjectByType<CollectorManager>();
        _inputReader = GameObject.FindFirstObjectByType<InputReader>();
        _collectorManager.OnLevelCompleted += HandleOnLevelCompleted;
        _playerStateMachine.OnFuelEmptyEvent += HandleFuelOut;
        _inputReader.OnToggleControlsEvent += InputReader_OnToggleControlsEvent;
        SceneManager.sceneLoaded += HandleSceneLoaded;
    }

    private void OnDisable()
    {
        if(_collectorManager != null)
        {
            _collectorManager.OnLevelCompleted -= HandleOnLevelCompleted;
        }

        if(_inputReader != null)
        {
            _inputReader.OnToggleControlsEvent -= InputReader_OnToggleControlsEvent;
        }

        SceneManager.sceneLoaded -= HandleSceneLoaded;
    }

    private void HandleSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if(!(arg0.buildIndex % 2 ==0)) // if the current scene has an odd build index nor is the final scene aka the end game scene
        {
            IsPlaying = true;
            _playerStateMachine = GameObject.FindFirstObjectByType<PlayerStateMachine>();
            _collectorManager = GameObject.FindFirstObjectByType<CollectorManager>();
            _inputReader = GameObject.FindFirstObjectByType<InputReader>();

            if(_collectorManager != null) // end menu scene does not have a collector manager
            {
                _collectorManager.OnLevelCompleted += HandleOnLevelCompleted;
            }

            if(_inputReader != null) 
            {
                _inputReader.OnToggleControlsEvent += InputReader_OnToggleControlsEvent;
            }
        }
    }

    private void HandleOnLevelCompleted(object sender, EventArgs e)
    {
        IsPlaying = false;
        StartCoroutine(TransitionToNextSceneRoutine());
        Debug.Log("Moving top next scene");
    }

    private void HandleFuelOut(object sender, EventArgs e)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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


    private void InputReader_OnToggleControlsEvent(object sender, bool checkControls)
    {
        if(checkControls)
        {
            Time.timeScale = 0f;
            FindFirstObjectByType<AudioSource>().volume = 0f;
        }

        else
        {
            Time.timeScale = 1f;
            FindFirstObjectByType<AudioSource>().volume = 1f;
        }
    }
}
