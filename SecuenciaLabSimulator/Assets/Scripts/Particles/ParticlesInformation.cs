using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParticlesInformation
{
    public string title;
    public string nameModel;
    [TextArea]
    public string description;
    public GameObject modelSystemGO;
    public Vector3 modelPosition, modelRotation, modelScale;

    public ParticlesInformation()
    {

    }

    public ParticlesInformation(string title, string nameModel, string description, GameObject modelSystemGO,
        Vector3 modelPosition, Vector3 modelRotation, Vector3 modelScale)
    {
        this.title = title;
        this.nameModel = nameModel;
        this.description = description;
        this.modelSystemGO = modelSystemGO;
        this.modelPosition = modelPosition;
        this.modelRotation = modelRotation;
        this.modelScale = modelScale;
    }
}
