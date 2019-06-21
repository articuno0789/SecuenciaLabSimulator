using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LuzRoja : MonoBehaviour
{
    [SerializeField] public bool pruebaDeLuz = true;
    [SerializeField] public string rutaPlasticoRojoApagado = "Assets/Materials/PLasticos/PlasticoTraslucidoRojoApagado.mat";
    [SerializeField] public string rutaPlasticoRojoEncendido = "Assets/Materials/PLasticos/PlasticoTraslucidoRojoEncendido.mat";
    [SerializeField] public Material plasticoRojoApagado;
    [SerializeField] public Material plasticoRojoEncendido;

    // Start is called before the first frame update
    void Start()
    {
        plasticoRojoApagado = (Material)AssetDatabase.LoadAssetAtPath(rutaPlasticoRojoApagado, typeof(Material));
        plasticoRojoEncendido = (Material)AssetDatabase.LoadAssetAtPath(rutaPlasticoRojoEncendido, typeof(Material));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Renderer luzRojaRen = transform.GetComponent<Renderer>();
        if (pruebaDeLuz)
        {
            luzRojaRen.material = plasticoRojoEncendido;
            Debug.Log("Cambio de material Luminoso - Luz Roja");
            pruebaDeLuz = false;
        }
        else
        {
            luzRojaRen.material = plasticoRojoApagado;
            Debug.Log("Cambio de material Opaco - Luz Roja");
            pruebaDeLuz = true;
        }
    }
}
