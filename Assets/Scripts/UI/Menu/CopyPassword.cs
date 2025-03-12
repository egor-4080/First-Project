using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CopyPassword : MonoBehaviour
{
    [SerializeField] private TMP_InputField password;

    public void CopyPasswordToBuffer()
    {
        //GUIUtility.systemCopyBuffer = password.text;
        TextEditor textEditor = new TextEditor();
        textEditor.text = password.text;
        textEditor.SelectAll();
        textEditor.Copy();
    }
}