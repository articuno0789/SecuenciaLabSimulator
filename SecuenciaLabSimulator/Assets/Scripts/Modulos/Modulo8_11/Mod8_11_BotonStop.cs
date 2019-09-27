﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mod8_11_BotonStop : MonoBehaviour
{
    #region Atributos
    [Header("Animaciones")]
    private new Animation animation;
    public Animation Animation { get => animation; set => animation = value; }
    #endregion

    #region Inicializacion
    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animation>();
    }
    #endregion

    #region Comportamiento Modulo
    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    private void OnMouseDown()
    {
        Debug.Log("Entra a presionar boton stop");
        if (animation != null)
        {
            animation.Play("StopButton");
        }
        else
        {
            Debug.LogError(this.name + ", Error. void OnMouseDown() - animation es nulo.");
        }
    }
}
