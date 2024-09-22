using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ChangeVignette : MonoBehaviour
{    
    [Header("Variaveis para Lerp")]
    [SerializeField] private float finalIntensity;
    [SerializeField] private float amount;

    [Header("Post Processing")]
    public Volume volume;
    [SerializeField] private Vignette vignette;

    private void Start()
    {
        volume.profile.TryGet<Vignette>(out vignette);
    }

    public void ChangeVignetteIntensity()
    {
        vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, finalIntensity, amount);
    }
}
