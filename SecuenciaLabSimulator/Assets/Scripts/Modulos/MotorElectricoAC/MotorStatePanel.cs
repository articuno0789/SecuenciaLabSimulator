using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class MotorStatePanel : MonoBehaviour
{
    #region Atributos
    public GameObject panel;
    public Text estadoMotor;
    public Text voltajeMotor;
    public Text veloRotaMotor;
    public Text direccionGiro;
    //Debug
    public bool debug = false;
    #endregion

    #region Inicializacion
    // Use this for initialization
    void Start()
    {

    }
    #endregion

    #region Comportamiento

    public void EstablecerTextoVoltajeMotor(float voltaje)
    {
        if(voltajeMotor != null)
        {
            voltajeMotor.text = "Voltaje: " + voltaje + " v";
        }
        else
        {
            Debug.LogError(this.name + ", Error. void EstablecerTextoVoltajeMotor(float voltaje) - voltajeMotor es nulo.");
        }
    }

    public void EstablecerTextoVeloMotor(float velocidad)
    {
        if (veloRotaMotor != null)
        {
            veloRotaMotor.text = "Velocidad de rotación: " + velocidad + " rpm";
        }
        else
        {
            Debug.LogError(this.name + ", Error. void EstablecerTextoVeloMotor(float velocidad) - veloRotaMotor es nulo.");
        }
    }

    public void EstablecerTextoEstadoMotor(bool todoBien, string textoExplicativo = "")
    {
        if (estadoMotor != null)
        {
            if (todoBien)
            {
                estadoMotor.text = "Estado: Ok";
            }
            else
            {
                estadoMotor.text = "Estado: Hay alguna falla. Descripción: " + textoExplicativo;
            }
        }
        else
        {
            Debug.LogError(this.name + ", Error. void EstablecerTextoEstadoMotor(bool todoBien, string textoExplicativo) - estadoMotor es nulo.");
        }
    }

    public void EstablecerTextoDireccionGiroMotor(int directGiro)
    {
        if (direccionGiro != null)
        {
            if (directGiro == (int)AuxiliarModulos.DireccionRotacion.SinRotar)
            {
                direccionGiro.text = "Dirección giro: -";
            }
            else if (directGiro == (int)AuxiliarModulos.DireccionRotacion.Horario)
            {
                direccionGiro.text = "Dirección giro: Horario";
            }
            else if (directGiro == (int)AuxiliarModulos.DireccionRotacion.Antihorario)
            {
                direccionGiro.text = "Dirección giro: Antihorario";
            }
            else
            {
                direccionGiro.text = "Dirección giro: --";
            }
        }
        else
        {
            Debug.LogError(this.name + ", Error. void EstablecerTextoDireccionGiroMotor(int directGiro) - direccionGiro es nulo.");
        }
    }

    public void PararMotorTodoBien()
    {
        if(voltajeMotor!=null && veloRotaMotor!=null && estadoMotor!=null && direccionGiro != null)
        {
            voltajeMotor.text = "Votaje: 0 v";
            veloRotaMotor.text = "Velocidad de rotación: 0 rpm";
            estadoMotor.text = "Estado: Ok";
            direccionGiro.text = "Dirección giro: -";
        }
        else
        {
            Debug.LogError(this.name + ", Error. void PararMotorTodoBien() - voltajeMotor o veloRotaMotor o estadoMotor o direccionGiro es nulo.");
        }
    }
    #endregion
}
