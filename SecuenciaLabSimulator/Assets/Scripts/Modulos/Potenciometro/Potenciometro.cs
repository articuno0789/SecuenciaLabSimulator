using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potenciometro : MonoBehaviour
{
    #region Atributos
    [Header("Encendido")]
    public bool moduloEncendido = true;
    [Header("Conexiones")]
    public Dictionary<string, string> plugsConnections;
    [Header("Diccionarios de elementos")]
    public Dictionary<string, GameObject> plugAnaranjadosDict;
    public Dictionary<string, GameObject> plugNegrosDict;
    [Header("Listas de elementos")]
    [SerializeField] public List<GameObject> plugAnaranjados;
    [SerializeField] public List<GameObject> plugNegros;
    [Header("Perilla")]
    [SerializeField] public GameObject perilla;
    [SerializeField] public float limiteGiroInferiorPerilla = 135.0f;
    [SerializeField] public float limiteGiroSuperiorPerilla = -135.0f;
    public float valorActualPerilla = 0.0f;
    public float valorMinimoPerilla = 0.0f;
    public float valorMaximoPerilla = 100.0f;
    private Quaternion originalRotationKnob;
    public bool potenciometroAveriado = false;
    [SerializeField] public float gradosActualesPerilla = 0.0f;
    [SerializeField] public float velocidadRotacion = 10;
    public bool rotarPerillaPrueba = false;
    [SerializeField] public int estaLimiteRotacion = -1;
    private readonly bool puederotar = true;
    [Header("Particulas")]
    private ParticlesError particleError;
    public int currentTypeParticleError = (int)AuxiliarModulos.ParticlesErrorTypes.SmokeEffect;
    public GameObject currentParticle;
    [Header("Parametros Plugs")]
    private string nombreTagPlugAnaranjado = "PlugAnaranjado";
    private string nombreTagPlugNegro = "PlugNegro";
    [Header("Parametros Perillas")]
    private string nombreTagPerilla = "Perilla";
    //Variables de debug
    [Header("Debug")]
    public bool mostrarDiccionarioConexiones = false; // Variable
    public bool mostrarPlugAnaranjados = false; // Variable
    public bool mostrarPlugNegros = false; // Variable
    public bool debugMode = false;
    public bool Puederotar => puederotar;
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

    public bool PotenciometroArreglado
    {
        get => potenciometroAveriado;
        set => potenciometroAveriado = value;
    }
    #endregion

    #region Inicializacion
    private void Awake()
    {
        //Inicialización de listas y diccionarios de elementos.
        plugsConnections = new Dictionary<string, string>();
        plugAnaranjadosDict = new Dictionary<string, GameObject>();
        plugNegrosDict = new Dictionary<string, GameObject>();

        plugAnaranjados = new List<GameObject>();
        plugNegros = new List<GameObject>();
        particleError = new ParticlesError();
        InicializarComponentes(gameObject);
        CurrentTypeParticleError = (int)AuxiliarModulos.ParticlesErrorTypes.PlasmaExplosionEffect;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void InicializarComponentes(GameObject nodo)
    {
        int numeroDeHijosHijos = nodo.transform.childCount;
        for (int i = 0; i < numeroDeHijosHijos; i++)
        {
            GameObject child = nodo.transform.GetChild(i).gameObject;
            if (child.name.Contains("EntradaPlugAnaranjado"))
            {
                plugAnaranjados.Add(child);
                child.AddComponent<CableComponent>();

                Plugs plug = child.AddComponent<Plugs>();
                plug.padreTotalComponente = this.gameObject;
                plugsConnections.Add(gameObject.name + "|" + child.name, "");

                plugAnaranjadosDict.Add(child.name, child);
                child.tag = nombreTagPlugAnaranjado;
            }
            else if (child.name.Contains("EntradaPlugNegro"))
            {
                plugNegros.Add(child);
                child.AddComponent<CableComponent>();

                Plugs plug = child.AddComponent<Plugs>();
                plug.padreTotalComponente = this.gameObject;
                plugsConnections.Add(gameObject.name + "|" + child.name, "");

                plugNegrosDict.Add(child.name, child);
                child.tag = nombreTagPlugNegro;
            }
            else if (child.name.Contains("PerillaPotenciometro"))
            {
                perilla = child;
                originalRotationKnob = perilla.transform.rotation;
                child.tag = nombreTagPerilla;
            }
            InicializarComponentes(child);
        }
    }
    #endregion

    #region Comportamiento Modulo
    // Update is called once per frame
    void Update()
    {
        ComprobarEstadosDiccionarios();
        if (moduloEncendido)
        {
            //Hacer algo si el modulo esta encendido.
            ComportamientoModulo();
        }
        else
        {
            //Hacer algo si el modulo esta apagado.
        }
    }

    void ComportamientoModulo()
    {
        Plugs plugIzquierdoCompPlug = plugAnaranjadosDict["EntradaPlugAnaranjado1"].GetComponent<Plugs>();
        Plugs plugDerechoCompPlug = plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>();

        if (plugIzquierdoCompPlug != null && plugDerechoCompPlug != null)
        {
            plugIzquierdoCompPlug.EstablecerPropiedadesConexionesEntrantes();
            plugDerechoCompPlug.EstablecerPropiedadesConexionesEntrantes();
            //Correcto - Linea y neutro conectado en de manera correcta
            if (plugIzquierdoCompPlug.Conectado && plugDerechoCompPlug.Conectado)
            {
                Plugs plugCentral = plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>();
                if (plugIzquierdoCompPlug.TipoConexion == (int)AuxiliarModulos.TiposConexiones.Linea &&
                    plugDerechoCompPlug.TipoConexion == (int)AuxiliarModulos.TiposConexiones.Neutro)
                {
                    potenciometroAveriado = false;
                    float nuevoVoltaje = plugIzquierdoCompPlug.Voltaje * (valorActualPerilla / 100);
                    plugCentral.EstablecerPropiedadesConexionesEntrantes(plugAnaranjadosDict["EntradaPlugAnaranjado1"]);
                    plugCentral.Voltaje = nuevoVoltaje;
                    Debug.LogError("nuevoVoltaje: " + nuevoVoltaje + ", plugIzquierdoCompPlug.Voltaje: " + plugIzquierdoCompPlug.Voltaje + ", valorActualPerilla: " + valorActualPerilla);
                    plugCentral.EstoConectado();
                    if (DebugMode)
                    {
                        Debug.Log(name + ") " + this.name + " - POTENCIOMETRO - if(plugIzquierdoCompPlug.TipoConexion == 1 && plugDerechoCompPlug.TipoConexion == 2) - Conectado");
                    }
                }
                else //Averia - Linea y neutro invertido
                if (plugIzquierdoCompPlug.TipoConexion == (int)AuxiliarModulos.TiposConexiones.Neutro &&
                    plugDerechoCompPlug.TipoConexion == (int)AuxiliarModulos.TiposConexiones.Linea)
                {
                    potenciometroAveriado = false;
                    float nuevoVoltaje = plugDerechoCompPlug.Voltaje * (valorActualPerilla / 100);
                    plugCentral.EstablecerPropiedadesConexionesEntrantes(plugAnaranjadosDict["EntradaPlugAnaranjado3"]);
                    plugCentral.Voltaje = nuevoVoltaje;
                    plugCentral.EstoConectado();
                    Debug.LogError("nuevoVoltaje: " + nuevoVoltaje + ", plugIzquierdoCompPlug.Voltaje: " + plugIzquierdoCompPlug.Voltaje + ", valorActualPerilla: " + valorActualPerilla);
                    if (DebugMode)
                    {
                        Debug.Log(name + ") " + this.name + " - POTENCIOMETRO - (plugArribaCompPlug.TipoConexion == 2 && plugAbajoCompPlug.TipoConexion == 1) - Conectado -");
                    }
                }
                else //Avaeria - Dos lineas conectadas al mismo tiempo
                if (plugIzquierdoCompPlug.TipoConexion == (int)AuxiliarModulos.TiposConexiones.Linea &&
                    plugDerechoCompPlug.TipoConexion == (int)AuxiliarModulos.TiposConexiones.Linea)
                {
                    if (plugIzquierdoCompPlug.Linea == plugDerechoCompPlug.Linea)
                    {
                        potenciometroAveriado = false;
                        plugCentral.EstablecerValoresNoConexion2();
                        if (DebugMode)
                        {
                            Debug.Log(name + ") " + this.name + " - POTENCIOMETRO - (plugArribaCompPlug.Linea == plugAbajoCompPlug.Linea) - Conectado - Debido a que son la misma linea simplemente no enciende.");
                        }
                    }
                    else if (plugIzquierdoCompPlug.Linea != plugDerechoCompPlug.Linea)
                    {
                        potenciometroAveriado = true;
                        if (DebugMode)
                        {
                            Debug.LogError(name + ") " + this.name + " - POTENCIOMETRO - if (plugArribaCompPlug.Linea != plugAbajoCompPlug.Linea) - Conectado - Debido a que son lineas diferentes el foco se quema.");
                        }
                    }
                    else
                    {
                        potenciometroAveriado = true;
                        AuxiliarModulos.EliminarMaterial(perilla);
                        if (DebugMode)
                        {
                            Debug.LogError(name + ") " + this.name + " - POTENCIOMETRO - NO DEBERIA ENTRAR AQUI - (plugArribaCompPlug.TipoConexion == 1 && plugAbajoCompPlug.TipoConexion == 1)");
                        }
                    }
                } //Correcto - Dos neutros conectados, no pasa nada
                else 
                if (plugIzquierdoCompPlug.TipoConexion == (int)AuxiliarModulos.TiposConexiones.Neutro &&
                    plugDerechoCompPlug.TipoConexion == (int)AuxiliarModulos.TiposConexiones.Neutro) 
                {
                    potenciometroAveriado = false;
                    plugCentral.EstablecerPropiedadesConexionesEntrantes(plugAnaranjadosDict["EntradaPlugAnaranjado3"]);
                    if (DebugMode)
                    {
                        Debug.Log(name + ") " + this.name + " - POTENCIOMETRO - (plugIzquierdoCompPlug.TipoConexion == 2 && plugDerechoCompPlug.TipoConexion == 2) - Conectado");
                    }
                }
                else
                {
                    potenciometroAveriado = true;
                    AuxiliarModulos.EliminarMaterial(perilla);
                    Debug.LogError(name + ") " + this.name + " - POTENCIOMETRO - Este caso de uso todavia no esta programado - No entro a ningun caso");
                }
            }
            else
            {
                potenciometroAveriado = false;
                //ApagarFoco();
                if (DebugMode)
                {
                    Debug.Log(name + ") " + this.name + " - POTENCIOMETRO - if (plugIzquierdoCompPlug.Conectado && plugDerechoCompPlug.Conectado) - NO esta conectados");
                }
            }
            ComprobarEstadoAveria();
        }
        else
        {
            //Debug.LogError(name + ") " + this.name + " - FOCO AMARILLO - if(plugArribaCompPlug != null && plugAbajoCompPlug != null) - Alguno de los dos es nulo, plugArribaCompPlug: " + plugIzquierdoCompPlug + ", plugAbajoCompPlug: " + plugDerechoCompPlug);
        }
    }

    void ComprobarEstadoAveria()
    {
        if (potenciometroAveriado)
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
        if (perilla != null)
        {
            currentParticle = particleError.CrearParticulasError(currentTypeParticleError, perilla.transform.position, perilla.transform.rotation.eulerAngles, new Vector3(0.5f, 0.5f, 0.5f));
            currentParticle.transform.parent = this.gameObject.transform;
            potenciometroAveriado = true;
        }
    }

    public void QuitarAveria()
    {
        particleError.DestruirParticulasError(currentParticle);
        potenciometroAveriado = false;
    }

    public void RotarPerilla()
    {
        if (perilla != null)
        {
            if (valorActualPerilla >= valorMinimoPerilla && valorActualPerilla <= valorMaximoPerilla)
            {
                perilla.transform.rotation = originalRotationKnob;
                float valorRotacionGrados = ((limiteGiroSuperiorPerilla * valorActualPerilla) / valorMaximoPerilla) * 2;
                Debug.Log("Modulo Potenciometro: valorRotacionGrados: " + valorRotacionGrados + ", valorActualPerilla: " + valorActualPerilla + ", limiteGiroSuperiorPerilla: " + limiteGiroSuperiorPerilla + ", valorMaximoPerilla: " + valorMaximoPerilla);
                perilla.transform.Rotate(0, 0, valorRotacionGrados);
            }
            else
            {
                Debug.LogError(this.name + ", Error. Modulo Potenciometro: rotarPerilla(float valorActual): El valor actual recibido sobrepasa los limites establecidos");
            }
        }
    }
    #endregion

    #region Conexiones Grafo
    public void CrearConexionPlugs(string startPlug, string endPlug)
    {
        plugsConnections[startPlug] = endPlug;
        Debug.Log("plugsConnections[" + startPlug + "]: " + endPlug);
    }

    void ComprobarEstadosDiccionarios()
    {
        if (mostrarDiccionarioConexiones)
        {
            ImprimirDiccionarioConexiones();
        }
        if (mostrarPlugAnaranjados)
        {
            ImprimirDiccionario(plugAnaranjadosDict, 1);
        }
        if (mostrarPlugNegros)
        {
            ImprimirDiccionario(plugNegrosDict, 2);
        }
    }

    public void ImprimirDiccionarioConexiones()
    {
        mostrarDiccionarioConexiones = false;
        Debug.Log("************************************************************************************");
        Debug.Log("************************** plugsConnections **********************************");
        foreach (KeyValuePair<string, string> entry in plugsConnections)
        {
            Debug.Log("Plug origen: " + entry.Key + ", Plug destino: " + entry.Value);
            // do something with entry.Value or entry.Key
        }
        Debug.Log("************************************************************************************");
    }

    public void ImprimirDiccionario(Dictionary<string, GameObject> diccionario, int bandera)
    {
        string nombreDiccionario = "No establecido";
        if (bandera == 1)
        {
            mostrarPlugAnaranjados = false;
            nombreDiccionario = "plugAnaranjadosDict";
        }
        else if (bandera == 2)
        {
            mostrarPlugNegros = false;
            nombreDiccionario = "plugNegrosDict";
        }
        Debug.Log("************************************************************************************");
        Debug.Log("************************** " + nombreDiccionario + "  **********************************");
        foreach (KeyValuePair<string, GameObject> entry in diccionario)
        {
            Debug.Log("Indice: " + entry.Key + ", Valor: " + entry.Value);
            // do something with entry.Value or entry.Key
        }
        Debug.Log("************************************************************************************");
    }

    #endregion
}
