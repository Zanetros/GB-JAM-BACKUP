using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public bool isHiding;
    public bool isRefilled;
    public bool isChasing;
    public Light lightObject;

    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PauseMenu pauseMenu;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private SpriteRenderer lightRenderer;
    [SerializeField] private Collider2D playerCollider;
    private PlayerLightShot playerLightShot;
    private SpriteRenderer spriteRenderer;

    private bool isDialogueActive = false;

    private float normalSpeed;
    public float mudSpeed = 2.5f;


    private void Start()
    {
        playerLightShot = GetComponent<PlayerLightShot>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement.getSpriteRenderer(spriteRenderer);

        lightObject = FindFirstObjectByType<Light>();
        lightObject.gameObject.SetActive(false);

        pauseMenu = FindAnyObjectByType<PauseMenu>();

        normalSpeed = playerMovement.moveSpeed;
    }

    private void Update()
    {
        if (!isDialogueActive && !pauseMenu.isMenuActive && !playerHealth.isRestarting)
        {
            Vector2 movement = playerMovement.HandleMovement();

            playerMovement.UpdateSprite(movement.x, movement.y);

            playerLightShot.ToggleLightning(lightObject.gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (!isDialogueActive && !pauseMenu.isMenuActive)
        {
            transform.Translate(playerMovement.moveSpeed * Time.fixedDeltaTime * playerMovement.HandleMovement());
        }
    }

    public void SetDialogueState(bool state)
    {
        isDialogueActive = state;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Refill"))
        {
            lightObject.RechargeLight(collider.gameObject);
            isRefilled = true;
        }

        if (collider.CompareTag("Bush"))
        {
            isHiding = true;
            spriteRenderer.renderingLayerMask = 0;
            lightRenderer.renderingLayerMask = 0;
            playerCollider.isTrigger = true;
        }

        if (collider.CompareTag("Mud"))
        {
            playerMovement.moveSpeed = mudSpeed;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Bush"))
        {
            isHiding = false;
            spriteRenderer.renderingLayerMask = 3;
            lightRenderer.renderingLayerMask = 2;
            playerCollider.isTrigger = false;
        }

        if (collider.CompareTag("Mud"))
        {
            playerMovement.moveSpeed = normalSpeed;
        }
    }
}
