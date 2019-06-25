using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FocoVerde : MonoBehaviour
{
    public GameObject padreTotalComponente;
    [SerializeField] public bool pruebaDeFocoVerde = true;
    [SerializeField] public string rutaPlasticoVerdeApagado = "Assets/Materials/PLasticos/PlasticoTraslucidoVerdeApagado.mat";
    [SerializeField] public string rutaPlasticoVerdeEncendido = "Assets/Materials/PLasticos/PlasticoTraslucidoVerdeEncendido.mat";
    [SerializeField] public Material plasticoVerdeApagado;
    [SerializeField] public Material plasticoVerdeEncendido;
    public ParticlesInformation[] particlesFocoVerde;
    private GameObject currentGO;
    public ParticlesInformation particle;

    // Start is called before the first frame update
    void Start()
    {
        plasticoVerdeApagado = (Material)AssetDatabase.LoadAssetAtPath(rutaPlasticoVerdeApagado, typeof(Material));
        plasticoVerdeEncendido = (Material)AssetDatabase.LoadAssetAtPath(rutaPlasticoVerdeEncendido, typeof(Material));
        GameObject particlesGroup = GameObject.Find("ParticlesGroup");
        if (particlesGroup != null)
        {
            //particlesFocoVerde = particlesGroup.GetComponent<ParticlesGroup>().particlesGroup.Clone();
            //particlesFocoVerde[0].modelScale.x = 50;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        Renderer focoVerdeRen = transform.GetComponent<Renderer>();
        if (pruebaDeFocoVerde)
        {
            focoVerdeRen.material = plasticoVerdeEncendido;
            Debug.Log("Cambio de material Luminoso - Foco Verde");
            pruebaDeFocoVerde = false;
            /*currentGO = Instantiate(particles22[0].modelSystemGO, particles22[0].modelPosition, Quaternion.Euler(particles22[0].modelRotation)) as GameObject;
            currentGO.transform.localScale = particles22[0].modelScale;*/
        }
        else
        {
            focoVerdeRen.material = plasticoVerdeApagado;
            Debug.Log("Cambio de material Opaco - Foco Verde");
            pruebaDeFocoVerde = true;
        }
    }
}
