using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Mod3PushButton : MonoBehaviour
{
    #region Atributos
    public bool botonActivado = false;
    public int tipo = 0; //0) sin tipo, 1) boton verde, 2)boton rojo
    [SerializeField] public string rutaPlasticoVerdeApagado = "Assets/Materials/Botones/RojoVerdeCircular/BotonVerderCircular.mat";
    [SerializeField] public string rutaPlasticoVerdeEncendido = "Assets/Materials/Botones/RojoVerdeCircular/BotonVerderCircularEncendido.mat";
    [SerializeField] public Material plasticoVerdeApagado;
    [SerializeField] public Material plasticoVerdeEncendido;

    [SerializeField] public string rutaPlasticoRojoApagado = "Assets/Materials/Botones/RojoVerdeCircular/BotonRojoCircular.mat";
    [SerializeField] public string rutaPlasticoRojoEncendido = "Assets/Materials/Botones/RojoVerdeCircular/BotonRojoCircularEncendido.mat";
    [SerializeField] public Material plasticoRojoApagado;
    [SerializeField] public Material plasticoRojoEncendido;

    private new Animation animation;
    public Animation Animation { get => animation; set => animation = value; }
    #endregion

    #region Inicializacion
    // Start is called before the first frame update
    void Awake()
    {
        Animation = GetComponent<Animation>();
        plasticoVerdeApagado = (Material)AssetDatabase.LoadAssetAtPath(rutaPlasticoVerdeApagado, typeof(Material));
        plasticoVerdeEncendido = (Material)AssetDatabase.LoadAssetAtPath(rutaPlasticoVerdeEncendido, typeof(Material));
        plasticoRojoApagado = (Material)AssetDatabase.LoadAssetAtPath(rutaPlasticoRojoApagado, typeof(Material));
        plasticoRojoEncendido = (Material)AssetDatabase.LoadAssetAtPath(rutaPlasticoRojoEncendido, typeof(Material));
    }

    public void EstablecerTipoVerde()
    {
        tipo = 1;
    }

    public void EstablecerTipoRojo()
    {
        tipo = 2;
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
        if (tipo == 1)
        {
            botonSeleccionado.materials[0] = plasticoVerdeEncendido;
            botonSeleccionado.material = plasticoVerdeEncendido;
            botonActivado = true;
        }
        else if (tipo == 2)
        {
            botonSeleccionado.materials[0] = plasticoRojoEncendido;
            botonSeleccionado.material = plasticoRojoEncendido;
            botonActivado = true;
        }
        else
        {
            Debug.LogError(name + " No se ha asignado el tipo para el botón.");
        }
    }

    public void DesiluminarBotonSeleccionado()
    {
        Renderer botonSeleccionado = transform.GetComponent<Renderer>();
        if (tipo == 1)
        {
            botonSeleccionado.material = plasticoVerdeApagado;
            botonActivado = false;
        }
        else if (tipo == 2)
        {
            botonSeleccionado.material = plasticoRojoApagado;
            botonActivado = false;
        }
        else
        {
            Debug.LogError(name + " No se ha asignado el tipo para el botón.");
        }
    }

    #endregion

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("Entra a presionar boton");
        if (botonActivado)
        {
            EstablecerBotonDespresionado();
        }
        else
        {
            EstablecerBotonPresionado();
        }
        Animation.Play("Mod3PresBotonCircular");
    }
}
