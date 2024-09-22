using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CapeloboDialogueEntry
{
    public string name;
    [TextArea(3, 10)]
    public string sentence;
    public bool shouldTransform;
    [HideInInspector] public GameObject character;
}

[Serializable]
public class CapeloboInteraction
{
    public List<CapeloboDialogueEntry> dialogues;
}
