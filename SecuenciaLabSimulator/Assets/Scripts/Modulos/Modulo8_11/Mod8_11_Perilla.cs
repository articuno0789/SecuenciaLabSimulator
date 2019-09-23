using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mod8_11_Perilla : MonoBehaviour
{
    #region Atributos
    [Header("Animaciones")]
    private new Animation animation;
    private bool ani = true;

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
        if (ani)
        {
            Debug.Log("Entra a presionar Perilla MA");
            animation.Play("PerillaMA");
            ani = false;
        }
        else
        {
            Debug.Log("Entra a presionar Perilla AM");
            animation.Play("PerillaAM");
            ani = true;
        }
    }
}
