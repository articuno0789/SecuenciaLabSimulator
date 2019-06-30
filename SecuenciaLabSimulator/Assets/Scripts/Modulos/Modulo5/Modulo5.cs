using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modulo5 : MonoBehaviour
{
    public Dictionary<string, string> plugsConnections;
    [SerializeField] public List<GameObject> plugAnaranjados;
    [SerializeField] public List<GameObject> plugNegros;
    [SerializeField] public GameObject agujaMedidora;
    [SerializeField] public float limiteGiroInferiorAguja = -34;  //-34° | 0.3318f
    [SerializeField] public float limiteGiroSuperiorAguja = -145; //-145° | -0.3318f
    [SerializeField] public float gradosActualesAguja = -34.0f;
    [SerializeField] public float limiteVoltajeInferiorAguja = 0.0f;
    [SerializeField] public static float limiteVoltajeSuperiorAguja = 300.0f;
    [SerializeField] public float valorActualAguja = 100.0f;
    [SerializeField] public float velocidadRotacion = 10;
    [SerializeField] public bool rotarAgujaPrueba = true;
    [SerializeField] public int estaLimiteRotacion = -1;
    [SerializeField] public float pasoMovimientoAgudas = 111 / limiteVoltajeSuperiorAguja; //Diferencia entre 145-135 / 300 (limite superior)
    [SerializeField] private bool puederotar = true;

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
        if (rotarAgujaPrueba)
        {
            RotarAguajaPrueba();
        }
    }

    public void RotarAguajaPrueba()
    {
        Vector3 agujaMedidoraRotation = UnityEditor.TransformUtils.GetInspectorRotation(agujaMedidora.gameObject.transform);
        if (agujaMedidoraRotation.x >= limiteGiroInferiorAguja && puederotar)
        {
            agujaMedidora.transform.Rotate(Vector3.up, velocidadRotacion * Time.deltaTime);
        }
        else
        if (agujaMedidoraRotation.x <= limiteGiroInferiorAguja && agujaMedidoraRotation.x >= limiteGiroSuperiorAguja && puederotar)
        {
            agujaMedidora.transform.Rotate(Vector3.up, velocidadRotacion * Time.deltaTime);
        }
        else
        {
            puederotar = false;
            agujaMedidora.transform.Rotate(Vector3.down, velocidadRotacion * Time.deltaTime);
            //agujaMedidora.transform.rotation = Quaternion.Euler(-35, -90, 90);
            //Debug.Log("Cambio de rotacion: " + agujaMedidoraRotation.x + " >= " + limiteGiroInferiorAguja);
            if (agujaMedidoraRotation.x >= limiteGiroInferiorAguja)
            {
                puederotar = true;
            }
        }
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
            else if (child.name.Contains("AgujaMedidora"))
            {
                agujaMedidora = child;
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
