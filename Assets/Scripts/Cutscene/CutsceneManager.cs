using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private Image cutsceneImage;
    [SerializeField] private TextMeshProUGUI cutsceneText;
    [SerializeField] private float typingSpeed = 0.05f;
    private bool isTyping = false;

    [SerializeField] private List<Cutscene> cutscenes;
    private Queue<Cutscene> cutsceneQueue;

    [SerializeField] private AudioClip audioClip;
    [SerializeField] private string nextScene;

    void Start()
    {
        cutsceneQueue = new Queue<Cutscene>();

        foreach (Cutscene cutscene in cutscenes)
        {
            cutsceneQueue.Enqueue(cutscene);
        }

        ShowNextCutscene();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isTyping)
        {
            ShowNextCutscene();
        }
    }

    public void ShowNextCutscene()
    {
        if (isTyping) return;

        if (cutsceneQueue.Count == 0)
        {
            EndCutscene();
            return;
        }

        Cutscene cutscene = cutsceneQueue.Dequeue();
        StopAllCoroutines();

        cutsceneImage.sprite = cutscene.image;

        // permite cutscenes sem imagens ou sem texto
        cutsceneImage.gameObject.SetActive(cutscene.image != null);
        cutsceneText.gameObject.SetActive(cutscene.text != null);

        StartCoroutine(TypeText(cutscene.text));
    }

    IEnumerator TypeText(string sentence)
    {
        isTyping = true;
        cutsceneText.text = "";

        foreach (char letter in sentence)
        {
            cutsceneText.text += letter;

            if (letter == ' ')
            {
                SoundFXManager.instance.PlaySoundFXClip(audioClip, transform, 1f);
            }
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }


    void EndCutscene()
    {
        Debug.Log("Cutscene terminada!");
        cutsceneImage.gameObject.SetActive(false);
        cutsceneText.text = "";

        Debug.Log("Carregando para a próxima cena.");
        SceneManager.LoadScene(nextScene);
    }
}