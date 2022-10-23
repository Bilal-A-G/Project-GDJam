using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DirectorySwitcher : MonoBehaviour
{
    public List<Ending> endings;
    
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

    public bool SwitchDirectory(string switchTo, FileType fileType)
    {
        if (currentDirectory.parentDirectory != null && switchTo == "..")
        {
            currentDirectory = currentDirectory.parentDirectory;
            InstantiateDirectory(currentDirectory);
            return true;
        }

        foreach (DirectoryObject directory in currentDirectory.childDirectories.Where(directory => directory.name == switchTo))
        {
            if (directory.type != fileType)
                return false;

            for (int i = 0; i < endings.Count; i++)
            {
                if (endings[i].directory != directory) continue;
                
                SceneSwitcher.Fade(endings[i].endingScene);
                return true;
            }
            
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

        if (currentDirectory.icons == null) return;
        
        _instantiatedDirectory = Instantiate(directory.icons, iconParent);
        _isInstantiatedDirectoryNotNull = true;
    }
}

[System.Serializable]
public struct Ending
{
    public DirectoryObject directory;
    public string endingScene;
}