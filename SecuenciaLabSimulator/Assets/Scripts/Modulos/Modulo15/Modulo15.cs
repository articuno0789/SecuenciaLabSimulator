using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modulo15 : MonoBehaviour
{
    #region Atributos
    public bool moduloEncendido = true;
    public Dictionary<string, string> plugsConnections;
    [SerializeField] public List<GameObject> plugAnaranjados;
    [SerializeField] public List<GameObject> plugNegros;
    [SerializeField] public List<GameObject> lucesRojas;
    public Dictionary<string, GameObject> plugAnaranjadosDict;
    public Dictionary<string, GameObject> plugNegrosDict;
    public Dictionary<string, GameObject> lucesRojasDict;
    [SerializeField] public float voltajeModulo = 220;

    //Variables de debug
    public bool mostrarDiccionarioConexiones = false; // Variable
    public bool mostrarPlugAnaranjados = false; // Variable
    public bool mostrarPlugNegros = false; // Variable
    public bool mostrarLucesRojas = false; // Variable
    #endregion

    #region Inicializacion
    private void Awake()
    {
        plugsConnections = new Dictionary<string, string>();
        plugAnaranjadosDict = new Dictionary<string, GameObject>();
        plugNegrosDict = new Dictionary<string, GameObject>();
        lucesRojasDict = new Dictionary<string, GameObject>();

        plugAnaranjados = new List<GameObject>();
        plugNegros = new List<GameObject>();
        lucesRojas = new List<GameObject>();
        inicializarComponentes(gameObject);
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
        if (moduloEncendido)
        {
            lucesRojasDict["LuzRoja1"].GetComponent<LuzRoja>().EncenderFoco();
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void inicializarPlugAnaranjado(string nombrePlug, int tipoLinea, bool estoyConectado = false)
    {
        plugAnaranjadosDict[nombrePlug].GetComponent<Plugs>().TipoConexion = 1;
        plugAnaranjadosDict[nombrePlug].GetComponent<Plugs>().voltaje = voltajeModulo;
        plugAnaranjadosDict[nombrePlug].GetComponent<Plugs>().Linea = tipoLinea;
        plugAnaranjadosDict[nombrePlug].GetComponent<Plugs>().tipoNodo = 0;
        if (!plugAnaranjadosDict[nombrePlug].GetComponent<Plugs>().estoConectado())
        {
            plugAnaranjadosDict[nombrePlug].GetComponent<Plugs>().QuitarAveria();
        }
        /*if (estoyConectado)
        {
            plugAnaranjadosDict[nombrePlug].GetComponent<Plugs>().estoConectado();
        }
        else
        {
            plugAnaranjadosDict[nombrePlug].GetComponent<Plugs>().QuitarAveria();
        }*/
    }

    private void inicializarPlugNegro(string nombrePlug, bool estoyConectado = false)
    {
        plugNegrosDict[nombrePlug].GetComponent<Plugs>().TipoConexion = 2;
        plugNegrosDict[nombrePlug].GetComponent<Plugs>().voltaje = voltajeModulo;
        if (estoyConectado)
        {
            plugNegrosDict[nombrePlug].GetComponent<Plugs>().estoConectado();
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
                child.tag = "PlugAnaranjado";
            }
            else if (child.name.Contains("EntradaPlugNegro"))
            {
                plugNegros.Add(child);
                child.AddComponent<CableComponent>();

                Plugs plug = child.AddComponent<Plugs>();
                plug.padreTotalComponente = this.gameObject;
                plugsConnections.Add(gameObject.name + "|" + child.name, "");

                plugNegrosDict.Add(child.name, child);
                child.tag = "PlugNegro";
            }
            else if (child.name.Contains("LuzRoja"))
            {
                lucesRojas.Add(child);
                child.AddComponent<LuzRoja>();

                lucesRojasDict.Add(child.name, child);
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
            lucesRojasDict["LuzRoja1"].GetComponent<LuzRoja>().EncenderFoco();
            lucesRojasDict["LuzRoja2"].GetComponent<LuzRoja>().EncenderFoco();
            lucesRojasDict["LuzRoja3"].GetComponent<LuzRoja>().EncenderFoco();
            inicializarPlugAnaranjado("EntradaPlugAnaranjado1", 1, true);
            inicializarPlugAnaranjado("EntradaPlugAnaranjado2", 1, true);
            inicializarPlugAnaranjado("EntradaPlugAnaranjado3", 1, true);
            inicializarPlugAnaranjado("EntradaPlugAnaranjado4", 2, true);
            inicializarPlugAnaranjado("EntradaPlugAnaranjado5", 2, true);
            inicializarPlugAnaranjado("EntradaPlugAnaranjado6", 2, true);
            inicializarPlugAnaranjado("EntradaPlugAnaranjado7", 3, true);
            inicializarPlugAnaranjado("EntradaPlugAnaranjado8", 3, true);
            inicializarPlugAnaranjado("EntradaPlugAnaranjado9", 3, true);

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
            lucesRojasDict["LuzRoja1"].GetComponent<LuzRoja>().ApagarFoco();
            lucesRojasDict["LuzRoja2"].GetComponent<LuzRoja>().ApagarFoco();
            lucesRojasDict["LuzRoja3"].GetComponent<LuzRoja>().ApagarFoco();
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
