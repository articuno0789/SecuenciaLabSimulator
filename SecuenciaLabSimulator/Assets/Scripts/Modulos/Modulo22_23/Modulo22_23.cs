using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modulo22_23 : MonoBehaviour
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
    [Header("Modulos relacionados")]
    public GameObject modulo22;
    public GameObject modulo23;
    [Header("Plugs de Entrada y Salida")]
    public string plug1Entrada = "";
    public string plug2Entrada = "";
    public string plug1Salida = "";
    public string plug2Salida = "";
    [Header("Parametros Plugs")]
    private string nombreTagPlugAnaranjado = "PlugAnaranjado";
    private string nombreTagPlugNegro = "PlugNegro";
    //Variables de debug
    [Header("Debug")]
    public bool debugMode = false;
    public bool mostrarDiccionarioConexiones = false; // Variable
    public bool mostrarPlugAnaranjados = false; // Variable
    public bool mostrarPlugNegros = false; // Variable
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
        InicializarComponentes(gameObject);

        string nombreModulo = this.name;
        if (this.name.Contains(AuxiliarModulos.tagMod22))
        {
            modulo22 = this.gameObject;
            nombreModulo = nombreModulo.Remove(1, 1).Insert(1, "3");
            modulo23 = GameObject.Find(nombreModulo);
        }else if (this.name.Contains(AuxiliarModulos.tagMod23))
        {
            modulo23 = this.gameObject;
            nombreModulo = nombreModulo.Remove(1, 1).Insert(1, "2");
            modulo22 = GameObject.Find(nombreModulo);
        }
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
        string nombreModulo = this.name;
        int numeroPlugsConect = 0;
        string plug1Entrada = "1";
        string plug2Entrada = "2";
        string plug1Salida = "3";
        string plug2Salida = "4";
        if (this.name.Contains(AuxiliarModulos.tagMod22))
        {
            if(modulo23 == null)
            {
                modulo22 = this.gameObject;

                nombreModulo = nombreModulo.Remove(1, 1).Insert(1, "3");
                modulo23 = GameObject.Find(nombreModulo);
            }
            else
            {
                numeroPlugsConect = NumeroPlugsConectados();
                if (numeroPlugsConect == 2) // COmprobar numeros de plugs, en modulo 22
                {
                    SaberPlugsConectados(ref plug1Entrada, ref plug2Entrada);

                    if (plug1Entrada.Contains("EntradaPlugAnaranjado0_")) // COmpruebo que el plug  este involucrado, modulo 22
                    {
                        if (plugAnaranjadosDict[plug1Entrada].GetComponent<Plugs>().Linea != plugAnaranjadosDict[plug2Entrada].GetComponent<Plugs>().Linea)
                        {
                            modulo23.GetComponent<Modulo22_23>().SaberPlugsConectados(ref plug1Salida, ref plug2Salida);
                            if (plug1Salida.Contains("EntradaPlugAnaranjado0_"))
                            {
                                string plugInteresMod23 = "";
                                string plugInteresMod22 = "";
                                float proporcionSalida = 0.0f;
                                float multiplicadorEntrada = 0.0f;
                                switch (plug2Entrada)
                                {
                                    case "EntradaPlugAnaranjado65_": // 0 - 6 (Entrada)
                                        multiplicadorEntrada = 0.65f;
                                        plugInteresMod22 = "EntradaPlugAnaranjado65_";
                                        switch (plug2Salida)
                                        {
                                            case "EntradaPlugAnaranjado65_": // 0 - 6 (Entrada)
                                                plugInteresMod23 = "EntradaPlugAnaranjado65_";
                                                proporcionSalida = 1.0f;
                                                break;
                                            case "EntradaPlugAnaranjado85_":
                                                plugInteresMod23 = "EntradaPlugAnaranjado85_";
                                                proporcionSalida = 1.3077f;
                                                break;
                                            case "EntradaPlugAnaranjado100_":
                                                plugInteresMod23 = "EntradaPlugAnaranjado100_";
                                                proporcionSalida = 1.5384f;
                                                break;
                                            default:
                                                Debug.LogError(this.name + ", Entro a caso por defecto, no deberia entrar. Plug1: " + plug1Entrada + ", Plug2: " + plug2Entrada);
                                                break;
                                        }
                                        break;
                                    case "EntradaPlugAnaranjado85_":
                                        multiplicadorEntrada = 0.85f;
                                        plugInteresMod22 = "EntradaPlugAnaranjado85_";
                                        switch (plug2Salida)
                                        {
                                            case "EntradaPlugAnaranjado65_": // 0 - 6 (Entrada)
                                                plugInteresMod23 = "EntradaPlugAnaranjado65_";
                                                proporcionSalida = 0.7647f;
                                                break;
                                            case "EntradaPlugAnaranjado85_":
                                                plugInteresMod23 = "EntradaPlugAnaranjado85_";
                                                proporcionSalida = 1.0f;
                                                break;
                                            case "EntradaPlugAnaranjado100_":
                                                plugInteresMod23 = "EntradaPlugAnaranjado100_";
                                                proporcionSalida = 1.1765f;
                                                break;
                                            default:
                                                Debug.LogError(this.name + ", --Entro a caso por defecto, no deberia entrar. Plug1: " + plug1Entrada + ", Plug2: " + plug2Entrada);
                                                break;
                                        }
                                        break;
                                    case "EntradaPlugAnaranjado100_":
                                        multiplicadorEntrada = 1.0f;
                                        plugInteresMod22 = "EntradaPlugAnaranjado100_";
                                        switch (plug2Salida)
                                        {
                                            case "EntradaPlugAnaranjado65_": // 0 - 6 (Entrada)
                                                plugInteresMod23 = "EntradaPlugAnaranjado65_";
                                                proporcionSalida = 0.65f;
                                                break;
                                            case "EntradaPlugAnaranjado85_":
                                                plugInteresMod23 = "EntradaPlugAnaranjado85_";
                                                proporcionSalida = 0.85f;
                                                break;
                                            case "EntradaPlugAnaranjado100_":
                                                plugInteresMod23 = "EntradaPlugAnaranjado100_";
                                                proporcionSalida = 1.0f;
                                                break;
                                            default:
                                                Debug.LogError(this.name + ", --Entro a caso por defecto, no deberia entrar. Plug1: " + plug1Entrada + ", Plug2: " + plug2Entrada);
                                                break;
                                        }
                                        break;
                                    default:
                                        Debug.LogError(this.name + ", -Hay conectados dos plugs, pero no son el 0 y otro. plug1Salida: " + plug1Salida + ", plug2Salida: " + plug2Salida);
                                        break;
                                }

                                //Fijar valores
                                modulo23.GetComponent<Modulo22_23>().plugAnaranjadosDict[plugInteresMod23].GetComponent<Plugs>().EstablecerValoresPlugDefinido(plugAnaranjadosDict[plugInteresMod22], proporcionSalida, multiplicadorEntrada);
                                //modulo23.GetComponent<Modulo22_23>().plugAnaranjadosDict["EntradaPlugAnaranjado65_"].GetComponent<Plugs>().EstablecerPropiedadesConexionesEntrantesPrueba();
                                if (modulo23.GetComponent<Modulo22_23>().plugAnaranjadosDict[plugInteresMod23].GetComponent<CableComponent>().EndPoint != null)
                                {
                                    modulo23.GetComponent<Modulo22_23>().plugAnaranjadosDict[plugInteresMod23].GetComponent<CableComponent>().EndPoint.GetComponent<Plugs>().EstablecerPropiedadesConexionesEntrantesPrueba();
                                    modulo23.GetComponent<Modulo22_23>().plugAnaranjadosDict[plugInteresMod23].GetComponent<Plugs>().TipoConexion = (int)AuxiliarModulos.TiposConexiones.Linea;
                                    modulo23.GetComponent<Modulo22_23>().plugAnaranjadosDict[plugInteresMod23].GetComponent<Plugs>().ComprobarCorto(modulo23.GetComponent<Modulo22_23>().plugAnaranjadosDict[plugInteresMod23].GetComponent<Plugs>(),
                                        modulo23.GetComponent<Modulo22_23>().plugAnaranjadosDict[plugInteresMod23].GetComponent<CableComponent>().EndPoint.GetComponent<Plugs>());
                                }
                                modulo23.GetComponent<Modulo22_23>().plugAnaranjadosDict["EntradaPlugAnaranjado0_"].GetComponent<Plugs>().EstablecerValoresPlugDefinido(plugAnaranjadosDict["EntradaPlugAnaranjado0_"], 1.0f, multiplicadorEntrada);
                                modulo23.GetComponent<Modulo22_23>().plugAnaranjadosDict["EntradaPlugAnaranjado0_"].GetComponent<Plugs>().ComprobarCorto(modulo23.GetComponent<Modulo22_23>().plugAnaranjadosDict["EntradaPlugAnaranjado0_"].GetComponent<Plugs>(),
                                        modulo23.GetComponent<Modulo22_23>().plugAnaranjadosDict["EntradaPlugAnaranjado0_"].GetComponent<CableComponent>().EndPoint.GetComponent<Plugs>());
                            }
                            else
                            {
                                Debug.LogError(this.name + ", ---Hay conectados dos plugs, pero no son el 0 y otro. plug1Salida: " + plug1Salida + ", plug2Salida: " + plug2Salida);
                            }
                        }
                        else
                        {
                            Debug.LogError(this.name + ", ----Las dos conexiones son la misma linea, deben ser lineas distintas, o linea y neutro.");
                        }
                    }
                    else
                    {
                        Debug.LogError(this.name + ", Hay conectados dos plugs, pero no son el 0 y otro. plug1Entrada: " + plug1Entrada + ", plug2Entrada: " + plug2Entrada);
                    }
                    modulo23.GetComponent<Modulo22_23>().RetsablecerTodosPlugs();
                }
                else
                {
                    if (debugMode)
                    {
                        Debug.LogError(this.name + ", No hay 2 plugs conectados. Hay: " + numeroPlugsConect);
                    }
                    modulo23.GetComponent<Modulo22_23>().RetsablecerTodosPlugs();
                }
            }
        }
        else
        {
            //Debug.LogError(this.name + ", Error. Comportamiento - El modulo 23 es nulo.");
        }

        //Modulo23
        if (this.name.Contains(AuxiliarModulos.tagMod23))
        {
            if (modulo22 == null)
            {
                modulo23 = this.gameObject;
                nombreModulo = nombreModulo.Remove(1, 1).Insert(1, "2");
                modulo22 = GameObject.Find(nombreModulo);
            }
        }

        //Para verificar plugs en el inspector
        this.plug1Entrada = plug1Entrada;
        this.plug2Entrada = plug2Entrada;
        this.plug1Salida = plug1Salida;
        this.plug2Salida = plug2Salida;
}

    public void RetsablecerTodosPlugs()
    {
        int lonLista = plugAnaranjados.Count;
        for (int i = 0; i < lonLista; i++)
        {
            Plugs plug = plugAnaranjados[i].GetComponent<Plugs>();
            if(plug != null)
            {
                plug.EstablecerValoresNoConexionVolLin();
                plug.QuitarAveria();
            }
        }
    }

    public void SaberPlugsConectados(ref string plug1, ref string plug2)
    {
        int lonLista = plugAnaranjados.Count;
        int numeroPlusConectados = 0;
        for (int i = 0; i < lonLista; i++)
        {
            Plugs plug = plugAnaranjados[i].GetComponent<Plugs>();
            if (plug != null && plug.EstoConectado())
            {
                numeroPlusConectados++;
                if(numeroPlusConectados == 1)
                {
                    plug1 = plug.name;
                }else if (numeroPlusConectados == 2)
                {
                    plug2 = plug.name;
                }
            }
        }
    }

    public int NumeroPlugsConectados()
    {
        int lonLista = plugAnaranjados.Count;
        int numeroPlusConectados = 0;
        for (int i=0; i< lonLista; i++)
        {
            Plugs plug = plugAnaranjados[i].GetComponent<Plugs>();
            if (plug != null)
            {
                if (plug.EstoConectado())
                {
                    numeroPlusConectados++;
                }
            }
        }
        return numeroPlusConectados;
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
