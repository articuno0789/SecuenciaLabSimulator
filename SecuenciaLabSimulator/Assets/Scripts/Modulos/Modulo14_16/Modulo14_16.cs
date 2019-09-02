using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Modulo14_16 : MonoBehaviour
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

        plugsConnections = new Dictionary<string, string>();
        plugAnaranjados = new List<GameObject>();
        plugNegros = new List<GameObject>();
        lucesRojas = new List<GameObject>();
        InicializarComponentes(gameObject);
        if (moduloEncendido)
        {
            lucesRojasDict["LuzRoja1"].GetComponent<LuzRoja>().EncenderFoco();
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
            }
            else if (child.name.Contains("EntradaPlugNegro"))
            {
                plugNegros.Add(child);
                child.AddComponent<CableComponent>();

                Plugs plug = child.AddComponent<Plugs>();
                plug.padreTotalComponente = this.gameObject;
                plugsConnections.Add(gameObject.name + "|" + child.name, "");

                plugNegrosDict.Add(child.name, child);
            }
            else if (child.name.Contains("LuzRoja"))
            {
                lucesRojas.Add(child);
                child.AddComponent<LuzRoja>();
                lucesRojasDict.Add(child.name, child);
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
            lucesRojasDict["LuzRoja1"].GetComponent<LuzRoja>().EncenderFoco();
            ComportamientoModulo();
        }
        else
        {
            //Hacer algo si el modulo esta apagado.
            lucesRojasDict["LuzRoja1"].GetComponent<LuzRoja>().ApagarFoco();
        }
    }

    private void ComportamientoModulo()
    {
        if (lucesRojasDict["LuzRoja1"].GetComponent<LuzRoja>().ComprobarEstado(plugAnaranjadosDict["EntradaPlugAnaranjado1"], plugNegrosDict["EntradaPlugNegro1"]))
        {
            BotonesNormalmenteCerradosYAbiertos("EntradaPlugAnaranjado4", "EntradaPlugAnaranjado3", "EntradaPlugAnaranjado2", true); //Principal, abierto, cerrado
            BotonesNormalmenteCerradosYAbiertos("EntradaPlugAnaranjado7", "EntradaPlugAnaranjado6", "EntradaPlugAnaranjado5", true);
            BotonesNormalmenteCerradosYAbiertos("EntradaPlugAnaranjado10", "EntradaPlugAnaranjado9", "EntradaPlugAnaranjado8", true);
        }
        else
        {
            BotonesNormalmenteCerradosYAbiertos("EntradaPlugAnaranjado4", "EntradaPlugAnaranjado3", "EntradaPlugAnaranjado2", false); //Principal, abierto, cerrado
            BotonesNormalmenteCerradosYAbiertos("EntradaPlugAnaranjado7", "EntradaPlugAnaranjado6", "EntradaPlugAnaranjado5", false);
            BotonesNormalmenteCerradosYAbiertos("EntradaPlugAnaranjado10", "EntradaPlugAnaranjado9", "EntradaPlugAnaranjado8", false);
        }
    }

    void BotonesNormalmenteCerradosYAbiertos(string nPlugPrincipal, string nPlugAbierto, string nPlugCerrado, bool botonLogicoActivo)
    {
        Plugs plugConexionIzquierdo = plugAnaranjadosDict[nPlugPrincipal].GetComponent<Plugs>();
        Plugs plugConexionIzquierdoAbierto = plugAnaranjadosDict[nPlugAbierto].GetComponent<Plugs>();
        Plugs plugConexionIzquierdoCerrado = plugAnaranjadosDict[nPlugCerrado].GetComponent<Plugs>();
        Time.timeScale = 0.0F;
        if (!botonLogicoActivo) //!botonLogicoActivo - botonCuadradoRojoIzquierdo.GetComponent<Mod2PushButton>().EstaActivado()
        {
            plugConexionIzquierdoAbierto.EstablecerValoresNoConexion2();
            plugConexionIzquierdo.EstablecerValoresNoConexion2();
        }
        else
        if (botonLogicoActivo) // botonLogicoActivo - botonCuadradoVerdeIzquierdo.GetComponent<Mod2PushButton>().EstaActivado()
        {
            plugConexionIzquierdoCerrado.EstablecerValoresNoConexion2();
            plugConexionIzquierdo.EstablecerValoresNoConexion2();
        }
        Time.timeScale = 1.0F;
        plugConexionIzquierdo.EstablecerPropiedadesConexionesEntrantes();
        plugConexionIzquierdoAbierto.EstablecerPropiedadesConexionesEntrantes();
        plugConexionIzquierdoCerrado.EstablecerPropiedadesConexionesEntrantes();
        if (botonLogicoActivo && plugConexionIzquierdoAbierto.Conectado && plugConexionIzquierdo.Voltaje == 0 && plugConexionIzquierdo.TipoConexion == 0)
        {
            plugConexionIzquierdo.EstablecerPropiedadesConexionesEntrantes(plugAnaranjadosDict[nPlugAbierto]);
        }
        else
        if (botonLogicoActivo && plugConexionIzquierdo.Conectado)
        {
            plugConexionIzquierdoAbierto.EstablecerPropiedadesConexionesEntrantes(plugAnaranjadosDict[nPlugPrincipal]);
        }
        else if (!botonLogicoActivo && plugConexionIzquierdoCerrado.Conectado && plugConexionIzquierdo.Voltaje == 0 && plugConexionIzquierdo.TipoConexion == 0)
        {
            plugConexionIzquierdo.EstablecerPropiedadesConexionesEntrantes(plugAnaranjadosDict[nPlugCerrado]);
        }
        else
       if (!botonLogicoActivo && plugConexionIzquierdo.Conectado)
        {
            plugConexionIzquierdoCerrado.EstablecerPropiedadesConexionesEntrantes(plugAnaranjadosDict[nPlugPrincipal]);
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
