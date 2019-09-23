using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class SetValueKnob : MonoBehaviour
{
    #region Atributos
    [Header("Parametros Panel Perilla")]
    public GameObject panel;
    public GameObject player;
    public Text currentModuleSelected;
    public Text minMaxKnobRange;
    public Text textInfoValueKnob;
    public InputField inputFieldCurrentValue;
    public GameObject padreTotal;
    public GameObject perillaSeleccionada;
    public Button buttonSetValueKnob;
    //Debug
    [Header("Debug")]
    public bool debug = false;
    #endregion

    #region Inicializacion
    // Use this for initialization
    void Start()
    {
        if (player == null)
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
        }
    }
    #endregion

    #region Comportamiento
    /*Este método se encarga de bajar y subir la opacidad del panel.*/
    public void CloseMenuSetValueKnob()
    {
        if (panel != null)
        {
            CanvasGroup canvasGP = panel.GetComponent<CanvasGroup>();
            if (canvasGP.alpha == 1)
            {
                canvasGP.alpha = 0;
            }
            else
            {
                canvasGP.alpha = 1;
            }
            /*if (!panel.activeSelf)
            {
                Debug.Log("Activa el panel perilla");
                panel.SetActive(true);
            }
            else
            {
                Debug.Log("Desactiva el panel perilla");
                panel.SetActive(false);
            }*/
        }
    }

    /*Este método se encarga de validar la entrada por teclado del usuario para el valor de la perilla y notificar
     si el valor introducido cumple con las especificaciones del componente.*/
    public void ValidateValueKnob()
    {

        if (debug)
        {
            Debug.Log(inputFieldCurrentValue.text + ": " + Regex.IsMatch(inputFieldCurrentValue.text, AuxiliarModulos.expreRegNumerosReales).ToString());
        }
        //Comprobar si el valor introducido no es un número real.
        if (!Regex.IsMatch(inputFieldCurrentValue.text, AuxiliarModulos.expreRegNumerosReales))
        {
            inputFieldCurrentValue.text = "0.0";
            buttonSetValueKnob.enabled = false;
        }
        /*Si entra en este caso la entrada es un número real, pero hay que 
         * comprobar si esta dentro de los límites de cada perilla.*/
        else
        {
            float limiteMinimo = 0.0f;
            float limiteMaximo = 0.0f;
            float valorCampoTexto = float.Parse(inputFieldCurrentValue.text);

            //Determinar los límites de los diferentes modulos con perilla.
            if (padreTotal != null && Regex.IsMatch(padreTotal.name, AuxiliarModulos.expreRegMod6))
            {
                Modulo6 mod6 = padreTotal.GetComponent<Modulo6>();
                limiteMinimo = mod6.valorMinimoPerilla;
                limiteMaximo = mod6.valorMaximoPerilla;
            }
            else if (padreTotal != null && Regex.IsMatch(padreTotal.name, AuxiliarModulos.expreRegMod7))
            {
                Modulo7 mod7 = padreTotal.GetComponent<Modulo7>();
                limiteMinimo = mod7.valorMinimoPerilla;
                limiteMaximo = mod7.valorMaximoPerilla;
            }
            else if (padreTotal != null && Regex.IsMatch(padreTotal.name, AuxiliarModulos.expreRegModPotenciometro))
            {
                Potenciometro modPoten = padreTotal.GetComponent<Potenciometro>();
                limiteMinimo = modPoten.valorMinimoPerilla;
                limiteMaximo = modPoten.valorMaximoPerilla;
            }
            if (debug)
            {
                Debug.Log("void ValidateValueKnob() - valorCampoTexto: " + valorCampoTexto + ", limiteMinimo: " + limiteMinimo + ", limiteMaximo: " + limiteMaximo + ", " + padreTotal);
            }
            /*Establecer los difernes mensajes de notificación de acuerdo cada situación.*/
            if (padreTotal != null && valorCampoTexto < limiteMinimo) // El valor es menor al mínimo permitido.
            {
                inputFieldCurrentValue.text = "0.0";
                textInfoValueKnob.text = "Información: Error. El valor introducido es menor al limite mínimo de la perilla.";
                buttonSetValueKnob.enabled = false;
            }
            else
            if (padreTotal != null && valorCampoTexto > limiteMaximo)// El valor es mayor al máximo permitido.
            {
                inputFieldCurrentValue.text = "0.0";
                textInfoValueKnob.text = "Información: Error. El valor introducido es mayor al limite máximo de la perilla.";
                buttonSetValueKnob.enabled = false;
            }
            else // El valor esta dentro de los límites adecuados.
            {
                textInfoValueKnob.text = "Información: OK";
                buttonSetValueKnob.enabled = true;
            }
        }
    }

    /*Este método se fija en el módulo, el valor de la perilla de acuerdo al valor introducido por el usuario.*/
    public void SetValueKnop()
    {
        //ClickDetector clickDetector = player.GetComponent<ClickDetector>();
        GameObject module = perillaSeleccionada;
        if (debug)
        {
            Debug.Log("void SetValueKnop() - Nombre de la perilla: " + perillaSeleccionada.name);
        }
        EncontrarPadreTotal(perillaSeleccionada);
        //En esta sección se realiza el fijado de los datos.
        if (padreTotal != null && currentModuleSelected != null)
        {
            currentModuleSelected.text = "Seleccionado: Perilla del modulo" + padreTotal.name;   
            if (Regex.IsMatch(padreTotal.name, AuxiliarModulos.expreRegMod6))
            {
                Modulo6 mod6 = padreTotal.GetComponent<Modulo6>();
                mod6.valorActualPerilla = float.Parse(inputFieldCurrentValue.text);
                mod6.RotarPerilla();
                minMaxKnobRange.text = "Valor [Min Max]: " + mod6.valorMinimoPerilla + " - " + mod6.valorMaximoPerilla;
                inputFieldCurrentValue.text = mod6.valorActualPerilla + "";
                if (debug)
                {
                    Debug.Log(name + "--Modulo6: SE HA ESTABLECIDO UN NUEVO VALOR EN LA PERILLA " +
                    inputFieldCurrentValue.text + ", " + float.Parse(inputFieldCurrentValue.text));
                }
            }
            else if (Regex.IsMatch(padreTotal.name, AuxiliarModulos.expreRegMod7))
            {
                Modulo7 mod7 = padreTotal.GetComponent<Modulo7>();
                mod7.valorActualPerilla = float.Parse(inputFieldCurrentValue.text);
                mod7.RotarPerilla();
                minMaxKnobRange.text = "Valor [Min Max]: " + mod7.valorMinimoPerilla + " - " + mod7.valorMaximoPerilla;
                inputFieldCurrentValue.text = mod7.valorActualPerilla + "";
                if (debug)
                {
                    Debug.Log(name + "++Modulo7: SE HA ESTABLECIDO UN NUEVO VALOR EN LA PERILLA " +
                    inputFieldCurrentValue.text + ", " + float.Parse(inputFieldCurrentValue.text));
                }
            }
            else if (Regex.IsMatch(padreTotal.name, AuxiliarModulos.expreRegModPotenciometro))
            {
                Potenciometro modPoten = padreTotal.GetComponent<Potenciometro>();
                modPoten.valorActualPerilla = float.Parse(inputFieldCurrentValue.text);
                modPoten.RotarPerilla();
                minMaxKnobRange.text = "Valor [Min Max]: " + modPoten.valorMinimoPerilla + " - " + modPoten.valorMaximoPerilla;
                inputFieldCurrentValue.text = modPoten.valorActualPerilla + "";
                if (debug)
                {
                    Debug.Log(name + "//ModuloPotenciometro: SE HA ESTABLECIDO UN NUEVO VALOR EN LA PERILLA " +
                        inputFieldCurrentValue.text + ", " + float.Parse(inputFieldCurrentValue.text));
                }
            }
            CloseMenuSetValueKnob();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*Este método recursivo se utiliza para encontrar el padretotal (Nodo raíz) de un componente.*/
    private void EncontrarPadreTotal(GameObject nodo)
    {
        if (nodo.transform.parent == null)
        {
            padreTotal = nodo;
        }
        else
        {
            GameObject padre = nodo.transform.parent.gameObject;
            EncontrarPadreTotal(padre);
        }
    }
    #endregion
}
