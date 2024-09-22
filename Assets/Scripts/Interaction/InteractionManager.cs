using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public TextMeshProUGUI interactionText;
    public TextMeshProUGUI nameText;
    public GameObject interactionBox;
    private Queue<string> sentences;
    private bool isTyping = false;
    private PlayerController controller;

    public event Action OnDialogueEnded;

    void Start()
    {
        sentences = new Queue<string>();
        controller = FindFirstObjectByType<PlayerController>();
    }

    public void StartInteraction(Interaction interaction)
    {
        Debug.Log("Interagindo com: " + string.Join(", ", interaction.dialogues.ConvertAll(d => d.name)));
        controller.SetDialogueState(true);
        sentences.Clear();

        if (interaction.dialogues.Count > 0)
        {
            foreach (var entry in interaction.dialogues)
            {
                nameText.text = entry.name;
                sentences.Enqueue(entry.sentence);
            }
        }
        else
        {
            nameText.text = "";
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (isTyping) return;

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        interactionBox.SetActive(true);
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        interactionText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            interactionText.text += letter;
            yield return null;
        }
        isTyping = false;
    }

    public void EndDialogue()
    {
        Debug.Log("Interação foi terminada!");
        interactionText.text = "";
        nameText.text = "";
        controller.SetDialogueState(false);
        interactionBox.SetActive(false);

        OnDialogueEnded?.Invoke();
    }
}
