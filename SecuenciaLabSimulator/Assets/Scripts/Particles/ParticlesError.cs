using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesError : MonoBehaviour
{
    #region Atributos
    [Header("Lista de Particulas")]
    public ParticlesInformation[] particlesError;
    #endregion

    #region Inicializacion
    /*En este método se hace la inicialización del grupo de particulas.*/
    public ParticlesError()
    {
        GameObject particlesGroup = GameObject.Find("ParticlesGroup");
        if (particlesGroup != null)
        {
            particlesError = particlesGroup.GetComponent<ParticlesGroup>().particlesGroup;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    #endregion

    #region Creacion Particulas
    /*En este método se crea la particula de averia solicitada, de acuerdo a las especificaciones de los componentes.
      Se especifica la posición y la rotación.*/
    public GameObject CrearParticulasError(int typeParticleError, Vector3 positionParticle, Vector3 rotationParticle)
    {
        //Debug.Log("INICIAR CREAR PARTICULA");
        GameObject currentParticle = null;
        if (particlesError != null && positionParticle != null && rotationParticle != null)
        {
            currentParticle = Instantiate(particlesError[typeParticleError].modelSystemGO, positionParticle, Quaternion.Euler(particlesError[typeParticleError].modelRotation)) as GameObject;
            currentParticle.transform.localScale = particlesError[typeParticleError].modelScale;
            //Debug.Log("SE CREO PARTICULA");
        }
        else
        {
            Debug.LogError(this.name + ", Error. GameObject CrearParticulasError(int typeParticleError, Vector3 positionParticle, Vector3 rotationParticle) - No se pudo crear las particulas ya que alguno de los parametros es nulo.");
        }
        return currentParticle;
    }

    /*En este método se crea la particula de averia solicitada, de acuerdo a las especificaciones de los componentes.
      Se especifica la posición, la rotación y la escala.*/
    public GameObject CrearParticulasError(int typeParticleError, Vector3 positionParticle, Vector3 rotationParticle, Vector3 scaleParticle)
    {
        //Debug.Log("INICIAR CREAR PARTICULA");
        GameObject currentParticle = null;
        if (particlesError != null && positionParticle != null && rotationParticle != null && scaleParticle != null)
        {
            currentParticle = Instantiate(particlesError[typeParticleError].modelSystemGO, positionParticle, Quaternion.Euler(particlesError[typeParticleError].modelRotation)) as GameObject;
            currentParticle.transform.localScale = scaleParticle;
            //Debug.Log("SE CREO PARTICULA");
        }
        else
        {
            Debug.LogError(this.name + ", Error. GameObject CrearParticulasError(int typeParticleError, Vector3 positionParticle, Vector3 rotationParticle, Vector3 scaleParticle) - No se pudo crear las particulas ya que alguno de los parametros es nulo.");
        }
        return currentParticle;
    }
    #endregion

    #region Destrucción Particulas
    public void DestruirParticulasError(GameObject currentParticle)
    {
        if (currentParticle != null)
        {
            Destroy(currentParticle);
        }
        else
        {
            Debug.LogError(this.name + ", Error. DestruirParticulasError(GameObject currentParticle) - No se pudo destruir el objeto ya que es nulo.");
        }
    }
    #endregion
}
