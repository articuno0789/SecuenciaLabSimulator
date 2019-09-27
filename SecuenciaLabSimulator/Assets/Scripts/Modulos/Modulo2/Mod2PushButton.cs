using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Mod2PushButton : MonoBehaviour
{
    #region Atributos
    [Header("Activado")]
    public bool botonActivado = false;
    [Header("Botón Contrario")]
    public GameObject botonContrario;
    [Header("Tipo Botón")]
    public int tipo = (int)AuxiliarModulos.TipoBoton.SinTipo; //0) sin tipo, 1) boton verde, 2)boton rojo
    [Header("Materiales")]
    [SerializeField] public string rutaPlasticoVerdeApagado = "Assets/Materials/Botones/RojoVerdesCuadrados/BotonVerdeCuadrado.mat";
    [SerializeField] public string rutaPlasticoVerdeEncendido = "Assets/Materials/Botones/RojoVerdesCuadrados/BotonVerdeCuadradoEncendido.mat";
    [SerializeField] public Material plasticoVerdeApagado;
    [SerializeField] public Material plasticoVerdeEncendido;

    [SerializeField] public string rutaPlasticoRojoApagado = "Assets/Materials/Botones/RojoVerdesCuadrados/BotonRojoCuadrado.mat";
    [SerializeField] public string rutaPlasticoRojoEncendido = "Assets/Materials/Botones/RojoVerdesCuadrados/BotonRojoCuadradoEncendido.mat";
    [SerializeField] public Material plasticoRojoApagado;
    [SerializeField] public Material plasticoRojoEncendido;
    [Header("Animaciones")]
    private new Animation animation;
    public Animation Animation { get => animation; set => animation = value; }
    #endregion

    #region Inicializacion
    // Start is called before the first frame update
    void Awake()
    {
        animation = GetComponent<Animation>();
        plasticoVerdeApagado = AuxiliarModulos.RegresarObjetoMaterial("BotonVerdeCuadrado");
        plasticoVerdeEncendido = AuxiliarModulos.RegresarObjetoMaterial("BotonVerdeCuadradoEncendido");
        plasticoRojoApagado = AuxiliarModulos.RegresarObjetoMaterial("BotonRojoCuadrado");
        plasticoRojoEncendido = AuxiliarModulos.RegresarObjetoMaterial("BotonRojoCuadradoEncendido");
        //plasticoVerdeApagado = (Material)AssetDatabase.LoadAssetAtPath(rutaPlasticoVerdeApagado, typeof(Material));
        //plasticoVerdeEncendido = (Material)AssetDatabase.LoadAssetAtPath(rutaPlasticoVerdeEncendido, typeof(Material));
        //plasticoRojoApagado = (Material)AssetDatabase.LoadAssetAtPath(rutaPlasticoRojoApagado, typeof(Material));
        //plasticoRojoEncendido = (Material)AssetDatabase.LoadAssetAtPath(rutaPlasticoRojoEncendido, typeof(Material));
    }

    public void EstablecerTipoVerde()
    {
        tipo = (int)AuxiliarModulos.TipoBoton.BotonVerde;
    }

    public void EstablecerTipoRojo()
    {
        tipo = (int)AuxiliarModulos.TipoBoton.BotonRojo;
    }
    
    public bool EstaActivado()
    {
        return botonActivado;
    }

    public void EstablecerBotonPresionado()
    {
        botonActivado = true;
        IluminarBotonSeleccionado();
    }

    public void EstablecerBotonDespresionado()
    {
        botonActivado = false;
        DesiluminarBotonSeleccionado();
    }

    public void IluminarBotonSeleccionado()
    {
        Renderer botonSeleccionado = transform.GetComponent<Renderer>();
        if(botonSeleccionado != null && tipo == (int)AuxiliarModulos.TipoBoton.BotonVerde)
        {
            botonSeleccionado.materials[0] = plasticoVerdeEncendido;
            botonSeleccionado.material = plasticoVerdeEncendido;
            botonActivado = true;
        }else if(botonSeleccionado != null && tipo == (int)AuxiliarModulos.TipoBoton.BotonRojo)
        {
            botonSeleccionado.materials[0] = plasticoRojoEncendido;
            botonSeleccionado.material = plasticoRojoEncendido;
            botonActivado = true;
        }
        else
        {
            Debug.LogError(this.name + ", void IluminarBotonSeleccionado() - Error. No se ha asignado el tipo para el botón.");
        }
    }

    public void DesiluminarBotonSeleccionado()
    {
        Renderer botonSeleccionado = transform.GetComponent<Renderer>();
        if (botonSeleccionado != null && tipo == (int)AuxiliarModulos.TipoBoton.BotonVerde)
        {
            botonSeleccionado.material = plasticoVerdeApagado;
            botonActivado = false;
        }
        else if (botonSeleccionado != null && tipo == (int)AuxiliarModulos.TipoBoton.BotonRojo)
        {
            botonSeleccionado.material = plasticoRojoApagado;
            botonActivado = false;
        }
        else
        {
            Debug.LogError(this.name + ", void DesiluminarBotonSeleccionado() - Error. No se ha asignado el tipo para el botón.");
        }
    }

    #endregion

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
        if (botonActivado)
        {
            EstablecerBotonDespresionado();
            botonContrario.GetComponent<Mod2PushButton>().EstablecerBotonPresionado();
        }
        else
        {
            EstablecerBotonPresionado();
            botonContrario.GetComponent<Mod2PushButton>().EstablecerBotonDespresionado();
        }
    }
}
