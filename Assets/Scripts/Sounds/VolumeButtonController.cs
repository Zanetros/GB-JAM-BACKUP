using UnityEngine;
using UnityEngine.UI;

public class VolumeButtonController : MonoBehaviour
{
    [SerializeField] private SoundMixerManager soundMixerManager;
    [SerializeField] private float volumeStep = 0.1f;
    [SerializeField] private Text volumeText;
    private float currentVolume = 1f;

    public void IncreaseVolume()
    {
        if (currentVolume < 1f)
        {
            currentVolume += volumeStep;
            currentVolume = Mathf.Clamp(currentVolume, 0f, 1f);
            // soundMixerManager.SetMasterVolume(currentVolume);
            UpdateVolumeText();
        }
    }

    public void DecreaseVolume()
    {
        if (currentVolume > 0f)
        {
            currentVolume -= volumeStep;
            currentVolume = Mathf.Clamp(currentVolume, 0f, 1f);
            //soundMixerManager.SetMasterVolume(currentVolume);
            UpdateVolumeText();
        }
    }

    private void UpdateVolumeText()
    {
        if (volumeText != null)
        {
            volumeText.text = Mathf.RoundToInt(currentVolume * 100).ToString() + "%";
        }
    }
}
