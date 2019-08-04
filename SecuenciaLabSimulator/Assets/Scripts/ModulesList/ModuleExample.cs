using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModuleExample
{
    #region Atributos
    public string title;
    public string nameModule;
    [TextArea]
    public string description;
    public GameObject modelSystemGO;
    public Vector3 modelPosition, modelRotation, modelScale;
    #endregion

    #region Comportamiento
    /*Contructor vacío.*/
    public ModuleExample()
    {

    }

    /*Contructor para llenar todos los parámetros.*/
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
    #endregion
}
