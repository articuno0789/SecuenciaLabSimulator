using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mod3PushButton : MonoBehaviour
{
    #region Atributos
    private new Animation animation;
    public Animation Animation { get => animation; set => animation = value; }
    #endregion

    #region Inicializacion
    // Start is called before the first frame update
    void Start()
    {
        Animation = GetComponent<Animation>();
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("Entra a presionar boton");
        Animation.Play("Mod3PresBotonCircular");
    }
}
