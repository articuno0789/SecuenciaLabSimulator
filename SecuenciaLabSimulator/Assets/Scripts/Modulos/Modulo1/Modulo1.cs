using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Modulo1 : MonoBehaviour
{
    #region Atributos
    [Header("Encendido")]
    public bool moduloEncendido = true;
    [Header("Conexiones")]
    public Dictionary<string, string> plugsConnections;
    [Header("Diccionarios de elementos")]
    public Dictionary<string, GameObject> plugAnaranjadosDict;
    public Dictionary<string, GameObject> plugNegrosDict;
    public Dictionary<string, GameObject> lucesRojasDict;
    [Header("Listas de elementos")]
    public List<GameObject> plugAnaranjados;
    public List<GameObject> plugNegros;
    public List<GameObject> lucesRojas;
    [Header("Parametros módulo")]
    [SerializeField] public float voltajeModulo = 127; // Variable
    [Header("Parametros Plugs")]
    private string nombreTagPlugAnaranjado = "PlugAnaranjado";
    private string nombreTagPlugNegro = "PlugNegro";
    [Header("Parametros Focos")]
    private string nombreTagFocoRojo = "FocoRojo";

    //Variables de debug
    [Header("Debug")]
    public bool pruebaDeLuz = true; // Variable
    public bool mostrarDiccionarioConexiones = false; // Variable
    public bool mostrarPlugAnaranjados = false; // Variable
    public bool mostrarPlugNegros = false; // Variable
    #endregion

    #region Inicializacion
    // Start is called before the first frame update
    private void Awake()
    {
        //Inicialización de listas y diccionarios de elementos.
        plugsConnections = new Dictionary<string, string>();
        plugAnaranjadosDict = new Dictionary<string, GameObject>();
        plugNegrosDict = new Dictionary<string, GameObject>();
        lucesRojasDict = new Dictionary<string, GameObject>();

        plugAnaranjados = new List<GameObject>();
        plugNegros = new List<GameObject>();
        lucesRojas = new List<GameObject>();

        InicializarComponentes(gameObject);
        if (moduloEncendido)
        {
            EncenderApagarLuzRoja(true);
        }
        InicializarPlugAnaranjado("EntradaPlugAnaranjado1");
        InicializarPlugAnaranjado("EntradaPlugAnaranjado2");
        InicializarPlugAnaranjado("EntradaPlugAnaranjado3");

        InicializarPlugNegro("EntradaPlugNegro1");
        InicializarPlugNegro("EntradaPlugNegro2");
        InicializarPlugNegro("EntradaPlugNegro3");
    }

    private void InicializarPlugAnaranjado(string nombrePlug, bool estoyConectado = false)
    {
        Plugs plug = plugAnaranjadosDict[nombrePlug].GetComponent<Plugs>();
        if (plug != null)
        {
            plug.TipoConexion = (int)AuxiliarModulos.TiposConexiones.Linea;
            plug.voltaje = voltajeModulo;
            plug.Linea = (int)AuxiliarModulos.NumeroLinea.PrimeraLinea;
            plug.tipoNodo = (int)AuxiliarModulos.TipoNodo.Poder;
            if (!plug.EstoConectado())
            {
                plug.QuitarAveria();
            }
        }
        else
        {
            Debug.LogError(this.name + ", Error. InicializarPlugAnaranjado(string nombrePlug, bool estoyConectado = false) - Elemento sin logica de plug.");
        }
    }

    private void InicializarPlugNegro(string nombrePlug, bool estoyConectado = false)
    {
        Plugs plug = plugNegrosDict[nombrePlug].GetComponent<Plugs>();
        if(plug != null)
        {
            plug.TipoConexion = (int)AuxiliarModulos.TiposConexiones.Neutro;
            plug.voltaje = voltajeModulo;
            plug.Linea = (int)AuxiliarModulos.NumeroLinea.SinLinea;
            plug.tipoNodo = (int)AuxiliarModulos.TipoNodo.Poder;
            if (!plug.EstoConectado())
            {
                plug.QuitarAveria();
            }
        }
        else
        {
            Debug.LogError(this.name + ", Error. InicializarPlugNegro(string nombrePlug, bool estoyConectado = false) - Elemento sin logica de plug.");
        }
        
    }

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
            else if (child.name.Contains("LuzRoja"))
            {
                lucesRojas.Add(child);
                LuzRoja luzRoja = child.AddComponent<LuzRoja>();
                luzRoja.CurrentTypeParticleError = (int)AuxiliarModulos.ParticlesErrorTypes.SmokeEffect;
                luzRoja.CurrentTypeParticleError = (int)AuxiliarModulos.ParticlesErrorTypes.ElectricalSparksEffect;
                luzRoja.padreTotalComponente = this.gameObject;
                lucesRojasDict.Add(child.name, child);

                lucesRojas.Add(child);
                child.AddComponent<LuzRoja>();
                child.tag = nombreTagFocoRojo;
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
            EncenderApagarLuzRoja(true);
            //Cargar plugs de energia
            InicializarPlugAnaranjado("EntradaPlugAnaranjado1", true);
            InicializarPlugAnaranjado("EntradaPlugAnaranjado2", true);
            InicializarPlugAnaranjado("EntradaPlugAnaranjado3", true);
            InicializarPlugNegro("EntradaPlugNegro1", true);
            InicializarPlugNegro("EntradaPlugNegro2", true);
            InicializarPlugNegro("EntradaPlugNegro3", true);
            //Pulso de nergia
            MandarPulsoEnergia("EntradaPlugAnaranjado1");
            MandarPulsoEnergia("EntradaPlugAnaranjado2");
            MandarPulsoEnergia("EntradaPlugAnaranjado3");
            MandarPulsoNeutro("EntradaPlugNegro1");
            MandarPulsoNeutro("EntradaPlugNegro2");
            MandarPulsoNeutro("EntradaPlugNegro3");
            /*inicializarPlugAnaranjado("EntradaPlugAnaranjado1", true);
            inicializarPlugAnaranjado("EntradaPlugAnaranjado2", true);
            inicializarPlugAnaranjado("EntradaPlugAnaranjado3", true);
            inicializarPlugNegro("EntradaPlugNegro1", true);
            inicializarPlugNegro("EntradaPlugNegro2", true);
            inicializarPlugNegro("EntradaPlugNegro3", true);*/
            /*ComprobarCorto("EntradaPlugAnaranjado1");
            ComprobarCorto("EntradaPlugAnaranjado2");
            ComprobarCorto("EntradaPlugAnaranjado3");*/
            /*plugAnaranjadosDict["EntradaPlugAnaranjado1"].GetComponent<Plugs>().TipoConexion = 1;
            plugAnaranjadosDict["EntradaPlugAnaranjado1"].GetComponent<Plugs>().voltaje = voltajeModulo;
            plugAnaranjadosDict["EntradaPlugAnaranjado1"].GetComponent<Plugs>().estoConectado();
            plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().TipoConexion = 1;
            plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().voltaje = voltajeModulo;
            plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().estoConectado();
            plugAnaranjadosDict["EntradaPlugAnaranjado1"].GetComponent<Plugs>().TipoConexion = 1;
            plugAnaranjadosDict["EntradaPlugAnaranjado1"].GetComponent<Plugs>().voltaje = voltajeModulo;
            plugAnaranjadosDict["EntradaPlugAnaranjado1"].GetComponent<Plugs>().estoConectado();
            plugNegrosDict["EntradaPlugNegro1"].GetComponent<Plugs>().voltaje = voltajeModulo;
            plugNegrosDict["EntradaPlugNegro1"].GetComponent<Plugs>().TipoConexion = 2;
            plugNegrosDict["EntradaPlugNegro1"].GetComponent<Plugs>().estoConectado();*/
        }
        else
        {
            //Hacer algo si el modulo esta apagado.
            EncenderApagarLuzRoja(false);
            //Descargar plugs de energia
            InicializarPlugAnaranjado("EntradaPlugAnaranjado1", false);
            InicializarPlugAnaranjado("EntradaPlugAnaranjado2", false);
            InicializarPlugAnaranjado("EntradaPlugAnaranjado3", false);
            InicializarPlugNegro("EntradaPlugNegro1", false);
            InicializarPlugNegro("EntradaPlugNegro2", false);
            InicializarPlugNegro("EntradaPlugNegro3", false);
        }
    }

    void EncenderApagarLuzRoja(bool encendida,string nombreLuz = "LuzRoja1")
    {
        LuzRoja luz = lucesRojasDict[nombreLuz].GetComponent<LuzRoja>();
        if (luz != null)
        {
            if (encendida)
            {
                luz.EncenderFoco();
            }
            else
            {
                luz.ApagarFoco();
            }
        }
        else
        {
            Debug.LogError(this.name + ", Error. EncenderApagarLuz(bool encendida) - No se pudo obtener el componente LuzRoja.");
        }
    }

    void MandarPulsoEnergia(string nombrePlug)
    {
        Plugs plug = plugAnaranjadosDict[nombrePlug].GetComponent<Plugs>();
        if (plug != null)
        {
            if (plug.Conectado)
            {
                CableComponent cable = plugAnaranjadosDict[nombrePlug].GetComponent<CableComponent>();
                GameObject plugRelacionado = cable.EndPoint;
                if (plugRelacionado != null)
                {
                    Plugs plugRela = plugRelacionado.GetComponent<Plugs>();
                    if (plugRela != null)
                    {
                        plugRela.EstablecerPropiedadesConexionesEntrantesPrueba();
                    }
                }
            }
        }
        else
        {
            Debug.LogError(this.name + ", Error. MandarPulsoEnergia(string nombrePlug) - No se pudo obtener el componente Plugs.");
        }
    }

    void MandarPulsoNeutro(string nombrePlug)
    {
        Plugs plug = plugNegrosDict[nombrePlug].GetComponent<Plugs>();
        if (plug != null)
        {
            if (plug.Conectado)
            {
                CableComponent cable = plugNegrosDict[nombrePlug].GetComponent<CableComponent>();
                GameObject plugRelacionado = cable.EndPoint;
                if (plugRelacionado != null)
                {
                    Plugs plugRela = plugRelacionado.GetComponent<Plugs>();
                    if (plugRela != null)
                    {
                        plugRela.EstablecerPropiedadesConexionesEntrantesPrueba();
                    }
                }
            }
        }
        else
        {
            Debug.LogError(this.name + ", Error. MandarPulsoNeutro(string nombrePlug) - No se pudo obtener el componente Plugs.");
        }
    }

    void ComprobarCorto(string nombrePlug)
    {
        Plugs plugConexionArribaCerrado = plugAnaranjadosDict[nombrePlug].GetComponent<Plugs>();
        //plugConexionArribaCerrado.DebugMode = true;
        if (plugConexionArribaCerrado != null)
        {
            if (plugConexionArribaCerrado.estaConectado)
            {
                //Debug.Log("if (plugConexionArribaCerrado.estaConectado)");
                Plugs conexionEntrante = plugConexionArribaCerrado.RegresarConexionEntrante();
                if (conexionEntrante != null)
                {
                    //Debug.Log("if (conexionEntrante != null)");
                    plugConexionArribaCerrado.ComprobarEstado1Y15(plugConexionArribaCerrado, conexionEntrante, false);
                }
            }
        }
        else
        {
            Debug.LogError(this.name + ", Error. ComprobarCorto(string nombrePlug) - No se pudo obtener el componente Plugs.");
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
        if(bandera == 1)
        {
            mostrarPlugAnaranjados = false;
            nombreDiccionario = "plugAnaranjadosDict";
        }else if(bandera == 2)
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
