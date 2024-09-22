using System.Collections;
using UnityEngine;

public class EnemyAggroCheck : MonoBehaviour
{
    public GameObject playerTarget {  get; set; }
    [SerializeField] private Enemy enemy;

    private void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player");

        enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //enemy.SetAggroStatus(true);
            Debug.Log("AGRO ON");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //StartCoroutine(WaitForStopChasing());
        }     
    }

    public IEnumerator WaitForStopChasing()
    {
        yield return new WaitForSeconds(1.5f);
        enemy.SetAggroStatus(false);
        enemy.StateMachine.ChangeState(enemy.IdleState);
        Debug.Log("AGRO OFF");
    }    
}
