using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Modulo4 : MonoBehaviour
{
    #region Atributos
    public Dictionary<string, string> plugsConnections;
    [SerializeField] public List<GameObject> plugAnaranjados;
    [SerializeField] public List<GameObject> plugNegros;
    [SerializeField] public List<GameObject> focosVerde;
    [SerializeField] public List<GameObject> focosAmarillos;
    public Dictionary<string, GameObject> plugAnaranjadosDict;
    public Dictionary<string, GameObject> plugNegrosDict;
    public Dictionary<string, GameObject> focosVerdesDict;
    public Dictionary<string, GameObject> focosAmarillosDict;
    public enum ParticlesError {
        BigExplosion,
        DrippingFlames,
        ElectricalSparksEffect,
        SmallExplosionEffect,
        SmokeEffect,
        SparksEffect,
        RibbonSmoke,
        PlasmaExplosionEffect
    }

    //Particulas
    private string rutaParticulaElectricalSparksEffect = "Assets/Assets Descargados/EffectExamples/Misc Effects/Prefabs/ElectricalSparksEffect.prefab";
    #endregion

    #region Inicializacion
    // Start is called before the first frame update
    void Start()
    {
        plugsConnections = new Dictionary<string, string>();
        plugAnaranjadosDict = new Dictionary<string, GameObject>();
        plugNegrosDict = new Dictionary<string, GameObject>();
        focosVerdesDict = new Dictionary<string, GameObject>();
        focosAmarillosDict = new Dictionary<string, GameObject>();

        plugAnaranjados = new List<GameObject>();
        plugNegros = new List<GameObject>();
        focosVerde = new List<GameObject>();
        focosAmarillos = new List<GameObject>();
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
            else if (child.name.Contains("FocoAmarilo"))
            {
                focosAmarillos.Add(child);
                FocoAmarillo focoAmarillo = child.AddComponent<FocoAmarillo>();
                focoAmarillo.currentTypeParticleError = (int)ParticlesError.SmokeEffect;
                focoAmarillo.padreTotalComponente = this.gameObject;
                focosAmarillosDict.Add(child.name, child);
            }
            else if (child.name.Contains("FocoVerde"))
            {
                focosVerde.Add(child);
                FocoVerde focoVerde = child.AddComponent<FocoVerde>();
                focoVerde.padreTotalComponente = this.gameObject;
                focoVerde.currentTypeParticleError = (int)ParticlesError.PlasmaExplosionEffect;
                focoVerde.padreTotalComponente = this.gameObject;
                focosVerdesDict.Add(child.name, child);
            }
            InicializarComponentes(child);
        }
    }

    #endregion

    #region Comportamiento Modulo
    // Update is called once per frame
    void Update()
    {
        ComportamientoModulo();
    }

    private void ComportamientoModulo()
    {
        focosVerdesDict["FocoVerde"].GetComponent<FocoVerde>().ComprobarEstado(plugAnaranjadosDict["EntradaPlugAnaranjado1"], plugNegrosDict["EntradaPlugNegro1"]);
        focosAmarillosDict["FocoAmarilo"].GetComponent<FocoAmarillo>().ComprobarEstado(plugAnaranjadosDict["EntradaPlugAnaranjado2"], plugNegrosDict["EntradaPlugNegro2"]);
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
