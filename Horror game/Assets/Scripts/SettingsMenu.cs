using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
//using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown GraphicsDropdown;
    Resolution[] resolutions;

    public static Action<bool> OnGamePaused;
    [SerializeField] bool isPaused = false;
    bool isGameOver;

    public GameObject PauseMenuUI;
    public GameObject GameOverUI;

    public GameObject VictoryUI;
    [SerializeField] PlayerStateMachine player;
    void OnEnable()
    {
        GameLoopManager.OnGameUpdate += TurnOnVictoryOrFailUI;
    }

    void OnDisable()
    {
        GameLoopManager.OnGameUpdate -= TurnOnVictoryOrFailUI;
    }
    void Start()
    {
        GetResolutions();
        GetQuality();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseStateChecker(GameLoopManager.currentGameState);
        }
        // if (Input.GetKeyDown(KeyCode.O)) ToggleGameOverUI(true);
    }

    void PauseStateChecker(GameLoopManager.GameState gameState)
    {
        switch (gameState)
        {
            case GameLoopManager.GameState.GameStart:
                TogglePauseUI();
                break;
            case GameLoopManager.GameState.LootCollected:
                TogglePauseUI();
                break;
            case GameLoopManager.GameState.GatesOpen:
                TogglePauseUI();
                break;
            case GameLoopManager.GameState.Victory:
                //
                break;
            case GameLoopManager.GameState.Lose:
                break;
            default:
                break;
        }
    }

    public void GetQuality()
    {
        var currentQuality = QualitySettings.GetQualityLevel();
        Debug.Log("currentQuality" + currentQuality);
        GraphicsDropdown.value = currentQuality;
        GraphicsDropdown.RefreshShownValue();
    }

    public void GetResolutions()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
               resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        Debug.Log("SetResolution --- " + resolution.width + " x " + resolution.height);
    }
    public void SetVolume(float volume)
    {
        //audioMixer.SetFloat("volume", volume);
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }
    public void SetQuality(int qualityIndex)
    {

        QualitySettings.SetQualityLevel(qualityIndex);
        Debug.Log("QualitySettings.SetQualityLevel --- " + qualityIndex);
        //Debug.Log("GetQualityLevel() -------" + QualitySettings.GetQualityLevel());
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    void TurnOnVictoryOrFailUI(GameLoopManager.GameState gameState)
    {
        switch (gameState)
        {
            case GameLoopManager.GameState.Lose:
                isGameOver = true;
                ToggleGameOverUI(isGameOver);
                break;
            case GameLoopManager.GameState.Victory:
                StartCoroutine(DelayedVictoryUI());
                break;
            default:
                break;
        }

    }
    public void TogglePauseUI()
    {
        Debug.Log("TogglePauseUI");
        if (isPaused)
        {
            TurnOffPauseUI();
        }
        else if (!isPaused)
        {
            TurnOnPauseUI();
        }
    }

    public void TurnOnPauseUI()
    {
        if (PauseMenuUI != null)
        {
            PauseGame();
            PauseMenuUI.SetActive(true);
        }
    }

    public void TurnOffPauseUI()
    {
        if (PauseMenuUI != null)
        {
            PauseGame();
            PauseMenuUI.SetActive(false);
        }
    }

    public void ExitToMainMenu() => SceneManager.LoadScene("MainMenu");

    public void ToggleGameOverUI(bool gameOver)
    {
        GameOverUI.SetActive(gameOver);
        PauseGame();
    }
    public void PauseGame()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            OnGamePaused?.Invoke(false);
        }
        else if (!isPaused)
        {
            isPaused = true;
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            OnGamePaused?.Invoke(true);
        }
    }



    IEnumerator DelayedVictoryUI()
    {
        VictoryUI.SetActive(true);
        yield return new WaitForSeconds(3); //
        PauseGame();
    }

    public void LoadNextMission()
    {
        //
    }

    public void LoadBase()
    {
        //
    }


    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
        GameLoopManager.instance.UpdateGameState(GameLoopManager.GameState.GatesOpen);
    }

}
