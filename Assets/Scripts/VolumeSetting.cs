using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider = null;

    [SerializeField] private TextMeshProUGUI volumeLevelUI = null;

    private void Start()
    {
        LoadValues();
    }

    public void VolumeSlider()
    {
        volumeLevelUI.text = volumeSlider.value.ToString("0.0");
    }

    public void SaveVolumeLevel()
    {
        float volumeLev = volumeSlider.value;
        PlayerPrefs.SetFloat("VolumeLevel", volumeLev);
        LoadValues();
    }

    public void LoadValues()
    {
        float volumeLevel = PlayerPrefs.GetFloat("VolumeLevel");
        volumeSlider.value = volumeLevel;
        AudioListener.volume = volumeLevel;
    }

}
