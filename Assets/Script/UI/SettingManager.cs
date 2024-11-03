using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [SerializeField] private Slider masterVolume;
    [SerializeField] private Slider musicVolume;
    [SerializeField] private Slider sfxVolume;
    [SerializeField] private Button closeMenuButton;
    [SerializeField] private Button quitGameButton;


    private void Start()
    {
        GetVolumeData();
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void GetVolumeData()
    {
        Volume volumes = LocalData.instance.GetVolume();

        masterVolume.SetValueWithoutNotify(volumes.master);
        musicVolume.SetValueWithoutNotify(volumes.music);
        sfxVolume.SetValueWithoutNotify(volumes.sfx);
    }


    public void SetMasterVolume(float volume)
    {
        AudioManager.instance.SetMasterVolume(volume);
    }

    public void SetMusicVolume(float volume)
    {
        AudioManager.instance.SetMusicVolume(volume);
    }

    public void SetSFXVolume(float volume)
    {
        AudioManager.instance.SetSFXVolume(volume);
    }
}
