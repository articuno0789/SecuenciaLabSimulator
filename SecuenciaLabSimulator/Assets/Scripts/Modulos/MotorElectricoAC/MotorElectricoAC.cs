using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorElectricoAC : MonoBehaviour
{
    #region Atributos
    [SerializeField] public GameObject ejeMotor;
    [SerializeField] public float velocidadRotacion = 10;
    [SerializeField] public bool rotaMotorPrueba = true;
    #endregion

    #region Inicializacion
    // Start is called before the first frame update
    void Start()
    {
        InicializarComponentes(gameObject);
    }

    private void InicializarComponentes(GameObject nodo)
    {
        int numeroDeHijosHijos = nodo.transform.childCount;
        for (int i = 0; i < numeroDeHijosHijos; i++)
        {
            GameObject child = nodo.transform.GetChild(i).gameObject;
            if (child.name.Contains("eje"))
            {
                ejeMotor = child;
            }
            InicializarComponentes(child);
        }
    }
    #endregion

    #region Comportamiento Modulo
    // Update is called once per frame
    void Update()
    {
        if (rotaMotorPrueba)
        {
            ejeMotor.transform.Rotate(Vector3.right, -velocidadRotacion * Time.deltaTime);
        }
    }
    #endregion
}
