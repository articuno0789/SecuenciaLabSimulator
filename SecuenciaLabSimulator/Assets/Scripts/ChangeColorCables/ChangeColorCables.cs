using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorCables : MonoBehaviour
{
    public GameObject panelChangeColor;
    public GameObject canvasChangeColor;
    public GameObject panelChangeColorManager;

    // Start is called before the first frame update
    void Start()
    {
        panelChangeColorManager = GameObject.Find("ChangeColorCableManager");
        panelChangeColor = GameObject.Find("PanelChangeColorCableMenu");
        canvasChangeColor = GameObject.Find("ChangeColorCableMenu");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenCloseMenuChangeColorCable()
    {
        OpenCloseChangeColorCable OCChangeColor = panelChangeColorManager.GetComponent<OpenCloseChangeColorCable>();
        OCChangeColor.ShowHideChangeColorCable();
        /*CanvasGroup canvas = canvasChangeColor.GetComponent<CanvasGroup>();
        if(canvas != null)
        {
            if(canvas.alpha == 0.0f)
            {
                canvas.alpha = 100.0f;
            }
            else
            {
                canvas.alpha = 0.0f;
            }
        }*/


        /*var rt = panelChangeColor.GetComponent<RectTransform>();
        Vector3 mostrar = new Vector3(425, 180, 0);
        Vector3 ocultar = new Vector3(0, 0, 0);
        Debug.Log("rt.position: " + rt.position + " | mostrar: " + mostrar);
        Debug.Log(rt.position.x + " - "+ mostrar.x +"|"+ rt.position.y +" - "+ mostrar.y +"|"+ rt.position.y +" - "+ mostrar.z);
        Debug.Log(rt.position.x == mostrar.x && rt.position.y == mostrar.y && rt.position.y == mostrar.z);
        if (rt.position.x == mostrar.x && rt.position.y == mostrar.y)
        {
            Debug.Log("Ocultar menu panel para cambiar color cable");
            rt.position = ocultar;
        }
        else
        {
            Debug.Log("Mostrar menu panel para cambiar color cable");
            rt.position = mostrar;
        }*/
        /*ClickDetector clickDetector = player.GetComponent<ClickDetector>();
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
        Vector3 menuPosition = new Vector3(module.transform.position.x, module.transform.position.y + 4, module.transform.position.z + 4);
        panel.transform.position = menuPosition;

        if (padreTotal != null && currentModuleSelected != null)
        {
            currentModuleSelected.text = "Seleccionado: " + padreTotal.name;
            changeModuleDropdown.selectedGameObjectRightClick = padreTotal;
        }
        //panel = GameObject.Find("PanelChangeModule");*/
        /*if (!panelChangeColor.activeSelf)
        {
            Debug.Log("Activa el panel para cambiar color cables");
            panelChangeColor.SetActive(true);
        }
        else
        {
            Debug.Log("Desactiva el panel  para cambiar color cables");
            panelChangeColor.SetActive(false);
        }*/
    }
}
