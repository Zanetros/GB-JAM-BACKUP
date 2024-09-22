using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CapeloboInteractionManager : MonoBehaviour
{
    public TextMeshProUGUI interactionText;
    public TextMeshProUGUI nameText;
    public GameObject interactionBox;
    private Queue<string> sentences;
    private bool isTyping = false;
    private PlayerController controller;

    public event Action OnDialogueEnded;
    private CapeloboInteraction currentInteraction;

    void Start()
    {
        sentences = new Queue<string>();
        controller = FindFirstObjectByType<PlayerController>();
    }

    public void StartInteraction(CapeloboInteraction interaction, GameObject character)
    {
        currentInteraction = interaction;
        controller.SetDialogueState(true);
        sentences.Clear();

        foreach (CapeloboDialogueEntry entry in interaction.dialogues)
        {
            entry.character = character;
            sentences.Enqueue(entry.sentence);
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
        CapeloboDialogueEntry currentEntry = currentInteraction.dialogues[currentInteraction.dialogues.Count - sentences.Count - 1];

        nameText.text = currentEntry.name;

        if (currentEntry.shouldTransform && currentEntry.character != null)
        {
            Debug.Log("Transformação chamada para: " + currentEntry.character.name);
            Capelobo capelobo = currentEntry.character.GetComponent<Capelobo>();

            if (capelobo != null)
            {
                capelobo.Transform();
            }
            else
            {
                Debug.LogError("Capelobo não encontrado no personagem.");
            }
        }

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

        if (currentInteraction?.dialogues != null && currentInteraction.dialogues.Count > 0)
        {
            CapeloboDialogueEntry lastEntry = currentInteraction.dialogues[currentInteraction.dialogues.Count - 1];
            if (lastEntry.character != null)
            {
                Capelobo capelobo = lastEntry.character.GetComponent<Capelobo>();
                if (capelobo != null)
                {
                    capelobo.DestroyInvisibleBarrier();
                }
                else
                {
                    Debug.LogWarning("Capelobo não encontrado no personagem.");
                }
            }
        }

        OnDialogueEnded?.Invoke();
    }

}