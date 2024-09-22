using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vida")]
    [SerializeField] private int health;
    [SerializeField] ChangeVignette vignette;

    [Header("Reload Level")]
    public GameObject restartPanel;

    public bool isRestarting;
    [SerializeField] private int currentScene;

    public static PlayerHealth instance;

    private void Update()
    {

        if (restartPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                RetryLevel();
            }
        }
    }
    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;

        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        if (health <= 1)
        {
            RetryLevelProcess();
        }
        else
        {
            health -= damage;
            vignette.ChangeVignetteIntensity();
        }
    }

    private void RetryLevelProcess()
    {
        isRestarting = true;
        restartPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f;
        isRestarting = false;
        SceneManager.LoadScene(currentScene);
    }
}
