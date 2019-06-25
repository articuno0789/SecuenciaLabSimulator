using System.Collections;
using System.Collections.Generic;
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
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("FirstPersonCharacter");
        currentModuleSelected = GameObject.Find("CurrentModuleSelectedKnob").GetComponent<Text>();
        minMaxKnobRange = GameObject.Find("MinMaxValueKnob").GetComponent<Text>();
        textInfoValueKnob = GameObject.Find("TextInfoValueKnob").GetComponent<Text>();
        panel = GameObject.Find("PanelSetValueKnob");
        inputFieldCurrentValue = GameObject.Find("InputFieldCurrentValue").GetComponent<InputField>();
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
            currentModuleSelected.text = "Seleccionado: Perilla del modulo" + padreTotal.name;
            textInfoValueKnob.text = "Información: OK";
            if (padreTotal.name.Contains("6_"))
            {
                Modulo6 mod6 = padreTotal.GetComponent<Modulo6>();
                minMaxKnobRange.text = "Valor [Min Max]: " + mod6.valorMinimoPerilla + " - " + mod6.valorMaximoPerilla;
                inputFieldCurrentValue.text = mod6.valorActualPerilla + "";
            }
            else if (padreTotal.name.Contains("7_"))
            {
                Modulo7 mod7 = padreTotal.GetComponent<Modulo7>();
                minMaxKnobRange.text = "Valor [Min Max]: " + mod7.valorMinimoPerilla + " - " + mod7.valorMaximoPerilla;
                inputFieldCurrentValue.text = mod7.valorActualPerilla + "";
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
}
