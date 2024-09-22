using UnityEngine;

[System.Serializable]
public class PlayerMovement
{
    public float moveSpeed = 5f;
    private Vector2 movement;

    [HideInInspector] public Animator animator;

    public Sprite spriteUp;
    public Sprite spriteDown;
    public Sprite spriteLeft;
    public Sprite spriteRight;

    private SpriteRenderer spriteRenderer;

    public Vector2 HandleMovement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if (movement != Vector2.zero)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }

        animator.SetFloat("Speed", movement.sqrMagnitude);

        if (moveX != 0)
        {
            moveY = 0;
        }

        movement = new Vector2(moveX, moveY).normalized;
        return movement;
    }
    public void getSpriteRenderer(SpriteRenderer spriteRenderer)
    {
        this.spriteRenderer = spriteRenderer;
    }

    public void UpdateSprite(float moveX, float moveY)
    {
        if (moveX > 0)
        {
            spriteRenderer.sprite = spriteRight;
            // animator.SetBool("right", true);
        }
        else if (moveY > 0)
        {
            spriteRenderer.sprite = spriteUp;
            // animator.SetBool("up", true);
        }
        else if (moveX < 0)
        {
            // animator.SetBool("left", true);
        }
        else if (moveY < 0)
        {
            // animator.SetBool("down", true);
        }
    }
}