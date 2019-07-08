using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Modulo2 : MonoBehaviour
{
    #region Atributos
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
                botonCuadradoRojoIzquierdo = child;
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
