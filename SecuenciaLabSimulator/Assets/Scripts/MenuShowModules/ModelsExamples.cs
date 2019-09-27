using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModelsExamples
{
    [Header("Atributos Modelo")]
    public string title;
    public string nameModel;
    [TextArea]
    public string description;
    public bool isWeaponEffect;
    public GameObject modelSystemGO;
    public Vector3 modelPosition, modelRotation, modelScale;
}
