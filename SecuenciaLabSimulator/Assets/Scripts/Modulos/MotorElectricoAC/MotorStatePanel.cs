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
        /*if (player == null)
        {
            player = GameObject.Find("FirstPersonCharacter");
        }
        if (currentModuleSelected == null)
        {
            currentModuleSelected = GameObject.Find("CurrentModuleSelectedKnob").GetComponent<Text>();
        }
        if (minMaxKnobRange == null)
        {
            minMaxKnobRange = GameObject.Find("MinMaxValueKnob").GetComponent<Text>();
        }
        if (textInfoValueKnob == null)
        {
            textInfoValueKnob = GameObject.Find("TextInfoValueKnob").GetComponent<Text>();
        }
        if (panel == null)
        {
            panel = GameObject.Find("PanelSetValueKnob");
        }
        if (inputFieldCurrentValue == null)
        {
            inputFieldCurrentValue = GameObject.Find("InputFieldCurrentValue").GetComponent<InputField>();
        }
        if (buttonSetValueKnob == null)
        {
            buttonSetValueKnob = GameObject.Find("ButtonSetValueKnob").GetComponent<Button>();
        }*/
    }
    #endregion

    #region Comportamiento

    public void EstablecerTextoVoltajeMotor(float voltaje)
    {
        if(voltajeMotor != null)
        {
            voltajeMotor.text = "Voltaje: " + voltaje + " v";
        }
    }

    public void EstablecerTextoVeloMotor(float velocidad)
    {
        veloRotaMotor.text = "Velocidad de rotación: " + velocidad + " rpm";
    }

    public void EstablecerTextoEstadoMotor(bool todoBien, string textoExplicativo = "")
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

    public void EstablecerTextoDireccionGiroMotor(int directGiro)
    {
        if (directGiro == 0)
        {
            direccionGiro.text = "Dirección giro: -";
        }
        else if(directGiro == 1)
        {
            direccionGiro.text = "Dirección giro: Horario";
        }else if (directGiro == 2)
        {
            direccionGiro.text = "Dirección giro: Antihorario";
        }
        else
        {
            direccionGiro.text = "Dirección giro: --";
        }
    }

    public void PararMotorTodoBien()
    {
        voltajeMotor.text = "Votaje: 0 v";
        veloRotaMotor.text = "Velocidad de rotación: 0 rpm";
        estadoMotor.text = "Estado: Ok";
        direccionGiro.text = "Dirección giro: -";
    }
    #endregion
}
