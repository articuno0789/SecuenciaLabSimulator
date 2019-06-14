using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mod2PushButton : MonoBehaviour
{
    private Animation ani;
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (name.Contains("BotonCuadradoRojo")) {
            Debug.Log("Entra a presionar boton rojo cuadrado");
            ani.Play("Mod2PresBotonCuadradoRojo");
        }else if (name.Contains("BotonCuadradoVerde"))
        {
            Debug.Log("Entra a presionar boton verde cuadrado");
            ani.Play("Mod2PresBotonCuadradoVerde");
        }
    }
}
