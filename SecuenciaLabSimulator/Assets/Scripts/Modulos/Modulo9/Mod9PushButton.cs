using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mod9PushButton : MonoBehaviour
{
    #region Atributos
    [Header("Animaciones")]
    private new Animation animation;
    public Animation Animation { get => animation; set => animation = value; }
    #endregion

    #region Inicializacion
    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponent<Animation>();
    }
    #endregion

    #region Comportamiento
    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    private void OnMouseDown()
    {
        Debug.Log("Entra a presionar boton azul circular modulo 9--");
        animation.Play("Mod9PresBotonCircularAzul");
    }
}
