using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

[Serializable]
public class DialogueEntry
{
    public string name;
    [TextArea(3, 10)]
    public string sentence;
}

[Serializable]
public class Interaction
{
    public List<DialogueEntry> dialogues;
}
