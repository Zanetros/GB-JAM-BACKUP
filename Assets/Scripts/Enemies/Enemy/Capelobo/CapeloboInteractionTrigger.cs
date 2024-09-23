using UnityEngine;

public class CapeloboInteractionTrigger : MonoBehaviour
{
    public CapeloboInteraction interaction;
    private bool isPlayerInRange = false;
    private bool interactionStarted = false;
    [SerializeField] private PauseMenu pauseMenu;
    private CapeloboInteractionManager interactionManager;

    private void Start()
    {
        pauseMenu = FindAnyObjectByType<PauseMenu>();
        interactionManager = FindObjectOfType<CapeloboInteractionManager>();
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Z))
        {
            if (pauseMenu.isMenuActive) return;

            if (!interactionStarted)
            {
                TriggerDialogue();
                interactionStarted = true;
            }
            else
            {
                interactionManager.DisplayNextSentence();
            }
        }
    }

    public void TriggerDialogue()
    {
        interactionManager.StartInteraction(interaction, gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entrou na range de: " + interaction.dialogues[0].name);
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player saiu da range de: " + interaction.dialogues[0].name);
            isPlayerInRange = false;
            interactionStarted = false;
        }
    }
}