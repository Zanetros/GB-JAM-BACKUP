using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static event Action LightTriggerEvent;
    public static event Action EnemyTriggerEvent;

    [Header("Outras Variaveis")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject enemy;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerController.isRefilled || collision.gameObject.CompareTag("Player") && playerController.lightObject.haveKilled)
        {
            if (playerController.isChasing)
            {
                Debug.Log("ja ta persguindo");
            }

            else
            {
                playerController.isRefilled = false;
                playerController.lightObject.haveKilled = false;
                playerController.isChasing = true;
                enemy.SetActive(true);
                Destroy(gameObject);
                Debug.Log("homi spawnado");
            }
        }
    }

    public void Update()
    {
        
    }
}
