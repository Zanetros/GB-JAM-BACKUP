using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstScreen : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(PlaySoundAndLoadScene());
        }
    }

    private IEnumerator PlaySoundAndLoadScene()
    {
        SoundFXManager.instance.PlaySoundFXClip(audioClip, transform, 1f);

        yield return new WaitForSeconds(audioClip.length);

        SceneManager.LoadScene("Menu Principal");
    }
}
