using System;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private GameObject[] masterIndicators;
    [SerializeField] private GameObject[] musicIndicators;
    [SerializeField] private GameObject[] sfxIndicators;
    private const int maxVolumeLevel = 10;

    private GameObject[][] volumeBars;
    [SerializeField] private TextMeshProUGUI[] volumeTexts;
    private int selectedBarIndex = 0;

    [SerializeField] private AudioClip navigationAudioClip;
    [SerializeField] private AudioClip confirmAudioClip;

    private void Start()
    {
        UpdateAllVolumes();
        InitializeVolumeBars();
        ResetTextColors();
    }

    private void InitializeVolumeBars()
    {
        volumeBars = new GameObject[][] { masterIndicators, sfxIndicators, musicIndicators };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SoundFXManager.instance.PlaySoundFXClip(navigationAudioClip, transform, 1f);
            selectedBarIndex = (selectedBarIndex > 0) ? selectedBarIndex - 1 : volumeBars.Length - 1;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SoundFXManager.instance.PlaySoundFXClip(navigationAudioClip, transform, 1f);
            selectedBarIndex = (selectedBarIndex < volumeBars.Length - 1) ? selectedBarIndex + 1 : 0;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SoundFXManager.instance.PlaySoundFXClip(confirmAudioClip, transform, 1f);
            IncreaseVolume(selectedBarIndex);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SoundFXManager.instance.PlaySoundFXClip(confirmAudioClip, transform, 1f);
            DecreaseVolume(selectedBarIndex);
        }

        UpdateAllVolumes();
        UpdateTextColors();
    }

    public void UpdateAllVolumes()
    {
        SetIndicatorsActive(masterIndicators, true);
        SetIndicatorsActive(sfxIndicators, true);
        SetIndicatorsActive(musicIndicators, true);

        UpdateVolumeFromMixer("masterVolume", masterIndicators);
        UpdateVolumeFromMixer("musicVolume", musicIndicators);
        UpdateVolumeFromMixer("soundFXVolume", sfxIndicators);
    }

    private void UpdateVolumeFromMixer(string parameterName, GameObject[] indicators)
    {
        audioMixer.GetFloat(parameterName, out float value);
        int level = Mathf.Clamp(Mathf.RoundToInt(Mathf.Pow(10, value / 20) * maxVolumeLevel), 0, maxVolumeLevel);

        for (int i = 0; i < indicators.Length; i++)
        {
            indicators[i].SetActive(i < level);
        }
    }

    private void SetIndicatorsActive(GameObject[] indicators, bool isActive)
    {
        foreach (var indicator in indicators)
        {
            indicator.SetActive(isActive);
        }
    }

    private void IncreaseVolume(int index)
    {
        if (index == 0) IncreaseMasterVolume();
        else if (index == 1) IncreaseMusicVolume();
        else if (index == 2) IncreaseSFXVolume();
    }

    private void DecreaseVolume(int index)
    {
        if (index == 0) DecreaseMasterVolume();
        else if (index == 1) DecreaseMusicVolume();
        else if (index == 2) DecreaseSFXVolume();
    }

    public void IncreaseMasterVolume() => UpdateVolume(masterIndicators, "masterVolume", true);
    public void DecreaseMasterVolume() => UpdateVolume(masterIndicators, "masterVolume", false);
    public void IncreaseSFXVolume() => UpdateVolume(sfxIndicators, "soundFXVolume", true);
    public void DecreaseSFXVolume() => UpdateVolume(sfxIndicators, "soundFXVolume", false);
    public void IncreaseMusicVolume() => UpdateVolume(musicIndicators, "musicVolume", true);
    public void DecreaseMusicVolume() => UpdateVolume(musicIndicators, "musicVolume", false);

    private void UpdateVolume(GameObject[] indicators, string parameterName, bool increase)
    {
        int level = GetCurrentLevel(indicators);

        if (increase && level < indicators.Length)
        {
            indicators[level].SetActive(true);
            level++;
        }
        else if (!increase && level > 0)
        {
            level--;
            indicators[level].SetActive(false);
        }

        audioMixer.SetFloat(parameterName, level > 0 ? Mathf.Log10((float)level / indicators.Length) * 20f : -80f);
    }


    private void ResetTextColors()
    {
        Color32 defaultColor = new Color32(76, 131, 92, 255); // 4C835C
        foreach (var text in volumeTexts)
        {
            text.color = defaultColor;
        }
    }

    private void UpdateTextColors()
    {
        Color32 selectedColor = new Color32(223, 250, 218, 255); // DFFADA
        ResetTextColors();

        volumeTexts[selectedBarIndex].color = selectedColor;
    }

    private int GetCurrentLevel(GameObject[] indicators)
    {
        for (int i = indicators.Length - 1; i >= 0; i--)
        {
            if (indicators[i].activeSelf) return i + 1;
        }
        return 0;
    }
}
