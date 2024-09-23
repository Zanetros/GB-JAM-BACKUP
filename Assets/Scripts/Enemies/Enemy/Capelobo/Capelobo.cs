using System.Collections;
using UnityEngine;

public class Capelobo : MonoBehaviour, IAnimation
{
    private Animator animator;
    private Enemy enemy;
    [SerializeField] private GameObject invisibleBarrier;
    [SerializeField] private GameObject projectilePrefab;

    [Header("Configurações de Ataque")]
    [SerializeField] private float attackInterval = 1f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();  
    }

    public void Transform()
    {
        if (animator != null)
        {
            animator.SetTrigger("transform");
            StartCoroutine(WaitTransform());
            Debug.Log("Começando a animação!");
        }
        else
        {
            Debug.LogWarning("Animator não encontrado no objeto.");
        }
    }

    public void DestroyInvisibleBarrier()
    {
        Destroy(invisibleBarrier);
    }

    public void StartAttacking()
    {
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            Vector3 playerPosition = FindPlayerPosition();
            if (playerPosition != Vector3.zero)
            {
                FireProjectile();
                yield return new WaitForSeconds(attackInterval);
            }

            yield return null;
        }
    }

    private Vector3 FindPlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        return player != null ? player.transform.position : Vector3.zero;
    }

    private void FireProjectile()
    {
        if (projectilePrefab != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Vector3 playerPosition = FindPlayerPosition();

            if (playerPosition != Vector3.zero)
            {
                Vector2 direction = (playerPosition - transform.position).normalized;
                CapeloboProjectile projectileScript = projectile.GetComponent<CapeloboProjectile>();
                if (projectileScript != null)
                {
                    projectileScript.SetDirection(direction);
                }
            }

            Destroy(projectile, 3);
            Debug.Log("Capelobo lançou um projétil!");
        }
    }

    public IEnumerator WaitTransform()
    {
        yield return new WaitForSeconds(1);
        enemy.enabled = true;
        animator.SetTrigger("transform");
    }
}
