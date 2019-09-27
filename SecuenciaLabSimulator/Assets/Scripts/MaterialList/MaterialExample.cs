using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MaterialExample
{
    #region Atributos
    [Header("Atributos Material")]
    public string title;
    public string nameMaterial;
    [TextArea]
    public string description;
    public Material materialSystemGO;
    #endregion

    #region Comportamiento
    /*Contructor vacío.*/
    public MaterialExample()
    {

    }

    /*Contructor para llenar todos los parámetros.*/
    public MaterialExample(string title, string nameMaterial, string description, Material materialSystemGO)
    {
        this.title = title;
        this.nameMaterial = nameMaterial;
        this.description = description;
        this.materialSystemGO = materialSystemGO;
    }
    #endregion
}

