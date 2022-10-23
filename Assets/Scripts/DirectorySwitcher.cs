using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DirectorySwitcher : MonoBehaviour
{
    public DirectoryObject currentDirectory;
    public Transform iconParent;
    public SceneFader screenFlicker;

    private GameObject _instantiatedDirectory;
    private bool _isInstantiatedDirectoryNotNull;

    private void Awake()
    {
        InstantiateDirectory(currentDirectory);
        _isInstantiatedDirectoryNotNull = _instantiatedDirectory != null;
    }

    public bool SwitchDirectory(string switchTo)
    {
        if (currentDirectory.parentDirectory != null && switchTo == "..")
        {
            currentDirectory = currentDirectory.parentDirectory;
            InstantiateDirectory(currentDirectory);
            return true;
        }

        foreach (DirectoryObject directory in currentDirectory.childDirectories.Where(directory => directory.name == switchTo))
        {
            currentDirectory = directory;
            InstantiateDirectory(currentDirectory);
            return true;
        }

        return false;
    }

    private void InstantiateDirectory(DirectoryObject directory)
    {
        if (_isInstantiatedDirectoryNotNull)
        {
            Destroy(_instantiatedDirectory);
            screenFlicker.Fade();
        }

        _instantiatedDirectory = Instantiate(directory.icons, iconParent);
        _isInstantiatedDirectoryNotNull = true;
    }
}