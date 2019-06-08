using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenCloseChangeModule : MonoBehaviour
{
    public GameObject panel;
    public Dropdown dropdown;
    public Text myText;
    public string myString;
    public float fateTime;
    public GameObject player;
    public Text currentModuleSelected;
    public GameObject padreTotal;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("FirstPersonCharacter");
        currentModuleSelected = GameObject.Find("CurrentModuleSelected").GetComponent<Text>();
        panel = GameObject.Find("PanelChangeModule");
        //myText = GameObject.Find("TextChangeModule").GetComponent<Text>();
        //myText.color = Color.clear;
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
    public void OpenCloseMenuChangeModule()
    {
        ClickDetector clickDetector = player.GetComponent<ClickDetector>();
        GameObject module = clickDetector.lastClickedGmObj;
        EncontrarPadreTotal(module);
        //module = padreTotal;
        Debug.Log("Entra al LookAt");
        Debug.Log("transform.position.x: " + transform.position.x +
            ", transform.position.y: " + transform.position.y +
            ", transform.position.z: " + transform.position.z);
        Vector3 targetPosition = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
        panel.transform.LookAt(targetPosition);
        panel.transform.rotation = player.transform.rotation;
        Vector3 menuPosition = new Vector3(module.transform.position.x, module.transform.position.y + 4, module.transform.position.z + 4);
        panel.transform.position = menuPosition;

        if (padreTotal != null && currentModuleSelected != null)
            currentModuleSelected.text = "Seleccionado: " + padreTotal.name;
        //panel = GameObject.Find("PanelChangeModule");
        if (!panel.activeSelf)
        {
            Debug.Log("Activa el panel");
            panel.SetActive(true);
            //dropdown = GameObject.Find("DropdownChangeModule").GetComponent<Dropdown>();
            //dropdown.Show();
            //myText.text = myString;
            //myText.color = Color.Lerp(myText.color, Color.red, fateTime * Time.deltaTime);
            //Renderer rend;
            //rend = GetComponent<Renderer>();
            //rend.material.shader = Shader.Find("Toon/Basic Outline");
            //rend.material.SetColor("_OutlineColor", Color.red);

            //dropdown.Show();
            //myText.transform.LookAt(player.transform);
            //myText.transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y, -player.transform.position.z));
        }
        else
        {
            Debug.Log("Desactiva el panel");
            panel.SetActive(false);
            //myText.color = Color.Lerp(myText.color, Color.clear, fateTime * Time.deltaTime);
            //Renderer rend;
            //rend = GetComponent<Renderer>();
            //rend.material.shader = Shader.Find("Standard");
            //dropdown.Hide();
        }
    }
}
