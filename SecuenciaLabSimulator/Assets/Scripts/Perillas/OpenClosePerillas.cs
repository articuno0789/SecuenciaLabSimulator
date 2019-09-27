using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class OpenClosePerillas : MonoBehaviour
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
    public Button buttonSetValueKnob;
    //Debug
    [Header("Debug")]
    public bool debug = false;
    #endregion

    #region Inicializacion
    // Use this for initialization
    void Start()
    {
        if(player == null)
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
    // Update is called once per frame
    void Update()
    {

    }
    
    /*Este método se encarga de validar la entrada por teclado del usuario para el valor de la perilla y notificar
     si el valor introducido cumple con las especificaciones del componente.*/
    public void ValidateValueKnob()
    {
        if (inputFieldCurrentValue != null && textInfoValueKnob != null)
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
                Debug.LogError(this.name + ", Error. void ValidateValueKnob() - El valor introducido no es un número real.");
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
                else if (Regex.IsMatch(padreTotal.name, AuxiliarModulos.expreRegModPotenciometro))
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
        else
        {
            Debug.LogError(this.name + ", Error. void ValidateValueKnob() - inputFieldCurrentValue o textInfoValueKnob es nulo.");
        }
    }

    /*Este método se encarga de abrir y posicionar el panel para introducir el valor de la perilla,
     * frente al usuario.*/
    public void OpenCloseMenuSetValueKnob()
    {
        ClickDetector clickDetector = player.GetComponent<ClickDetector>();
        GameObject module = clickDetector.lastClickedGmObj;
        if(module != null)
        {
            EncontrarPadreTotal(module);
            //module = padreTotal;
            if (debug)
            {
                Debug.Log("Entra al LookAt-------------------");
                Debug.Log("transform.position.x: " + transform.position.x +
                    ", transform.position.y: " + transform.position.y +
                    ", transform.position.z: " + transform.position.z);
            }
            if(panel!=null && player != null)
            {
                Vector3 targetPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
                panel.transform.LookAt(targetPosition);
                panel.transform.rotation = player.transform.rotation;
                Vector3 menuPosition = new Vector3(module.transform.position.x, module.transform.position.y + 2, module.transform.position.z + 0);
                panel.transform.position = menuPosition;
            }
            else
            {
                Debug.LogError(this.name + ", void OpenCloseMenuSetValueKnob() - El panel o el player es nulo.");
            }
            //Se muestra en el panel el valor mínimo y máximo de ese componente.
            if (padreTotal != null && currentModuleSelected != null)
            {
                currentModuleSelected.text = "Seleccionado: Perilla del modulo " + padreTotal.name;
                textInfoValueKnob.text = "Información: OK";
                if (Regex.IsMatch(padreTotal.name, AuxiliarModulos.expreRegMod6))
                {
                    Modulo6 mod6 = padreTotal.GetComponent<Modulo6>();
                    if (mod6 != null)
                    {
                        minMaxKnobRange.text = "Valor [Min Max]: " + mod6.valorMinimoPerilla + " - " + mod6.valorMaximoPerilla;
                        inputFieldCurrentValue.text = mod6.valorActualPerilla + "";
                        ValidateValueKnob();
                    }
                    else
                    {
                        Debug.LogError(this.name + ", void OpenCloseMenuSetValueKnob() - El módulo no tiene lógica de módulo 6.");
                    }
                }
                else if (Regex.IsMatch(padreTotal.name, AuxiliarModulos.expreRegMod7))
                {
                    Modulo7 mod7 = padreTotal.GetComponent<Modulo7>();
                    if (mod7 != null)
                    {
                        minMaxKnobRange.text = "Valor [Min Max]: " + mod7.valorMinimoPerilla + " - " + mod7.valorMaximoPerilla;
                        inputFieldCurrentValue.text = mod7.valorActualPerilla + "";
                        ValidateValueKnob();
                    }
                    else
                    {
                        Debug.LogError(this.name + ", void OpenCloseMenuSetValueKnob() - El módulo no tiene lógica de módulo 7.");
                    }
                }
                else if (Regex.IsMatch(padreTotal.name, AuxiliarModulos.expreRegModPotenciometro))
                {
                    Potenciometro modPoten = padreTotal.GetComponent<Potenciometro>();
                    if (modPoten != null)
                    {
                        minMaxKnobRange.text = "Valor [Min Max]: " + modPoten.valorMinimoPerilla + " - " + modPoten.valorMaximoPerilla;
                        inputFieldCurrentValue.text = modPoten.valorActualPerilla + "";
                        ValidateValueKnob();
                    }
                    else
                    {
                        Debug.LogError(this.name + ", void OpenCloseMenuSetValueKnob() - El módulo no tiene lógica de potenciometro.");
                    }
                }
            }
            //Aparece o desaparece el panl bajandole o subiendo la opacidad.
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
            }
        }
        else
        {
            Debug.LogError(this.name + ", void OpenCloseMenuSetValueKnob() - El modulo recibido del clicdetector es nulo.");
        }
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
