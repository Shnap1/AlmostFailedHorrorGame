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

    public Action<bool> OnGamePaused;
    bool isPaused;

    public GameObject PauseMenu;

    void Start()
    {
        GetResolutions();
        TurnOffPauseUI();
        GetQuality();

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) TogglePause();
    }
    
    public void GetQuality()
    {
        var currentQuality = QualitySettings.GetQualityLevel();
        Debug.Log("currentQuality" + currentQuality);
        GraphicsDropdown.value = currentQuality;
        GraphicsDropdown.RefreshShownValue();
    }
    //
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


    public void TogglePause()
    {
        if(isPaused)
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
        if (PauseMenu)
        {
            isPaused = true;
            PauseMenu.SetActive(isPaused);
            OnGamePaused?.Invoke(isPaused);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }
    }

    public void TurnOffPauseUI()
    {
        if (PauseMenu)
        {
            isPaused = false;
            PauseMenu.SetActive(isPaused);
            OnGamePaused?.Invoke(isPaused);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
        }
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");

    }

}
