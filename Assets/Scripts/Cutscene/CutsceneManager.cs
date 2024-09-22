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
        if (Input.GetMouseButtonDown(0) && !isTyping)
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

        foreach (char letter in sentence.ToCharArray())
        {
            cutsceneText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void EndCutscene()
    {
        Debug.Log("Cutscene terminada!");
        cutsceneImage.gameObject.SetActive(false);
        cutsceneText.text = "";

        if (SceneManager.GetActiveScene().name == "cena1")
        {
            Debug.Log("Carregando para a próxima cena.");
            SceneManager.LoadScene("cena2");
        }
    }
}