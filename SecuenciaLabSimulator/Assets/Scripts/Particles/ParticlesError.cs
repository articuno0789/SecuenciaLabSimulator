using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesError : MonoBehaviour
{
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

    public ParticlesError()
    {
        GameObject particlesGroup = GameObject.Find("ParticlesGroup");
        if (particlesGroup != null)
        {
            particlesError = particlesGroup.GetComponent<ParticlesGroup>().particlesGroup;
        }
    }

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

    public void DestruirParticulasError(GameObject currentParticle)
    {
        if (currentParticle != null)
        {
            Destroy(currentParticle);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
