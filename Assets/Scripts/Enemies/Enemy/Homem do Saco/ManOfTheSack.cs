using UnityEngine;

public class ManOfTheSack : MonoBehaviour
{
    [SerializeField] private int damageDealt;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           PlayerHealth.instance.TakeDamage(damageDealt);
        }
    }
}
