using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class OpenClosePerillas : MonoBehaviour
{
    public GameObject panel;
    public GameObject player;
    public Text currentModuleSelected;
    public Text minMaxKnobRange;
    public Text textInfoValueKnob;
    public InputField inputFieldCurrentValue;
    public GameObject padreTotal;
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

    public void ValidateValueKnob()
    {
        Debug.Log(inputFieldCurrentValue.text + ": " + Regex.IsMatch(inputFieldCurrentValue.text, "^[0-9]*([.][0-9]{1,5})?$").ToString());
        if (!Regex.IsMatch(inputFieldCurrentValue.text, "^[0-9]*([.][0-9]+)?$"))
        {
            inputFieldCurrentValue.text = "0.0";
            buttonSetValueKnob.enabled = false;
        }
        else
        {
            float limiteMinimo = 0.0f;
            float limiteMaximo = 0.0f;
            float valorCampoTexto = float.Parse(inputFieldCurrentValue.text);
            if (padreTotal != null && Regex.IsMatch(padreTotal.name, @"^6_\d*$"))
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

    public void OpenCloseMenuSetValueKnob()
    {
        ClickDetector clickDetector = player.GetComponent<ClickDetector>();
        GameObject module = clickDetector.lastClickedGmObj;
        EncontrarPadreTotal(module);
        //module = padreTotal;
        Debug.Log("Entra al LookAt-------------------");
        Debug.Log("transform.position.x: " + transform.position.x +
            ", transform.position.y: " + transform.position.y +
            ", transform.position.z: " + transform.position.z);
        Vector3 targetPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        panel.transform.LookAt(targetPosition);
        panel.transform.rotation = player.transform.rotation;
        Vector3 menuPosition = new Vector3(module.transform.position.x, module.transform.position.y + 2, module.transform.position.z + 0);
        panel.transform.position = menuPosition;

        if (padreTotal != null && currentModuleSelected != null)
        {
            currentModuleSelected.text = "Seleccionado: Perilla del modulo " + padreTotal.name;
            textInfoValueKnob.text = "Información: OK";
            if (Regex.IsMatch(padreTotal.name, @"^6_\d*$"))
            {
                Modulo6 mod6 = padreTotal.GetComponent<Modulo6>();
                minMaxKnobRange.text = "Valor [Min Max]: " + mod6.valorMinimoPerilla + " - " + mod6.valorMaximoPerilla;
                inputFieldCurrentValue.text = mod6.valorActualPerilla + "";
                ValidateValueKnob();
            }
            else if (Regex.IsMatch(padreTotal.name, @"^7_\d*$"))
            {
                Modulo7 mod7 = padreTotal.GetComponent<Modulo7>();
                minMaxKnobRange.text = "Valor [Min Max]: " + mod7.valorMinimoPerilla + " - " + mod7.valorMaximoPerilla;
                inputFieldCurrentValue.text = mod7.valorActualPerilla + "";
                ValidateValueKnob();
            }
            else if (Regex.IsMatch(padreTotal.name, @"^Potenciometro_\d*$"))
            {
                Potenciometro modPoten = padreTotal.GetComponent<Potenciometro>();
                minMaxKnobRange.text = "Valor [Min Max]: " + modPoten.valorMinimoPerilla + " - " + modPoten.valorMaximoPerilla;
                inputFieldCurrentValue.text = modPoten.valorActualPerilla + "";
                ValidateValueKnob();
            }
        }
        //panel = GameObject.Find("PanelChangeModule");
        if(panel != null)
        {
            CanvasGroup canvasGP = panel.GetComponent<CanvasGroup>();
            if(canvasGP.alpha == 1)
            {
                canvasGP.alpha = 0;
            }
            else
            {
                canvasGP.alpha = 1;
            }
        }
    }
}
