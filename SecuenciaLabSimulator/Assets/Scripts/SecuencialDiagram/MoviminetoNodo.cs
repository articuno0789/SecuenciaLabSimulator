using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoviminetoNodo : MonoBehaviour
{
    #region Atributos
    //Debug
    public bool debug = true;
    #endregion

    #region Comportamiento
    /* En este método se realiza el movimiento de un objeto mientras que el mouse arrastre al objeto.
     * Se toma en cuenta la posición de la camara principal, para que todos los elementos se muevan en
     * la misma posición.*/
    void OnMouseDrag()
    {
        if (debug)
        {
            Debug.Log("Se esta moviendo objeto.");
        }
        float distance_to_screen = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance_to_screen));
    }
    #endregion
}
