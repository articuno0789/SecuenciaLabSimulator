using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorElectricoAC : MonoBehaviour
{
    [SerializeField] public GameObject ejeMotor;
    [SerializeField] public float velocidadRotacion = 10;
    [SerializeField] public bool rotaMotorPrueba = true;

    // Start is called before the first frame update
    void Start()
    {
        inicializarComponentes(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (rotaMotorPrueba)
        {
            ejeMotor.transform.Rotate(Vector3.right, -velocidadRotacion * Time.deltaTime);
        }
    }

    private void inicializarComponentes(GameObject nodo)
    {
        int numeroDeHijosHijos = nodo.transform.childCount;
        for (int i = 0; i < numeroDeHijosHijos; i++)
        {
            GameObject child = nodo.transform.GetChild(i).gameObject;
            if (child.name.Contains("eje"))
            {
                ejeMotor = child;
            }
            inicializarComponentes(child);
        }
    }

}
