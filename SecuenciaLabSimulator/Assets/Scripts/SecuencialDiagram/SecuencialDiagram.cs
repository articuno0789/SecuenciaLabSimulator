using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecuencialDiagram : MonoBehaviour
{
    /*List<Nodo> nodos;

    const int RADIO = 20;

    Nodo selOrig;
    Nodo selNodo;

    private void Start()
    {
        nodos = new List<Nodo>();
        Debug.Log("se carga la forma");
    }

    private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
    {
        nodos.Add(new Nodo(e.X, e.Y, RADIO));
        //Refresh();
        Debug.Log("soble click");
    }

    private void Form1_Paint(object sender, PaintEventArgs e)
    {
        foreach (Nodo item in nodos)
        {
            item.DibujaArista(e.Graphics);
        }
        foreach (Nodo item in nodos)
        {
            item.DibujaNodo(e.Graphics);
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("CLIC SOBRE LA VENTANA");
        //detecta
        Nodo n = null;
        foreach (Nodo item in nodos)
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector2 mousePosition2d = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            if (item.Adentro(mousePosition2d))
            {
                n = item;
                break;
            }
        }

        // conecta dos nodos
        if (Input.GetMouseButtonDown(1))//right
        {
            if (selOrig == null)
            {
                selOrig = n;
            }
            else
            {
                if (n != null) selOrig.ConectarA(n);
                selOrig = null;
                //Refresh();
            }
        }

        // inicia movimiento del nodo
        if (Input.GetMouseButtonDown(0))//left
        {
            selNodo = n;
        }
    }

    private void OnMouseOver()
    {
        if (selNodo == null) return;
        // mueve nodo
        if (Input.GetMouseButtonDown(0))//left
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector2 mousePosition2d = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            selNodo.Posicion(mousePosition2d);
            //Refresh();
            Debug.Log("MOUSE EN MOVIMIETNO.");
        }
    }
    /*
    private void Form1_KeyDown(object sender, KeyEventArgs e)
    {
        //elimina el nodos seleccionado
        if (e.KeyCode == Keys.A)
        {
            if (selNodo != null)
            {
                Console.WriteLine("Destruir nodo");
                nodos.Remove(selNodo);
                foreach (Nodo item in nodos)
                {
                    item.Desconectar(selNodo);
                }
                Refresh();
                selNodo = null;
            }
        }
    }
    */
}
