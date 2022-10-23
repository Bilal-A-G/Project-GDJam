using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DirectorySwitcher : MonoBehaviour
{
    public DirectoryObject currentDirectory;
    public Transform iconParent;
    public SceneSwitcher screenFlicker;
    public float directorySwitchSpeed;

    private GameObject _instantiatedDirectory;
    private bool _isInstantiatedDirectoryNotNull;

    private void Awake()
    {
        StartCoroutine(InstantiateDirectory(currentDirectory));
        _isInstantiatedDirectoryNotNull = _instantiatedDirectory != null;
    }

    public bool SwitchDirectory(string switchTo)
    {
        if (currentDirectory.parentDirectory != null && switchTo == "..")
        {
            currentDirectory = currentDirectory.parentDirectory;
            StartCoroutine(InstantiateDirectory(currentDirectory));
            return true;
        }

        foreach (DirectoryObject directory in currentDirectory.childDirectories.Where(directory => directory.name == switchTo))
        {
            currentDirectory = directory;
            StartCoroutine(InstantiateDirectory(currentDirectory));
            return true;
        }

        return false;
    }

    private IEnumerator InstantiateDirectory(DirectoryObject directory)
    {
        if (_isInstantiatedDirectoryNotNull)
        {
            Destroy(_instantiatedDirectory);
            yield return new WaitForSeconds(1 / directorySwitchSpeed);
            screenFlicker.Fade();
        }

        _instantiatedDirectory = Instantiate(directory.icons, iconParent);
        _isInstantiatedDirectoryNotNull = true;
    }
}