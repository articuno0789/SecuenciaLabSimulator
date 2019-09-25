using UnityEngine;
using System.Collections;
using UnityEditor;

public class ClickDetector : MonoBehaviour
{
    [Header("Camara")]
    public Camera camara;
    [Header("Capa")]
    public LayerMask layerMask;
    [Header("Objetos Seleccionados")]
    public GameObject lastClickedGmObj;
    public GameObject clickedGmObj;
    [Header("Tipos de Clics Habilitados")]
    public bool HandleLeftClick = false;
    public bool HandleRightClick = true;
    public bool HandleMiddleClick = false;
    [Header("Acciones por clic")]
    public string OnLeftClickMethodName = "Prueba";
    public string OnRightClickMethodName = "Prueba";
    public string OnMiddleClickMethodName = "Prueba";
    [Header("Auxiliar Clic")]
    public bool acabaDeCrearConexion = false;
    [Header("Materiales")]
    public Material cableMaterial;
    private string rutaMaterialPlugAnaranjado = "Assets/Materials/EntradaPlug/AnaranjadoPlug.mat";
    private string rutaMaterialPlugNegro = "Assets/Materials/EntradaPlug/ObscuroPlug.mat";
    public ColorPicker colorPicker;
    void Update()
    {
        clickedGmObj = null;
        bool clickedGmObjAcquired = false;
        // Left click
        if (HandleLeftClick && Input.GetMouseButtonDown(0))
        {
            if (!clickedGmObjAcquired)
            {
                clickedGmObj = GetClickedGameObject();
                //lastClickedGmObj = clickedGmObj;
                clickedGmObjAcquired = true;
            }
            if (clickedGmObj != null)
            {
                bool segundoPlug = false;
                Debug.Log("Manda mensaje, click izquierdo: " + OnLeftClickMethodName + ", *******Objeto clic: " + clickedGmObj.name);
                if (lastClickedGmObj != null)
                {
                    Debug.Log("lastClickedGmObj != null");
                    if (!lastClickedGmObj.name.Contains("EntradaPlug"))
                    {
                        Debug.Log("Entra !lastClickedGmObj.name.Contains(\"EntradaPlug\")");
                        if (clickedGmObj.name.Contains("EntradaPlug"))
                        {
                            Debug.Log("NO HAY CAMBIO DE COLOR");
                            //Fetch the Renderer from the GameObject
                            Renderer rend = clickedGmObj.GetComponent<Renderer>();
                            //Set the main Color of the Material to green
                            //rend.material.shader = Shader.Find("_Color");
                            //rend.material.SetColor("_Color", Color.green);
                            rend.material = new Material(Shader.Find("Sprites/Default"));
                            rend.material.color = AuxiliarModulos.colorPlugsDestacados;
                        }
                        else
                        {
                            Renderer rend = clickedGmObj.GetComponent<Renderer>();
                            //rend.material.color = Color.yellow;
                            Debug.Log("-------CAMBIO DE COLOR");
                            //changeOriginalColorPlug(lastClickedGmObj);
                            //Fetch the Renderer from the GameObject
                            //Renderer rend = clickedGmObj.GetComponent<Renderer>();
                            //Set the main Color of the Material to green
                            //rend.material.shader = Shader.Find("_Color");
                            //rend.material.SetColor("_Color", Color.green);
                        }
                    }
                    else
                    {
                        if (clickedGmObj.name.Contains("EntradaPlug") && clickedGmObj != lastClickedGmObj)
                        {
                            segundoPlug = true;
                            CrearConexionCable(clickedGmObj);
                        }
                        else
                        {
                            changeOriginalColorPlug(lastClickedGmObj);
                            //Renderer rend = lastClickedGmObj.GetComponent<Renderer>();
                            //rend.material.color = Color.white;
                            segundoPlug = true;
                        }
                    }
                }

                if (clickedGmObj.name.Contains("EntradaPlug") && !acabaDeCrearConexion)
                {
                    Renderer rend = clickedGmObj.GetComponent<Renderer>();
                    rend.material = new Material(Shader.Find("Sprites/Default"));
                    rend.material.color = AuxiliarModulos.colorPlugsDestacados;
                }
                else
                {
                    acabaDeCrearConexion = false;
                }

                lastClickedGmObj = clickedGmObj;
                if (segundoPlug)
                {
                    lastClickedGmObj = GameObject.Find("Plane");
                }
                //clickedGmObj.SendMessage(OnLeftClickMethodName, null, SendMessageOptions.DontRequireReceiver);
            }
        }
        // Right click
        if (HandleRightClick && Input.GetMouseButtonDown(1))
        {
            if (!clickedGmObjAcquired)
            {
                clickedGmObj = GetClickedGameObject();
                lastClickedGmObj = clickedGmObj;
                clickedGmObjAcquired = true;
            }
            if (clickedGmObj != null)
            {
                Debug.Log("Manda mensaje clic derecho, " + OnRightClickMethodName + ", *******Objeto clic: " + clickedGmObj.name);
                if (clickedGmObj.name.Contains("BaseModulo"))
                {
                    clickedGmObj.SendMessage("OpenCloseMenuChangeModule", 1, SendMessageOptions.DontRequireReceiver);
                }
                else
                if (clickedGmObj.name.Contains("EntradaPlug"))
                {
                    clickedGmObj.SendMessage("OpenCloseMenuChangeColorCable", 1, SendMessageOptions.DontRequireReceiver);
                    colorPicker.selectedPlug = clickedGmObj;
                    clickedGmObj = null;
                    lastClickedGmObj = null;
                }
                else if (clickedGmObj.name.Contains("Total_Perilla"))
                {
                    GameObject buttonSetValueKnob = GameObject.Find("ButtonSetValueKnob");
                    GameObject inputFieldCurrentValue = GameObject.Find("InputFieldCurrentValue");
                    clickedGmObj.SendMessage("OpenCloseMenuSetValueKnob", 1, SendMessageOptions.DontRequireReceiver);
                    if (buttonSetValueKnob != null && inputFieldCurrentValue != null)
                    {
                        buttonSetValueKnob.GetComponent<SetValueKnob>().perillaSeleccionada = clickedGmObj;
                        inputFieldCurrentValue.GetComponent<SetValueKnob>().perillaSeleccionada = clickedGmObj;
                    }
                }
                else if (clickedGmObj.name.Contains("PerillaPotenciometro"))
                {
                    GameObject buttonSetValueKnob = GameObject.Find("ButtonSetValueKnob");
                    GameObject inputFieldCurrentValue = GameObject.Find("InputFieldCurrentValue");
                    clickedGmObj.SendMessage("OpenCloseMenuSetValueKnob", 1, SendMessageOptions.DontRequireReceiver);
                    if (buttonSetValueKnob != null && inputFieldCurrentValue != null)
                    {
                        buttonSetValueKnob.GetComponent<SetValueKnob>().perillaSeleccionada = clickedGmObj;
                        inputFieldCurrentValue.GetComponent<SetValueKnob>().perillaSeleccionada = clickedGmObj;
                    }
                }
            }

            //clickedGmObj.SendMessage(OnRightClickMethodName, null, SendMessageOptions.DontRequireReceiver);
        }
        // Middle click
        if (HandleMiddleClick && Input.GetMouseButtonDown(2))
        {
            if (!clickedGmObjAcquired)
            {
                clickedGmObj = GetClickedGameObject();
                clickedGmObjAcquired = true;
            }
            if (clickedGmObj != null)
                clickedGmObj.SendMessage(OnMiddleClickMethodName, null, SendMessageOptions.DontRequireReceiver);
        }
    }

