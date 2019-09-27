using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modulo5 : MonoBehaviour
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
    [Header("Parametros Aguaja Medidora")]
    [SerializeField] public GameObject agujaMedidora;
    [SerializeField] public float limiteGiroInferiorAguja = -35.0f;
    [SerializeField] public float limiteGiroSuperiorAguja = -145.0f;
    public float valorActualAguja = 0.0f;
    public float valorMinimoAguja = 0.0f;
    public float valorMaximoAguja = 250.0f;
    private Quaternion originalRotationNeedle;
    [SerializeField] public float gradosActualesAguja = -90.0f;
    [SerializeField] public float velocidadRotacion = 10;
    [SerializeField] public int estaLimiteRotacion = -1;
    private bool puederotar = true;
    [Header("Particulas")]
    public GameObject currentParticle;
    private ParticlesError particleError;
    public int currentTypeParticleError = 0;
    public bool moduloAveriado = false;
    [Header("Parametros Plugs")]
    private string nombreTagPlugAnaranjado = "PlugAnaranjado";
    private string nombreTagPlugNegro = "PlugNegro";
    [Header("Parametros Aguja Medidora")]
    private string nombreTagAgujaMedidora = "AgujaMedidora";
    //Variables de debug
    [Header("Debug")]
    public bool mostrarDiccionarioConexiones = false; // Variable
    public bool mostrarPlugAnaranjados = false; // Variable
    public bool mostrarPlugNegros = false; // Variable
    public bool rotarAgujaPrueba = false;
    public bool debug = false;
    #endregion

    #region Propiedades

    public bool ModuloAveriado
    {
        get => moduloAveriado;
        set => moduloAveriado = value;
    }

    public float ValorActualAguja
    {
        get => valorActualAguja;
        set => valorActualAguja = value;
    }

    public float ValorMaximoAguja
    {
        get => valorMaximoAguja;
        set => valorMaximoAguja = value;
    }

    public float ValorMinimoAguja
    {
        get => valorMinimoAguja;
        set => valorMinimoAguja = value;
    }
    #endregion

    #region Inicializacion
    private void Awake()
    {
        //Inicialización de listas y diccionarios de elementos.
        plugsConnections = new Dictionary<string, string>();
        plugAnaranjadosDict = new Dictionary<string, GameObject>();
        plugNegrosDict = new Dictionary<string, GameObject>();

        particleError = new ParticlesError();
        plugAnaranjados = new List<GameObject>();
        plugNegros = new List<GameObject>();
        InicializarComponentes(gameObject);
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
                plug.tipoNodo = 2;
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
                plug.tipoNodo = 2;
                plug.padreTotalComponente = this.gameObject;
                plugsConnections.Add(gameObject.name + "|" + child.name, "");

                plugNegrosDict.Add(child.name, child);
                child.tag = nombreTagPlugNegro;
            }
            else if (child.name.Contains("AgujaMedidora"))
            {
                agujaMedidora = child;
                originalRotationNeedle = agujaMedidora.transform.rotation;
                currentTypeParticleError = (int)AuxiliarModulos.ParticlesErrorTypes.ElectricalSparksEffect;
                child.tag = nombreTagAgujaMedidora;
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
            RotarAguja();
        }
        else
        {
            //Hacer algo si el modulo esta apagado.
        }
    }

    private void ComportamientoModulo()
    {
        Plugs plugIzquierdoCompPlug = plugAnaranjadosDict["EntradaPlugAnaranjado1"].GetComponent<Plugs>();
        Plugs plugDerechoCompPlug = plugNegrosDict["EntradaPlugNegro1"].GetComponent<Plugs>();
        if (plugIzquierdoCompPlug.Conectado && plugDerechoCompPlug.Conectado)
        {
            plugIzquierdoCompPlug.EstablecerPropiedadesConexionesEntrantes();
            plugDerechoCompPlug.EstablecerPropiedadesConexionesEntrantes();
            //Caso exito - Medir voltaje
            if (plugIzquierdoCompPlug.TipoConexion == (int)AuxiliarModulos.TiposConexiones.Linea &&
                plugDerechoCompPlug.TipoConexion == (int)AuxiliarModulos.TiposConexiones.Neutro) 
            {
                ValorActualAguja = plugIzquierdoCompPlug.Voltaje;
                if (ValorActualAguja > ValorMaximoAguja) // Caso Avaria - El voltaje suministrado supera los limites de medición
                {
                    ModuloAveriado = true;
                    ComprobarEstadoAveria();
                    Debug.LogError(this.name + " - if (ValorActualAguja > ValorMaximoAguja)");
                }
                if (debug)
                {
                    Debug.Log(this.name + " - if (plugIzquierdoCompPlug.TipoConexion == 1 && plugDerechoCompPlug.TipoConexion == 2)");
                }
            }
            else //Caso averia????? - Conectores conectados, pero estan invertidos.
            if (plugIzquierdoCompPlug.TipoConexion == (int)AuxiliarModulos.TiposConexiones.Neutro &&
                plugDerechoCompPlug.TipoConexion == (int)AuxiliarModulos.TiposConexiones.Linea) 
            {
                //-----------------Falta comportamiento
                if (debug)
                {
                    Debug.Log(this.name + " - if (plugIzquierdoCompPlug.TipoConexion == 2 && plugDerechoCompPlug.TipoConexion == 1)");
                }
            }//Caso averia - Dos conectores de linea conectados, en lugar de una linea y un neutro.
            else 
            if (plugIzquierdoCompPlug.TipoConexion == (int)AuxiliarModulos.TiposConexiones.Linea &&
                plugDerechoCompPlug.TipoConexion == (int)AuxiliarModulos.TiposConexiones.Linea) 
            {
                ModuloAveriado = true;
                ComprobarEstadoAveria();
                Debug.LogError(this.name + " - if (plugIzquierdoCompPlug.TipoConexion == 1 && plugDerechoCompPlug.TipoConexion == 1)- Conectado");
            }//Caso Neutro - Dos conectores neutros, No se va a averiar, pero no mide el voltaje.
            else 
            if (plugIzquierdoCompPlug.TipoConexion == (int)AuxiliarModulos.TiposConexiones.Neutro &&
                plugDerechoCompPlug.TipoConexion == (int)AuxiliarModulos.TiposConexiones.Neutro) 
            {
                if (debug)
                {
                    Debug.Log(this.name + " - if (plugIzquierdoCompPlug.TipoConexion == 2 && plugDerechoCompPlug.TipoConexion == 2)");
                }
            }
        }
        else
        {
            ValorActualAguja = 0;
            if (debug)
            {
                Debug.Log(this.name + " - No conectado");
            }
        }
    }

    public void CrearAveria()
    {
        currentParticle = particleError.CrearParticulasError(currentTypeParticleError, transform.position, transform.rotation.eulerAngles, new Vector3(2f, 2f, 2f));
        currentParticle.transform.parent = this.gameObject.transform;
        ModuloAveriado = true;
    }

    public void QuitarAveria()
    {
        particleError.DestruirParticulasError(currentParticle);
        ModuloAveriado = false;
    }

    void ComprobarEstadoAveria()
    {
        if (ModuloAveriado)
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

    public void RotarAguja()
    {
        if (valorActualAguja >= valorMinimoAguja && valorActualAguja <= valorMaximoAguja)
        {
            float valorRotacionGrados = 0.0f;
            agujaMedidora.transform.rotation = originalRotationNeedle;
            if (valorActualAguja >= valorMinimoAguja && valorActualAguja <= 50)
            {
                valorRotacionGrados = 0.52f * valorActualAguja;
                //Debug.Log("if(valorActualAguja >= 0 && valorActualAguja <= 50)");
            }
            else if (valorActualAguja > 50 && valorActualAguja <= 100)
            {
                valorRotacionGrados = (0.465f * (valorActualAguja));
                //Debug.Log("if (valorActualAguja > 50 && valorActualAguja <= 100)");
            }
            else if (valorActualAguja > 100 && valorActualAguja <= 150)
            {
                valorRotacionGrados = (0.45f * (valorActualAguja));
                //Debug.Log("if (valorActualAguja > 100 && valorActualAguja <= 150)");
            }
            else if (valorActualAguja > 150 && valorActualAguja <= 200)
            {
                valorRotacionGrados = (0.4425f * (valorActualAguja));
                //Debug.Log("if (valorActualAguja > 150 && valorActualAguja <= 200)");
            }
            else if (valorActualAguja > 200 && valorActualAguja <= valorMaximoAguja)
            {
                valorRotacionGrados = (0.44f * (valorActualAguja));
                //Debug.Log("if (valorActualAguja > 200 && valorActualAguja <= 250)");
            }
            else
            {
                //valorRotacionGrados = -1 * (limiteGiroSuperiorAguja * valorActualAguja) / valorMaximoAguja;
                valorRotacionGrados = ((Mathf.Abs(limiteGiroSuperiorAguja) - Mathf.Abs(limiteGiroInferiorAguja)) / valorMaximoAguja) * valorActualAguja;
                Debug.LogError(this.name + ", Error. Modulo 5: El valor actual de la aguja supera el limite maximo establecido");
            }
            //agujaMedidora.transform.Rotate(0, 0, valorRotacionGrados, Space.Self); //z
            agujaMedidora.transform.Rotate(0, valorRotacionGrados, 0); //y
            //agujaMedidora.transform.Rotate(valorRotacionGrados, 0, 0, Space.Self); //x
            /*Debug.Log("Modulo 5: valorRotacionGrados: " + valorRotacionGrados 
                + ", valorActualPerilla: " + valorActualAguja 
                + ", limiteGiroSuperiorPerilla: " + limiteGiroSuperiorAguja 
                + ", valorMaximoPerilla: " + valorMaximoAguja
                + ", Posición: " + agujaMedidora.transform.rotation.eulerAngles);*/
        }
        else
        {
            //agujaMedidora.transform.rotation = originalRotationNeedle;
            Debug.LogError(this.name + ", Error. Modulo 5: rotarPerilla(float valorActual): El valor actual recibido sobrepasa los limites establecidos");
        }
    }

    public void RotarAguajaPrueba()
    {
        /*Vector3 agujaMedidoraRotation = UnityEditor.TransformUtils.GetInspectorRotation(agujaMedidora.gameObject.transform);
        if (agujaMedidoraRotation.x >= limiteGiroInferiorAguja && puederotar)
        {
            agujaMedidora.transform.Rotate(Vector3.up, velocidadRotacion * Time.deltaTime);
        }
        else
        if (agujaMedidoraRotation.x <= limiteGiroInferiorAguja && agujaMedidoraRotation.x >= limiteGiroSuperiorAguja && puederotar)
        {
            agujaMedidora.transform.Rotate(Vector3.up, velocidadRotacion * Time.deltaTime);
        }
        else
        {
            puederotar = false;
            agujaMedidora.transform.Rotate(Vector3.down, velocidadRotacion * Time.deltaTime);
            //agujaMedidora.transform.rotation = Quaternion.Euler(-35, -90, 90);
            //Debug.Log("Cambio de rotacion: " + agujaMedidoraRotation.x + " >= " + limiteGiroInferiorAguja);
            if (agujaMedidoraRotation.x >= limiteGiroInferiorAguja)
            {
                puederotar = true;
            }
        }*/
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
