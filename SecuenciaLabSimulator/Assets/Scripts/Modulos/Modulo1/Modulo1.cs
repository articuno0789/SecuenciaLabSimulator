using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Modulo1 : MonoBehaviour
{
    #region Atributos
    public bool moduloEncendido = true;
    public Dictionary<string, string> plugsConnections;
    public Dictionary<string, GameObject> plugAnaranjadosDict;
    public Dictionary<string, GameObject> plugNegrosDict;
    public Dictionary<string, GameObject> lucesRojasDict;
    [SerializeField] public List<GameObject> plugAnaranjados;
    [SerializeField] public List<GameObject> plugNegros;
    [SerializeField] public List<GameObject> lucesRojas;
    [SerializeField] public float voltajeModulo = 127; // Variable
    [SerializeField] public bool pruebaDeLuz = true; // Variable
    public enum ParticlesErrorTypes
    {
        BigExplosion,
        DrippingFlames,
        ElectricalSparksEffect,
        SmallExplosionEffect,
        SmokeEffect,
        SparksEffect,
        RibbonSmoke,
        PlasmaExplosionEffect
    }

    //Variables de debug
    public bool mostrarDiccionarioConexiones = false; // Variable
    public bool mostrarPlugAnaranjados = false; // Variable
    public bool mostrarPlugNegros = false; // Variable
    #endregion

    #region Inicializacion
    // Start is called before the first frame update
    private void Awake()
    {
        plugsConnections = new Dictionary<string, string>();
        plugAnaranjadosDict = new Dictionary<string, GameObject>();
        plugNegrosDict = new Dictionary<string, GameObject>();
        lucesRojasDict = new Dictionary<string, GameObject>();

        plugAnaranjados = new List<GameObject>();
        plugNegros = new List<GameObject>();
        lucesRojas = new List<GameObject>();
        InicializarComponentes(gameObject);
        inicializarPlugAnaranjado("EntradaPlugAnaranjado1");
        inicializarPlugAnaranjado("EntradaPlugAnaranjado2");
        inicializarPlugAnaranjado("EntradaPlugAnaranjado3");

        inicializarPlugNegro("EntradaPlugNegro1");
        inicializarPlugNegro("EntradaPlugNegro2");
        inicializarPlugNegro("EntradaPlugNegro3");
        if (moduloEncendido)
        {
            lucesRojasDict["LuzRoja1"].GetComponent<LuzRoja>().EncenderFoco();
        }
    }

    private void inicializarPlugAnaranjado(string nombrePlug, bool estoyConectado = false)
    {
        plugAnaranjadosDict[nombrePlug].GetComponent<Plugs>().TipoConexion = 1;
        plugAnaranjadosDict[nombrePlug].GetComponent<Plugs>().voltaje = voltajeModulo;
        plugAnaranjadosDict[nombrePlug].GetComponent<Plugs>().Linea = 1;
        if (estoyConectado)
        {
            plugAnaranjadosDict[nombrePlug].GetComponent<Plugs>().estoConectado();
        }
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
                LuzRoja luzRoja = child.AddComponent<LuzRoja>();
                luzRoja.CurrentTypeParticleError = (int)ParticlesErrorTypes.SmokeEffect;
                luzRoja.CurrentTypeParticleError = (int)ParticlesErrorTypes.ElectricalSparksEffect;
                luzRoja.padreTotalComponente = this.gameObject;
                lucesRojasDict.Add(child.name, child);

                lucesRojas.Add(child);
                child.AddComponent<LuzRoja>();
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

            inicializarPlugAnaranjado("EntradaPlugAnaranjado1", true);
            inicializarPlugAnaranjado("EntradaPlugAnaranjado2", true);
            inicializarPlugAnaranjado("EntradaPlugAnaranjado3", true);

            inicializarPlugNegro("EntradaPlugNegro1", true);
            inicializarPlugNegro("EntradaPlugNegro2", true);
            inicializarPlugNegro("EntradaPlugNegro3", true);

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
            lucesRojasDict["LuzRoja1"].GetComponent<LuzRoja>().ApagarFoco();
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
