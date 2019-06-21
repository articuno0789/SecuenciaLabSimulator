using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mod2PushButton : MonoBehaviour
{
    private Animation animation;

    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (name.Contains("BotonCuadradoRojo")) {
            Debug.Log("Entra a presionar boton rojo cuadrado");
            animation.Play("Mod2PresBotonCuadradoRojo");
        }else if (name.Contains("BotonCuadradoVerde"))
        {
            Debug.Log("Entra a presionar boton verde cuadrado");
            animation.Play("Mod2PresBotonCuadradoVerde");
        }
    }
}
