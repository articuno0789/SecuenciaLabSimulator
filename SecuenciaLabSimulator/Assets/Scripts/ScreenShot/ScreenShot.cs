﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    #region Atributos
    //Sonidos
    public GameObject camaraSound;
    //Debug
    public bool debug = true;
    #endregion

    #region Inicializacion
    //Inicialización de los componentes.
    void Awake()
    {
        DontDestroyOnLoad(this);
        if(camaraSound == null)
        {
            camaraSound = GameObject.Find("CamaraEffect");
        }
    }
    #endregion

    #region Comportamiento
    //En este método se comprueba las entradas por teclado.
    void Update()
    {
        //Se comprueba si el usuario presiono el atajo por teclado para esta función.
        if (Input.GetKey(KeyCode.F10))
        {
            TomarCapturaDePantalla();
        }
    }

    //Se encarga de tomar la captura de pantalla de acuerdo a la visión de la camara principal.
    public void TomarCapturaDePantalla()
    {
        /* Se obtiene la fecha del sistema y se le da el formato adecuado para estar en el nombre de un archivo.
         * Es decir, se eliminan los carácteres no válidos.*/
        string screenshotIMGName = System.DateTime.Now.ToString();
        string subString = screenshotIMGName.Replace('/', '_');
        string gypsy = subString.Replace(':', '_');

        //Se guarda la imagen en la ruta especificada.
        ScreenCapture.CaptureScreenshot(gypsy + ".png");

        //Se reproduce el sonido de camara.
        if (camaraSound != null)
        {
            AudioSource sound = camaraSound.GetComponent<AudioSource>();
            if (sound != null)
            {
                Debug.Log("Suena sonido camara");
                sound.Play();
            }
        }
        if (debug)
        {
            Debug.Log("Screen shot captured: " + gypsy + ".png");
        }
    }
    #endregion
}
