using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Modulo3 : MonoBehaviour
{
    #region Atributos
    public Dictionary<string, string> plugsConnections;
    [SerializeField] public List<GameObject> plugAnaranjados;
    [SerializeField] public List<GameObject> plugNegros;
    [SerializeField] public List<GameObject> botonesCircularesRojos;
    [SerializeField] public List<GameObject> botonesCircularesVerdes;
    public Dictionary<string, GameObject> plugAnaranjadosDict;
    public Dictionary<string, GameObject> plugNegrosDict;
    public Dictionary<string, GameObject> botonesCircularesRojosDict;
    public Dictionary<string, GameObject> botonesCircularesVerdesDict;
    private string rutaAnimacionBotonCircular = "Assets/Animation/Modulos/Modulo3/Mod3PresBotonCircular.anim";
    private string nombreAnimacionBotonCircular = "Mod3PresBotonCircular";
    #endregion

    #region Inicializacion
    // Start is called before the first frame update
    void Start()
    {
        plugsConnections = new Dictionary<string, string>();
        plugAnaranjadosDict = new Dictionary<string, GameObject>();
        plugNegrosDict = new Dictionary<string, GameObject>();
        botonesCircularesRojosDict = new Dictionary<string, GameObject>();
        botonesCircularesVerdesDict = new Dictionary<string, GameObject>();

        plugAnaranjados = new List<GameObject>();
        plugNegros = new List<GameObject>();
        botonesCircularesRojos = new List<GameObject>();
        botonesCircularesVerdes = new List<GameObject>();
        InicializarComponentes(gameObject);
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
                botonesCircularesVerdes.Add(child);
                Animation ani = child.AddComponent<Animation>();
                ani.playAutomatically = false;
                ani.AddClip(((AnimationClip)AssetDatabase.LoadAssetAtPath(rutaAnimacionBotonCircular, typeof(AnimationClip))), nombreAnimacionBotonCircular);
                child.AddComponent<Mod3PushButton>();

                botonesCircularesVerdesDict.Add(child.name, child);
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
