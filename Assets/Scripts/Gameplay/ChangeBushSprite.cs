using UnityEngine;

public class ChangeBushSprite : MonoBehaviour
{
    private PlayerController playerController;
    private SpriteRenderer spriteRenderer;

    public Sprite emptySprite;
    public Sprite fullSprite;       

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (playerController.isHiding)
        {
            this.spriteRenderer.sprite = fullSprite;
        }

        else
        {
            this.spriteRenderer.sprite = emptySprite;
        }
    }
}
