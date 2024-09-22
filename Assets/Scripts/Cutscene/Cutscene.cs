using UnityEngine;
[System.Serializable]
public class Cutscene
{
    public Sprite image;
    [TextArea(3, 10)]
    public string text;

    public Cutscene(Sprite image, string text)
    {
        this.image = image;
        this.text = text;
    }
}