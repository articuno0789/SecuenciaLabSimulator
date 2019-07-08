using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mod3PushButton : MonoBehaviour
{
    #region Atributos
    private Animation animation;
    #endregion

    #region Inicializacion
    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animation>();
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("Entra a presionar boton");
        animation.Play("Mod3PresBotonCircular");
    }
}
