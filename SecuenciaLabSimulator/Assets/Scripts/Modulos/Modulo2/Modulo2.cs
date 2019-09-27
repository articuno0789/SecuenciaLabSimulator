using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Modulo2 : MonoBehaviour
{
    #region Atributos
    [Header("Encendido")]
    public bool moduloEncendido = true;
    [Header("Conexiones")]
    public Dictionary<string, string> plugsConnections;
    [Header("Diccionarios de elementos")]
    public Dictionary<string, GameObject> plugAnaranjadosDict;
    public Dictionary<string, GameObject> plugNegrosDict;
    [Header("Listas de elementos")]
    public List<GameObject> plugAnaranjados;
    [SerializeField] public List<GameObject> plugNegros;
    [Header("Botones")]
    [SerializeField] public GameObject botonCuadradoVerdeIzquierdo;
    [SerializeField] public GameObject botonCuadradoRojoIzquierdo;
    [SerializeField] public GameObject botonCuadradoVerdeDerecho;
    [SerializeField] public GameObject botonCuadradoRojoDerecho;
    [Header("Animaciones")]
    private string rutaAnimacionBotonCuadradoVerde = "Assets/Animation/Modulos/Modulo2/Mod2PresBotonCuadradoVerde.anim";
    private string rutaAnimacionBotonCuadradoRojo = "Assets/Animation/Modulos/Modulo2/Mod2PresBotonCuadradoRojo.anim";
    private string nombreAnimacionBotonCuadradoVerde = "Mod2PresBotonCuadradoVerde";
    private string nombreAnimacionBotonCuadradoRojo = "Mod2PresBotonCuadradoRojo";
    [Header("Parametros Plugs")]
    private string nombreTagPlugAnaranjado = "PlugAnaranjado";
    private string nombreTagPlugNegro = "PlugNegro";
    [Header("Parametros Botones")]
    private string nombreTagPlugBotonVerdeCuadrado = "BotonVerdeCuadrado";
    private string nombreTagPlugBotonRojoCuadrado = "BotonRojoCuadrado";
    //Variables de debug
    [Header("Debug")]
    public bool mostrarDiccionarioConexiones = false; // Variable
    public bool mostrarPlugAnaranjados = false; // Variable
    public bool mostrarPlugNegros = false; // Variable
    #endregion

    #region Inicializacion
    private void Awake()
    {
        //Inicialización de listas y diccionarios de elementos.
        plugsConnections = new Dictionary<string, string>();
        plugAnaranjadosDict = new Dictionary<string, GameObject>();
        plugNegrosDict = new Dictionary<string, GameObject>();

        plugAnaranjados = new List<GameObject>();
        plugNegros = new List<GameObject>();
        InicializarComponentes(gameObject);
        //Inicializar contractores
        IncializacionContractores3("EntradaPlugAnaranjado1", "EntradaPlugAnaranjado2", "EntradaPlugAnaranjado3");
        IncializacionContractores3("EntradaPlugAnaranjado4", "EntradaPlugAnaranjado5", "EntradaPlugAnaranjado6");
        /*plugAnaranjadosDict["EntradaPlugAnaranjado1"].GetComponent<Plugs>().EstablecerPlugRelacionado(plugAnaranjadosDict["EntradaPlugAnaranjado3"], true);
        plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().EstablecerPlugRelacionado(plugAnaranjadosDict["EntradaPlugAnaranjado1"], false);
        plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().EstablecerPlugRelacionado(plugAnaranjadosDict["EntradaPlugAnaranjado1"], true);*/
        /*plugAnaranjadosDict["EntradaPlugAnaranjado1"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado3"];
        plugAnaranjadosDict["EntradaPlugAnaranjado1"].GetComponent<Plugs>().relacionCerrada = true;
        plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado1"];
        plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().relacionCerrada = false;
        plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado1"];
        plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().relacionCerrada = true;*/
        /*
        plugAnaranjadosDict["EntradaPlugAnaranjado"].GetComponent<Plugs>().EstablecerPlugRelacionado(plugAnaranjadosDict["EntradaPlugAnaranjado3"], true);
        plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().EstablecerPlugRelacionado(plugAnaranjadosDict["EntradaPlugAnaranjado1"], false);
        plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().EstablecerPlugRelacionado(plugAnaranjadosDict["EntradaPlugAnaranjado1"], true);
        */
        /*plugAnaranjadosDict["EntradaPlugAnaranjado4"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado6"];
        plugAnaranjadosDict["EntradaPlugAnaranjado4"].GetComponent<Plugs>().relacionCerrada = true;
        plugAnaranjadosDict["EntradaPlugAnaranjado5"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado4"];
        plugAnaranjadosDict["EntradaPlugAnaranjado5"].GetComponent<Plugs>().relacionCerrada = false;
        plugAnaranjadosDict["EntradaPlugAnaranjado6"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado4"];
        plugAnaranjadosDict["EntradaPlugAnaranjado6"].GetComponent<Plugs>().relacionCerrada = true;*/
    }

    void IncializacionContractores3(string nPlugComun, string nPlugNormalmenteAbierto, string nplugNormalmenteCerrado)
    {
        Plugs plugComun = plugAnaranjadosDict[nPlugComun].GetComponent<Plugs>();
        Plugs plugNormalmenteAbierto = plugAnaranjadosDict[nPlugNormalmenteAbierto].GetComponent<Plugs>();
        Plugs plugNormalmenteCerrado = plugAnaranjadosDict[nplugNormalmenteCerrado].GetComponent<Plugs>();
        if (plugComun != null && plugComun!=null && plugNormalmenteCerrado != null)
        {
            plugComun.EstablecerPlugRelacionado(plugAnaranjadosDict[nplugNormalmenteCerrado], true);
            plugNormalmenteAbierto.EstablecerPlugRelacionado(plugAnaranjadosDict[nPlugComun], false);
            plugNormalmenteCerrado.EstablecerPlugRelacionado(plugAnaranjadosDict[nPlugComun], true);
        }
        else
        {
            Debug.LogError(this.name + ", Error. IncializacionContractores3(string nPlugComun, string nPlugNormalmenteAbierto, string nplugNormalmenteCerrado) - Algunos de los plugs es nulo.");
        }
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
            else if (child.name.Contains("BotonCuadradoVerdeIzquierdo"))
            {
                botonCuadradoVerdeIzquierdo = child;
                Animation ani = child.AddComponent<Animation>();
                ani.playAutomatically = false;
                ani.AddClip(((AnimationClip)AuxiliarModulos.RegresarObjetoAnimation(nombreAnimacionBotonCuadradoVerde)), nombreAnimacionBotonCuadradoVerde);
                //ani.AddClip(((AnimationClip)AssetDatabase.LoadAssetAtPath(rutaAnimacionBotonCuadradoVerde, typeof(AnimationClip))), nombreAnimacionBotonCuadradoVerde);
                child.AddComponent<Mod2PushButton>();
                child.tag = nombreTagPlugBotonVerdeCuadrado;
            }
            else if (child.name.Contains("BotonCuadradoRojoIzquierdo"))
            {
                botonCuadradoRojoIzquierdo = child;
                Animation ani = child.AddComponent<Animation>();
                ani.playAutomatically = false;
                ani.AddClip(((AnimationClip)AuxiliarModulos.RegresarObjetoAnimation(nombreAnimacionBotonCuadradoRojo)), nombreAnimacionBotonCuadradoRojo);
                //ani.AddClip(((AnimationClip)AssetDatabase.LoadAssetAtPath(rutaAnimacionBotonCuadradoRojo, typeof(AnimationClip))), nombreAnimacionBotonCuadradoRojo);
                child.AddComponent<Mod2PushButton>();
                child.tag = nombreTagPlugBotonRojoCuadrado;
            }
            else if (child.name.Contains("BotonCuadradoVerdeDerecho"))
            {
                botonCuadradoVerdeDerecho = child;
                Animation ani = child.AddComponent<Animation>();
                ani.playAutomatically = false;
                ani.AddClip(((AnimationClip)AuxiliarModulos.RegresarObjetoAnimation(nombreAnimacionBotonCuadradoVerde)), nombreAnimacionBotonCuadradoVerde);
                //ani.AddClip(((AnimationClip)AssetDatabase.LoadAssetAtPath(rutaAnimacionBotonCuadradoVerde, typeof(AnimationClip))), nombreAnimacionBotonCuadradoVerde);
                child.AddComponent<Mod2PushButton>();
                child.tag = nombreTagPlugBotonVerdeCuadrado;
            }
            else if (child.name.Contains("BotonCuadradoRojoDerecho"))
            {
                botonCuadradoRojoDerecho = child;
                Animation ani = child.AddComponent<Animation>();
                ani.playAutomatically = false;
                ani.AddClip(((AnimationClip)AuxiliarModulos.RegresarObjetoAnimation(nombreAnimacionBotonCuadradoRojo)), nombreAnimacionBotonCuadradoRojo);
                //ani.AddClip(((AnimationClip)AssetDatabase.LoadAssetAtPath(rutaAnimacionBotonCuadradoRojo, typeof(AnimationClip))), nombreAnimacionBotonCuadradoRojo);
                child.AddComponent<Mod2PushButton>();
                child.tag = nombreTagPlugBotonRojoCuadrado;
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
            //Lado izquierdo
            Mod2PushButton btnCuadraRojoIzq = botonCuadradoRojoIzquierdo.GetComponent<Mod2PushButton>();
            if (btnCuadraRojoIzq != null && btnCuadraRojoIzq.EstaActivado())
            {
                 ActivarContractorNormalmenteCerrado("EntradaPlugAnaranjado1", "EntradaPlugAnaranjado2", "EntradaPlugAnaranjado3");
                 /*plugAnaranjadosDict["EntradaPlugAnaranjado1"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
                 plugAnaranjadosDict["EntradaPlugAnaranjado1"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado3"];
                 plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
                 plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().plugRelacionado = null;
                 plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().EliminarPropiedadesConexionesEntradaPrueba();
                 plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
                 plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado1"];*/
            }
            else
            {
                ActivarContractorNormalmenteAbierto("EntradaPlugAnaranjado1", "EntradaPlugAnaranjado2", "EntradaPlugAnaranjado3");
                /*plugAnaranjadosDict["EntradaPlugAnaranjado1"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
                plugAnaranjadosDict["EntradaPlugAnaranjado1"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado2"];
                plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
                plugAnaranjadosDict["EntradaPlugAnaranjado2"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado1"];
                plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
                plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().plugRelacionado = null;
                plugAnaranjadosDict["EntradaPlugAnaranjado3"].GetComponent<Plugs>().EliminarPropiedadesConexionesEntradaPrueba();*/
            }

            //Lado Lado derecho
            Mod2PushButton btnCuadraRojoDer = botonCuadradoRojoDerecho.GetComponent<Mod2PushButton>();
            if (btnCuadraRojoDer != null && btnCuadraRojoDer.EstaActivado())
            {
                ActivarContractorNormalmenteCerrado("EntradaPlugAnaranjado4", "EntradaPlugAnaranjado5", "EntradaPlugAnaranjado6");
                /*plugAnaranjadosDict["EntradaPlugAnaranjado4"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
                plugAnaranjadosDict["EntradaPlugAnaranjado4"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado6"];
                plugAnaranjadosDict["EntradaPlugAnaranjado5"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
                plugAnaranjadosDict["EntradaPlugAnaranjado5"].GetComponent<Plugs>().plugRelacionado = null;
                plugAnaranjadosDict["EntradaPlugAnaranjado5"].GetComponent<Plugs>().EliminarPropiedadesConexionesEntradaPrueba();
                plugAnaranjadosDict["EntradaPlugAnaranjado6"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
                plugAnaranjadosDict["EntradaPlugAnaranjado6"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado4"];*/
            }
            else
            {
                ActivarContractorNormalmenteAbierto("EntradaPlugAnaranjado4", "EntradaPlugAnaranjado5", "EntradaPlugAnaranjado6");
                /*plugAnaranjadosDict["EntradaPlugAnaranjado4"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
                plugAnaranjadosDict["EntradaPlugAnaranjado4"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado5"];
                plugAnaranjadosDict["EntradaPlugAnaranjado5"].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
                plugAnaranjadosDict["EntradaPlugAnaranjado5"].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict["EntradaPlugAnaranjado4"];
                plugAnaranjadosDict["EntradaPlugAnaranjado6"].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
                plugAnaranjadosDict["EntradaPlugAnaranjado6"].GetComponent<Plugs>().plugRelacionado = null;
                plugAnaranjadosDict["EntradaPlugAnaranjado6"].GetComponent<Plugs>().EliminarPropiedadesConexionesEntradaPrueba();*/
            }

            //Viejo
            //Circuito izquierdo
            //BotonesNormalmenteCerradosYAbiertos("EntradaPlugAnaranjado1", "EntradaPlugAnaranjado2", "EntradaPlugAnaranjado3", !botonCuadradoRojoIzquierdo.GetComponent<Mod2PushButton>().EstaActivado());

            //Circuito Derecho
            //BotonesNormalmenteCerradosYAbiertos("EntradaPlugAnaranjado4", "EntradaPlugAnaranjado5", "EntradaPlugAnaranjado6", !botonCuadradoRojoDerecho.GetComponent<Mod2PushButton>().EstaActivado());

            //Circuito izquierdo
            //BotonesNormalmenteCerradosYAbiertosIzquierdo("EntradaPlugAnaranjado1", "EntradaPlugAnaranjado2", "EntradaPlugAnaranjado3");

            //Circuito Derecho
            //BotonesNormalmenteCerradosYAbiertosDerecho("EntradaPlugAnaranjado4", "EntradaPlugAnaranjado5", "EntradaPlugAnaranjado6");
        }
        else
        {
            //Hacer algo si el modulo esta apagado.
        }
    }

    void ActivarContractorNormalmenteCerrado(string nPlugComun, string nPlugNormalmenteAbierto, string nplugNormalmenteCerrado)
    {
        Plugs plugComun = plugAnaranjadosDict[nPlugComun].GetComponent<Plugs>();
        Plugs plugNormalmenteAbierto = plugAnaranjadosDict[nPlugNormalmenteAbierto].GetComponent<Plugs>();
        Plugs plugNormalmenteCerrado = plugAnaranjadosDict[nplugNormalmenteCerrado].GetComponent<Plugs>();
        if (plugComun != null && plugComun != null && plugNormalmenteCerrado != null)
        {
            plugComun.EstablecerRelacionCerrado(true);
            plugComun.plugRelacionado = plugAnaranjadosDict[nplugNormalmenteCerrado];
            plugNormalmenteAbierto.EstablecerRelacionCerrado(false);
            plugNormalmenteAbierto.plugRelacionado = null;
            plugNormalmenteAbierto.EliminarPropiedadesConexionesEntradaPrueba();
            plugNormalmenteCerrado.EstablecerRelacionCerrado(true);
            plugNormalmenteCerrado.plugRelacionado = plugAnaranjadosDict[nPlugComun];
        }
        else
        {
            Debug.LogError(this.name + ", Error. ActivarContractorNormalmenteCerrado(string nPlugComun, string nPlugNormalmenteAbierto, string nplugNormalmenteCerrado) - Algunos de los plugs es nulo.");
        }
    }


    void ActivarContractorNormalmenteAbierto(string nPlugComun, string nPlugNormalmenteAbierto, string nplugNormalmenteCerrado)
    {
        Plugs plugComun = plugAnaranjadosDict[nPlugComun].GetComponent<Plugs>();
        Plugs plugNormalmenteAbierto = plugAnaranjadosDict[nPlugNormalmenteAbierto].GetComponent<Plugs>();
        Plugs plugNormalmenteCerrado = plugAnaranjadosDict[nplugNormalmenteCerrado].GetComponent<Plugs>();
        if (plugComun != null && plugComun != null && plugNormalmenteCerrado != null)
        {
            plugComun.EstablecerRelacionCerrado(true);
            plugComun.plugRelacionado = plugAnaranjadosDict[nPlugNormalmenteAbierto];
            plugNormalmenteAbierto.EstablecerRelacionCerrado(true);
            plugNormalmenteAbierto.plugRelacionado = plugAnaranjadosDict[nPlugComun];
            plugNormalmenteCerrado.EstablecerRelacionCerrado(false);
            plugNormalmenteCerrado.plugRelacionado = null;
            plugNormalmenteCerrado.EliminarPropiedadesConexionesEntradaPrueba();
        }
        else
        {
            Debug.LogError(this.name + ", Error. ActivarContractorNormalmenteAbierto(string nPlugComun, string nPlugNormalmenteAbierto, string nplugNormalmenteCerrado) - Algunos de los plugs es nulo.");
        }
    }

    //Implementacion anterior
    /*void BotonesNormalmenteCerradosYAbiertos(string nPlugPrincipal, string nPlugAbierto, string nPlugCerrado, bool botonLogicoActivo)
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
        plugConexionIzquierdo.EstablecerPropiedadesConexionesEntrantes();
        plugConexionIzquierdoAbierto.EstablecerPropiedadesConexionesEntrantes();
        plugConexionIzquierdoCerrado.EstablecerPropiedadesConexionesEntrantes();
        if (!cortoElectrico)
        {
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

    void BotonesNormalmenteCerradosYAbiertosIzquierdo(string nPlugPrincipal, string nPlugAbierto, string nPlugCerrado)
    {
        Plugs plugConexionIzquierdo = plugAnaranjadosDict[nPlugPrincipal].GetComponent<Plugs>();
        Plugs plugConexionIzquierdoAbierto = plugAnaranjadosDict[nPlugAbierto].GetComponent<Plugs>();
        Plugs plugConexionIzquierdoCerrado = plugAnaranjadosDict[nPlugCerrado].GetComponent<Plugs>();
        Time.timeScale = 0.0F;
        bool cortoElectrico = false;
        if (botonCuadradoRojoIzquierdo.GetComponent<Mod2PushButton>().EstaActivado())
        {
            plugConexionIzquierdoAbierto.EstablecerValoresNoConexion2();
            plugConexionIzquierdo.EstablecerValoresNoConexion2();
            cortoElectrico = plugConexionIzquierdo.ComprobarEstado(plugConexionIzquierdo, plugConexionIzquierdoCerrado, false);
        }
        else
        if (botonCuadradoVerdeIzquierdo.GetComponent<Mod2PushButton>().EstaActivado())
        {
            plugConexionIzquierdoCerrado.EstablecerValoresNoConexion2();
            plugConexionIzquierdo.EstablecerValoresNoConexion2();
            cortoElectrico = plugConexionIzquierdo.ComprobarEstado(plugConexionIzquierdo, plugConexionIzquierdoAbierto, false);
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

    void BotonesNormalmenteCerradosYAbiertosDerecho(string nPlugPrincipal, string nPlugAbierto, string nPlugCerrado)
    {
        Plugs plugConexionIzquierdo = plugAnaranjadosDict[nPlugPrincipal].GetComponent<Plugs>();
        Plugs plugConexionIzquierdoAbierto = plugAnaranjadosDict[nPlugAbierto].GetComponent<Plugs>();
        Plugs plugConexionIzquierdoCerrado = plugAnaranjadosDict[nPlugCerrado].GetComponent<Plugs>();
        Time.timeScale = 0.0F;
        bool cortoElectrico = false;
        if (botonCuadradoRojoDerecho.GetComponent<Mod2PushButton>().EstaActivado())
        {
            plugConexionIzquierdoAbierto.EstablecerValoresNoConexion2();
            plugConexionIzquierdo.EstablecerValoresNoConexion2();
            cortoElectrico = plugConexionIzquierdo.ComprobarEstado(plugConexionIzquierdo, plugConexionIzquierdoCerrado, false);
        }
        else
        if (botonCuadradoVerdeDerecho.GetComponent<Mod2PushButton>().EstaActivado())
        {
            plugConexionIzquierdoCerrado.EstablecerValoresNoConexion2();
            plugConexionIzquierdo.EstablecerValoresNoConexion2();
            cortoElectrico = plugConexionIzquierdo.ComprobarEstado(plugConexionIzquierdo, plugConexionIzquierdoAbierto, false);
        }
        Time.timeScale = 1.0F;
        plugConexionIzquierdo.EstablecerPropiedadesConexionesEntrantes();
        plugConexionIzquierdoAbierto.EstablecerPropiedadesConexionesEntrantes();
        plugConexionIzquierdoCerrado.EstablecerPropiedadesConexionesEntrantes();
        if (botonCuadradoVerdeDerecho.GetComponent<Mod2PushButton>().EstaActivado() && plugConexionIzquierdoAbierto.Conectado && plugConexionIzquierdo.Voltaje == 0 && plugConexionIzquierdo.TipoConexion == 0)
        {
            plugConexionIzquierdo.EstablecerPropiedadesConexionesEntrantes(plugAnaranjadosDict[nPlugAbierto]);
        }
        else
        if (botonCuadradoVerdeDerecho.GetComponent<Mod2PushButton>().EstaActivado() && plugConexionIzquierdo.Conectado)
        {
            plugConexionIzquierdoAbierto.EstablecerPropiedadesConexionesEntrantes(plugAnaranjadosDict[nPlugPrincipal]);
        }
        else if (botonCuadradoRojoDerecho.GetComponent<Mod2PushButton>().EstaActivado() && plugConexionIzquierdoCerrado.Conectado && plugConexionIzquierdo.Voltaje == 0 && plugConexionIzquierdo.TipoConexion == 0)
        {
            plugConexionIzquierdo.EstablecerPropiedadesConexionesEntrantes(plugAnaranjadosDict[nPlugCerrado]);
        }
        else
       if (botonCuadradoRojoDerecho.GetComponent<Mod2PushButton>().EstaActivado() && plugConexionIzquierdo.Conectado)
        {
            plugConexionIzquierdoCerrado.EstablecerPropiedadesConexionesEntrantes(plugAnaranjadosDict[nPlugPrincipal]);
        }
    }
    */
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
