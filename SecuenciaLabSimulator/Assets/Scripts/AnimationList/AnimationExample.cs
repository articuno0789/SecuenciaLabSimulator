using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimationExample
{
    #region Atributos
    [Header("Atributos Animación")]
    public string title;
    public string nameAnimation;
    [TextArea]
    public string description;
    public AnimationClip animationSystemGO;
    #endregion

    #region Comportamiento
    /*Contructor vacío.*/
    public AnimationExample()
    {

    }

    /*Contructor para llenar todos los parámetros.*/
    public AnimationExample(string title, string nameAnimation, string description, AnimationClip animationSystemGO)
    {
        this.title = title;
        this.nameAnimation = nameAnimation;
        this.description = description;
        this.animationSystemGO = animationSystemGO;
    }
    #endregion
}
