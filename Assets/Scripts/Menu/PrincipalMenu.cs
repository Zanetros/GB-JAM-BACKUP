using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrincipalMenu : MonoBehaviour
{
    [SerializeField] private GameObject initialMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private List<Button> menuButtons;

    private int selectedButtonIndex = 0;

    [SerializeField] private AudioClip navigationAudioClip;
    [SerializeField] private AudioClip confirmAudioClip;

    [SerializeField] private Sprite defaultButtonSprite;
    [SerializeField] private Sprite hoverButtonSprite;

    private void Start()
    {
        UpdateButtonSelection();
    }

    private void Update()
    {
        NavigateMenu();

        if (settingsMenu.activeSelf && Input.GetKeyDown(KeyCode.X))
        {
            CloseSettings();
        }

        if (creditsMenu.activeSelf && Input.GetKeyDown(KeyCode.X))
        {
            CloseCredits();
        }
    }

    public void Play()
    {
    }

    public void Settings()
    {
        settingsMenu.SetActive(true);
        initialMenu.SetActive(false);
        selectedButtonIndex = 0;
        UpdateButtonSelection();
    }

    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
        initialMenu.SetActive(true);
        UpdateButtonSelection();
    }

    public void Credits()
    {
        creditsMenu.SetActive(true);
        initialMenu.SetActive(false);
        selectedButtonIndex = 0;
        UpdateButtonSelection();
    }

    public void CloseCredits()
    {
        creditsMenu.SetActive(false);
        initialMenu.SetActive(true);
        UpdateButtonSelection();
    }

    private void NavigateMenu()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SoundFXManager.instance.PlaySoundFXClip(navigationAudioClip, transform, 1f);
            selectedButtonIndex = (selectedButtonIndex > 0) ? selectedButtonIndex - 1 : menuButtons.Count - 1;
            UpdateButtonSelection();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SoundFXManager.instance.PlaySoundFXClip(navigationAudioClip, transform, 1f);
            selectedButtonIndex = (selectedButtonIndex < menuButtons.Count - 1) ? selectedButtonIndex + 1 : 0;
            UpdateButtonSelection();
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            SoundFXManager.instance.PlaySoundFXClip(confirmAudioClip, transform, 1f);
            menuButtons[selectedButtonIndex].onClick.Invoke();
        }
    }

    private void UpdateButtonSelection()
    {
        foreach (var button in menuButtons)
        {
            var image = button.GetComponent<Image>();
            image.sprite = defaultButtonSprite;
        }

        var selectedButton = menuButtons[selectedButtonIndex];
        var selectedImage = selectedButton.GetComponent<Image>();
        selectedImage.sprite = hoverButtonSprite;
    }
}
