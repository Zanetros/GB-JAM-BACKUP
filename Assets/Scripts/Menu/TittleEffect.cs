using UnityEngine;
using System.Collections;
using TMPro;

public class TittleEffect : MonoBehaviour
{
    private TextMeshProUGUI titleText;
    [SerializeField] private float blinkDuration = 0.5f;
    [SerializeField] private AudioClip audioClip;
    private void Awake()
    {
        titleText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            SoundFXManager.instance.PlaySoundFXClip(audioClip, transform, 1f);
            titleText.color = new Color(titleText.color.r, titleText.color.g, titleText.color.b, 1f);
            yield return new WaitForSeconds(blinkDuration);

            titleText.color = new Color(titleText.color.r, titleText.color.g, titleText.color.b, 0f);
            yield return new WaitForSeconds(blinkDuration);
        }
    }
}
