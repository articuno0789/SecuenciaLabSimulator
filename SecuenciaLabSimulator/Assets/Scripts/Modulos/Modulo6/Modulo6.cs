using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modulo6 : MonoBehaviour
{
    #region Atributos
    public Dictionary<string, string> plugsConnections;
    [SerializeField] public List<GameObject> plugAnaranjados;
    [SerializeField] public List<GameObject> plugNegros;
    public Dictionary<string, GameObject> plugAnaranjadosDict;
    public Dictionary<string, GameObject> plugNegrosDict;
    [SerializeField] public GameObject perilla;
    [SerializeField] public float limiteGiroInferiorPerilla = -90.0f;
    [SerializeField] public float limiteGiroSuperiorPerilla = 320.0f;
    public float valorActualPerilla = 0.0f;
    public float valorMinimoPerilla = 0.0f;
    public float valorMaximoPerilla = 40.0f;
    private Quaternion originalRotationKnob;


    [SerializeField] public float gradosActualesPerilla = -90.0f;
    [SerializeField] public float velocidadRotacion = 10;
    public bool rotarPerillaPrueba = false;
    [SerializeField] public int estaLimiteRotacion = -1;
    private bool puederotar = true;
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
            else if (child.name.Contains("Total_Perilla"))
            {
                perilla = child;
                originalRotationKnob = perilla.transform.rotation;
            }
            InicializarComponentes(child);
        }
    }

    #endregion

    #region Comportamiento Modulo
    // Update is called once per frame
    void Update()
    {
        if (rotarPerillaPrueba)
        {
            RotarPerillaPrueba();
        }
    }

    public void RotarPerilla()
    {
        if(valorActualPerilla >= valorMinimoPerilla && valorActualPerilla <= valorMaximoPerilla)
        {
            perilla.transform.rotation = originalRotationKnob;
            float valorRotacionGrados = (limiteGiroSuperiorPerilla * valorActualPerilla) / valorMaximoPerilla;
            Debug.Log("Modulo 6: valorRotacionGrados: " + valorRotacionGrados + ", valorActualPerilla: " + valorActualPerilla + ", limiteGiroSuperiorPerilla: " + limiteGiroSuperiorPerilla + ", valorMaximoPerilla: " + valorMaximoPerilla);
            perilla.transform.Rotate(0, 0, valorRotacionGrados);
        }
        else
        {
            Debug.LogError("Error. Modulo 6: rotarPerilla(float valorActual): El valor actual recibido sobrepasa los limites establecidos");
        }
    }

    public void RotarPerillaPrueba()
    {
        Vector3 perillaRotation = UnityEditor.TransformUtils.GetInspectorRotation(perilla.gameObject.transform);
        //Debug.Log("perillaRotation: " + perillaRotation);
        if (perillaRotation.z >= limiteGiroSuperiorPerilla && puederotar)
        {
            //Debug.Log("if (perillaRotation.z >= limiteGiroInferiorPerilla && puederotar)");
            perilla.transform.Rotate(Vector3.back, velocidadRotacion * Time.deltaTime);
        }
        else
        if (perillaRotation.z >= limiteGiroInferiorPerilla && perillaRotation.z <= limiteGiroSuperiorPerilla && puederotar)
        {
            //Debug.Log("---if (perillaRotation.z <= limiteGiroInferiorPerilla && perillaRotation.z >= limiteGiroSuperiorPerilla && puederotar)");
            perilla.transform.Rotate(Vector3.forward, velocidadRotacion * Time.deltaTime);
        }
        else
        {
            puederotar = false;
            perilla.transform.Rotate(Vector3.forward, velocidadRotacion * Time.deltaTime);
            //agujaMedidora.transform.rotation = Quaternion.Euler(-35, -90, 90);
            //Debug.Log("Cambio de rotacion: " + agujaMedidoraRotation.x + " >= " + limiteGiroInferiorPerilla);
            if (perillaRotation.z >= limiteGiroInferiorPerilla)
            {
                puederotar = true;
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
    #endregion
}
