using System.Collections.Generic;
using TMPro;
using Unity1Week.URA.typing;
using UnityEngine;

public class TypingTest : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private TMP_InputField _inputField;
    [SerializeReference, SubclassSelector]
    private List<ICardEffect> _effect;
    private void Awake()
    {
        LoadText.AssemblyWords(LoadText.GetCSVFile());
        _text.text = LoadText.WordList[Random.Range(0, LoadText.WordList.Count)];
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Answer(_inputField.text);
        }
    }
    private void Answer(string text)
    {
        Debug.Log(text.Length);
        Debug.Log(_text.text.Length);

        if (text == _text.text)
        {
            Collect();
        }
        else
        {
            Mistake();
        }
    }

    private void Collect()
    {
        foreach (var effect in _effect)
        {
            effect.Exucute();
        }
        Debug.Log("正解");
    }

    private void Mistake()
    {
        Debug.Log("不正解、お前バカ");
    }
}
