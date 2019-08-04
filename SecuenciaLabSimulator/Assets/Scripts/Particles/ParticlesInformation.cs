using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ParticlesInformation
{
    #region Atributos
    public string title;
    public string nameModel;
    [TextArea]
    public string description;
    public GameObject modelSystemGO;
    public Vector3 modelPosition, modelRotation, modelScale;
    #endregion

    #region Inicializacion
    /*Contructor vacío.*/
    public ParticlesInformation()
    {

    }

    /*Contructor donde se especifica el valor para todos sus atributos.*/
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
    #endregion
}
