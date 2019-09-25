using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    [Header("Parametros Módulo")]
    public string primerPlugConectadoIzquierdo = "";
    public string primerPlugConectadoDerecho = "";
    [Header("Parametros Plugs Izquierdo")]
    public string izqPlugIzqSuperior = "";
    public string izqPlugCentSuperior = "";
    public string izqPlugDerSuperior = "";
    public string izqPlugIzq = "";
    public string izqPlugDer = "";
    public string izqPlugIzqInferior = "";
    public string izqPlugCentInferior = "";
    public string izqPlugDerInferior = "";
    [Header("Parametros Plugs Derecho")]
    public string derPlugIzqSuperior = "";
    public string derPlugCentSuperior = "";
    public string derPlugDerSuperior = "";
    public string derPlugIzq = "";
    public string derPlugDer = "";
    public string derPlugIzqInferior = "";
    public string derPlugCentInferior = "";
    public string derPlugDerInferior = "";
    [Header("Parametros Plugs")]
    private string nombreTagPlugAnaranjado = "PlugAnaranjado";
    [Header("Materiales")]
    private string RutaMaterialPlugAnaranjado = "Assets/Materials/EntradaPlug/AnaranjadoPlug.mat";
    private string RutaMaterialPlugNegro = "Assets/Materials/EntradaPlug/ObscuroPlug.mat";
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
                                   "EntradaPlugAnaranjado7", "EntradaPlugAnaranjado8", "EntradaPlugAnaranjado9", true);
        IncializacionMulticonector("EntradaPlugAnaranjado10", "EntradaPlugAnaranjado11", "EntradaPlugAnaranjado12",
                                   "EntradaPlugAnaranjado13", "EntradaPlugAnaranjado14", "EntradaPlugAnaranjado15",
                                   "EntradaPlugAnaranjado16", "EntradaPlugAnaranjado17", "EntradaPlugAnaranjado18", true);
    }

    void IncializacionMulticonector(string nplugSuperiorIzquierdo, string nplugSuperiorCentro, string nplugSuperiorDerecho,
        string nplugIzquierdo, string nplugCentro, string nplugDerecho, string nplugInferiorIzquierdo, string nplugInferiorCentro,
        string nplugInferiorDerecho, bool cerrado)
    {
        plugAnaranjadosDict[nplugCentro].GetComponent<Plugs>().EstablecerPlugRelacionado(plugAnaranjadosDict[nplugCentro], cerrado);
        //plugAnaranjadosDict[nplugCentro].GetComponent<Plugs>().EstablecerPlugRelacionado(plugAnaranjadosDict[nplugSuperiorIzquierdo], cerrado);

        plugAnaranjadosDict[nplugSuperiorIzquierdo].GetComponent<Plugs>().EstablecerPlugRelacionado(plugAnaranjadosDict[nplugCentro], cerrado);
        plugAnaranjadosDict[nplugSuperiorCentro].GetComponent<Plugs>().EstablecerPlugRelacionado(plugAnaranjadosDict[nplugCentro], cerrado);
        plugAnaranjadosDict[nplugSuperiorDerecho].GetComponent<Plugs>().EstablecerPlugRelacionado(plugAnaranjadosDict[nplugCentro], cerrado);
        plugAnaranjadosDict[nplugIzquierdo].GetComponent<Plugs>().EstablecerPlugRelacionado(plugAnaranjadosDict[nplugCentro], cerrado);
        plugAnaranjadosDict[nplugDerecho].GetComponent<Plugs>().EstablecerPlugRelacionado(plugAnaranjadosDict[nplugCentro], cerrado);
        plugAnaranjadosDict[nplugInferiorIzquierdo].GetComponent<Plugs>().EstablecerPlugRelacionado(plugAnaranjadosDict[nplugCentro], cerrado);
        plugAnaranjadosDict[nplugInferiorCentro].GetComponent<Plugs>().EstablecerPlugRelacionado(plugAnaranjadosDict[nplugCentro], cerrado);
        plugAnaranjadosDict[nplugInferiorDerecho].GetComponent<Plugs>().EstablecerPlugRelacionado(plugAnaranjadosDict[nplugCentro], cerrado);
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
            //Desde el cualquier nodo con cambio de color*****************************************************************************************************
            //Izquierdo
            if (NumeroPlugsConectados(true) > 0)
            {
                if(primerPlugConectadoIzquierdo.Length != 0)
                {
                    DestacarPlugSelect(primerPlugConectadoIzquierdo);
                }
                NombresDemasConectores(true, ref izqPlugIzqSuperior, ref izqPlugCentSuperior, ref izqPlugDerSuperior, ref izqPlugIzq,
                                      ref izqPlugDer, ref izqPlugIzqInferior, ref izqPlugCentInferior, ref izqPlugDerInferior);
                IncializacionMulticonector(izqPlugIzqSuperior, izqPlugCentSuperior, izqPlugDerSuperior,
                                 izqPlugIzq, primerPlugConectadoIzquierdo, izqPlugDer,
                                 izqPlugIzqInferior, izqPlugCentInferior, izqPlugDerInferior, true);
                EstablecerPropiedadesMultiples(izqPlugIzqSuperior, izqPlugCentSuperior, izqPlugDerSuperior,
                                 izqPlugIzq, primerPlugConectadoIzquierdo, izqPlugDer,
                                 izqPlugIzqInferior, izqPlugCentInferior, izqPlugDerInferior);
            }
            else
            {
                if (primerPlugConectadoIzquierdo.Length != 0)
                {
                    ReiniciarPlugs(izqPlugIzqSuperior, izqPlugCentSuperior, izqPlugDerSuperior,
                                 izqPlugIzq, primerPlugConectadoIzquierdo, izqPlugDer,
                                 izqPlugIzqInferior, izqPlugCentInferior, izqPlugDerInferior);
                    IncializacionMulticonector(izqPlugIzqSuperior, izqPlugCentSuperior, izqPlugDerSuperior,
                                 izqPlugIzq, primerPlugConectadoIzquierdo, izqPlugDer,
                                 izqPlugIzqInferior, izqPlugCentInferior, izqPlugDerInferior, true);
                    changeOriginalColorPlug(plugAnaranjadosDict[primerPlugConectadoIzquierdo]);
                }
                primerPlugConectadoIzquierdo = "";
            }
            //Derecho
            if (NumeroPlugsConectados(false) > 0)
            {
                if (primerPlugConectadoDerecho.Length != 0)
                {
                    DestacarPlugSelect(primerPlugConectadoDerecho);
                }
                NombresDemasConectores(false, ref derPlugIzqSuperior, ref derPlugCentSuperior, ref derPlugDerSuperior, ref derPlugIzq,
                                      ref derPlugDer, ref derPlugIzqInferior, ref derPlugCentInferior, ref derPlugDerInferior);
                IncializacionMulticonector(derPlugIzqSuperior, derPlugCentSuperior, derPlugDerSuperior,
                                 derPlugIzq, primerPlugConectadoDerecho, derPlugDer,
                                 derPlugIzqInferior, derPlugCentInferior, derPlugDerInferior, true);
                EstablecerPropiedadesMultiples(derPlugIzqSuperior, derPlugCentSuperior, derPlugDerSuperior,
                                 derPlugIzq, primerPlugConectadoDerecho, derPlugDer,
                                 derPlugIzqInferior, derPlugCentInferior, derPlugDerInferior);
            }
            else
            {
                if (primerPlugConectadoDerecho.Length != 0)
                {
                    ReiniciarPlugs(derPlugIzqSuperior, derPlugCentSuperior, derPlugDerSuperior,
                                 derPlugIzq, primerPlugConectadoDerecho, derPlugDer,
                                 derPlugIzqInferior, derPlugCentInferior, derPlugDerInferior);
                    IncializacionMulticonector(derPlugIzqSuperior, derPlugCentSuperior, derPlugDerSuperior,
                                 derPlugIzq, primerPlugConectadoDerecho, derPlugDer,
                                 derPlugIzqInferior, derPlugCentInferior, derPlugDerInferior, true);
                    changeOriginalColorPlug(plugAnaranjadosDict[primerPlugConectadoDerecho]);
                }
                primerPlugConectadoDerecho = "";
            }
            //*********************************************************************************************************************


            //Desde el centro con cambio de color*****************************************************************************************************
            //Izquierdo
            /*if (plugAnaranjadosDict["EntradaPlugAnaranjado5"].GetComponent<Plugs>().EstoConectado())
            {
                DestacarPlugSelect("EntradaPlugAnaranjado5");
            }
            else
            {
                    ReiniciarPlugs("EntradaPlugAnaranjado1", "EntradaPlugAnaranjado2", "EntradaPlugAnaranjado3",
                                 "EntradaPlugAnaranjado4", "EntradaPlugAnaranjado5", "EntradaPlugAnaranjado6",
                                 "EntradaPlugAnaranjado7", "EntradaPlugAnaranjado8", "EntradaPlugAnaranjado9");
                    IncializacionMulticonector("EntradaPlugAnaranjado1", "EntradaPlugAnaranjado2", "EntradaPlugAnaranjado3",
                                 "EntradaPlugAnaranjado4", "EntradaPlugAnaranjado5", "EntradaPlugAnaranjado6",
                                 "EntradaPlugAnaranjado7", "EntradaPlugAnaranjado8", "EntradaPlugAnaranjado9", true);
                    changeOriginalColorPlug(plugAnaranjadosDict["EntradaPlugAnaranjado5"]);
            }
            //Derecho
            if (plugAnaranjadosDict["EntradaPlugAnaranjado14"].GetComponent<Plugs>().EstoConectado())
            {
                DestacarPlugSelect("EntradaPlugAnaranjado14");
            }
            else
            {
                ReiniciarPlugs("EntradaPlugAnaranjado10", "EntradaPlugAnaranjado11", "EntradaPlugAnaranjado12",
                                 "EntradaPlugAnaranjado13", "EntradaPlugAnaranjado14", "EntradaPlugAnaranjado15",
                                 "EntradaPlugAnaranjado16", "EntradaPlugAnaranjado17", "EntradaPlugAnaranjado18");
                IncializacionMulticonector("EntradaPlugAnaranjado10", "EntradaPlugAnaranjado11", "EntradaPlugAnaranjado12",
                                 "EntradaPlugAnaranjado13", "EntradaPlugAnaranjado14", "EntradaPlugAnaranjado15",
                                 "EntradaPlugAnaranjado16", "EntradaPlugAnaranjado17", "EntradaPlugAnaranjado18", true);
                changeOriginalColorPlug(plugAnaranjadosDict["EntradaPlugAnaranjado14"]);
            }*/
            //*********************************************************************************************************************

            //Desde el centro sin cambio de color
            /*EstablecerPropiedadesMultiples("EntradaPlugAnaranjado1", "EntradaPlugAnaranjado2", "EntradaPlugAnaranjado3",
                                 "EntradaPlugAnaranjado4", "EntradaPlugAnaranjado5", "EntradaPlugAnaranjado6",
                                 "EntradaPlugAnaranjado7", "EntradaPlugAnaranjado8", "EntradaPlugAnaranjado9");
            EstablecerPropiedadesMultiples("EntradaPlugAnaranjado10", "EntradaPlugAnaranjado11", "EntradaPlugAnaranjado12",
                                 "EntradaPlugAnaranjado13", "EntradaPlugAnaranjado14", "EntradaPlugAnaranjado15",
                                 "EntradaPlugAnaranjado16", "EntradaPlugAnaranjado17", "EntradaPlugAnaranjado18");*/


            /*ActivarMulticonector("EntradaPlugAnaranjado1", "EntradaPlugAnaranjado2", "EntradaPlugAnaranjado3",
                                 "EntradaPlugAnaranjado4", "EntradaPlugAnaranjado5", "EntradaPlugAnaranjado6",
                                 "EntradaPlugAnaranjado7", "EntradaPlugAnaranjado8", "EntradaPlugAnaranjado9");
            ActivarMulticonector("EntradaPlugAnaranjado10", "EntradaPlugAnaranjado11", "EntradaPlugAnaranjado12",
                                 "EntradaPlugAnaranjado13", "EntradaPlugAnaranjado14", "EntradaPlugAnaranjado15",
                                 "EntradaPlugAnaranjado16", "EntradaPlugAnaranjado17", "EntradaPlugAnaranjado18");*/
        }
        else
        {
            //Hacer algo si el modulo esta apagado.
        }
    }

    void ReiniciarPlugs(string nplugSuperiorIzquierdo, string nplugSuperiorCentro, string nplugSuperiorDerecho,
        string nplugIzquierdo, string nplugCentro, string nplugDerecho, string nplugInferiorIzquierdo, string nplugInferiorCentro,
        string nplugInferiorDerecho)
    {
        plugAnaranjadosDict[nplugCentro].GetComponent<Plugs>().EstablecerValoresNoConexionVolLin();
        plugAnaranjadosDict[nplugSuperiorIzquierdo].GetComponent<Plugs>().EstablecerValoresNoConexionVolLin();
        plugAnaranjadosDict[nplugSuperiorCentro].GetComponent<Plugs>().EstablecerValoresNoConexionVolLin();
        plugAnaranjadosDict[nplugSuperiorDerecho].GetComponent<Plugs>().EstablecerValoresNoConexionVolLin();
        plugAnaranjadosDict[nplugIzquierdo].GetComponent<Plugs>().EstablecerValoresNoConexionVolLin();
        plugAnaranjadosDict[nplugDerecho].GetComponent<Plugs>().EstablecerValoresNoConexionVolLin();
        plugAnaranjadosDict[nplugInferiorIzquierdo].GetComponent<Plugs>().EstablecerValoresNoConexionVolLin();
        plugAnaranjadosDict[nplugInferiorCentro].GetComponent<Plugs>().EstablecerValoresNoConexionVolLin();
        plugAnaranjadosDict[nplugInferiorDerecho].GetComponent<Plugs>().EstablecerValoresNoConexionVolLin();
    }

    void DestacarPlugSelect(string nPlug)
    {
        Renderer rend = plugAnaranjadosDict[nPlug].GetComponent<Renderer>();
        rend.material = new Material(Shader.Find("Sprites/Default"));
        rend.material.color = Color.cyan;
    }

    private void changeOriginalColorPlug(GameObject objectClick)
    {
        if (objectClick.name.Contains("EntradaPlugNegro"))
        {
            objectClick.GetComponent<Renderer>().material = (Material)AssetDatabase.LoadAssetAtPath(RutaMaterialPlugNegro, typeof(Material));
        }
        else
        {
            objectClick.GetComponent<Renderer>().material = (Material)AssetDatabase.LoadAssetAtPath(RutaMaterialPlugAnaranjado, typeof(Material));
        }
    }

    void MandarPulsoEnergia(string nombrePlug)
    {
        if (plugAnaranjadosDict[nombrePlug].GetComponent<Plugs>().Conectado)
        {
            CableComponent cable = plugAnaranjadosDict[nombrePlug].GetComponent<CableComponent>();
            GameObject plugRelacionado = cable.EndPoint;
            if (plugRelacionado != null)
            {
                Plugs plugRela = plugRelacionado.GetComponent<Plugs>();
                if (plugRela != null)
                {
                    plugRela.EstablecerPropiedadesConexionesEntrantesPrueba();
                }
            }
        }
    }

    void EstablecerPropiedadesMultiples(string nplugSuperiorIzquierdo, string nplugSuperiorCentro, string nplugSuperiorDerecho,
        string nplugIzquierdo, string nplugCentro, string nplugDerecho, string nplugInferiorIzquierdo, string nplugInferiorCentro,
        string nplugInferiorDerecho)
    {

        plugAnaranjadosDict[nplugCentro].GetComponent<Plugs>().EstablecerPropiedadesConexionesEntrantesPrueba();
        plugAnaranjadosDict[nplugSuperiorIzquierdo].GetComponent<Plugs>().EstablecerValoresPlugDefinidoCompleto(plugAnaranjadosDict[nplugCentro]);
        plugAnaranjadosDict[nplugSuperiorCentro].GetComponent<Plugs>().EstablecerValoresPlugDefinidoCompleto(plugAnaranjadosDict[nplugCentro]);
        plugAnaranjadosDict[nplugSuperiorDerecho].GetComponent<Plugs>().EstablecerValoresPlugDefinidoCompleto(plugAnaranjadosDict[nplugCentro]);
        plugAnaranjadosDict[nplugIzquierdo].GetComponent<Plugs>().EstablecerValoresPlugDefinidoCompleto(plugAnaranjadosDict[nplugCentro]);
        plugAnaranjadosDict[nplugDerecho].GetComponent<Plugs>().EstablecerValoresPlugDefinidoCompleto(plugAnaranjadosDict[nplugCentro]);
        plugAnaranjadosDict[nplugInferiorIzquierdo].GetComponent<Plugs>().EstablecerValoresPlugDefinidoCompleto(plugAnaranjadosDict[nplugCentro]);
        plugAnaranjadosDict[nplugInferiorCentro].GetComponent<Plugs>().EstablecerValoresPlugDefinidoCompleto(plugAnaranjadosDict[nplugCentro]);
        plugAnaranjadosDict[nplugInferiorDerecho].GetComponent<Plugs>().EstablecerValoresPlugDefinidoCompleto(plugAnaranjadosDict[nplugCentro]);

        MandarPulsoEnergia(nplugSuperiorIzquierdo);
        MandarPulsoEnergia(nplugSuperiorCentro);
        MandarPulsoEnergia(nplugSuperiorDerecho);
        MandarPulsoEnergia(nplugIzquierdo);
        MandarPulsoEnergia(nplugDerecho);
        MandarPulsoEnergia(nplugInferiorIzquierdo);
        MandarPulsoEnergia(nplugInferiorCentro);
        MandarPulsoEnergia(nplugInferiorDerecho);
    }

    public void SaberPlugsConectados(ref string plug1, ref string plug2)
    {
        int lonLista = plugAnaranjados.Count;
        int numeroPlusConectados = 0;
        for (int i = 0; i < lonLista; i++)
        {
            if (plugAnaranjados[i].GetComponent<Plugs>().EstoConectado())
            {
                numeroPlusConectados++;
                if (numeroPlusConectados == 1)
                {
                    plug1 = plugAnaranjados[i].GetComponent<Plugs>().name;
                }
                else if (numeroPlusConectados == 2)
                {
                    plug2 = plugAnaranjados[i].GetComponent<Plugs>().name;
                }
            }
        }
    }

    public int NumeroPlugsConectados(bool izquierdo)
    {
        int lonLista = plugAnaranjados.Count;
        int numeroPlusConectados = 0;
        int valorInicial = 0;
        if (izquierdo)
        {
            valorInicial = 0;
            lonLista = 9;
        }
        else
        {
            valorInicial = 9;
            lonLista = 18;
        }
        for (int i = valorInicial; i < lonLista; i++)
        {
            if (plugAnaranjados[i].GetComponent<Plugs>().EstoConectado())
            {
                numeroPlusConectados++;
                if (numeroPlusConectados == 1 && izquierdo && primerPlugConectadoIzquierdo.Length == 0)
                {
                    primerPlugConectadoIzquierdo = plugAnaranjados[i].GetComponent<Plugs>().name;
                }else if (numeroPlusConectados == 1 && !izquierdo && primerPlugConectadoDerecho.Length == 0)
                {
                    primerPlugConectadoDerecho = plugAnaranjados[i].GetComponent<Plugs>().name;
                }else if (numeroPlusConectados == 1 && izquierdo && primerPlugConectadoIzquierdo.Length != 0)
                {
                    if (!plugAnaranjadosDict[primerPlugConectadoIzquierdo].GetComponent<Plugs>().EstoConectado())
                    {
                        primerPlugConectadoIzquierdo = plugAnaranjados[i].GetComponent<Plugs>().name;
                    }
                }
                else if (numeroPlusConectados == 1 && !izquierdo && primerPlugConectadoDerecho.Length != 0)
                {
                    if (!plugAnaranjadosDict[primerPlugConectadoDerecho].GetComponent<Plugs>().EstoConectado())
                    {
                        primerPlugConectadoDerecho = plugAnaranjados[i].GetComponent<Plugs>().name;
                    }
                }
            }
        }
        return numeroPlusConectados;
    }

    public int NombresDemasConectores(bool izquierdo, ref string plug1, ref string plug2, ref string plug3,
                                      ref string plug4, ref string plug5, ref string plug6, ref string plug7, 
                                      ref string plug8)
    {
        int lonLista = plugAnaranjados.Count;
        int numeroPlusConectados = 0;
        int valorInicial = 0;
        if (izquierdo)
        {
            valorInicial = 0;
            lonLista = 9;
        }
        else
        {
            valorInicial = 9;
            lonLista = 18;
        }
        for (int i = valorInicial; i < lonLista; i++)
        {
            if (izquierdo && primerPlugConectadoIzquierdo.Length != 0 && plugAnaranjados[i].GetComponent<Plugs>().name != primerPlugConectadoIzquierdo)
            {
                numeroPlusConectados++;
                switch (numeroPlusConectados)
                {
                    case 1:
                        plug1 = plugAnaranjados[i].GetComponent<Plugs>().name;
                        break;
                    case 2:
                        plug2 = plugAnaranjados[i].GetComponent<Plugs>().name;
                        break;
                    case 3:
                        plug3 = plugAnaranjados[i].GetComponent<Plugs>().name;
                        break;
                    case 4:
                        plug4 = plugAnaranjados[i].GetComponent<Plugs>().name;
                        break;
                    case 5:
                        plug5 = plugAnaranjados[i].GetComponent<Plugs>().name;
                        break;
                    case 6:
                        plug6 = plugAnaranjados[i].GetComponent<Plugs>().name;
                        break;
                    case 7:
                        plug7 = plugAnaranjados[i].GetComponent<Plugs>().name;
                        break;
                    case 8:
                        plug8 = plugAnaranjados[i].GetComponent<Plugs>().name;
                        break;
                    default:
                        break;
                }
            }
            else 
            if (!izquierdo && primerPlugConectadoDerecho.Length != 0 && plugAnaranjados[i].GetComponent<Plugs>().name != primerPlugConectadoDerecho)
            {
                numeroPlusConectados++;
                switch (numeroPlusConectados)
                {
                    case 1:
                        plug1 = plugAnaranjados[i].GetComponent<Plugs>().name;
                        break;
                    case 2:
                        plug2 = plugAnaranjados[i].GetComponent<Plugs>().name;
                        break;
                    case 3:
                        plug3 = plugAnaranjados[i].GetComponent<Plugs>().name;
                        break;
                    case 4:
                        plug4 = plugAnaranjados[i].GetComponent<Plugs>().name;
                        break;
                    case 5:
                        plug5 = plugAnaranjados[i].GetComponent<Plugs>().name;
                        break;
                    case 6:
                        plug6 = plugAnaranjados[i].GetComponent<Plugs>().name;
                        break;
                    case 7:
                        plug7 = plugAnaranjados[i].GetComponent<Plugs>().name;
                        break;
                    case 8:
                        plug8 = plugAnaranjados[i].GetComponent<Plugs>().name;
                        break;
                    default:
                        break;
                }
            }
        }
        return numeroPlusConectados;
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