    GameObject GetClickedGameObject()
    {
        // Builds a ray from camera point of view to the mouse position<br />
        Ray ray = camara.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        // Casts the ray and get the first game object hit<br />
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            Debug.Log("Encuentra objeto -------------------------");
            return hit.transform.gameObject;
        }
        else
        {
            Debug.Log("eNTRA A NULO, x: " + Input.mousePosition.x + ", y: " + Input.mousePosition.y + ", z: " + Input.mousePosition.z);
            return null;
        }
    }

    private void changeOriginalColorPlug(GameObject objectClick)
    {
        if (objectClick != null)
        {
            if (objectClick.name.Contains("EntradaPlugNegro"))
            {
                objectClick.GetComponent<Renderer>().material = (Material)AssetDatabase.LoadAssetAtPath(rutaMaterialPlugNegro, typeof(Material));
            }
            else
            {
                objectClick.GetComponent<Renderer>().material = (Material)AssetDatabase.LoadAssetAtPath(rutaMaterialPlugAnaranjado, typeof(Material));
            }
        }
        else
        {
            Debug.LogError(this.name + ", Error. changeOriginalColorPlug(GameObject objectClick), Mandaste un objeto vacio.");
        }
        
    }

    private void CrearConexionCable(GameObject clickedGmObj)
    {
        Renderer rend = clickedGmObj.GetComponent<Renderer>();
        //rend.material.color = Color.red;
        //segundoPlug = true;

        CableComponent cableCompStart = lastClickedGmObj.GetComponent<CableComponent>();
        CableComponent cableCompEnd = clickedGmObj.GetComponent<CableComponent>();


        /*if (cableCompStart.endPoint != null)
        {
            CableComponent cableCompLastEndPointStart = cableCompStart.endPoint.GetComponent<CableComponent>();
            GameObject endPoint = cableCompStart.endPoint;
            Debug.Log("GameObject endPoint = cableCompStart.endPoint;");
            Destroy(cableCompLastEndPointStart);
            Debug.Log("Destroy(cableCompLastEndPointStart);");
            Destroy(cableCompStart);
            Debug.Log("Destroy(cableCompStart);");
            //cableCompLastEndPointStart.endPoint = null;
            //cableCompLastEndPointStart.startPoint = null;
            Destroy(lastClickedGmObj.GetComponent<LineRenderer>());
            Destroy(cableCompStart.endPoint.GetComponent<LineRenderer>());
            endPoint.AddComponent(typeof(CableComponent));
            Debug.Log("endPoint.AddComponent(typeof(CableComponent));");
            cableCompStart = lastClickedGmObj.AddComponent(typeof(CableComponent)) as CableComponent;
            Debug.Log("lastClickedGmObj.AddComponent(typeof(CableComponent)) as CableComponent;");
        }*/
        //CableComponent cableCompEnd2 = cableCompEnd;
        // GameObject endEndPoint = cableCompEnd2.endPoint;
        //    if (cableCompEnd2.endPoint != null)
        //    {
        //        CableComponent cableCompLastEndPointEnd = endEndPoint.GetComponent<CableComponent>();
        //        Destroy(cableCompLastEndPointEnd);
        //        Destroy(clickedGmObj.GetComponent<CableComponent>());
        //         Destroy(clickedGmObj.GetComponent<LineRenderer>());
        //         Destroy(endEndPoint.GetComponent<LineRenderer>());

        //         endEndPoint.AddComponent(typeof(CableComponent));
        //         cableCompEnd = clickedGmObj.AddComponent(typeof(CableComponent)) as CableComponent;
        //     Debug.Log("**********************************************cableCompEnd2.endPoint != null");
        // }

        bool eliminarCable = ComprobarEliminarConexion(cableCompStart, lastClickedGmObj);
        ComprobarEliminarConexion(cableCompEnd, clickedGmObj);
        if (!eliminarCable)
        {
            cableCompStart.startPoint = lastClickedGmObj;
            cableCompStart.endPoint = clickedGmObj;

            //cableCompStart.cableMaterial = (Material)Resources.Load("CableMaterial.mat", typeof(Material));
            //Primer conector en  ser seleccionado
            if (cableCompStart.todoCableMismoColor)
            {
                cableCompStart.startColor = AuxiliarModulos.endColor;
                cableCompStart.endColor = AuxiliarModulos.endColor;
            }
            else
            {
                cableCompStart.startColor = AuxiliarModulos.startColor;
                cableCompStart.endColor = AuxiliarModulos.endColor;
            }
            cableCompStart.cableMaterial = cableMaterial;
            cableCompStart.InitCableParticles();
            cableCompStart.InitLineRenderer();

            //Segundo conector en eser seleccionado
            if (cableCompEnd.todoCableMismoColor)
            {
                cableCompEnd.startColor = AuxiliarModulos.endColor;
                cableCompEnd.endColor = AuxiliarModulos.endColor;
            }
            else
            {
                cableCompEnd.startColor = AuxiliarModulos.startColor;
                cableCompEnd.endColor = AuxiliarModulos.endColor;
            }
            cableCompEnd.startPoint = clickedGmObj;
            cableCompEnd.endPoint = lastClickedGmObj;
            cableCompEnd.cableMaterial = cableMaterial;



            //lastClickedGmObj.GetComponent<Renderer>().material.color = Color.white;
            //clickedGmObj.GetComponent<Renderer>().material.color = Color.white;
            if (lastClickedGmObj.GetComponent<ChangeColorCables>() == null)
            {
                lastClickedGmObj.AddComponent<ChangeColorCables>();
            }
            if (clickedGmObj.GetComponent<ChangeColorCables>() == null)
            {
                clickedGmObj.AddComponent<ChangeColorCables>();
            }

            //Pasar energia a todos los cables
            lastClickedGmObj.GetComponent<Plugs>().EstablecerPropiedadesConexionesEntrantesPrueba();
            clickedGmObj.GetComponent<Plugs>().EstablecerPropiedadesConexionesEntrantesPrueba();
            lastClickedGmObj.GetComponent<Plugs>().Conectado = true;
            clickedGmObj.GetComponent<Plugs>().Conectado = true;
        }

        //Crear conexiones entre plugs
        clickedGmObj.SendMessage("CrearConexionPlugs", false, SendMessageOptions.DontRequireReceiver);
        lastClickedGmObj.SendMessage("CrearConexionPlugs", false, SendMessageOptions.DontRequireReceiver);

        //Regresar color original a conectores
        changeOriginalColorPlug(lastClickedGmObj);
        changeOriginalColorPlug(clickedGmObj);

        acabaDeCrearConexion = true;
    }

    private bool ComprobarEliminarConexion(CableComponent cableCompStart, GameObject objectStart)
    {
        bool eliminarCable = false;
        if (cableCompStart.endPoint != null)//Si este valor es diferente a nulo, kquiere decir que este plug ya tiene una conexión
        {
            eliminarCable = true;
            GameObject endPoint = cableCompStart.endPoint;
            CableComponent cableCompLastEndPointStart = endPoint.GetComponent<CableComponent>();

            //Deterner energia a todos los cables
            objectStart.GetComponent<Plugs>().EliminarPropiedadesConexionesEntradaPrueba();
            endPoint.GetComponent<Plugs>().EliminarPropiedadesConexionesEntradaPrueba();
            objectStart.GetComponent<Plugs>().Conectado = false;
            endPoint.GetComponent<Plugs>().Conectado = false;

            Debug.Log("GameObject endPoint = cableCompStart.endPoint;");
            cableCompLastEndPointStart.endPoint = null;
            cableCompStart.endPoint = null;
            cableCompLastEndPointStart.showRender = true;
            cableCompStart.showRender = true;

            //Desturir elementos Line Render
            LineRenderer lineRenderEndPoint = endPoint.GetComponent<LineRenderer>();
            if (lineRenderEndPoint != null)
            {
                Destroy(lineRenderEndPoint);
                endPoint.AddComponent<LineRenderer>();
            }

            LineRenderer lineRenderStartPoint = objectStart.GetComponent<LineRenderer>();
            if (lineRenderStartPoint != null)
            {
                Destroy(lineRenderStartPoint);
                endPoint.AddComponent<LineRenderer>();
            }
        }
        return eliminarCable;
    }

}