
using UnityEngine;
using System.Collections;
public class ClickDetector : MonoBehaviour
{
    public bool HandleLeftClick = true;
    public bool HandleRightClick = true;
    public bool HandleMiddleClick = false;
    public string OnLeftClickMethodName = "Prueba";
    public string OnRightClickMethodName = "Prueba";
    public string OnMiddleClickMethodName = "Prueba";
    public LayerMask layerMask;
    public Camera camara;
    public GameObject lastClickedGmObj;
    void Update()
    {
        GameObject clickedGmObj = null;
        bool clickedGmObjAcquired = false;
        // Left click
        if (HandleLeftClick && Input.GetMouseButtonDown(0))
        {
            /*if (!clickedGmObjAcquired)
            {*/
            clickedGmObj = GetClickedGameObject();
            clickedGmObjAcquired = true;
            /*}*/
            if (clickedGmObj != null)
                clickedGmObj.SendMessage(OnLeftClickMethodName, null, SendMessageOptions.DontRequireReceiver);
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
                Debug.Log("Manda mensaje, " + OnRightClickMethodName + ", *******Objeto clic: " + clickedGmObj.name);
                //clickedGmObj.SendMessage("Prueba");
                clickedGmObj.SendMessage("OpenCloseMenuChangeModule", 1, SendMessageOptions.DontRequireReceiver);
                //clickedGmObj.SendMessage("Prueba", 1);
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


}
