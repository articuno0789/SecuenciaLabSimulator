using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Modulo13 : MonoBehaviour
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
    [SerializeField] public string rutaPlasticoRojoApagado = "Assets/Materials/PLasticos/PlasticoTraslucidoRojoApagado.mat";
    [SerializeField] public string rutaPlasticoRojoEncendido = "Assets/Materials/PLasticos/PlasticoTraslucidoRojoEncendido.mat";
    [SerializeField] public Material plasticoRojoApagado;
    [SerializeField] public Material plasticoRojoEncendido;
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
    private void Awake()
    {
        plasticoRojoApagado = (Material)AssetDatabase.LoadAssetAtPath(rutaPlasticoRojoApagado, typeof(Material));
        plasticoRojoEncendido = (Material)AssetDatabase.LoadAssetAtPath(rutaPlasticoRojoEncendido, typeof(Material));

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
                LuzRoja luzRoja = child.AddComponent<LuzRoja>();
                luzRoja.CurrentTypeParticleError = (int)ParticlesErrorTypes.SmokeEffect;
                luzRoja.CurrentTypeParticleError = (int)ParticlesErrorTypes.ElectricalSparksEffect;
                luzRoja.padreTotalComponente = this.gameObject;
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
        }
        
    }

    void FuncionamientoContractorRojo(string nPlugConexionArribaCerrado, string nPlugConexionAbajoCerrado, bool botonLogicoActivo)
    {
        Plugs plugConexionArribaCerrado = plugAnaranjadosDict[nPlugConexionArribaCerrado].GetComponent<Plugs>();
        Plugs plugConexionAbajoCerrado = plugAnaranjadosDict[nPlugConexionAbajoCerrado].GetComponent<Plugs>();
        Time.timeScale = 0.0F;
        if (botonLogicoActivo)
        {
            //plugConexionArribaCerrado.EstablecerValoresNoConexion2();
            //plugConexionAbajoCerrado.EstablecerValoresNoConexion2();
        }
        plugConexionArribaCerrado.EstablecerValoresNoConexion2();
        plugConexionAbajoCerrado.EstablecerValoresNoConexion2();
        Time.timeScale = 1.0F;
        plugConexionArribaCerrado.EstablecerPropiedadesConexionesEntrantes();
        plugConexionAbajoCerrado.EstablecerPropiedadesConexionesEntrantes();
        if (!botonLogicoActivo && plugConexionArribaCerrado.Conectado && plugConexionAbajoCerrado.Voltaje == 0 && plugConexionAbajoCerrado.TipoConexion == 0)
        {
            plugConexionAbajoCerrado.EstablecerPropiedadesConexionesEntrantes(plugAnaranjadosDict[nPlugConexionArribaCerrado]);
        }
        else
        if (!botonLogicoActivo && plugConexionAbajoCerrado.Conectado)
        {
            plugConexionArribaCerrado.EstablecerPropiedadesConexionesEntrantes(plugAnaranjadosDict[nPlugConexionAbajoCerrado]);
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
