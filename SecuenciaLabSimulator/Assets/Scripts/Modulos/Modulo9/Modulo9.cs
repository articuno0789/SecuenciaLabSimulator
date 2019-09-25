using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Modulo9 : MonoBehaviour
{
    #region Atributos
    [Header("Encendido")]
    public bool moduloEncendido = true;
    [Header("Conexiones")]
    public Dictionary<string, string> plugsConnections;
    [Header("Diccionarios de elementos")]
    public Dictionary<string, GameObject> plugAnaranjadosDict;
    public Dictionary<string, GameObject> plugNegrosDict;
    public Dictionary<string, GameObject> focosCircularesAzulesDict;
    [Header("Listas de elementos")]
    public List<GameObject> plugAnaranjados;
    public List<GameObject> plugNegros;
    public List<GameObject> focosCircularesAzules;
    [Header("Animaciones")]
    private readonly string rutaAnimacionBotonCircularAzul = "Assets/Animation/Modulos/Modulo9/Mod9PresBotonCircularAzul.anim";
    private readonly string nombreAnimacionBotonCircularAzul = "Mod9PresBotonCircularAzul";
    public string RutaAnimacionBotonCircularAzul => rutaAnimacionBotonCircularAzul;
    public string NombreAnimacionBotonCircularAzul => nombreAnimacionBotonCircularAzul;
    [Header("Parametros Plugs")]
    private string nombreTagPlugAnaranjado = "PlugAnaranjado";
    private string nombreTagPlugNegro = "PlugNegro";
    [Header("Parametros Focos")]
    private string nombreTagFocoAzul = "FocoAzul";
    //Variables de debug
    [Header("Debug")]
    public bool mostrarDiccionarioConexiones = false; // Variable
    public bool mostrarPlugAnaranjados = false; // Variable
    public bool mostrarPlugNegros = false; // Variable
    public bool mostrarFocosCircularesAzules = false; // Variable
    #endregion

    #region Inicializacion
    private void Awake()
    {
        plugsConnections = new Dictionary<string, string>();
        plugAnaranjadosDict = new Dictionary<string, GameObject>();
        plugNegrosDict = new Dictionary<string, GameObject>();
        focosCircularesAzulesDict = new Dictionary<string, GameObject>();

        plugAnaranjados = new List<GameObject>();
        plugNegros = new List<GameObject>();
        focosCircularesAzules = new List<GameObject>();
        InicializarComponentes(gameObject);
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
            //Debug.Log("child.name: " + child.name);
            if (child.name.Contains("EntradaPlugAnaranjado"))
            {
                plugAnaranjados.Add(child);
                child.AddComponent<CableComponent>();

                Plugs plug = child.AddComponent<Plugs>();
                plug.tipoNodo = 2;
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
                plug.tipoNodo = 2;
                plug.padreTotalComponente = this.gameObject;
                plugsConnections.Add(gameObject.name + "|" + child.name, "");

                plugNegrosDict.Add(child.name, child);
                child.tag = nombreTagPlugNegro;
            }
            else if (child.name.Contains("FocoCircularAzul"))
            {
                focosCircularesAzules.Add(child);
                FocoCircularAzul fococircularAzul = child.AddComponent<FocoCircularAzul>();
                fococircularAzul.CurrentTypeParticleError = (int)AuxiliarModulos.ParticlesErrorTypes.SmokeEffect;
                fococircularAzul.CurrentTypeParticleError = (int)AuxiliarModulos.ParticlesErrorTypes.ElectricalSparksEffect;
                fococircularAzul.padreTotalComponente = this.gameObject;
                focosCircularesAzulesDict.Add(child.name, child);


                //fococircularAzul.currentTypeParticleError = (int)ParticlesError.SmokeEffect;
                /*Animation ani = child.AddComponent<Animation>();
                ani.playAutomatically = false;
                ani.AddClip(((AnimationClip)AssetDatabase.LoadAssetAtPath(rutaAnimacionBotonCircularAzul, typeof(AnimationClip))), nombreAnimacionBotonCircularAzul);
                child.AddComponent<Mod9PushButton>();*/
                child.tag = nombreTagFocoAzul;
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
            ComportamientoModulo();
        }
        else
        {
            //Hacer algo si el modulo esta apagado.
            ApagarFocos();
        }
    }
    
    private void ComportamientoModulo()
    {
        ComportamientoFocoAzul("FocoCircularAzul1", "EntradaPlugAnaranjado1", "EntradaPlugNegro1");
        ComportamientoFocoAzul("FocoCircularAzul2", "EntradaPlugAnaranjado2", "EntradaPlugNegro2");
        ComportamientoFocoAzul("FocoCircularAzul3", "EntradaPlugAnaranjado3", "EntradaPlugNegro3");
        ComportamientoFocoAzul("FocoCircularAzul4", "EntradaPlugAnaranjado4", "EntradaPlugNegro4");
        ComportamientoFocoAzul("FocoCircularAzul5", "EntradaPlugAnaranjado5", "EntradaPlugNegro5");
        ComportamientoFocoAzul("FocoCircularAzul6", "EntradaPlugAnaranjado6", "EntradaPlugNegro6");
        /*focosCircularesAzulesDict["FocoCircularAzul1"].GetComponent<FocoCircularAzul>().ComprobarEstado(plugAnaranjadosDict["EntradaPlugAnaranjado1"], plugNegrosDict["EntradaPlugNegro1"]);
        focosCircularesAzulesDict["FocoCircularAzul2"].GetComponent<FocoCircularAzul>().ComprobarEstado(plugAnaranjadosDict["EntradaPlugAnaranjado2"], plugNegrosDict["EntradaPlugNegro2"]);
        focosCircularesAzulesDict["FocoCircularAzul3"].GetComponent<FocoCircularAzul>().ComprobarEstado(plugAnaranjadosDict["EntradaPlugAnaranjado3"], plugNegrosDict["EntradaPlugNegro3"]);
        focosCircularesAzulesDict["FocoCircularAzul4"].GetComponent<FocoCircularAzul>().ComprobarEstado(plugAnaranjadosDict["EntradaPlugAnaranjado4"], plugNegrosDict["EntradaPlugNegro4"]);
        focosCircularesAzulesDict["FocoCircularAzul5"].GetComponent<FocoCircularAzul>().ComprobarEstado(plugAnaranjadosDict["EntradaPlugAnaranjado5"], plugNegrosDict["EntradaPlugNegro5"]);
        focosCircularesAzulesDict["FocoCircularAzul6"].GetComponent<FocoCircularAzul>().ComprobarEstado(plugAnaranjadosDict["EntradaPlugAnaranjado6"], plugNegrosDict["EntradaPlugNegro6"]);*/
    }

    private void ComportamientoFocoAzul(string nFocoAzul, string nPlug1, string nPlug2)
    {
        GameObject focoAzul = null;
        if ((focoAzul = focosCircularesAzulesDict[nFocoAzul]) != null)
        {
            FocoCircularAzul focoAzulComp = focoAzul.GetComponent<FocoCircularAzul>();
            if (focoAzulComp != null)
            {
                focoAzulComp.ComprobarEstado(plugAnaranjadosDict[nPlug1], plugNegrosDict[nPlug2]);
            }
        }
    }

    private void ApagarFocos()
    {
        ApagarFoco("FocoCircularAzul1", "EntradaPlugAnaranjado1", "EntradaPlugNegro1");
        ApagarFoco("FocoCircularAzul2", "EntradaPlugAnaranjado2", "EntradaPlugNegro2");
        ApagarFoco("FocoCircularAzul3", "EntradaPlugAnaranjado3", "EntradaPlugNegro3");
        ApagarFoco("FocoCircularAzul4", "EntradaPlugAnaranjado4", "EntradaPlugNegro4");
        ApagarFoco("FocoCircularAzul5", "EntradaPlugAnaranjado5", "EntradaPlugNegro5");
        ApagarFoco("FocoCircularAzul6", "EntradaPlugAnaranjado6", "EntradaPlugNegro6");
    }

    private void ApagarFoco(string nFocoAzul, string nPlug1, string nPlug2)
    {
        GameObject focoAzul = null;
        if ((focoAzul = focosCircularesAzulesDict[nFocoAzul]) != null)
        {
            FocoCircularAzul focoAzulComp = focoAzul.GetComponent<FocoCircularAzul>();
            if (focoAzulComp != null)
            {
                focoAzulComp.ApagarFoco();
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
        if (mostrarFocosCircularesAzules)
        {
            ImprimirDiccionario(focosCircularesAzulesDict, 3);
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
            mostrarFocosCircularesAzules = false;
            nombreDiccionario = "focosCircularesAzulesDict";
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
