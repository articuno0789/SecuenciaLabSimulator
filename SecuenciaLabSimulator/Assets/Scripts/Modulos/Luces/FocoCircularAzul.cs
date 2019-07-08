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

        plugIzquierdoCompPlug.EstablecerPropiedadesConexionesEntrantes();
        plugDerechoCompPlug.EstablecerPropiedadesConexionesEntrantes();

        if (plugIzquierdoCompPlug.Conectado && plugDerechoCompPlug.Conectado)
        {
            if(plugIzquierdoCompPlug.TipoConexion == 1 && plugDerechoCompPlug.TipoConexion == 2)
            {
                focoAveriado = false;
                EncenderFoco();
                if (DebugMode)
                {
                    Debug.Log("Modulo9 - if(plugIzquierdoCompPlug.TipoConexion == 1 && plugDerechoCompPlug.TipoConexion == 2) - Conectado");
                }
            }
            else if (plugIzquierdoCompPlug.TipoConexion == 2 && plugDerechoCompPlug.TipoConexion == 1)
            {
                focoAveriado = false;
                EncenderFoco();
                if (DebugMode)
                {
                    Debug.Log("Modulo9 - if (plugIzquierdoCompPlug.TipoConexion == 2 && plugDerechoCompPlug.TipoConexion == 1) - Conectado");
                }
            }
            else if (plugIzquierdoCompPlug.TipoConexion == 1 && plugDerechoCompPlug.TipoConexion == 1)
            {
                focoAveriado = true;
                ApagarFoco();
                if (DebugMode)
                {
                    Debug.LogError("Modulo9 - if (plugIzquierdoCompPlug.TipoConexion == 1 && plugDerechoCompPlug.TipoConexion == 1) - Conectado");
                }
            }
            else if (plugIzquierdoCompPlug.TipoConexion == 2 && plugDerechoCompPlug.TipoConexion == 2)
            {
                focoAveriado = false;
                ApagarFoco();
                if (DebugMode)
                {
                    Debug.Log("Modulo9 - if (plugIzquierdoCompPlug.TipoConexion == 2 && plugDerechoCompPlug.TipoConexion == 2) - Conectado");
                }
            }
        }
        else
        {
            focoAveriado = false;
            ApagarFoco();
            if (DebugMode)
            {
                Debug.Log("Modulo9 - if (plugIzquierdoCompPlug.Conectado && plugDerechoCompPlug.Conectado) - NO esta conectados");
            }
        }
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
            if(currentParticle == null)
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
