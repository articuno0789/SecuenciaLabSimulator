
using UnityEngine;
using System.Collections;
using UnityEditor;

public class ClickDetector : MonoBehaviour
{
    public Material cableMaterial;
    public bool HandleLeftClick = false;
    public bool HandleRightClick = true;
    public bool HandleMiddleClick = false;
    public string OnLeftClickMethodName = "Prueba";
    public string OnRightClickMethodName = "Prueba";
    public string OnMiddleClickMethodName = "Prueba";
    public LayerMask layerMask;
    public Camera camara;
    public GameObject lastClickedGmObj;
    public ColorPicker colorPicker;
    private string rutaMaterialPlugAnaranjado = "Assets/Materials/EntradaPlug/AnaranjadoPlug.mat";
    private string rutaMaterialPlugNegro = "Assets/Materials/EntradaPlug/ObscuroPlug.mat";
    void Update()
    {
        GameObject clickedGmObj = null;
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
                if(lastClickedGmObj != null) {
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
                            rend.material.color = Color.green;
                        }
                        else
                        {
                            Renderer rend = clickedGmObj.GetComponent<Renderer>();
                            //rend.material.color = Color.yellow;
                            Debug.Log("-------CAMBIO DE COLOR");
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
                            Renderer rend = clickedGmObj.GetComponent<Renderer>();
                            //rend.material.color = Color.red;
                            segundoPlug = true;

                            CableComponent cableCompStart = lastClickedGmObj.GetComponent<CableComponent>();
                            CableComponent cableCompEnd = clickedGmObj.GetComponent<CableComponent>();

                            if (cableCompStart.endPoint != null)
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
                            }
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


                            cableCompStart.endPoint = clickedGmObj;
                            cableCompStart.startPoint = lastClickedGmObj;

                            //cableCompStart.cableMaterial = (Material)Resources.Load("CableMaterial.mat", typeof(Material));
                            cableCompStart.cableMaterial = cableMaterial;
                            cableCompStart.InitCableParticles();
                            cableCompStart.InitLineRenderer();

                            

                            cableCompEnd.endPoint = lastClickedGmObj;
                            cableCompEnd.startPoint = clickedGmObj;
                            cableCompEnd.cableMaterial = cableMaterial;

                            //lastClickedGmObj.GetComponent<Renderer>().material.color = Color.white;
                            //clickedGmObj.GetComponent<Renderer>().material.color = Color.white;
                            if(lastClickedGmObj.GetComponent<ChangeColorCables>() == null)
                            {
                                lastClickedGmObj.AddComponent<ChangeColorCables>();
                            }
                            if (clickedGmObj.GetComponent<ChangeColorCables>() == null)
                            {
                                clickedGmObj.AddComponent<ChangeColorCables>();
                            }
                            changeOriginalColorPlug(lastClickedGmObj);
                            changeOriginalColorPlug(clickedGmObj);
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
                }else
                if (clickedGmObj.name.Contains("EntradaPlug")){
                    clickedGmObj.SendMessage("OpenCloseMenuChangeColorCable", 1, SendMessageOptions.DontRequireReceiver);
                    colorPicker.selectedPlug = clickedGmObj;
                    clickedGmObj = null;
                    lastClickedGmObj = null;
                }
                else if (clickedGmObj.name.Contains("Total_Perilla"))
                {
                    GameObject panelSetValueKnob = GameObject.Find("ButtonSetValueKnob");
                    GameObject inputFieldCurrentValue = GameObject.Find("InputFieldCurrentValue");
                    if (panelSetValueKnob != null && inputFieldCurrentValue != null)
                    {
                        panelSetValueKnob.GetComponent<SetValueKnob>().perillaSeleccionada = clickedGmObj;
                        inputFieldCurrentValue.GetComponent<SetValueKnob>().perillaSeleccionada = clickedGmObj;
                    }
                    clickedGmObj.SendMessage("OpenCloseMenuSetValueKnob", 1, SendMessageOptions.DontRequireReceiver);
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
        if (objectClick.name.Contains("EntradaPlugNegro"))
        {
            objectClick.GetComponent<Renderer>().material = (Material)AssetDatabase.LoadAssetAtPath(rutaMaterialPlugNegro, typeof(Material));
        }
        else
        {
            objectClick.GetComponent<Renderer>().material = (Material)AssetDatabase.LoadAssetAtPath(rutaMaterialPlugAnaranjado, typeof(Material));
        }
    }

}
