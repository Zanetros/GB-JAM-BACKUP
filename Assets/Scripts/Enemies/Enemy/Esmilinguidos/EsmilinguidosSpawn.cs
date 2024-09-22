using System.Collections;
using UnityEngine;

public class EsmilinguidosSpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 4f;
    private bool playerInZone = false;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = true;
            StartCoroutine(SpawnEnemies());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInZone = false;
            StopCoroutine(SpawnEnemies());
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (playerInZone)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy()
    {
        BoxCollider2D spawnArea = GetComponent<BoxCollider2D>();
        Vector2 spawnPosition = new Vector2(
            Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
            Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y)
        );

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
