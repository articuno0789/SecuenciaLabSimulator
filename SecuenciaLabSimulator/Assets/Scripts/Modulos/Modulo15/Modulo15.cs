using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modulo15 : MonoBehaviour
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
    [SerializeField] public List<GameObject> plugAnaranjados;
    [SerializeField] public List<GameObject> plugNegros;
    [SerializeField] public List<GameObject> lucesRojas;
    [Header("Parametros módulo")]
    [SerializeField] public float voltajeModulo = 220;
    [Header("Parametros Plugs")]
    private string nombreTagPlugAnaranjado = "PlugAnaranjado";
    private string nombreTagPlugNegro = "PlugNegro";
    [Header("Parametros Focos")]
    private string nombreTagFocoRojo = "FocoRojo";

    //Variables de debug
    [Header("Debug")]
    public bool mostrarDiccionarioConexiones = false; // Variable
    public bool mostrarPlugAnaranjados = false; // Variable
    public bool mostrarPlugNegros = false; // Variable
    public bool mostrarLucesRojas = false; // Variable
    #endregion

    #region Inicializacion
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
        inicializarComponentes(gameObject);
        if (moduloEncendido)
        {
            EncenderApagarLuzRoja(true, "LuzRoja1");
            EncenderApagarLuzRoja(true, "LuzRoja2");
            EncenderApagarLuzRoja(true, "LuzRoja3");
        }
        inicializarPlugAnaranjado("EntradaPlugAnaranjado1", 1);
        inicializarPlugAnaranjado("EntradaPlugAnaranjado2", 1);
        inicializarPlugAnaranjado("EntradaPlugAnaranjado3", 1);
        inicializarPlugAnaranjado("EntradaPlugAnaranjado4", 2);
        inicializarPlugAnaranjado("EntradaPlugAnaranjado5", 2);
        inicializarPlugAnaranjado("EntradaPlugAnaranjado6", 2);
        inicializarPlugAnaranjado("EntradaPlugAnaranjado7", 3);
        inicializarPlugAnaranjado("EntradaPlugAnaranjado8", 3);
        inicializarPlugAnaranjado("EntradaPlugAnaranjado9", 3);
        /*plugAnaranjadosDict["EntradaPlugAnaranjado1"].GetComponent<Plugs>().TipoConexion = 1;
        plugAnaranjadosDict["EntradaPlugAnaranjado1"].GetComponent<Plugs>().voltaje = voltajeModulo;
        plugAnaranjadosDict["EntradaPlugAnaranjado1"].GetComponent<Plugs>().Linea = 1;
        plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().TipoConexion = 1;
        plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().voltaje = voltajeModulo;
        plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().Linea = 1;
        plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().TipoConexion = 1;
        plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().voltaje = voltajeModulo;
        plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().Linea = 2;
        plugAnaranjadosDict["EntradaPlugAnaranjado4"].GetComponent<Plugs>().TipoConexion = 1;
        plugAnaranjadosDict["EntradaPlugAnaranjado4"].GetComponent<Plugs>().voltaje = voltajeModulo;
        plugAnaranjadosDict["EntradaPlugAnaranjado4"].GetComponent<Plugs>().Linea = 2;
        plugAnaranjadosDict["EntradaPlugAnaranjado5"].GetComponent<Plugs>().TipoConexion = 1;
        plugAnaranjadosDict["EntradaPlugAnaranjado5"].GetComponent<Plugs>().voltaje = voltajeModulo;
        plugAnaranjadosDict["EntradaPlugAnaranjado5"].GetComponent<Plugs>().Linea = 3;
        plugAnaranjadosDict["EntradaPlugAnaranjado6"].GetComponent<Plugs>().TipoConexion = 1;
        plugAnaranjadosDict["EntradaPlugAnaranjado6"].GetComponent<Plugs>().voltaje = voltajeModulo;
        plugAnaranjadosDict["EntradaPlugAnaranjado6"].GetComponent<Plugs>().Linea = 3;*/
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void inicializarPlugAnaranjado(string nombrePlug, int tipoLinea, bool estoyConectado = false)
    {
        plugAnaranjadosDict[nombrePlug].GetComponent<Plugs>().TipoConexion = (int)AuxiliarModulos.TiposConexiones.Linea;
        plugAnaranjadosDict[nombrePlug].GetComponent<Plugs>().voltaje = voltajeModulo;
        plugAnaranjadosDict[nombrePlug].GetComponent<Plugs>().Linea = tipoLinea;
        plugAnaranjadosDict[nombrePlug].GetComponent<Plugs>().tipoNodo = (int)AuxiliarModulos.TipoNodo.Poder;
        if (!plugAnaranjadosDict[nombrePlug].GetComponent<Plugs>().EstoConectado())
        {
            plugAnaranjadosDict[nombrePlug].GetComponent<Plugs>().QuitarAveria();
        }
    }

    private void inicializarPlugNegro(string nombrePlug, bool estoyConectado = false)
    {
        plugNegrosDict[nombrePlug].GetComponent<Plugs>().TipoConexion = 2;
        plugNegrosDict[nombrePlug].GetComponent<Plugs>().voltaje = voltajeModulo;
        if (estoyConectado)
        {
            plugNegrosDict[nombrePlug].GetComponent<Plugs>().EstoConectado();
        }
    }

    private void inicializarComponentes(GameObject nodo)
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
                child.AddComponent<LuzRoja>();

                lucesRojasDict.Add(child.name, child);
                child.tag = nombreTagFocoRojo;
            }
            inicializarComponentes(child);
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
            EncenderApagarLuzRoja(true, "LuzRoja1");
            EncenderApagarLuzRoja(true, "LuzRoja2");
            EncenderApagarLuzRoja(true, "LuzRoja3");
            //Cargar plugs de energia
            inicializarPlugAnaranjado("EntradaPlugAnaranjado1", (int)AuxiliarModulos.NumeroLinea.PrimeraLinea, true);
            inicializarPlugAnaranjado("EntradaPlugAnaranjado2", (int)AuxiliarModulos.NumeroLinea.PrimeraLinea, true);
            inicializarPlugAnaranjado("EntradaPlugAnaranjado3", (int)AuxiliarModulos.NumeroLinea.PrimeraLinea, true);
            inicializarPlugAnaranjado("EntradaPlugAnaranjado4", (int)AuxiliarModulos.NumeroLinea.SegundaLinea, true);
            inicializarPlugAnaranjado("EntradaPlugAnaranjado5", (int)AuxiliarModulos.NumeroLinea.SegundaLinea, true);
            inicializarPlugAnaranjado("EntradaPlugAnaranjado6", (int)AuxiliarModulos.NumeroLinea.SegundaLinea, true);
            inicializarPlugAnaranjado("EntradaPlugAnaranjado7", (int)AuxiliarModulos.NumeroLinea.TerceraLinea, true);
            inicializarPlugAnaranjado("EntradaPlugAnaranjado8", (int)AuxiliarModulos.NumeroLinea.TerceraLinea, true);
            inicializarPlugAnaranjado("EntradaPlugAnaranjado9", (int)AuxiliarModulos.NumeroLinea.TerceraLinea, true);
            //Pulso de nergi
            MandarPulsoEnergia("EntradaPlugAnaranjado1");
            MandarPulsoEnergia("EntradaPlugAnaranjado2");
            MandarPulsoEnergia("EntradaPlugAnaranjado3");
            MandarPulsoEnergia("EntradaPlugAnaranjado4");
            MandarPulsoEnergia("EntradaPlugAnaranjado5");
            MandarPulsoEnergia("EntradaPlugAnaranjado6");
            MandarPulsoEnergia("EntradaPlugAnaranjado7");
            MandarPulsoEnergia("EntradaPlugAnaranjado8");
            MandarPulsoEnergia("EntradaPlugAnaranjado9");
            /*ComprobarCorto("EntradaPlugAnaranjado1");
            ComprobarCorto("EntradaPlugAnaranjado2");
            ComprobarCorto("EntradaPlugAnaranjado3");
            ComprobarCorto("EntradaPlugAnaranjado4");
            ComprobarCorto("EntradaPlugAnaranjado5");
            ComprobarCorto("EntradaPlugAnaranjado6");
            ComprobarCorto("EntradaPlugAnaranjado7");
            ComprobarCorto("EntradaPlugAnaranjado8");
            ComprobarCorto("EntradaPlugAnaranjado9");*/
            /*plugAnaranjadosDict["EntradaPlugAnaranjado1"].GetComponent<Plugs>().TipoConexion = 1;
            plugAnaranjadosDict["EntradaPlugAnaranjado1"].GetComponent<Plugs>().voltaje = voltajeModulo;
            plugAnaranjadosDict["EntradaPlugAnaranjado1"].GetComponent<Plugs>().Linea = 1;
            plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().TipoConexion = 1;
            plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().voltaje = voltajeModulo;
            plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().Linea = 1;
            plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().TipoConexion = 1;
            plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().voltaje = voltajeModulo;
            plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().Linea = 2;
            plugAnaranjadosDict["EntradaPlugAnaranjado4"].GetComponent<Plugs>().TipoConexion = 1;
            plugAnaranjadosDict["EntradaPlugAnaranjado4"].GetComponent<Plugs>().voltaje = voltajeModulo;
            plugAnaranjadosDict["EntradaPlugAnaranjado4"].GetComponent<Plugs>().Linea = 2;
            plugAnaranjadosDict["EntradaPlugAnaranjado5"].GetComponent<Plugs>().TipoConexion = 1;
            plugAnaranjadosDict["EntradaPlugAnaranjado5"].GetComponent<Plugs>().voltaje = voltajeModulo;
            plugAnaranjadosDict["EntradaPlugAnaranjado5"].GetComponent<Plugs>().Linea = 3;
            plugAnaranjadosDict["EntradaPlugAnaranjado6"].GetComponent<Plugs>().TipoConexion = 1;
            plugAnaranjadosDict["EntradaPlugAnaranjado6"].GetComponent<Plugs>().voltaje = voltajeModulo;
            plugAnaranjadosDict["EntradaPlugAnaranjado6"].GetComponent<Plugs>().Linea = 3;

            plugAnaranjadosDict["EntradaPlugAnaranjado1"].GetComponent<Plugs>().estoConectado();
            plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().estoConectado();
            plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().estoConectado();
            plugAnaranjadosDict["EntradaPlugAnaranjado4"].GetComponent<Plugs>().estoConectado();
            plugAnaranjadosDict["EntradaPlugAnaranjado5"].GetComponent<Plugs>().estoConectado();
            plugAnaranjadosDict["EntradaPlugAnaranjado6"].GetComponent<Plugs>().estoConectado();*/
        }
        else
        {
            //Hacer algo si el modulo esta apagado.
            EncenderApagarLuzRoja(false, "LuzRoja1");
            EncenderApagarLuzRoja(false, "LuzRoja2");
            EncenderApagarLuzRoja(false, "LuzRoja3");
            //Descargar plugs de energia
            inicializarPlugAnaranjado("EntradaPlugAnaranjado1", (int)AuxiliarModulos.NumeroLinea.PrimeraLinea, false);
            inicializarPlugAnaranjado("EntradaPlugAnaranjado2", (int)AuxiliarModulos.NumeroLinea.PrimeraLinea, false);
            inicializarPlugAnaranjado("EntradaPlugAnaranjado3", (int)AuxiliarModulos.NumeroLinea.PrimeraLinea, false);
            inicializarPlugAnaranjado("EntradaPlugAnaranjado4", (int)AuxiliarModulos.NumeroLinea.SegundaLinea, false);
            inicializarPlugAnaranjado("EntradaPlugAnaranjado5", (int)AuxiliarModulos.NumeroLinea.SegundaLinea, false);
            inicializarPlugAnaranjado("EntradaPlugAnaranjado6", (int)AuxiliarModulos.NumeroLinea.SegundaLinea, false);
            inicializarPlugAnaranjado("EntradaPlugAnaranjado7", (int)AuxiliarModulos.NumeroLinea.TerceraLinea, false);
            inicializarPlugAnaranjado("EntradaPlugAnaranjado8", (int)AuxiliarModulos.NumeroLinea.TerceraLinea, false);
            inicializarPlugAnaranjado("EntradaPlugAnaranjado9", (int)AuxiliarModulos.NumeroLinea.TerceraLinea, false);
        }
    }

    void EncenderApagarLuzRoja(bool encendida, string nombreLuz = "LuzRoja1")
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
        if (plugAnaranjadosDict[nombrePlug].GetComponent<Plugs>().Conectado)
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

    void ComprobarCorto(string nombrePlug)
    {
        Plugs plugConexionArribaCerrado = plugAnaranjadosDict[nombrePlug].GetComponent<Plugs>();
        //plugConexionArribaCerrado.DebugMode = true;
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
        if (mostrarLucesRojas)
        {
            ImprimirDiccionario(lucesRojasDict, 3);
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
        else if (bandera == 3)
        {
            mostrarLucesRojas = false;
            nombreDiccionario = "lucesRojasDict";
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
