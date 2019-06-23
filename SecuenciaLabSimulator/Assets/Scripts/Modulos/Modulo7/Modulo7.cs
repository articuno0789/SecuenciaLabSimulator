using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modulo7 : MonoBehaviour
{
    [SerializeField] public List<GameObject> plugAnaranjados;
    [SerializeField] public List<GameObject> plugNegros;
    [SerializeField] public GameObject perilla;
    [SerializeField] public float limiteGiroInferiorPerilla = -90.0f;
    [SerializeField] public float limiteGiroSuperiorPerilla = 180.0f;
    [SerializeField] public float gradosActualesPerilla = -90.0f;
    [SerializeField] public float valorActualPerilla = 0.0f;
    [SerializeField] public float velocidadRotacion = 10;
    [SerializeField] public bool rotarPerillaPrueba = true;
    [SerializeField] public int estaLimiteRotacion = -1;
    [SerializeField] private bool puederotar = true;

    // Start is called before the first frame update
    void Start()
    {
        plugAnaranjados = new List<GameObject>();
        plugNegros = new List<GameObject>();
        inicializarComponentes(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (rotarPerillaPrueba)
        {
            RotarPerillaPrueba();
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
            }
            else if (child.name.Contains("EntradaPlugNegro"))
            {
                plugNegros.Add(child);
                child.AddComponent<CableComponent>();
            }
            else if (child.name.Contains("Total_Perilla"))
            {
                perilla = child;
            }
            inicializarComponentes(child);
        }
    }
}
