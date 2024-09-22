using UnityEngine;

public class Capelobo : MonoBehaviour, IAnimation
{
    private Animator animator;
    [SerializeField] private GameObject invisibleBarrier;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Transform()
    {
        if (animator != null)
        {
            animator.SetTrigger("transform");
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
}