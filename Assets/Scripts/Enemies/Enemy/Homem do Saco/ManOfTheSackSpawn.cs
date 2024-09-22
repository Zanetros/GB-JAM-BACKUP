using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ManOfTheSackSpawn : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    private void Start()
    {
        EventManager.LightTriggerEvent += EnableEnemy;
    }

    private void EnableEnemy()
    {
        enemy.SetActive(true);
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        EventManager.LightTriggerEvent -= EnableEnemy;
    }
}
