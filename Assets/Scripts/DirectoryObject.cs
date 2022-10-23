using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Directories/New Directory")]
public class DirectoryObject : ScriptableObject
{
    public List<DirectoryObject> childDirectories;
    public new string name;
    public GameObject icons;
    public DirectoryObject parentDirectory;
    public bool textFile;
}
