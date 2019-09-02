using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Modulo3 : MonoBehaviour
{
    #region Atributos
    public bool moduloEncendido = true;
    public Dictionary<string, string> plugsConnections;
    [SerializeField] public List<GameObject> plugAnaranjados;
    [SerializeField] public List<GameObject> plugNegros;
    [SerializeField] public List<GameObject> botonesCircularesRojos;
    //[SerializeField] public List<GameObject> botonesCircularesVerdes;
    public Dictionary<string, GameObject> plugAnaranjadosDict;
    public Dictionary<string, GameObject> plugNegrosDict;
    public Dictionary<string, GameObject> botonesCircularesRojosDict;
    //public Dictionary<string, GameObject> botonesCircularesVerdesDict;
    private string rutaAnimacionBotonCircular = "Assets/Animation/Modulos/Modulo3/Mod3PresBotonCircular.anim";
    private string nombreAnimacionBotonCircular = "Mod3PresBotonCircular";

    //Variables de debug
    public bool mostrarDiccionarioConexiones = false; // Variable
    public bool mostrarPlugAnaranjados = false; // Variable
    public bool mostrarPlugNegros = false; // Variable
    public bool mostrarBotonesCircularesRojos = false; // Variable
    public bool mostrarBotonesCircularesVerdes = false; // Variable
    #endregion

    #region Inicializacion
    private void Awake()
    {
        plugsConnections = new Dictionary<string, string>();
        plugAnaranjadosDict = new Dictionary<string, GameObject>();
        plugNegrosDict = new Dictionary<string, GameObject>();
        botonesCircularesRojosDict = new Dictionary<string, GameObject>();
        //botonesCircularesVerdesDict = new Dictionary<string, GameObject>();

        plugAnaranjados = new List<GameObject>();
        plugNegros = new List<GameObject>();
        botonesCircularesRojos = new List<GameObject>();
        //botonesCircularesVerdes = new List<GameObject>();
        InicializarComponentes(gameObject);
    }

    void Start()
    {
        botonesCircularesRojosDict["BotonCircularVerde1"].GetComponent<Mod3PushButton>().EstablecerTipoVerde();
        botonesCircularesRojosDict["BotonCircularVerde1"].GetComponent<Mod3PushButton>().EstablecerBotonDespresionado();
        botonesCircularesRojosDict["BotonCircularRojo1"].GetComponent<Mod3PushButton>().EstablecerTipoRojo();
        botonesCircularesRojosDict["BotonCircularRojo1"].GetComponent<Mod3PushButton>().EstablecerBotonDespresionado();
        botonesCircularesRojosDict["BotonCircularRojo2"].GetComponent<Mod3PushButton>().EstablecerTipoRojo();
        botonesCircularesRojosDict["BotonCircularRojo2"].GetComponent<Mod3PushButton>().EstablecerBotonDespresionado();
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
            else if (child.name.Contains("BotonCircularRojo"))
            {
                botonesCircularesRojos.Add(child);
                Animation ani = child.AddComponent<Animation>();
                ani.playAutomatically = false;
                ani.AddClip(((AnimationClip)AssetDatabase.LoadAssetAtPath(rutaAnimacionBotonCircular, typeof(AnimationClip))), nombreAnimacionBotonCircular);
                child.AddComponent<Mod3PushButton>();

                botonesCircularesRojosDict.Add(child.name, child);
            }
            else if (child.name.Contains("BotonCircularVerde"))
            {
                botonesCircularesRojos.Add(child);
                //botonesCircularesVerdes.Add(child);
                Animation ani = child.AddComponent<Animation>();
                ani.playAutomatically = false;
                ani.AddClip(((AnimationClip)AssetDatabase.LoadAssetAtPath(rutaAnimacionBotonCircular, typeof(AnimationClip))), nombreAnimacionBotonCircular);
                child.AddComponent<Mod3PushButton>();

                botonesCircularesRojosDict.Add(child.name, child);
                //botonesCircularesVerdesDict.Add(child.name, child);
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
            FuncionamientoContractorRojo("EntradaPlugAnaranjado1", "EntradaPlugAnaranjado2", "EntradaPlugAnaranjado3",
                "EntradaPlugAnaranjado4", "BotonCircularRojo1");
            //Circuito cenctro
            FuncionamientoContractorRojo("EntradaPlugAnaranjado5", "EntradaPlugAnaranjado6", "EntradaPlugAnaranjado7",
                "EntradaPlugAnaranjado8", "BotonCircularVerde1");
            //Circuito Derecho
            FuncionamientoContractorRojo("EntradaPlugAnaranjado9", "EntradaPlugAnaranjado10", "EntradaPlugAnaranjado11",
                "EntradaPlugAnaranjado12", "BotonCircularRojo2");
        }
        else
        {

        }
    }

    void FuncionamientoContractorRojo(string nPlugConexionArribaCerrado, string nPlugConexionAbajoCerrado, string nPlugConexionArribaAbierto,
        string nPlugConexionAbajoAbierto, string nBoton)
    {
        Plugs plugConexionArribaCerrado = plugAnaranjadosDict[nPlugConexionArribaCerrado].GetComponent<Plugs>();
        Plugs plugConexionAbajoCerrado = plugAnaranjadosDict[nPlugConexionAbajoCerrado].GetComponent<Plugs>();
        Plugs plugConexionArribaAbierto = plugAnaranjadosDict[nPlugConexionArribaAbierto].GetComponent<Plugs>();
        Plugs plugConexionAbajoAbierto = plugAnaranjadosDict[nPlugConexionAbajoAbierto].GetComponent<Plugs>();
        Time.timeScale = 0.0F;
        if (!botonesCircularesRojosDict[nBoton].GetComponent<Mod3PushButton>().EstaActivado())
        {
            plugConexionArribaAbierto.EstablecerValoresNoConexion2();
            plugConexionAbajoAbierto.EstablecerValoresNoConexion2();
        }
        else
        if (botonesCircularesRojosDict[nBoton].GetComponent<Mod3PushButton>().EstaActivado())
        {
            plugConexionArribaCerrado.EstablecerValoresNoConexion2();
            plugConexionAbajoCerrado.EstablecerValoresNoConexion2();
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
