using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Modulo1 : MonoBehaviour
{
    #region Atributos
    public Dictionary<string, string> plugsConnections;
    public Dictionary<string, GameObject> plugAnaranjadosDict;
    public Dictionary<string, GameObject> plugNegrosDict;
    [SerializeField] public List<GameObject> plugAnaranjados;
    [SerializeField] public List<GameObject> plugNegros;
    [SerializeField] public List<GameObject> luzRojos;
    [SerializeField] public const float voltajeModulo = 127;
    [SerializeField] public bool pruebaDeLuz = true;
    #endregion

    #region Inicializacion
    // Start is called before the first frame update
    void Start()
    {
        plugsConnections = new Dictionary<string, string>();
        plugAnaranjadosDict = new Dictionary<string, GameObject>();
        plugNegrosDict = new Dictionary<string, GameObject>();

        plugAnaranjados = new List<GameObject>();
        plugNegros = new List<GameObject>();
        luzRojos = new List<GameObject>();
        InicializarComponentes(gameObject);
        plugAnaranjadosDict["EntradaPlugAnaranjado1"].GetComponent<Plugs>().TipoConexion = 1;
        plugNegrosDict["EntradaPlugNegro1"].GetComponent<Plugs>().TipoConexion = 2;
        plugAnaranjadosDict["EntradaPlugAnaranjado1"].GetComponent<Plugs>().voltaje = voltajeModulo;
        plugNegrosDict["EntradaPlugNegro1"].GetComponent<Plugs>().voltaje = voltajeModulo;
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
                luzRojos.Add(child);
                child.AddComponent<LuzRoja>();
            }
            InicializarComponentes(child);
        }
    }
    #endregion

    #region Comportamiento Modulo
    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region Conexiones Grafo
    public void CrearConexionPlugs(string startPlug, string endPlug)
    {
        plugsConnections[startPlug] = endPlug;
        Debug.Log("plugsConnections[" + startPlug + "]: " + endPlug);
    }
    #endregion
}
