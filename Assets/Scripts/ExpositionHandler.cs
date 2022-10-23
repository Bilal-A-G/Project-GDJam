using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpositionHandler : MonoBehaviour
{
    public TextMeshProUGUI outputText;
    public Scrollbar scrollbar;
    public float typeSpeed;
    public List<ExpositionObject> exposition;
    public string gameScene;

    private bool _selected;
    private bool _processingInput;
    private int _currentIndex;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (gameScene != "")
            {
                SceneSwitcher.Fade(gameScene);
            }
        }
        
        if(_processingInput)
            return;
        
        if(exposition.Count <= _currentIndex)
            return;

        StartCoroutine(PrintText(exposition[_currentIndex].exposition, exposition[_currentIndex].delay));
    }

    private IEnumerator PrintText(string text, float delay)
    {
        _processingInput = true;

        char[] textChars = text.ToCharArray();
        outputText.text += "\n > $ROOT: ";
        foreach (char character in textChars)
        {
            yield return new WaitForSeconds(1 / typeSpeed);
            scrollbar.value = 0;
            outputText.text += character;
        }

        yield return new WaitForSeconds(delay);

        _currentIndex++;
        _processingInput = false;
    }
}

[System.Serializable]
public struct ExpositionObject
{
    public string exposition;
    public float delay;
}
