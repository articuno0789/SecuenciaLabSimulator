using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Modulo2 : MonoBehaviour
{
    public Dictionary<string, string> plugsConnections;
    [SerializeField] public List<GameObject> plugAnaranjados;
    [SerializeField] public List<GameObject> plugNegros;
    [SerializeField] public GameObject botonCuadradoVerdeIzquierdo;
    [SerializeField] public GameObject botonCuadradoRojoIzquierdo;
    [SerializeField] public GameObject botonCuadradoVerdeDerecho;
    [SerializeField] public GameObject botonCuadradoRojoDerecho;
    private string rutaAnimacionBotonCuadradoVerde = "Assets/Animation/Modulos/Modulo2/Mod2PresBotonCuadradoVerde.anim";
    private string rutaAnimacionBotonCuadradoRojo = "Assets/Animation/Modulos/Modulo2/Mod2PresBotonCuadradoRojo.anim";
    private string nombreAnimacionBotonCuadradoVerde = "Mod2PresBotonCuadradoVerde";
    private string nombreAnimacionBotonCuadradoRojo = "Mod2PresBotonCuadradoRojo";

    // Start is called before the first frame update
    void Start()
    {
        plugsConnections = new Dictionary<string, string>();

        plugAnaranjados = new List<GameObject>();
        plugNegros = new List<GameObject>();
        inicializarComponentes(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void inicializarComponentes(GameObject nodo)
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
            }
            else if (child.name.Contains("EntradaPlugNegro"))
            {
                plugNegros.Add(child);
                child.AddComponent<CableComponent>();

                Plugs plug = child.AddComponent<Plugs>();
                plug.padreTotalComponente = this.gameObject;
                plugsConnections.Add(gameObject.name + "|" + child.name, "");
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
            inicializarComponentes(child);
        }
    }

    public void CrearConexionPlugs(string startPlug, string endPlug)
    {
        plugsConnections[startPlug] = endPlug;
        Debug.Log("plugsConnections[" + startPlug + "]: " + endPlug);
    }

}
