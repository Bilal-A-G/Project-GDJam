using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    public TMP_InputField inputText;
    public TextMeshProUGUI outputText;
    public Scrollbar scrollbar;
    public DirectorySwitcher directorySwitcher;

    public string changeDirectoryCommand;

    public float typeSpeed;

    private bool _selected;
    private bool _processingInput;

    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Return) || !_selected || _processingInput) return;
        if(ProcessSpecialCases()) return;
        
        StartCoroutine(PrintText(inputText.text));
    }

    IEnumerator PrintText(string text)
    {
        char[] textChars = text.ToCharArray();
        outputText.text += "\n > $ROOT: ";
        foreach (char character in textChars)
        {
            yield return new WaitForSeconds(1 / typeSpeed);
            scrollbar.value = 0;
            outputText.text += character;
            _processingInput = true;
        }

        _processingInput = false;
    }

    private bool ProcessSpecialCases()
    {
        bool specialCaseHandled = false;
        string processedInput = inputText.text.Replace(" ", "").ToLower();

        if (processedInput == "shutdown")
        {
            Application.Quit();
            specialCaseHandled = true;
        }
        else if (processedInput[..2] == changeDirectoryCommand)
        {
            specialCaseHandled = true;

            string directoryName = inputText.text.Substring(3, processedInput.ToCharArray().Length - 2);
            
            bool success = directorySwitcher.SwitchDirectory(directoryName);
            directoryName = directorySwitcher.currentDirectory.name;
            
            StartCoroutine(success
                ? PrintText("Opened " + directoryName)
                : PrintText("Operation failed, directory or file: " + directoryName + " not found"));
        }

        return specialCaseHandled;
    }

    public void SetSelectedTrue() => _selected = true;
    public void SetSelectedFalse() => _selected = false;
}
