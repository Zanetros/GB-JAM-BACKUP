using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    public Interaction interaction;
    private bool isPlayerInRange = false;
    private bool interactionStarted = false;
    [SerializeField] private PauseMenu pauseMenu;
    private InteractionManager interactionManager;

    private void Start()
    {
        pauseMenu = FindAnyObjectByType<PauseMenu>();
        interactionManager = FindObjectOfType<InteractionManager>();
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
        interactionManager.StartInteraction(interaction);
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
