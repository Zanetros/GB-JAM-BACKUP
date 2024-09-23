using UnityEngine;

public class CapeloboProjectile : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }

    private void Start()
    {
        Destroy(gameObject, 10f);
    }

    private void Update()
    {
        if (direction != Vector2.zero)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
