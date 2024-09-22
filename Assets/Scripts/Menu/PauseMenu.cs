using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private Button[] menuButtons;
    [SerializeField] private GameObject indicator;
    public bool isMenuActive;
    private int selectedButtonIndex = 0;

    [SerializeField] private AudioClip navigationAudioClip;
    [SerializeField] private AudioClip confirmAudioClip;

    private void Update()
    {
        PauseGame();
        if (isMenuActive && !settingsMenu.activeSelf)
        {
            NavigateMenu();
        }
    }

    public void Resume()
    {
        menu.SetActive(false);
        isMenuActive = false;
        Time.timeScale = 1;
    }

    public void Settings()
    {
        settingsMenu.SetActive(true);
        menu.SetActive(false);
    }

    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
        menu.SetActive(true);
        selectedButtonIndex = 0;
        UpdateButtonSelection();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu Principal");
    }

    private void PauseGame()
    {
        if (!isMenuActive && Input.GetKeyDown(KeyCode.Escape))
        {
            SoundFXManager.instance.PlaySoundFXClip(navigationAudioClip, transform, 1f);
            menu.SetActive(true);
            isMenuActive = true;
            Time.timeScale = 0;
            UpdateButtonSelection();
        }
        else if (isMenuActive && Input.GetKeyDown(KeyCode.Escape))
        {
            SoundFXManager.instance.PlaySoundFXClip(navigationAudioClip, transform, 1f);
            if (settingsMenu.activeSelf)
            {
                CloseSettings();
            }
            else
            {
                Resume();
            }
        }

        if (settingsMenu.activeSelf && Input.GetKeyDown(KeyCode.X))
        {
            SoundFXManager.instance.PlaySoundFXClip(confirmAudioClip, transform, 1f);
            CloseSettings();
        }
        else if (isMenuActive && Input.GetKeyDown(KeyCode.X))
        {
            SoundFXManager.instance.PlaySoundFXClip(confirmAudioClip, transform, 1f);
            Resume();
        }
    }

    private void NavigateMenu()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SoundFXManager.instance.PlaySoundFXClip(navigationAudioClip, transform, 1f);
            selectedButtonIndex = (selectedButtonIndex > 0) ? selectedButtonIndex - 1 : menuButtons.Length - 1;
            UpdateButtonSelection();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SoundFXManager.instance.PlaySoundFXClip(navigationAudioClip, transform, 1f);
            selectedButtonIndex = (selectedButtonIndex < menuButtons.Length - 1) ? selectedButtonIndex + 1 : 0;
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
        RectTransform buttonRect = menuButtons[selectedButtonIndex].GetComponent<RectTransform>();
        indicator.SetActive(true);
        indicator.transform.position = buttonRect.position + new Vector3(-2, 0, 0);
    }
}
