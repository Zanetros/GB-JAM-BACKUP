using System.Collections;
using UnityEngine;

public class Light : MonoBehaviour
{
    [SerializeField] private Vector3 originalScale;
    public bool haveKilled;

    private void Start()
    {
        originalScale = transform.localScale;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Esmilinguido"))
        {
            Debug.Log("RECEBA A ILUMINAÇÃO");
            Destroy(collider.gameObject);
            StartCoroutine(Disable());
        }
    }
    public void RechargeLight(GameObject refillObject)
    {
        transform.localScale = originalScale;
        Destroy(refillObject);
        Debug.Log("LUZ RECUPERADA A 100%");
    }

    public IEnumerator Disable()
    {
        haveKilled = true;
        yield return new WaitForSeconds(2);
        haveKilled = false;
    }
}
