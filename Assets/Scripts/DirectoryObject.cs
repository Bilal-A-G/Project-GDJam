using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public enum FileType
{
    Text,
    Image,
    Executable,
    Folder
}

    [CreateAssetMenu(menuName = "Directories/New Directory")]
public class DirectoryObject : ScriptableObject
{
    public List<DirectoryObject> childDirectories;
    public new string name;
    public GameObject icons;
    public DirectoryObject parentDirectory;
    public FileType type;
}
