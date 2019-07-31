using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class SetValueKnob : MonoBehaviour
{
    public GameObject panel;
    public GameObject player;
    public Text currentModuleSelected;
    public Text minMaxKnobRange;
    public Text textInfoValueKnob;
    public InputField inputFieldCurrentValue;
    public GameObject padreTotal;
    public GameObject perillaSeleccionada;
    public Button buttonSetValueKnob;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("FirstPersonCharacter");
        currentModuleSelected = GameObject.Find("CurrentModuleSelectedKnob").GetComponent<Text>();
        minMaxKnobRange = GameObject.Find("MinMaxValueKnob").GetComponent<Text>();
        textInfoValueKnob = GameObject.Find("TextInfoValueKnob").GetComponent<Text>();
        panel = GameObject.Find("PanelSetValueKnob");
        inputFieldCurrentValue = GameObject.Find("InputFieldCurrentValue").GetComponent<InputField>();
        buttonSetValueKnob = GameObject.Find("ButtonSetValueKnob").GetComponent<Button>();
    }

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

    public void ValidateValueKnob()
    {
        Debug.Log(inputFieldCurrentValue.text + ": "  + Regex.IsMatch(inputFieldCurrentValue.text, "^[0-9]*([.][0-9]{1,5})?$").ToString());
        if(!Regex.IsMatch(inputFieldCurrentValue.text, "^[0-9]*([.][0-9]+)?$"))
        {
            inputFieldCurrentValue.text = "0.0";
            buttonSetValueKnob.enabled = false;
        }
        else
        {
            float limiteMinimo = 0.0f;
            float limiteMaximo = 0.0f;
            float valorCampoTexto = float.Parse(inputFieldCurrentValue.text);
            if (perillaSeleccionada != null)
            {
                EncontrarPadreTotal(perillaSeleccionada);
            }
            if (padreTotal!= null && Regex.IsMatch(padreTotal.name, @"^6_\d*$"))
            {
                Modulo6 mod6 = padreTotal.GetComponent<Modulo6>();
                limiteMinimo = mod6.valorMinimoPerilla;
                limiteMaximo = mod6.valorMaximoPerilla;
            }
            else if (padreTotal != null && Regex.IsMatch(padreTotal.name, @"^7_\d*$"))
            {
                Modulo7 mod7 = padreTotal.GetComponent<Modulo7>();
                limiteMinimo = mod7.valorMinimoPerilla;
                limiteMaximo = mod7.valorMaximoPerilla;
            }
            else if (Regex.IsMatch(padreTotal.name, @"^Potenciometro_\d*$"))
            {
                Potenciometro modPoten = padreTotal.GetComponent<Potenciometro>();
                limiteMinimo = modPoten.valorMinimoPerilla;
                limiteMaximo = modPoten.valorMaximoPerilla;
            }
            Debug.Log("------------------------------------------------------------------------------------------------------valorCampoTexto: " + valorCampoTexto + ", limiteMinimo: " + limiteMinimo + ", limiteMaximo: " + limiteMaximo + ", " + padreTotal);
            if (padreTotal != null && valorCampoTexto < limiteMinimo)
            {
                inputFieldCurrentValue.text = "0.0";
                textInfoValueKnob.text = "Información: Error. El valor introducido es menor al limite mínimo de la perilla.";
                buttonSetValueKnob.enabled = false;
            }
            else
            if (padreTotal != null && valorCampoTexto > limiteMaximo)
            {
                inputFieldCurrentValue.text = "0.0";
                textInfoValueKnob.text = "Información: Error. El valor introducido es mayor al limite máximo de la perilla.";
                buttonSetValueKnob.enabled = false;
            }
            else
            {
                textInfoValueKnob.text = "Información: OK";
                buttonSetValueKnob.enabled = true;
            }
        }
    }

    public void SetValueKnop()
    {
        //ClickDetector clickDetector = player.GetComponent<ClickDetector>();
        GameObject module = perillaSeleccionada;
        Debug.Log(perillaSeleccionada.name);
        EncontrarPadreTotal(perillaSeleccionada);
        if (padreTotal != null && currentModuleSelected != null)
        {
            currentModuleSelected.text = "Seleccionado: Perilla del modulo" + padreTotal.name;
            
            if (Regex.IsMatch(padreTotal.name, @"^6_\d*$"))
            {
                Modulo6 mod6 = padreTotal.GetComponent<Modulo6>();
                mod6.valorActualPerilla = float.Parse(inputFieldCurrentValue.text);
                mod6.RotarPerilla();
                minMaxKnobRange.text = "Valor [Min Max]: " + mod6.valorMinimoPerilla + " - " + mod6.valorMaximoPerilla;
                inputFieldCurrentValue.text = mod6.valorActualPerilla + "";
                Debug.Log(name + "--Modulo6: SE HA ESTABLECIDO UN NUEVO VALOR EN LA PERILLA " + inputFieldCurrentValue.text + ", " + float.Parse(inputFieldCurrentValue.text));
            }
            else if (Regex.IsMatch(padreTotal.name, @"^7_\d*$"))
            {
                Modulo7 mod7 = padreTotal.GetComponent<Modulo7>();
                mod7.valorActualPerilla = float.Parse(inputFieldCurrentValue.text);
                mod7.RotarPerilla();
                minMaxKnobRange.text = "Valor [Min Max]: " + mod7.valorMinimoPerilla + " - " + mod7.valorMaximoPerilla;
                inputFieldCurrentValue.text = mod7.valorActualPerilla + "";
                Debug.Log(name + "++Modulo7: SE HA ESTABLECIDO UN NUEVO VALOR EN LA PERILLA " + inputFieldCurrentValue.text + ", " + float.Parse(inputFieldCurrentValue.text));
            }
            else if (Regex.IsMatch(padreTotal.name, @"^Potenciometro_\d*$"))
            {
                Potenciometro modPoten = padreTotal.GetComponent<Potenciometro>();
                modPoten.valorActualPerilla = float.Parse(inputFieldCurrentValue.text);
                modPoten.RotarPerilla();
                minMaxKnobRange.text = "Valor [Min Max]: " + modPoten.valorMinimoPerilla + " - " + modPoten.valorMaximoPerilla;
                inputFieldCurrentValue.text = modPoten.valorActualPerilla + "";
                Debug.Log(name + "//ModuloPotenciometro: SE HA ESTABLECIDO UN NUEVO VALOR EN LA PERILLA " + inputFieldCurrentValue.text + ", " + float.Parse(inputFieldCurrentValue.text));
            }
            CloseMenuSetValueKnob();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

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
}
