using System.Collections;
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
    public string openTextFileCommand;

    public float typeSpeed;

    private bool _selected;
    private bool _processingInput;

    void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Return) || !_selected || _processingInput) return;

        ProcessText();
    }

    private void ProcessText()
    {
        string command = inputText.text.Split(" ")[0];
        string args = "";
        for (int i = 1; i < inputText.text.Split(" ").Length; i++)
        {
            args += inputText.text.Split(" ")[i];
        }

        if (inputText.text == "shutdown")
        {
            Application.Quit();
        }
        else if (command == changeDirectoryCommand || command == openTextFileCommand)
        {
            bool success = directorySwitcher.SwitchDirectory(args, command == openTextFileCommand);

            StartCoroutine(success
                ? PrintText("Opened " + directorySwitcher.currentDirectory.name)
                : PrintText("Operation failed, directory or file not found"));
        }
        else
        {
            StartCoroutine(PrintText("Command: " + command + " not found, type 'help' for a list of all commands"));
        }
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

    public void SetSelectedTrue() => _selected = true;
    public void SetSelectedFalse() => _selected = false;
}
