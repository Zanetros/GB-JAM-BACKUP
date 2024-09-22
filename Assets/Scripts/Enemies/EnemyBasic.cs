using UnityEngine;

public class EnemyBasic : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;

    private void OnEnable()
    {
        playerHealth = FindAnyObjectByType<PlayerHealth>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth.TakeDamage(1);
        }
    }
}
