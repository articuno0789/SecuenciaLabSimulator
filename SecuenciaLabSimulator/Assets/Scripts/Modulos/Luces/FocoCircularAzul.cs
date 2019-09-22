using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FocoCircularAzul : MonoBehaviour
{
    #region Atributos
    [SerializeField] public bool pruebaDeFocoCircularAzul = true;
    [SerializeField] public string rutaPlasticoCircularAzulApagado = "Assets/Materials/PLasticos/PlasticoTraslucidoCircularAzulApagado.mat";
    [SerializeField] public string rutaPlasticoCircularAzulEncendido = "Assets/Materials/PLasticos/PlasticoTraslucidoCircularAzulEncendido.mat";
    [SerializeField] public Material plasticoCircularAzulApagado;
    [SerializeField] public Material plasticoCircularAzulEncendido;
    public GameObject padreTotalComponente;
    public GameObject currentParticle;
    private ParticlesError particleError;
    public int currentTypeParticleError = 0;
    public bool focoCircularAzulEncendido = false;
    public bool focoAveriado = false;
    public bool debugMode = false;
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

    public bool FocoCircularAzulEncendido
    {
        get => focoCircularAzulEncendido;
        set => focoCircularAzulEncendido = value;
    }
    #endregion

    #region Inicializacion
    // Start is called before the first frame update
    void Start()
    {
        particleError = new ParticlesError();
        padreTotalComponente = new GameObject();
        plasticoCircularAzulApagado = (Material)AssetDatabase.LoadAssetAtPath(rutaPlasticoCircularAzulApagado, typeof(Material));
        plasticoCircularAzulEncendido = (Material)AssetDatabase.LoadAssetAtPath(rutaPlasticoCircularAzulEncendido, typeof(Material));
    }
    #endregion

    // Update is called once per frame
    void Update()
    {

    }

    #region Comportamiento
    public void ComprobarEstado(GameObject plugIzquierdo, GameObject plugDerecho)
    {
        Plugs plugIzquierdoCompPlug = plugIzquierdo.GetComponent<Plugs>();
        Plugs plugDerechoCompPlug = plugDerecho.GetComponent<Plugs>();

        if (plugIzquierdoCompPlug != null && plugDerechoCompPlug != null)
        {
            plugIzquierdoCompPlug.EstablecerPropiedadesConexionesEntrantes();
            plugDerechoCompPlug.EstablecerPropiedadesConexionesEntrantes();

            if (plugIzquierdoCompPlug.Conectado && plugDerechoCompPlug.Conectado)
            {
                if (plugIzquierdoCompPlug.TipoConexion == 1 && plugDerechoCompPlug.TipoConexion == 2)// Correcto - Linea y neutro conectado en de manera correcta
                {
                    focoAveriado = false;
                    EncenderFoco();
                    if (DebugMode)
                    {
                        Debug.Log(padreTotalComponente.name + ") " + this.name + " - FOCO CiRCULAR AZUL - if(plugIzquierdoCompPlug.TipoConexion == 1 && plugDerechoCompPlug.TipoConexion == 2) - Conectado");
                    }
                }
                else if (plugIzquierdoCompPlug.TipoConexion == 2 && plugDerechoCompPlug.TipoConexion == 1) //Averia - Linea y neutro invertido
                {
                    focoAveriado = false;
                    ApagarFoco();
                    if (DebugMode)
                    {
                        Debug.Log(padreTotalComponente.name + ") " + this.name + " - FOCO CiRCULAR AZUL - (plugArribaCompPlug.TipoConexion == 2 && plugAbajoCompPlug.TipoConexion == 1) - Conectado - Debido a que los focos tienen polaridad, al invertir la conexión nom encienden.");
                    }
                }
                else if (plugIzquierdoCompPlug.TipoConexion == 1 && plugDerechoCompPlug.TipoConexion == 1) // Avaeria - Dos lineas conectadas al mismo tiempo
                {
                    if (plugIzquierdoCompPlug.Linea == plugDerechoCompPlug.Linea)
                    {
                        focoAveriado = false;
                        ApagarFoco();
                        if (DebugMode)
                        {
                            Debug.Log(padreTotalComponente.name + ") " + this.name + " - FOCO CiRCULAR AZUL - (plugArribaCompPlug.Linea == plugAbajoCompPlug.Linea) - Conectado - Debido a que son la misma linea simplemente no enciende.");
                        }
                    }
                    else if (plugIzquierdoCompPlug.Linea != plugDerechoCompPlug.Linea)
                    {
                        focoAveriado = true;
                        ApagarFoco();
                        if (DebugMode)
                        {
                            Debug.LogError(padreTotalComponente.name + ") " + this.name + " - FOCO CiRCULAR AZUL - if (plugArribaCompPlug.Linea != plugAbajoCompPlug.Linea) - Conectado - Debido a que son lineas diferentes el foco se quema.");
                        }
                    }
                    else
                    {
                        EliminarMaterial();
                        focoAveriado = false;
                        ApagarFoco();
                        if (DebugMode)
                        {
                            Debug.LogError(padreTotalComponente.name + ") " + this.name + " - FOCO CiRCULAR AZUL - NO DEBERIA ENTRAR AQUI - (plugArribaCompPlug.TipoConexion == 1 && plugAbajoCompPlug.TipoConexion == 1)");
                        }
                    }

                }
                else if (plugIzquierdoCompPlug.TipoConexion == 2 && plugDerechoCompPlug.TipoConexion == 2) // Correcto - Dos neutros conectados, no pasa nada
                {
                    focoAveriado = false;
                    ApagarFoco();
                    if (DebugMode)
                    {
                        Debug.Log(padreTotalComponente.name + ") " + this.name + " - FOCO CiRCULAR AZUL - (plugIzquierdoCompPlug.TipoConexion == 2 && plugDerechoCompPlug.TipoConexion == 2) - Conectado");
                    }
                }
                else
                {
                    //EliminarMaterial();
                    focoAveriado = false;
                    ApagarFoco();
                    if (DebugMode)
                    {
                        Debug.LogError(padreTotalComponente.name + ") " + this.name + " - FOCO CiRCULAR AZUL - Este caso de uso todavia no esta programado - No entro a ningun caso");
                    }
                }
            }
            else
            {
                focoAveriado = false;
                ApagarFoco();
                if (DebugMode)
                {
                    Debug.Log(padreTotalComponente.name + ") " + this.name + " - FOCO CiRCULAR AZUL - if (plugIzquierdoCompPlug.Conectado && plugDerechoCompPlug.Conectado) - NO esta conectados");
                }
            }
        }
        else
        {
            Debug.LogError(padreTotalComponente.name + ") " + this.name + " - FOCO CiRCULAR AZUL - if(plugArribaCompPlug != null && plugAbajoCompPlug != null) - Alguno de los dos es nulo, plugArribaCompPlug: " + plugIzquierdoCompPlug + ", plugAbajoCompPlug: " + plugDerechoCompPlug);
        }
    }

    void EliminarMaterial()
    {
        Renderer focoAzul = transform.GetComponent<Renderer>();
        focoAzul.material = null;
    }

    public void EncenderFoco()
    {
        Renderer focoCircularAzul = transform.GetComponent<Renderer>();
        focoCircularAzul.material = plasticoCircularAzulEncendido;
        focoCircularAzulEncendido = true;
        ComprobarEstadoAveria();
    }

    public void ApagarFoco()
    {
        Renderer focoCircularAzul = transform.GetComponent<Renderer>();
        focoCircularAzul.material = plasticoCircularAzulApagado;
        focoCircularAzulEncendido = false;
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
        Renderer focoCircularAzul = transform.GetComponent<Renderer>();
        if (pruebaDeFocoCircularAzul)
        {
            focoCircularAzul.material = plasticoCircularAzulEncendido;
            Debug.Log("Cambio de material Luminoso - Foco Circular Azul");
            pruebaDeFocoCircularAzul = false;
            //currentParticle = particleError.CrearParticulasError(currentTypeParticleError, transform.position, transform.rotation.eulerAngles);
            //currentParticle.transform.parent = this.gameObject.transform;
            focoCircularAzulEncendido = true;
        }
        else
        {
            focoCircularAzul.material = plasticoCircularAzulApagado;
            Debug.Log("Cambio de material Opaco - Foco Circular Azul");
            pruebaDeFocoCircularAzul = true;
            //particleError.DestruirParticulasError(currentParticle);
            focoCircularAzulEncendido = false;
        }
    }
}
