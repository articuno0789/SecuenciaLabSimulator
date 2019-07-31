using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModuleExample
{
    public string title;
    public string nameModule;
    [TextArea]
    public string description;
    public GameObject modelSystemGO;
    public Vector3 modelPosition, modelRotation, modelScale;

    public ModuleExample()
    {

    }

    public ModuleExample(string title, string nameModule, string description, GameObject modelSystemGO,
        Vector3 modelPosition, Vector3 modelRotation, Vector3 modelScale)
    {
        this.title = title;
        this.nameModule = nameModule;
        this.description = description;
        this.modelSystemGO = modelSystemGO;
        this.modelPosition = modelPosition;
        this.modelRotation = modelRotation;
        this.modelScale = modelScale;
    }
}
