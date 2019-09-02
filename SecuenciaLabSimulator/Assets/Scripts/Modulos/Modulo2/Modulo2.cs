﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Modulo2 : MonoBehaviour
{
    #region Atributos
    public bool moduloEncendido = true;
    public Dictionary<string, string> plugsConnections;
    [SerializeField] public List<GameObject> plugAnaranjados;
    [SerializeField] public List<GameObject> plugNegros;
    public Dictionary<string, GameObject> plugAnaranjadosDict;
    public Dictionary<string, GameObject> plugNegrosDict;
    [SerializeField] public GameObject botonCuadradoVerdeIzquierdo;
    [SerializeField] public GameObject botonCuadradoRojoIzquierdo;
    [SerializeField] public GameObject botonCuadradoVerdeDerecho;
    [SerializeField] public GameObject botonCuadradoRojoDerecho;
    private string rutaAnimacionBotonCuadradoVerde = "Assets/Animation/Modulos/Modulo2/Mod2PresBotonCuadradoVerde.anim";
    private string rutaAnimacionBotonCuadradoRojo = "Assets/Animation/Modulos/Modulo2/Mod2PresBotonCuadradoRojo.anim";
    private string nombreAnimacionBotonCuadradoVerde = "Mod2PresBotonCuadradoVerde";
    private string nombreAnimacionBotonCuadradoRojo = "Mod2PresBotonCuadradoRojo";

    //Variables de debug
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

        plugAnaranjados = new List<GameObject>();
        plugNegros = new List<GameObject>();
        InicializarComponentes(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        botonCuadradoVerdeIzquierdo.GetComponent<Mod2PushButton>().botonContrario = botonCuadradoRojoIzquierdo;
        botonCuadradoVerdeIzquierdo.GetComponent<Mod2PushButton>().EstablecerTipoVerde();
        botonCuadradoVerdeIzquierdo.GetComponent<Mod2PushButton>().EstablecerBotonDespresionado();
        botonCuadradoRojoIzquierdo.GetComponent<Mod2PushButton>().botonContrario = botonCuadradoVerdeIzquierdo;
        botonCuadradoRojoIzquierdo.GetComponent<Mod2PushButton>().EstablecerTipoRojo();
        botonCuadradoRojoIzquierdo.GetComponent<Mod2PushButton>().EstablecerBotonPresionado();

        botonCuadradoVerdeDerecho.GetComponent<Mod2PushButton>().botonContrario = botonCuadradoRojoDerecho;
        botonCuadradoVerdeDerecho.GetComponent<Mod2PushButton>().EstablecerTipoVerde();
        botonCuadradoVerdeDerecho.GetComponent<Mod2PushButton>().EstablecerBotonDespresionado();
        botonCuadradoRojoDerecho.GetComponent<Mod2PushButton>().botonContrario = botonCuadradoVerdeDerecho;
        botonCuadradoRojoDerecho.GetComponent<Mod2PushButton>().EstablecerTipoRojo();
        botonCuadradoRojoDerecho.GetComponent<Mod2PushButton>().EstablecerBotonPresionado();
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
            else if (child.name.Contains("BotonCuadradoVerdeIzquierdo"))
            {
                botonCuadradoVerdeIzquierdo = child;
                Animation ani = child.AddComponent<Animation>();
                ani.playAutomatically = false;
                ani.AddClip(((AnimationClip)AssetDatabase.LoadAssetAtPath(rutaAnimacionBotonCuadradoVerde, typeof(AnimationClip))), nombreAnimacionBotonCuadradoVerde);
                child.AddComponent<Mod2PushButton>();
            }
            else if (child.name.Contains("BotonCuadradoRojoIzquierdo"))
            {
                botonCuadradoRojoIzquierdo = child;
                Animation ani = child.AddComponent<Animation>();
                ani.playAutomatically = false;
                ani.AddClip(((AnimationClip)AssetDatabase.LoadAssetAtPath(rutaAnimacionBotonCuadradoRojo, typeof(AnimationClip))), nombreAnimacionBotonCuadradoRojo);
                child.AddComponent<Mod2PushButton>();
            }
            else if (child.name.Contains("BotonCuadradoVerdeDerecho"))
            {
                botonCuadradoVerdeDerecho = child;
                Animation ani = child.AddComponent<Animation>();
                ani.playAutomatically = false;
                ani.AddClip(((AnimationClip)AssetDatabase.LoadAssetAtPath(rutaAnimacionBotonCuadradoVerde, typeof(AnimationClip))), nombreAnimacionBotonCuadradoVerde);
                child.AddComponent<Mod2PushButton>();
            }
            else if (child.name.Contains("BotonCuadradoRojoDerecho"))
            {
                botonCuadradoRojoDerecho = child;
                Animation ani = child.AddComponent<Animation>();
                ani.playAutomatically = false;
                ani.AddClip(((AnimationClip)AssetDatabase.LoadAssetAtPath(rutaAnimacionBotonCuadradoRojo, typeof(AnimationClip))), nombreAnimacionBotonCuadradoRojo);
                child.AddComponent<Mod2PushButton>();
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
            BotonesNormalmenteCerradosYAbiertos("EntradaPlugAnaranjado1", "EntradaPlugAnaranjado2", "EntradaPlugAnaranjado3");
            //Circuito Derecho
            BotonesNormalmenteCerradosYAbiertos("EntradaPlugAnaranjado4", "EntradaPlugAnaranjado5", "EntradaPlugAnaranjado6");
        }
        else
        {
            //Hacer algo si el modulo esta apagado.
        }
    }

    void BotonesNormalmenteCerradosYAbiertos(string nPlugPrincipal, string nPlugAbierto, string nPlugCerrado)
    {
        Plugs plugConexionIzquierdo = plugAnaranjadosDict[nPlugPrincipal].GetComponent<Plugs>();
        Plugs plugConexionIzquierdoAbierto = plugAnaranjadosDict[nPlugAbierto].GetComponent<Plugs>();
        Plugs plugConexionIzquierdoCerrado = plugAnaranjadosDict[nPlugCerrado].GetComponent<Plugs>();
        Time.timeScale = 0.0F;
        if (botonCuadradoRojoIzquierdo.GetComponent<Mod2PushButton>().EstaActivado())
        {
            plugConexionIzquierdoAbierto.EstablecerValoresNoConexion2();
            plugConexionIzquierdo.EstablecerValoresNoConexion2();
        }
        else
        if (botonCuadradoVerdeIzquierdo.GetComponent<Mod2PushButton>().EstaActivado())
        {
            plugConexionIzquierdoCerrado.EstablecerValoresNoConexion2();
            plugConexionIzquierdo.EstablecerValoresNoConexion2();
        }
        Time.timeScale = 1.0F;
        plugConexionIzquierdo.EstablecerPropiedadesConexionesEntrantes();
        plugConexionIzquierdoAbierto.EstablecerPropiedadesConexionesEntrantes();
        plugConexionIzquierdoCerrado.EstablecerPropiedadesConexionesEntrantes();
        if (botonCuadradoVerdeIzquierdo.GetComponent<Mod2PushButton>().EstaActivado() && plugConexionIzquierdoAbierto.Conectado && plugConexionIzquierdo.Voltaje == 0 && plugConexionIzquierdo.TipoConexion == 0)
        {
            plugConexionIzquierdo.EstablecerPropiedadesConexionesEntrantes(plugAnaranjadosDict[nPlugAbierto]);
        }
        else
        if (botonCuadradoVerdeIzquierdo.GetComponent<Mod2PushButton>().EstaActivado() && plugConexionIzquierdo.Conectado)
        {
            plugConexionIzquierdoAbierto.EstablecerPropiedadesConexionesEntrantes(plugAnaranjadosDict[nPlugPrincipal]);
        }
        else if (botonCuadradoRojoIzquierdo.GetComponent<Mod2PushButton>().EstaActivado() && plugConexionIzquierdoCerrado.Conectado && plugConexionIzquierdo.Voltaje == 0 && plugConexionIzquierdo.TipoConexion == 0)
        {
            plugConexionIzquierdo.EstablecerPropiedadesConexionesEntrantes(plugAnaranjadosDict[nPlugCerrado]);
        }
        else
       if (botonCuadradoRojoIzquierdo.GetComponent<Mod2PushButton>().EstaActivado() && plugConexionIzquierdo.Conectado)
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
