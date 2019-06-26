using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FocoVerde : MonoBehaviour
{
    
    [SerializeField] public bool pruebaDeFocoVerde = true;
    [SerializeField] public string rutaPlasticoVerdeApagado = "Assets/Materials/PLasticos/PlasticoTraslucidoVerdeApagado.mat";
    [SerializeField] public string rutaPlasticoVerdeEncendido = "Assets/Materials/PLasticos/PlasticoTraslucidoVerdeEncendido.mat";
    [SerializeField] public Material plasticoVerdeApagado;
    [SerializeField] public Material plasticoVerdeEncendido;
    public GameObject padreTotalComponente;
    public GameObject currentParticle;
    private ParticlesError particleError;
    public int currentTypeParticleError = 0;

    // Start is called before the first frame update
    void Start()
    {
        particleError = new ParticlesError();
        padreTotalComponente = new GameObject();
        plasticoVerdeApagado = (Material)AssetDatabase.LoadAssetAtPath(rutaPlasticoVerdeApagado, typeof(Material));
        plasticoVerdeEncendido = (Material)AssetDatabase.LoadAssetAtPath(rutaPlasticoVerdeEncendido, typeof(Material));
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
            currentParticle = particleError.CrearParticulasError(currentTypeParticleError, transform.position, transform.rotation.eulerAngles);
        }
        else
        {
            focoVerdeRen.material = plasticoVerdeApagado;
            Debug.Log("Cambio de material Opaco - Foco Verde");
            pruebaDeFocoVerde = true;
            particleError.DestruirParticulasError(currentParticle);
        }
    }
}
