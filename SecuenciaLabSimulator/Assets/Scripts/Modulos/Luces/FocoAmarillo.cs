using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FocoAmarillo : MonoBehaviour
{
    [SerializeField] public bool pruebaDeFocoAmarillo = true;
    [SerializeField] public string rutaPlasticoAmarilloApagado = "Assets/Materials/PLasticos/PlasticoTraslucidoAmarilloApagado.mat";
    [SerializeField] public string rutaPlasticoAmarilloEncendido = "Assets/Materials/PLasticos/PlasticoTraslucidoAmarilloEncendido.mat";
    [SerializeField] public Material plasticoAmarilloApagado;
    [SerializeField] public Material plasticoAmarilloEncendido;
    public GameObject padreTotalComponente;
    public GameObject currentParticle;
    private ParticlesError particleError;
    public int currentTypeParticleError = 0;

    // Start is called before the first frame update
    void Start()
    {
        particleError = new ParticlesError();
        padreTotalComponente = new GameObject();
        plasticoAmarilloApagado = (Material)AssetDatabase.LoadAssetAtPath(rutaPlasticoAmarilloApagado, typeof(Material));
        plasticoAmarilloEncendido = (Material)AssetDatabase.LoadAssetAtPath(rutaPlasticoAmarilloEncendido, typeof(Material));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        Renderer focoVerdeRen = transform.GetComponent<Renderer>();
        if (pruebaDeFocoAmarillo)
        {
            focoVerdeRen.material = plasticoAmarilloEncendido;
            Debug.Log("Cambio de material Luminoso - Foco Amarillo");
            pruebaDeFocoAmarillo = false;
            currentParticle = particleError.CrearParticulasError(currentTypeParticleError, transform.position, transform.rotation.eulerAngles);
        }
        else
        {
            focoVerdeRen.material = plasticoAmarilloApagado;
            Debug.Log("Cambio de material Opaco - Foco Amarillo");
            pruebaDeFocoAmarillo = true;
            particleError.DestruirParticulasError(currentParticle);
        }
    }
}
