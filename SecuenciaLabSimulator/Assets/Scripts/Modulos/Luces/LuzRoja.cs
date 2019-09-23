using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LuzRoja : MonoBehaviour
{
    #region Atributos
    [Header("Parametros Focos")]
    [Header("Materiales")]
    [SerializeField] public string rutaPlasticoRojoApagado = "Assets/Materials/PLasticos/PlasticoTraslucidoRojoApagado.mat";
    [SerializeField] public string rutaPlasticoRojoEncendido = "Assets/Materials/PLasticos/PlasticoTraslucidoRojoEncendido.mat";
    [SerializeField] public Material plasticoRojoApagado;
    [SerializeField] public Material plasticoRojoEncendido;
    [Header("Padre Total")]
    public GameObject padreTotalComponente;
    [Header("Particulas")]
    public GameObject currentParticle;
    private ParticlesError particleError;
    public int currentTypeParticleError = 0;
    public bool focoRojoEncendido = false;
    public bool focoAveriado = false;
    //Variables de debug
    [Header("Debug")]
    public bool debugMode = false;
    [SerializeField] public bool pruebaDeLuz = true;
    #endregion

    #region Propiedades
    public bool DebugMode
    {
        get => debugMode;
        set => debugMode = value;
    }

    public int CurrentTypeParticleError
    {
        get => currentTypeParticleError;
        set => currentTypeParticleError = value;
    }

    public bool FocoAveriado
    {
        get => focoAveriado;
        set => focoAveriado = value;
    }

    public bool FocoRojoEncendido
    {
        get => focoRojoEncendido;
        set => focoRojoEncendido = value;
    }
    #endregion

    #region Inicializacion
    // Start is called before the first frame update
    void Start()
    {
        particleError = new ParticlesError();
        padreTotalComponente = new GameObject();
        plasticoRojoApagado = (Material)AssetDatabase.LoadAssetAtPath(rutaPlasticoRojoApagado, typeof(Material));
        plasticoRojoEncendido = (Material)AssetDatabase.LoadAssetAtPath(rutaPlasticoRojoEncendido, typeof(Material));
    }
    #endregion

    // Update is called once per frame
    void Update()
    {

    }

    #region Comportamiento
    public bool ComprobarEstado(GameObject plugArriba, GameObject plugAbajo)
    {
        bool estaCorrectaConexion = true;
        Plugs plugArribaCompPlug = plugArriba.GetComponent<Plugs>();
        Plugs plugAbajoCompPlug = plugAbajo.GetComponent<Plugs>();

        if (plugArribaCompPlug != null && plugAbajoCompPlug != null)
        {
            plugArribaCompPlug.EstablecerPropiedadesConexionesEntrantes();
            plugAbajoCompPlug.EstablecerPropiedadesConexionesEntrantes();

            if (plugArribaCompPlug.Conectado && plugAbajoCompPlug.Conectado)
            {
                if (plugArribaCompPlug.TipoConexion == (int)AuxiliarModulos.TiposConexiones.Linea
                    && plugAbajoCompPlug.TipoConexion == (int)AuxiliarModulos.TiposConexiones.Neutro)// Correcto - Linea y neutro conectado en de manera correcta
                {
                    estaCorrectaConexion = true;
                    focoAveriado = false;
                    EncenderFoco();
                    if (DebugMode)
                    {
                        Debug.Log(padreTotalComponente.name + ") " + this.name + " - " + this.tag + " - if(plugIzquierdoCompPlug.TipoConexion == 1 && plugDerechoCompPlug.TipoConexion == 2) - Conectado");
                    }
                }
                else if (plugArribaCompPlug.TipoConexion == (int)AuxiliarModulos.TiposConexiones.Neutro
                    && plugAbajoCompPlug.TipoConexion == (int)AuxiliarModulos.TiposConexiones.Linea) //Averia - Linea y neutro invertido
                {
                    estaCorrectaConexion = false;
                    focoAveriado = true;
                    ApagarFoco();
                    if (DebugMode)
                    {
                        Debug.Log(padreTotalComponente.name + ") " + this.name + " - " + this.tag + " - (plugArribaCompPlug.TipoConexion == 2 && plugAbajoCompPlug.TipoConexion == 1) - Conectado - Debido a que los focos tienen polaridad, al invertir la conexión nom encienden.");
                    }
                }
                else if (plugArribaCompPlug.TipoConexion == (int)AuxiliarModulos.TiposConexiones.Linea
                    && plugAbajoCompPlug.TipoConexion == (int)AuxiliarModulos.TiposConexiones.Linea) // Avaeria - Dos lineas conectadas al mismo tiempo
                {
                    if (plugArribaCompPlug.Linea == plugAbajoCompPlug.Linea)
                    {
                        estaCorrectaConexion = false;
                        focoAveriado = false;
                        ApagarFoco();
                        if (DebugMode)
                        {
                            Debug.Log(padreTotalComponente.name + ") " + this.name + " - " + this.tag + " - (plugArribaCompPlug.Linea == plugAbajoCompPlug.Linea) - Conectado - Debido a que son la misma linea simplemente no enciende.");
                        }
                    }
                    else if (plugArribaCompPlug.Linea != plugAbajoCompPlug.Linea)
                    {
                        estaCorrectaConexion = false;
                        focoAveriado = true;
                        ApagarFoco();
                        if (DebugMode)
                        {
                            Debug.LogError(padreTotalComponente.name + ") " + this.name + " - " + this.tag + " - if (plugArribaCompPlug.Linea != plugAbajoCompPlug.Linea) - Conectado - Debido a que son lineas diferentes el foco se quema.");
                        }
                    }
                    else
                    {
                        estaCorrectaConexion = false;
                        focoAveriado = false;
                        //ApagarFoco();
                        //EliminarMaterial();
                        //AuxiliarModulos.EliminarMaterial(this.gameObject);
                        if (DebugMode)
                        {
                            Debug.LogError(padreTotalComponente.name + ") " + this.name + " - " + this.tag + " - NO DEBERIA ENTRAR AQUI - (plugArribaCompPlug.TipoConexion == 1 && plugAbajoCompPlug.TipoConexion == 1)");
                        }
                    }

                }
                else if (plugArribaCompPlug.TipoConexion == (int)AuxiliarModulos.TiposConexiones.Neutro
                    && plugAbajoCompPlug.TipoConexion == (int)AuxiliarModulos.TiposConexiones.Neutro) // Correcto - Dos neutros conectados, no pasa nada
                {
                    estaCorrectaConexion = false;
                    focoAveriado = false;
                    ApagarFoco();
                    if (DebugMode)
                    {
                        Debug.Log(padreTotalComponente.name + ") " + this.name + " - " + this.tag + " - (plugIzquierdoCompPlug.TipoConexion == 2 && plugDerechoCompPlug.TipoConexion == 2) - Conectado");
                    }
                }
                else
                {
                    estaCorrectaConexion = false;
                    focoAveriado = false;
                    //ApagarFoco();
                    //EliminarMaterial();
                    //AuxiliarModulos.EliminarMaterial(this.gameObject);
                    Debug.LogError(padreTotalComponente.name + ") " + this.name + " - " + this.tag + " - Este caso de uso todavia no esta programado - No entro a ningun caso");
                }
            }
            else
            {
                estaCorrectaConexion = false;
                focoAveriado = false;
                //ApagarFoco();
                if (DebugMode)
                {
                    Debug.Log(padreTotalComponente.name + ") " + this.name + " - " + this.tag + " - if (plugIzquierdoCompPlug.Conectado && plugDerechoCompPlug.Conectado) - NO esta conectados");
                }
            }
        }
        else
        {
            estaCorrectaConexion = false;
            Debug.LogError(padreTotalComponente.name + ") " + this.name + " - " + this.tag + " - if(plugArribaCompPlug != null && plugAbajoCompPlug != null) - Alguno de los dos es nulo, plugArribaCompPlug: " + plugArribaCompPlug + ", plugAbajoCompPlug: " + plugAbajoCompPlug);
        }
        return estaCorrectaConexion;
    }

    /*void EliminarMaterial()
    {
        Renderer focoAmarillo = transform.GetComponent<Renderer>();
        focoAmarillo.material = null;
    }*/

    public void EncenderFoco()
    {
        Renderer focoCircularAzul = transform.GetComponent<Renderer>();
        focoCircularAzul.material = plasticoRojoEncendido;
        focoRojoEncendido = true;
        ComprobarEstadoAveria();
    }

    public void ApagarFoco()
    {
        Renderer focoCircularAzul = transform.GetComponent<Renderer>();
        focoCircularAzul.material = plasticoRojoApagado;
        focoRojoEncendido = false;
        ComprobarEstadoAveria();
    }

    void ComprobarEstadoAveria()
    {
        if (focoAveriado)
        {
            if (currentParticle == null)
            {
                CrearAveria();
            }
        }
        else
        {
            if (currentParticle != null)
            {
                QuitarAveria();
            }
        }
    }

    public void CrearAveria()
    {
        currentParticle = particleError.CrearParticulasError(currentTypeParticleError, transform.position, transform.rotation.eulerAngles, new Vector3(0.5f, 0.5f, 0.5f));
        currentParticle.transform.parent = this.gameObject.transform;
        focoAveriado = true;
    }

    public void QuitarAveria()
    {
        particleError.DestruirParticulasError(currentParticle);
        focoAveriado = false;
    }
    #endregion

    private void OnMouseDown()
    {
        Renderer luzRojaRen = transform.GetComponent<Renderer>();
        if (pruebaDeLuz)
        {
            luzRojaRen.material = plasticoRojoEncendido;
            Debug.Log("Cambio de material Luminoso - Luz Roja");
            pruebaDeLuz = false;
        }
        else
        {
            luzRojaRen.material = plasticoRojoApagado;
            Debug.Log("Cambio de material Opaco - Luz Roja");
            pruebaDeLuz = true;
        }
    }
}
