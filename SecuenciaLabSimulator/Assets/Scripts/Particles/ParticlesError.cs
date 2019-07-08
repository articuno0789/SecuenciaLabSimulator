using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesError : MonoBehaviour
{
    #region Atributos
    public enum ParticlesErrorType
    {
        BigExplosion,
        DrippingFlames,
        ElectricalSparksEffect,
        SmallExplosionEffect,
        SmokeEffect,
        SparksEffect,
        RibbonSmoke,
        PlasmaExplosionEffect
    }
    public ParticlesInformation[] particlesError;
    #endregion

    #region Inicializacion
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
    public GameObject CrearParticulasError(int typeParticleError, Vector3 positionParticle, Vector3 rotationParticle)
    {
        //Debug.Log("INICIAR CREAR PARTICULA");
        GameObject currentParticle = null;
        if (particlesError != null)
        {
            currentParticle = Instantiate(particlesError[typeParticleError].modelSystemGO, positionParticle, Quaternion.Euler(particlesError[typeParticleError].modelRotation)) as GameObject;
            currentParticle.transform.localScale = particlesError[typeParticleError].modelScale;
            //Debug.Log("SE CREO PARTICULA");
        } 
        return currentParticle;
    }

    public GameObject CrearParticulasError(int typeParticleError, Vector3 positionParticle, Vector3 rotationParticle, Vector3 scaleParticle)
    {
        //Debug.Log("INICIAR CREAR PARTICULA");
        GameObject currentParticle = null;
        if (particlesError != null)
        {
            currentParticle = Instantiate(particlesError[typeParticleError].modelSystemGO, positionParticle, Quaternion.Euler(particlesError[typeParticleError].modelRotation)) as GameObject;
            currentParticle.transform.localScale = scaleParticle;
            //Debug.Log("SE CREO PARTICULA");
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
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        
    }
}
