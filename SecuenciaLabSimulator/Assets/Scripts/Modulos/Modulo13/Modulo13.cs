﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Modulo13 : MonoBehaviour
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
        //Contractores
        IncializacionContractores("EntradaPlugAnaranjado2", "EntradaPlugAnaranjado3", true);
        IncializacionContractores("EntradaPlugAnaranjado4", "EntradaPlugAnaranjado5", true);
        IncializacionContractores("EntradaPlugAnaranjado6", "EntradaPlugAnaranjado7", true);
        IncializacionContractores("EntradaPlugAnaranjado8", "EntradaPlugAnaranjado9", true);
        IncializacionContractores("EntradaPlugAnaranjado10", "EntradaPlugAnaranjado11", false);
        IncializacionContractores("EntradaPlugAnaranjado12", "EntradaPlugAnaranjado13", false);
        IncializacionContractores("EntradaPlugAnaranjado14", "EntradaPlugAnaranjado15", false);
        IncializacionContractores("EntradaPlugAnaranjado16", "EntradaPlugAnaranjado17", false);

        /*plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado3"];
        plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().relacionCerrada = true;
        plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado2"];
        plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().relacionCerrada = true;

        plugAnaranjadosDict["EntradaPlugAnaranjado4"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado5"];
        plugAnaranjadosDict["EntradaPlugAnaranjado4"].GetComponent<Plugs>().relacionCerrada = true;
        plugAnaranjadosDict["EntradaPlugAnaranjado5"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado4"];
        plugAnaranjadosDict["EntradaPlugAnaranjado5"].GetComponent<Plugs>().relacionCerrada = true;

        plugAnaranjadosDict["EntradaPlugAnaranjado6"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado7"];
        plugAnaranjadosDict["EntradaPlugAnaranjado6"].GetComponent<Plugs>().relacionCerrada = true;
        plugAnaranjadosDict["EntradaPlugAnaranjado7"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado6"];
        plugAnaranjadosDict["EntradaPlugAnaranjado7"].GetComponent<Plugs>().relacionCerrada = true;

        plugAnaranjadosDict["EntradaPlugAnaranjado8"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado9"];
        plugAnaranjadosDict["EntradaPlugAnaranjado8"].GetComponent<Plugs>().relacionCerrada = true;
        plugAnaranjadosDict["EntradaPlugAnaranjado9"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado8"];
        plugAnaranjadosDict["EntradaPlugAnaranjado9"].GetComponent<Plugs>().relacionCerrada = true;

        plugAnaranjadosDict["EntradaPlugAnaranjado10"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado11"];
        plugAnaranjadosDict["EntradaPlugAnaranjado10"].GetComponent<Plugs>().relacionCerrada = false;
        plugAnaranjadosDict["EntradaPlugAnaranjado11"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado10"];
        plugAnaranjadosDict["EntradaPlugAnaranjado11"].GetComponent<Plugs>().relacionCerrada = false;

        plugAnaranjadosDict["EntradaPlugAnaranjado12"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado13"];
        plugAnaranjadosDict["EntradaPlugAnaranjado12"].GetComponent<Plugs>().relacionCerrada = false;
        plugAnaranjadosDict["EntradaPlugAnaranjado13"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado12"];
        plugAnaranjadosDict["EntradaPlugAnaranjado13"].GetComponent<Plugs>().relacionCerrada = false;

        plugAnaranjadosDict["EntradaPlugAnaranjado14"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado15"];
        plugAnaranjadosDict["EntradaPlugAnaranjado14"].GetComponent<Plugs>().relacionCerrada = false;
        plugAnaranjadosDict["EntradaPlugAnaranjado15"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado14"];
        plugAnaranjadosDict["EntradaPlugAnaranjado15"].GetComponent<Plugs>().relacionCerrada = false;

        plugAnaranjadosDict["EntradaPlugAnaranjado16"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado17"];
        plugAnaranjadosDict["EntradaPlugAnaranjado16"].GetComponent<Plugs>().relacionCerrada = false;
        plugAnaranjadosDict["EntradaPlugAnaranjado17"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado16"];
        plugAnaranjadosDict["EntradaPlugAnaranjado17"].GetComponent<Plugs>().relacionCerrada = false;*/
    }


    public GameObject[] pLugs;
    void BuscartodosLosPlugs()
    {
        //plugAnaranjadosTodos = new List<GameObject>();
        pLugs = GameObject.FindGameObjectsWithTag("PlugAnaranjado");
    }

    void reiniciarTodosPlugsAnaranjados()
    {
        int plugsLeng = pLugs.Length;
        for(int i= 0; i<plugsLeng; i++)
        {
            pLugs[i].GetComponent<Plugs>().EstablecerValoresNoConexion();
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void IncializacionContractores(string nPlug1, string nPlug2, bool cerrado)
    {
        plugAnaranjadosDict[nPlug1].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict[nPlug2];
        plugAnaranjadosDict[nPlug1].GetComponent<Plugs>().relacionCerrada = cerrado;
        plugAnaranjadosDict[nPlug2].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict[nPlug1];
        plugAnaranjadosDict[nPlug2].GetComponent<Plugs>().relacionCerrada = cerrado;
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
            //Normalmente cerrados
            plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
            plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
            plugAnaranjadosDict["EntradaPlugAnaranjado4"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
            plugAnaranjadosDict["EntradaPlugAnaranjado5"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
            plugAnaranjadosDict["EntradaPlugAnaranjado6"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
            plugAnaranjadosDict["EntradaPlugAnaranjado7"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
            plugAnaranjadosDict["EntradaPlugAnaranjado8"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
            plugAnaranjadosDict["EntradaPlugAnaranjado9"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
            //Normalmente Abiertos
            plugAnaranjadosDict["EntradaPlugAnaranjado10"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            plugAnaranjadosDict["EntradaPlugAnaranjado11"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            plugAnaranjadosDict["EntradaPlugAnaranjado12"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            plugAnaranjadosDict["EntradaPlugAnaranjado13"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            plugAnaranjadosDict["EntradaPlugAnaranjado14"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            plugAnaranjadosDict["EntradaPlugAnaranjado15"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            plugAnaranjadosDict["EntradaPlugAnaranjado16"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            plugAnaranjadosDict["EntradaPlugAnaranjado17"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
        }
        else
        {
            //Normalmente cerrados
            plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            plugAnaranjadosDict["EntradaPlugAnaranjado4"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            plugAnaranjadosDict["EntradaPlugAnaranjado5"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            plugAnaranjadosDict["EntradaPlugAnaranjado6"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            plugAnaranjadosDict["EntradaPlugAnaranjado7"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            plugAnaranjadosDict["EntradaPlugAnaranjado8"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            plugAnaranjadosDict["EntradaPlugAnaranjado9"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
            //Normalmente Abiertos
            plugAnaranjadosDict["EntradaPlugAnaranjado10"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
            plugAnaranjadosDict["EntradaPlugAnaranjado11"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
            plugAnaranjadosDict["EntradaPlugAnaranjado12"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
            plugAnaranjadosDict["EntradaPlugAnaranjado13"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
            plugAnaranjadosDict["EntradaPlugAnaranjado14"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
            plugAnaranjadosDict["EntradaPlugAnaranjado15"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
            plugAnaranjadosDict["EntradaPlugAnaranjado16"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
            plugAnaranjadosDict["EntradaPlugAnaranjado17"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
        }

        //Anterior
        /*if (lucesRojasDict["LuzRoja1"].GetComponent<LuzRoja>().ComprobarEstado(plugAnaranjadosDict["EntradaPlugAnaranjado1"], plugNegrosDict["EntradaPlugNegro1"]))
        {
            FuncionamientoContractorRojo("EntradaPlugAnaranjado2", "EntradaPlugAnaranjado3", true);
            FuncionamientoContractorRojo("EntradaPlugAnaranjado4", "EntradaPlugAnaranjado5", true);
            FuncionamientoContractorRojo("EntradaPlugAnaranjado6", "EntradaPlugAnaranjado7", true);
            FuncionamientoContractorRojo("EntradaPlugAnaranjado8", "EntradaPlugAnaranjado9", true);
            FuncionamientoContractorRojo("EntradaPlugAnaranjado10", "EntradaPlugAnaranjado11", false);
            FuncionamientoContractorRojo("EntradaPlugAnaranjado12", "EntradaPlugAnaranjado13", false);
            FuncionamientoContractorRojo("EntradaPlugAnaranjado14", "EntradaPlugAnaranjado15", false);
            FuncionamientoContractorRojo("EntradaPlugAnaranjado16", "EntradaPlugAnaranjado17", false);
        }
        else
        {
            FuncionamientoContractorRojo("EntradaPlugAnaranjado2", "EntradaPlugAnaranjado3", false);
            FuncionamientoContractorRojo("EntradaPlugAnaranjado4", "EntradaPlugAnaranjado5", false);
            FuncionamientoContractorRojo("EntradaPlugAnaranjado6", "EntradaPlugAnaranjado7", false);
            FuncionamientoContractorRojo("EntradaPlugAnaranjado8", "EntradaPlugAnaranjado9", false);
            FuncionamientoContractorRojo("EntradaPlugAnaranjado10", "EntradaPlugAnaranjado11", true);
            FuncionamientoContractorRojo("EntradaPlugAnaranjado12", "EntradaPlugAnaranjado13", true);
            FuncionamientoContractorRojo("EntradaPlugAnaranjado14", "EntradaPlugAnaranjado15", true);
            FuncionamientoContractorRojo("EntradaPlugAnaranjado16", "EntradaPlugAnaranjado17", true);
        }*/
    }

    void FuncionamientoContractorRojo(string nPlugConexionArribaCerrado, string nPlugConexionAbajoCerrado, bool conexionAbierta)
    {
        Plugs plugConexionArribaCerrado = plugAnaranjadosDict[nPlugConexionArribaCerrado].GetComponent<Plugs>();
        Plugs plugConexionAbajoCerrado = plugAnaranjadosDict[nPlugConexionAbajoCerrado].GetComponent<Plugs>();
        Time.timeScale = 0.0F;
        //reiniciarTodosPlugsAnaranjados();
        //plugConexionArribaCerrado.EstablecerValoresNoConexion3(plugConexionAbajoCerrado);
        //plugConexionAbajoCerrado.EstablecerValoresNoConexion3(plugConexionArribaCerrado);
        /*if (!plugConexionArribaCerrado.estaConectado)
        {
            plugConexionArribaCerrado.EstablecerValoresNoConexion3(plugConexionAbajoCerrado);
        }
        if (!plugConexionAbajoCerrado.estaConectado)
        {
            plugConexionAbajoCerrado.EstablecerValoresNoConexion3(plugConexionArribaCerrado);
        }*/
        //plugConexionArribaCerrado.EstablecerValoresNoConexion2();
        //plugConexionAbajoCerrado.EstablecerValoresNoConexion2();
        bool cortoElectrico = plugConexionArribaCerrado.ComprobarEstado(plugConexionArribaCerrado, plugConexionAbajoCerrado, conexionAbierta);
        Time.timeScale = 1.0F;
        /*if (!conexionAbierta && !cortoElectrico) // Para comprobar cortos
        {*/
            plugConexionArribaCerrado.EstablecerPropiedadesConexionesEntrantes();
            plugConexionAbajoCerrado.EstablecerPropiedadesConexionesEntrantes();
            
            if (!conexionAbierta && plugConexionArribaCerrado.Conectado && plugConexionAbajoCerrado.Voltaje == 0 && plugConexionAbajoCerrado.TipoConexion == 0)// 
            {
                plugConexionAbajoCerrado.EstablecerPropiedadesConexionesEntrantes(plugAnaranjadosDict[nPlugConexionArribaCerrado]);
            }
            else
            if (!conexionAbierta && plugConexionAbajoCerrado.Conectado)
            {
                plugConexionArribaCerrado.EstablecerPropiedadesConexionesEntrantes(plugAnaranjadosDict[nPlugConexionAbajoCerrado]);
            }
            else
            {
                //Debug.Log("Modulo 13: No entra a ningun caso");
            }
       // }
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
