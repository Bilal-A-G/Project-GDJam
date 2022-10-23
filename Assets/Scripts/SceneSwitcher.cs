using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneSwitcher : MonoBehaviour
{
    public float fadeSpeed;
    public int hangTime;

    public Image cover;
    public List<Scene> scenes;
    public Scene currentScene;

    private static Image _cover;
    private static List<Scene> _scenes;
    private static Scene _currentScene;

    private float _coverAlpha;
    private bool _reverse;
    private static bool _fade;

    private static GameObject _instantiatedScene;

    private void Awake()
    {
        _cover = cover;
        _scenes = scenes;
        _currentScene = currentScene;
        _instantiatedScene = currentScene.scene;
    }

    public static void Fade(string switchTo)
    {
        if(_fade) return;

        if (_scenes.Count == 0)
            return;

        for (int i = 0; i < _scenes.Count; i++)
        {
            if (switchTo != _scenes[i].name) continue;
            Destroy(_instantiatedScene);
            _currentScene = _scenes[i];
        }

        _instantiatedScene = Instantiate(_currentScene.scene);
        _instantiatedScene.SetActive(false);
        _cover.gameObject.SetActive(true);
        _fade = true;
    }

    private void Update()
    {
        if(!_fade) return;
        
        _coverAlpha += (_reverse ? -1 : 1) * Time.deltaTime * fadeSpeed;
        _coverAlpha = Mathf.Clamp(_coverAlpha, 0, 1 + hangTime);

        if (_coverAlpha == 1 + hangTime)
        {
            _instantiatedScene.SetActive(true);
            _reverse = true;
        }
        else if (_coverAlpha == 0 && _reverse)
        {
            _fade = false;
            _reverse = false;
            _cover.gameObject.SetActive(false);
            return;
        }

        _cover.color = new Color(_cover.color.r, _cover.color.g, _cover.color.b, _coverAlpha);
    }
}

[System.Serializable]
public struct Scene
{
    public string name;
    public GameObject scene;
}
