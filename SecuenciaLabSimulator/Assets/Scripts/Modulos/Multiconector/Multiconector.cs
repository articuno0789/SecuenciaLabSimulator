using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiconector : MonoBehaviour
{
    #region Atributos
    [Header("Encendido")]
    public bool moduloEncendido = true;
    [Header("Conexiones")]
    public Dictionary<string, string> plugsConnections;
    [Header("Diccionarios de elementos")]
    public Dictionary<string, GameObject> plugAnaranjadosDict;
    [Header("Listas de elementos")]
    public List<GameObject> plugAnaranjados;
    [Header("Parametros Plugs")]
    private string nombreTagPlugAnaranjado = "PlugAnaranjado";
    //Variables de debug
    [Header("Debug")]
    public bool mostrarDiccionarioConexiones = false; // Variable
    public bool mostrarPlugAnaranjados = false; // Variable
    #endregion

    #region Inicializacion
    private void Awake()
    {
        plugsConnections = new Dictionary<string, string>();
        plugAnaranjadosDict = new Dictionary<string, GameObject>();

        plugAnaranjados = new List<GameObject>();
        InicializarComponentes(gameObject);
        //Inicializar contractores
        IncializacionMulticonector("EntradaPlugAnaranjado1", "EntradaPlugAnaranjado2", "EntradaPlugAnaranjado3",
                                   "EntradaPlugAnaranjado4", "EntradaPlugAnaranjado5", "EntradaPlugAnaranjado6",
                                   "EntradaPlugAnaranjado7", "EntradaPlugAnaranjado8", "EntradaPlugAnaranjado9");
        IncializacionMulticonector("EntradaPlugAnaranjado10", "EntradaPlugAnaranjado11", "EntradaPlugAnaranjado12",
                                   "EntradaPlugAnaranjado13", "EntradaPlugAnaranjado14", "EntradaPlugAnaranjado15",
                                   "EntradaPlugAnaranjado16", "EntradaPlugAnaranjado17", "EntradaPlugAnaranjado18");
    }

    void IncializacionMulticonector(string nplugSuperiorIzquierdo, string nplugSuperiorCentro, string nplugSuperiorDerecho,
        string nplugIzquierdo, string nplugCentro, string nplugDerecho, string nplugInferiorIzquierdo, string nplugInferiorCentro,
        string nplugInferiorDerecho)
    {
        plugAnaranjadosDict[nplugCentro].GetComponent<Plugs>().EstablecerPlugRelacionado(plugAnaranjadosDict[nplugSuperiorIzquierdo], true);

        plugAnaranjadosDict[nplugSuperiorIzquierdo].GetComponent<Plugs>().EstablecerPlugRelacionado(plugAnaranjadosDict[nplugCentro], true);
        plugAnaranjadosDict[nplugSuperiorCentro].GetComponent<Plugs>().EstablecerPlugRelacionado(plugAnaranjadosDict[nplugCentro], true);
        plugAnaranjadosDict[nplugSuperiorDerecho].GetComponent<Plugs>().EstablecerPlugRelacionado(plugAnaranjadosDict[nplugCentro], true);
        plugAnaranjadosDict[nplugIzquierdo].GetComponent<Plugs>().EstablecerPlugRelacionado(plugAnaranjadosDict[nplugCentro], true);
        plugAnaranjadosDict[nplugDerecho].GetComponent<Plugs>().EstablecerPlugRelacionado(plugAnaranjadosDict[nplugCentro], true);
        plugAnaranjadosDict[nplugInferiorIzquierdo].GetComponent<Plugs>().EstablecerPlugRelacionado(plugAnaranjadosDict[nplugCentro], true);
        plugAnaranjadosDict[nplugInferiorCentro].GetComponent<Plugs>().EstablecerPlugRelacionado(plugAnaranjadosDict[nplugCentro], true);
        plugAnaranjadosDict[nplugInferiorDerecho].GetComponent<Plugs>().EstablecerPlugRelacionado(plugAnaranjadosDict[nplugCentro], true);
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
            ActivarMulticonector("EntradaPlugAnaranjado1", "EntradaPlugAnaranjado2", "EntradaPlugAnaranjado3",
                                 "EntradaPlugAnaranjado4", "EntradaPlugAnaranjado5", "EntradaPlugAnaranjado6",
                                 "EntradaPlugAnaranjado7", "EntradaPlugAnaranjado8", "EntradaPlugAnaranjado9");
            ActivarMulticonector("EntradaPlugAnaranjado10", "EntradaPlugAnaranjado11", "EntradaPlugAnaranjado12",
                                 "EntradaPlugAnaranjado13", "EntradaPlugAnaranjado14", "EntradaPlugAnaranjado15",
                                 "EntradaPlugAnaranjado16", "EntradaPlugAnaranjado17", "EntradaPlugAnaranjado18");
        }
        else
        {
            //Hacer algo si el modulo esta apagado.
        }
    }

    void ActivarRelacionPlugComun(string nplugCentro, string nPlugObjetivo)
    {
        plugAnaranjadosDict[nplugCentro].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict[nPlugObjetivo];
        plugAnaranjadosDict[nplugCentro].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
        
    }

    void ActivarMulticonector(string nplugSuperiorIzquierdo, string nplugSuperiorCentro, string nplugSuperiorDerecho,
        string nplugIzquierdo, string nplugCentro, string nplugDerecho, string nplugInferiorIzquierdo, string nplugInferiorCentro,
        string nplugInferiorDerecho)
    {
        ActivarRelacionPlugComun(nplugCentro, nplugSuperiorIzquierdo);
        plugAnaranjadosDict[nplugSuperiorIzquierdo].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
        ActivarRelacionPlugComun(nplugCentro, nplugSuperiorCentro);
        plugAnaranjadosDict[nplugSuperiorCentro].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
        ActivarRelacionPlugComun(nplugCentro, nplugSuperiorDerecho);
        plugAnaranjadosDict[nplugSuperiorDerecho].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
        ActivarRelacionPlugComun(nplugCentro, nplugIzquierdo);
        plugAnaranjadosDict[nplugIzquierdo].GetComponent<Plugs>().EstablecerRelacionCerrado(true);

        //plugAnaranjadosDict[nplugCentro].GetComponent<Plugs>().EstablecerRelacionCerrado(true);

        ActivarRelacionPlugComun(nplugCentro, nplugDerecho);
        plugAnaranjadosDict[nplugDerecho].GetComponent<Plugs>().EstablecerRelacionCerrado(true);

        ActivarRelacionPlugComun(nplugCentro, nplugInferiorIzquierdo);
        plugAnaranjadosDict[nplugInferiorIzquierdo].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
        ActivarRelacionPlugComun(nplugCentro, nplugInferiorCentro);
        plugAnaranjadosDict[nplugInferiorCentro].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
        ActivarRelacionPlugComun(nplugCentro, nplugInferiorDerecho);
        plugAnaranjadosDict[nplugInferiorDerecho].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
        /*plugAnaranjadosDict[nplugCentro].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
        plugAnaranjadosDict[nplugCentro].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict[nplugSuperiorIzquierdo];*/

        /*plugAnaranjadosDict[nplugSuperiorIzquierdo].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
        plugAnaranjadosDict[nplugSuperiorCentro].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
        plugAnaranjadosDict[nplugSuperiorDerecho].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
        plugAnaranjadosDict[nplugIzquierdo].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
        
        plugAnaranjadosDict[nplugDerecho].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
        plugAnaranjadosDict[nplugInferiorIzquierdo].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
        plugAnaranjadosDict[nplugInferiorCentro].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
        plugAnaranjadosDict[nplugInferiorDerecho].GetComponent<Plugs>().EstablecerRelacionCerrado(true);*/

        /*plugAnaranjadosDict[nPlugNormalmenteAbierto].GetComponent<Plugs>().EstablecerRelacionCerrado(false);
        plugAnaranjadosDict[nPlugNormalmenteAbierto].GetComponent<Plugs>().plugRelacionado = null;
        plugAnaranjadosDict[nPlugNormalmenteAbierto].GetComponent<Plugs>().EliminarPropiedadesConexionesEntradaPrueba();
        plugAnaranjadosDict[nplugNormalmenteCerrado].GetComponent<Plugs>().EstablecerRelacionCerrado(true);
        plugAnaranjadosDict[nplugNormalmenteCerrado].GetComponent<Plugs>().plugRelacionado = plugAnaranjadosDict[nPlugComun];*/
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
