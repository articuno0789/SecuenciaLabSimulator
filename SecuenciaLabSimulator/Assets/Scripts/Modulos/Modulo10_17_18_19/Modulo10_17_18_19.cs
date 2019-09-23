using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Modulo10_17_18_19 : MonoBehaviour
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
    public bool DebugMode = false;
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
        InicializarComponentes(gameObject);
        if (moduloEncendido)
        {
            lucesRojasDict["LuzRoja1"].GetComponent<LuzRoja>().EncenderFoco();
        }
        //Inicializar contractores
        IncializacionContractores3("EntradaPlugAnaranjado4", "EntradaPlugAnaranjado2", "EntradaPlugAnaranjado3");
        IncializacionContractores3("EntradaPlugAnaranjado7", "EntradaPlugAnaranjado6", "EntradaPlugAnaranjado5");
        /*plugAnaranjadosDict["EntradaPlugAnaranjado4"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado3"];
        plugAnaranjadosDict["EntradaPlugAnaranjado4"].GetComponent<Plugs>().relacionCerrada = true;
        plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado4"];
        plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().relacionCerrada = false;
        plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado4"];
        plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().relacionCerrada = true;


        plugAnaranjadosDict["EntradaPlugAnaranjado7"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado6"];
        plugAnaranjadosDict["EntradaPlugAnaranjado7"].GetComponent<Plugs>().relacionCerrada = true;
        plugAnaranjadosDict["EntradaPlugAnaranjado5"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado7"];
        plugAnaranjadosDict["EntradaPlugAnaranjado5"].GetComponent<Plugs>().relacionCerrada = false;
        plugAnaranjadosDict["EntradaPlugAnaranjado6"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado7"];
        plugAnaranjadosDict["EntradaPlugAnaranjado6"].GetComponent<Plugs>().relacionCerrada = true;*/
    }

    void IncializacionContractores3(string nPlugComun, string nPlugNormalmenteAbierto, string nplugNormalmenteCerrado)
    {
        plugAnaranjadosDict[nPlugComun].GetComponent<Plugs>().EstablecerPlugRelacionado(plugAnaranjadosDict[nplugNormalmenteCerrado], true);
        plugAnaranjadosDict[nPlugNormalmenteAbierto].GetComponent<Plugs>().EstablecerPlugRelacionado(plugAnaranjadosDict[nPlugComun], false);
        plugAnaranjadosDict[nplugNormalmenteCerrado].GetComponent<Plugs>().EstablecerPlugRelacionado(plugAnaranjadosDict[nPlugComun], true);
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
            else if (child.name.Contains("LuzRoja"))
            {
                lucesRojas.Add(child);
                LuzRoja luzRoja = child.AddComponent<LuzRoja>();
                luzRoja.CurrentTypeParticleError = (int)AuxiliarModulos.ParticlesErrorTypes.SmokeEffect;
                luzRoja.CurrentTypeParticleError = (int)AuxiliarModulos.ParticlesErrorTypes.ElectricalSparksEffect;
                luzRoja.padreTotalComponente = this.gameObject;
                lucesRojasDict.Add(child.name, child);
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
            //Lado izquierdo
            ActivarContractorNormalmenteAbierto("EntradaPlugAnaranjado4", "EntradaPlugAnaranjado2", "EntradaPlugAnaranjado3");
            /*plugAnaranjadosDict["EntradaPlugAnaranjado4"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            plugAnaranjadosDict["EntradaPlugAnaranjado4"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado3"];
            plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
            plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().plugRelacionado = null;
            plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().EliminarPropiedadesConexionesEntradaPrueba();
            plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado4"];*/
            //Lado derecho
            ActivarContractorNormalmenteAbierto("EntradaPlugAnaranjado7", "EntradaPlugAnaranjado6", "EntradaPlugAnaranjado5");
            /*plugAnaranjadosDict["EntradaPlugAnaranjado7"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            plugAnaranjadosDict["EntradaPlugAnaranjado7"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado6"];
            plugAnaranjadosDict["EntradaPlugAnaranjado5"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
            plugAnaranjadosDict["EntradaPlugAnaranjado5"].GetComponent<Plugs>().plugRelacionado = null;
            plugAnaranjadosDict["EntradaPlugAnaranjado5"].GetComponent<Plugs>().EliminarPropiedadesConexionesEntradaPrueba();
            plugAnaranjadosDict["EntradaPlugAnaranjado6"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            plugAnaranjadosDict["EntradaPlugAnaranjado6"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado7"];*/
        }
        else
        {
            //Lado izquierdo
            ActivarContractorNormalmenteCerrado("EntradaPlugAnaranjado4", "EntradaPlugAnaranjado2", "EntradaPlugAnaranjado3");
            /*plugAnaranjadosDict["EntradaPlugAnaranjado4"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            plugAnaranjadosDict["EntradaPlugAnaranjado4"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado2"];
            plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado4"];
            plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
            plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().plugRelacionado = null;
            plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().EliminarPropiedadesConexionesEntradaPrueba();*/
            //Lado derecho
            ActivarContractorNormalmenteCerrado("EntradaPlugAnaranjado7", "EntradaPlugAnaranjado6", "EntradaPlugAnaranjado5");
            /*plugAnaranjadosDict["EntradaPlugAnaranjado7"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            plugAnaranjadosDict["EntradaPlugAnaranjado7"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado5"];
            plugAnaranjadosDict["EntradaPlugAnaranjado5"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            plugAnaranjadosDict["EntradaPlugAnaranjado5"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado7"];
            plugAnaranjadosDict["EntradaPlugAnaranjado6"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
            plugAnaranjadosDict["EntradaPlugAnaranjado6"].GetComponent<Plugs>().plugRelacionado = null;
            plugAnaranjadosDict["EntradaPlugAnaranjado6"].GetComponent<Plugs>().EliminarPropiedadesConexionesEntradaPrueba();*/
        }
    }

    void ActivarContractorNormalmenteCerrado(string nPlugComun, string nPlugNormalmenteAbierto, string nplugNormalmenteCerrado)
    {
        plugAnaranjadosDict[nPlugComun].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
        plugAnaranjadosDict[nPlugComun].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict[nplugNormalmenteCerrado];
        plugAnaranjadosDict[nPlugNormalmenteAbierto].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
        plugAnaranjadosDict[nPlugNormalmenteAbierto].GetComponent<Plugs>().plugRelacionado = null;
        plugAnaranjadosDict[nPlugNormalmenteAbierto].GetComponent<Plugs>().EliminarPropiedadesConexionesEntradaPrueba();
        plugAnaranjadosDict[nplugNormalmenteCerrado].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
        plugAnaranjadosDict[nplugNormalmenteCerrado].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict[nPlugComun];
    }

    void ActivarContractorNormalmenteAbierto(string nPlugComun, string nPlugNormalmenteAbierto, string nplugNormalmenteCerrado)
    {
        plugAnaranjadosDict[nPlugComun].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
        plugAnaranjadosDict[nPlugComun].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict[nPlugNormalmenteAbierto];
        plugAnaranjadosDict[nPlugNormalmenteAbierto].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
        plugAnaranjadosDict[nPlugNormalmenteAbierto].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict[nPlugComun];
        plugAnaranjadosDict[nplugNormalmenteCerrado].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
        plugAnaranjadosDict[nplugNormalmenteCerrado].GetComponent<Plugs>().plugRelacionado = null;
        plugAnaranjadosDict[nplugNormalmenteCerrado].GetComponent<Plugs>().EliminarPropiedadesConexionesEntradaPrueba();
    }

    //No se usan por el momento
    void FuncionamientoContractorRojo(string nPlugConexionArribaCerrado, string nPlugConexionAbajoCerrado, bool conexionAbierta)
    {
        Plugs plugConexionArribaCerrado = plugAnaranjadosDict[nPlugConexionArribaCerrado].GetComponent<Plugs>();
        Plugs plugConexionAbajoCerrado = plugAnaranjadosDict[nPlugConexionAbajoCerrado].GetComponent<Plugs>();
        Time.timeScale = 0.0F;
        plugConexionArribaCerrado.EstablecerValoresNoConexion2();
        plugConexionAbajoCerrado.EstablecerValoresNoConexion2();
        bool cortoElectrico = plugConexionArribaCerrado.ComprobarEstado(plugConexionArribaCerrado, plugConexionAbajoCerrado, conexionAbierta);
        Time.timeScale = 1.0F;
        if (!conexionAbierta && !cortoElectrico) // Para comprobar cortos
        {
            plugConexionArribaCerrado.EstablecerPropiedadesConexionesEntrantes();
            plugConexionAbajoCerrado.EstablecerPropiedadesConexionesEntrantes();
            if (!conexionAbierta && plugConexionArribaCerrado.Conectado && plugConexionAbajoCerrado.Voltaje == 0 && plugConexionAbajoCerrado.TipoConexion == 0)
            {
                plugConexionAbajoCerrado.EstablecerPropiedadesConexionesEntrantes(plugAnaranjadosDict[nPlugConexionArribaCerrado]);
            }
            else
            if (!conexionAbierta && plugConexionAbajoCerrado.Conectado)
            {
                plugConexionArribaCerrado.EstablecerPropiedadesConexionesEntrantes(plugAnaranjadosDict[nPlugConexionAbajoCerrado]);
            }
        }
    }

    void BotonesNormalmenteCerradosYAbiertos(string nPlugPrincipal, string nPlugAbierto, string nPlugCerrado, bool botonLogicoActivo)
    {
        Plugs plugConexionIzquierdo = plugAnaranjadosDict[nPlugPrincipal].GetComponent<Plugs>();
        Plugs plugConexionIzquierdoAbierto = plugAnaranjadosDict[nPlugAbierto].GetComponent<Plugs>();
        Plugs plugConexionIzquierdoCerrado = plugAnaranjadosDict[nPlugCerrado].GetComponent<Plugs>();
        Time.timeScale = 0.0F;
        bool cortoElectrico = false;
        if (!botonLogicoActivo) //!botonLogicoActivo - botonCuadradoRojoIzquierdo.GetComponent<Mod2PushButton>().EstaActivado()
        {
            plugConexionIzquierdoAbierto.EstablecerValoresNoConexion2();
            plugConexionIzquierdo.EstablecerValoresNoConexion2();
            cortoElectrico = plugConexionIzquierdo.ComprobarEstado(plugConexionIzquierdo, plugConexionIzquierdoCerrado, false);
        }
        else
        if (botonLogicoActivo) // botonLogicoActivo - botonCuadradoVerdeIzquierdo.GetComponent<Mod2PushButton>().EstaActivado()
        {
            plugConexionIzquierdoCerrado.EstablecerValoresNoConexion2();
            plugConexionIzquierdo.EstablecerValoresNoConexion2();
            cortoElectrico = plugConexionIzquierdo.ComprobarEstado(plugConexionIzquierdo, plugConexionIzquierdoAbierto, false);
        }
        Time.timeScale = 1.0F;
        if (!cortoElectrico)
        {
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
            //Debug.Log("No hay corto");
        }
        else
        {
            //Debug.Log("Hay corto");
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
