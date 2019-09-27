using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Modulo3 : MonoBehaviour
{
    #region Atributos
    [Header("Encendido")]
    public bool moduloEncendido = true;
    [Header("Conexiones")]
    public Dictionary<string, string> plugsConnections;
    [Header("Diccionarios de elementos")]
    public Dictionary<string, GameObject> plugAnaranjadosDict;
    public Dictionary<string, GameObject> plugNegrosDict;
    public Dictionary<string, GameObject> botonesCircularesRojosDict;
    [Header("Listas de elementos")]
    [SerializeField] public List<GameObject> plugAnaranjados;
    [SerializeField] public List<GameObject> plugNegros;
    [SerializeField] public List<GameObject> botonesCircularesRojos;
    [Header("Animaciones")]
    private string rutaAnimacionBotonCircular = "Assets/Animation/Modulos/Modulo3/Mod3PresBotonCircular.anim";
    private string nombreAnimacionBotonCircular = "Mod3PresBotonCircular";
    [Header("Parametros Plugs")]
    private string nombreTagPlugAnaranjado = "PlugAnaranjado";
    private string nombreTagPlugNegro = "PlugNegro";
    [Header("Parametros Botones")]
    private string nombreTagPlugBotonVerdeCircular = "BotonVerdeCircular";
    private string nombreTagPlugBotonRojoCircular = "BotonRojoCircular";
    //Variables de debug
    [Header("Debug")]
    public bool mostrarDiccionarioConexiones = false; // Variable
    public bool mostrarPlugAnaranjados = false; // Variable
    public bool mostrarPlugNegros = false; // Variable
    public bool mostrarBotonesCircularesRojos = false; // Variable
    public bool mostrarBotonesCircularesVerdes = false; // Variable
    #endregion

    #region Inicializacion
    private void Awake()
    {
        //Inicialización de listas y diccionarios de elementos.
        plugsConnections = new Dictionary<string, string>();
        plugAnaranjadosDict = new Dictionary<string, GameObject>();
        plugNegrosDict = new Dictionary<string, GameObject>();
        botonesCircularesRojosDict = new Dictionary<string, GameObject>();

        plugAnaranjados = new List<GameObject>();
        plugNegros = new List<GameObject>();
        botonesCircularesRojos = new List<GameObject>();
        InicializarComponentes(gameObject);
        //Botones
        InicializarTiposBotones();
        //Contractores
        IncializacionContractores("EntradaPlugAnaranjado1", "EntradaPlugAnaranjado2", true);
        IncializacionContractores("EntradaPlugAnaranjado3", "EntradaPlugAnaranjado4", false);
        IncializacionContractores("EntradaPlugAnaranjado5", "EntradaPlugAnaranjado6", true);
        IncializacionContractores("EntradaPlugAnaranjado7", "EntradaPlugAnaranjado8", false);
        IncializacionContractores("EntradaPlugAnaranjado9", "EntradaPlugAnaranjado10", true);
        IncializacionContractores("EntradaPlugAnaranjado11", "EntradaPlugAnaranjado12", false);
        /*
        plugAnaranjadosDict["EntradaPlugAnaranjado1"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado2"];
        plugAnaranjadosDict["EntradaPlugAnaranjado1"].GetComponent<Plugs>().relacionCerrada = true;
        plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado1"];
        plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().relacionCerrada = true;

        plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado4"];
        plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().relacionCerrada = false;
        plugAnaranjadosDict["EntradaPlugAnaranjado4"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado3"];
        plugAnaranjadosDict["EntradaPlugAnaranjado4"].GetComponent<Plugs>().relacionCerrada = false;

        plugAnaranjadosDict["EntradaPlugAnaranjado5"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado6"];
        plugAnaranjadosDict["EntradaPlugAnaranjado5"].GetComponent<Plugs>().relacionCerrada = true;
        plugAnaranjadosDict["EntradaPlugAnaranjado6"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado5"];
        plugAnaranjadosDict["EntradaPlugAnaranjado6"].GetComponent<Plugs>().relacionCerrada = true;

        plugAnaranjadosDict["EntradaPlugAnaranjado7"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado8"];
        plugAnaranjadosDict["EntradaPlugAnaranjado7"].GetComponent<Plugs>().relacionCerrada = false;
        plugAnaranjadosDict["EntradaPlugAnaranjado8"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado7"];
        plugAnaranjadosDict["EntradaPlugAnaranjado8"].GetComponent<Plugs>().relacionCerrada = false;

        plugAnaranjadosDict["EntradaPlugAnaranjado9"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado10"];
        plugAnaranjadosDict["EntradaPlugAnaranjado9"].GetComponent<Plugs>().relacionCerrada = true;
        plugAnaranjadosDict["EntradaPlugAnaranjado10"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado9"];
        plugAnaranjadosDict["EntradaPlugAnaranjado10"].GetComponent<Plugs>().relacionCerrada = true;

        plugAnaranjadosDict["EntradaPlugAnaranjado11"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado12"];
        plugAnaranjadosDict["EntradaPlugAnaranjado11"].GetComponent<Plugs>().relacionCerrada = false;
        plugAnaranjadosDict["EntradaPlugAnaranjado12"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado11"];
        plugAnaranjadosDict["EntradaPlugAnaranjado12"].GetComponent<Plugs>().relacionCerrada = false;*/
    }

    void Start()
    {
        InicializarTiposBotones();
    }

    void InicializarTiposBotones()
    {
        botonesCircularesRojosDict["BotonCircularVerde1"].GetComponent<Mod3PushButton>().EstablecerTipoVerde();
        botonesCircularesRojosDict["BotonCircularVerde1"].GetComponent<Mod3PushButton>().EstablecerBotonDespresionado();
        botonesCircularesRojosDict["BotonCircularRojo1"].GetComponent<Mod3PushButton>().EstablecerTipoRojo();
        botonesCircularesRojosDict["BotonCircularRojo1"].GetComponent<Mod3PushButton>().EstablecerBotonDespresionado();
        botonesCircularesRojosDict["BotonCircularRojo2"].GetComponent<Mod3PushButton>().EstablecerTipoRojo();
        botonesCircularesRojosDict["BotonCircularRojo2"].GetComponent<Mod3PushButton>().EstablecerBotonDespresionado();
    }

    void IncializacionContractores(string nPlug1, string nPlug2, bool cerrado)
    {
        Plugs primerPlug = plugAnaranjadosDict[nPlug1].GetComponent<Plugs>();
        Plugs segundoPlug = plugAnaranjadosDict[nPlug2].GetComponent<Plugs>();
        if(primerPlug != null && segundoPlug != null)
        {
            primerPlug.plugRelacionado = plugAnaranjadosDict[nPlug2];
            primerPlug.relacionCerrada = cerrado;
            segundoPlug.plugRelacionado = plugAnaranjadosDict[nPlug1];
            segundoPlug.relacionCerrada = cerrado;
        }
        else
        {
            Debug.LogError(this.name + ", Error. IncializacionContractores(string nPlug1, string nPlug2, bool cerrado) - primero o segundo plug es nulo.");
        }
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
            else if (child.name.Contains("BotonCircularRojo"))
            {
                botonesCircularesRojos.Add(child);
                Animation ani = child.AddComponent<Animation>();
                ani.playAutomatically = false;
                ani.AddClip(((AnimationClip)AuxiliarModulos.RegresarObjetoAnimation(nombreAnimacionBotonCircular)), nombreAnimacionBotonCircular);
                //ani.AddClip(((AnimationClip)AssetDatabase.LoadAssetAtPath(rutaAnimacionBotonCircular, typeof(AnimationClip))), nombreAnimacionBotonCircular);
                Mod3PushButton boton = child.AddComponent<Mod3PushButton>();
                boton.EstablecerTipoRojo();
                boton.EstablecerBotonDespresionado();

                botonesCircularesRojosDict.Add(child.name, child);
                child.tag = nombreTagPlugBotonRojoCircular;
            }
            else if (child.name.Contains("BotonCircularVerde"))
            {
                botonesCircularesRojos.Add(child);
                //botonesCircularesVerdes.Add(child);
                Animation ani = child.AddComponent<Animation>();
                ani.playAutomatically = false;
                ani.AddClip(((AnimationClip)AuxiliarModulos.RegresarObjetoAnimation(nombreAnimacionBotonCircular)), nombreAnimacionBotonCircular);
                //ani.AddClip(((AnimationClip)AssetDatabase.LoadAssetAtPath(rutaAnimacionBotonCircular, typeof(AnimationClip))), nombreAnimacionBotonCircular);
                Mod3PushButton boton = child.AddComponent<Mod3PushButton>();
                boton.EstablecerTipoVerde();
                boton.EstablecerBotonDespresionado();

                botonesCircularesRojosDict.Add(child.name, child);
                //botonesCircularesVerdesDict.Add(child.name, child);
                child.tag = nombreTagPlugBotonVerdeCircular;
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
            //Circuito izquierdo
            if (!botonesCircularesRojosDict["BotonCircularRojo1"].GetComponent<Mod3PushButton>().EstaActivado())
            {
                plugAnaranjadosDict["EntradaPlugAnaranjado1"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
                plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
                plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
                plugAnaranjadosDict["EntradaPlugAnaranjado4"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
            }
            else
            {
                plugAnaranjadosDict["EntradaPlugAnaranjado1"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
                plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
                plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
                plugAnaranjadosDict["EntradaPlugAnaranjado4"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            }
            //Circuito centro
            if (!botonesCircularesRojosDict["BotonCircularVerde1"].GetComponent<Mod3PushButton>().EstaActivado())
            {
                plugAnaranjadosDict["EntradaPlugAnaranjado5"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
                plugAnaranjadosDict["EntradaPlugAnaranjado6"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
                plugAnaranjadosDict["EntradaPlugAnaranjado7"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
                plugAnaranjadosDict["EntradaPlugAnaranjado8"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
            }
            else
            {
                plugAnaranjadosDict["EntradaPlugAnaranjado5"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
                plugAnaranjadosDict["EntradaPlugAnaranjado6"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
                plugAnaranjadosDict["EntradaPlugAnaranjado7"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
                plugAnaranjadosDict["EntradaPlugAnaranjado8"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            }
            //Circuito Derecho
            if (!botonesCircularesRojosDict["BotonCircularRojo2"].GetComponent<Mod3PushButton>().EstaActivado())
            {
                plugAnaranjadosDict["EntradaPlugAnaranjado9"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
                plugAnaranjadosDict["EntradaPlugAnaranjado10"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
                plugAnaranjadosDict["EntradaPlugAnaranjado11"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
                plugAnaranjadosDict["EntradaPlugAnaranjado12"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
            }
            else
            {
                plugAnaranjadosDict["EntradaPlugAnaranjado9"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
                plugAnaranjadosDict["EntradaPlugAnaranjado10"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
                plugAnaranjadosDict["EntradaPlugAnaranjado11"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
                plugAnaranjadosDict["EntradaPlugAnaranjado12"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            }
            /*FuncionamientoContractorRojo("EntradaPlugAnaranjado1", "EntradaPlugAnaranjado2", "EntradaPlugAnaranjado3",
                "EntradaPlugAnaranjado4", "BotonCircularRojo1");
            //Circuito cenctro
            FuncionamientoContractorRojo("EntradaPlugAnaranjado5", "EntradaPlugAnaranjado6", "EntradaPlugAnaranjado7",
                "EntradaPlugAnaranjado8", "BotonCircularVerde1");
            //Circuito Derecho
            FuncionamientoContractorRojo("EntradaPlugAnaranjado9", "EntradaPlugAnaranjado10", "EntradaPlugAnaranjado11",
                "EntradaPlugAnaranjado12", "BotonCircularRojo2");*/
        }
        else
        {

        }
    }

    //Estas funciones no se usan por el momento
    /*void FuncionamientoContractorRojo(string nPlugConexionArribaCerrado, string nPlugConexionAbajoCerrado, bool conexionAbierta)
    {
        Plugs plugConexionArribaCerrado = plugAnaranjadosDict[nPlugConexionArribaCerrado].GetComponent<Plugs>();
        Plugs plugConexionAbajoCerrado = plugAnaranjadosDict[nPlugConexionAbajoCerrado].GetComponent<Plugs>();
        Time.timeScale = 0.0F;
        plugConexionArribaCerrado.EstablecerValoresNoConexion2();
        plugConexionAbajoCerrado.EstablecerValoresNoConexion2();
        bool cortoElectrico = plugConexionArribaCerrado.ComprobarEstado(plugConexionArribaCerrado, plugConexionAbajoCerrado, conexionAbierta);
        Time.timeScale = 1.0F;
        //if (!conexionAbierta && !cortoElectrico) // Para comprobar cortos
        //{
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
        //}
    }

    void FuncionamientoContractorRojo(string nPlugConexionArribaCerrado, string nPlugConexionAbajoCerrado, string nPlugConexionArribaAbierto,
        string nPlugConexionAbajoAbierto, string nBoton)
    {
        Plugs plugConexionArribaCerrado = plugAnaranjadosDict[nPlugConexionArribaCerrado].GetComponent<Plugs>();
        Plugs plugConexionAbajoCerrado = plugAnaranjadosDict[nPlugConexionAbajoCerrado].GetComponent<Plugs>();
        Plugs plugConexionArribaAbierto = plugAnaranjadosDict[nPlugConexionArribaAbierto].GetComponent<Plugs>();
        Plugs plugConexionAbajoAbierto = plugAnaranjadosDict[nPlugConexionAbajoAbierto].GetComponent<Plugs>();
        Time.timeScale = 0.0F;
        bool cortoElectrico = false;
        if (!botonesCircularesRojosDict[nBoton].GetComponent<Mod3PushButton>().EstaActivado())
        {
            plugConexionArribaAbierto.EstablecerValoresNoConexion2();
            plugConexionAbajoAbierto.EstablecerValoresNoConexion2();
            cortoElectrico = plugConexionArribaCerrado.ComprobarEstado(plugConexionArribaCerrado, plugConexionAbajoCerrado, false);
        }
        else
        if (botonesCircularesRojosDict[nBoton].GetComponent<Mod3PushButton>().EstaActivado())
        {
            plugConexionArribaCerrado.EstablecerValoresNoConexion2();
            plugConexionAbajoCerrado.EstablecerValoresNoConexion2();
            cortoElectrico = plugConexionArribaAbierto.ComprobarEstado(plugConexionArribaAbierto, plugConexionAbajoAbierto, false);
        }
        Time.timeScale = 1.0F;
        plugConexionArribaCerrado.EstablecerPropiedadesConexionesEntrantes();
        plugConexionAbajoCerrado.EstablecerPropiedadesConexionesEntrantes();
        plugConexionArribaAbierto.EstablecerPropiedadesConexionesEntrantes();
        plugConexionAbajoAbierto.EstablecerPropiedadesConexionesEntrantes();
        if (!botonesCircularesRojosDict[nBoton].GetComponent<Mod3PushButton>().EstaActivado() && plugConexionArribaCerrado.Conectado && plugConexionAbajoCerrado.Voltaje == 0 && plugConexionAbajoCerrado.TipoConexion == 0)
        {
            plugConexionAbajoCerrado.EstablecerPropiedadesConexionesEntrantes(plugAnaranjadosDict[nPlugConexionArribaCerrado]);
        }
        else
        if (!botonesCircularesRojosDict[nBoton].GetComponent<Mod3PushButton>().EstaActivado() && plugConexionAbajoCerrado.Conectado)
        {
            plugConexionArribaCerrado.EstablecerPropiedadesConexionesEntrantes(plugAnaranjadosDict[nPlugConexionAbajoCerrado]);
        }
        else if (botonesCircularesRojosDict[nBoton].GetComponent<Mod3PushButton>().EstaActivado() && plugConexionArribaAbierto.Conectado && plugConexionAbajoAbierto.Voltaje == 0 && plugConexionAbajoAbierto.TipoConexion == 0)
        {
            plugConexionAbajoAbierto.EstablecerPropiedadesConexionesEntrantes(plugAnaranjadosDict[nPlugConexionArribaAbierto]);
        }
        else
        if (botonesCircularesRojosDict[nBoton].GetComponent<Mod3PushButton>().EstaActivado() && plugConexionAbajoAbierto.Conectado)
        {
            plugConexionArribaAbierto.EstablecerPropiedadesConexionesEntrantes(plugAnaranjadosDict[nPlugConexionAbajoAbierto]);
        }
    }*/

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
        if (mostrarBotonesCircularesVerdes)
        {
            ImprimirDiccionario(botonesCircularesRojosDict, 3);
        }
        if (mostrarBotonesCircularesRojos)
        {
            ImprimirDiccionario(botonesCircularesRojosDict, 4);
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
            mostrarBotonesCircularesVerdes = false;
            nombreDiccionario = "botonesCircularesVerdesDict";
        }
        else if (bandera == 4)
        {
            mostrarBotonesCircularesRojos = false;
            nombreDiccionario = "botonesCircularesRojosDict";
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
