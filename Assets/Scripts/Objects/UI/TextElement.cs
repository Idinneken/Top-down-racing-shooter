using TMPro;
using UnityEngine;

public class TextElement : MonoBehaviour
{
    TMP_Text tmp_Text;
    public string initialText;
    internal string text;

    void OnEnable()
    {        
        tmp_Text = GetComponent<TMP_Text>();
        SetText(initialText);
    }

    public void SetText(string text_)
    {
        text = text_;
        tmp_Text.text = text_;
    }
}
