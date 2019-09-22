﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FocoAmarillo : MonoBehaviour
{
    #region Atributos
    [SerializeField] public bool pruebaDeFocoAmarillo = true;
    [SerializeField] public string rutaPlasticoAmarilloApagado = "Assets/Materials/PLasticos/PlasticoTraslucidoAmarilloApagado.mat";
    [SerializeField] public string rutaPlasticoAmarilloEncendido = "Assets/Materials/PLasticos/PlasticoTraslucidoAmarilloEncendido.mat";
    [SerializeField] public Material plasticoAmarilloApagado;
    [SerializeField] public Material plasticoAmarilloEncendido;
    public GameObject padreTotalComponente;
    public GameObject currentParticle;
    private ParticlesError particleError;
    public int currentTypeParticleError = 0;
    public bool focoAmarilloEncendido = false;
    public bool focoAveriado = false;
    public bool debugMode = false;
    #endregion

    #region Propiedades
    public bool DebugMode
    {
        get => debugMode;
        set => debugMode = value;
    }

    public int CurrentTypeParticleError
    {
        get => currentTypeParticleError;
        set => currentTypeParticleError = value;
    }

    public bool FocoAveriado
    {
        get => focoAveriado;
        set => focoAveriado = value;
    }

    public bool FocoAmarilloEncendido
    {
        get => focoAmarilloEncendido;
        set => focoAmarilloEncendido = value;
    }
    #endregion

    #region Inicializacion
    // Start is called before the first frame update
    void Start()
    {
        particleError = new ParticlesError();
        padreTotalComponente = new GameObject();
        plasticoAmarilloApagado = (Material)AssetDatabase.LoadAssetAtPath(rutaPlasticoAmarilloApagado, typeof(Material));
        plasticoAmarilloEncendido = (Material)AssetDatabase.LoadAssetAtPath(rutaPlasticoAmarilloEncendido, typeof(Material));
    }
    #endregion

    // Update is called once per frame
    void Update()
    {

    }

    #region Comportamiento
    public void ComprobarEstado(GameObject plugArriba, GameObject plugAbajo)
    {
        Plugs plugArribaCompPlug = plugArriba.GetComponent<Plugs>();
        Plugs plugAbajoCompPlug = plugAbajo.GetComponent<Plugs>();

        if (plugArribaCompPlug != null && plugAbajoCompPlug != null)
        {
            plugArribaCompPlug.EstablecerPropiedadesConexionesEntrantes();
            plugAbajoCompPlug.EstablecerPropiedadesConexionesEntrantes();

            if (plugArribaCompPlug.Conectado && plugAbajoCompPlug.Conectado)
            {
                if (plugArribaCompPlug.TipoConexion == 1 && plugAbajoCompPlug.TipoConexion == 2)// Correcto - Linea y neutro conectado en de manera correcta
                {
                    focoAveriado = false;
                    EncenderFoco();
                    if (DebugMode)
                    {
                        Debug.Log(padreTotalComponente.name + ") " + this.name + " - FOCO AMARILLO - if(plugIzquierdoCompPlug.TipoConexion == 1 && plugDerechoCompPlug.TipoConexion == 2) - Conectado");
                    }
                }
                else if (plugArribaCompPlug.TipoConexion == 2 && plugAbajoCompPlug.TipoConexion == 1) //Averia - Linea y neutro invertido
                {
                    focoAveriado = false;
                    ApagarFoco();
                    if (DebugMode)
                    {
                        Debug.Log(padreTotalComponente.name + ") " + this.name + " - FOCO AMARILLO - (plugArribaCompPlug.TipoConexion == 2 && plugAbajoCompPlug.TipoConexion == 1) - Conectado - Debido a que los focos tienen polaridad, al invertir la conexión nom encienden.");
                    }
                }
                else if (plugArribaCompPlug.TipoConexion == 1 && plugAbajoCompPlug.TipoConexion == 1) // Avaeria - Dos lineas conectadas al mismo tiempo
                {
                    if (plugArribaCompPlug.Linea == plugAbajoCompPlug.Linea)
                    {
                        focoAveriado = false;
                        ApagarFoco();
                        if (DebugMode)
                        {
                            Debug.Log(padreTotalComponente.name + ") " + this.name + " - FOCO AMARILLO - (plugArribaCompPlug.Linea == plugAbajoCompPlug.Linea) - Conectado - Debido a que son la misma linea simplemente no enciende.");
                        }
                    }
                    else if (plugArribaCompPlug.Linea != plugAbajoCompPlug.Linea)
                    {
                        focoAveriado = true;
                        ApagarFoco();
                        if (DebugMode)
                        {
                            Debug.LogError(padreTotalComponente.name + ") " + this.name + " - FOCO AMARILLO - if (plugArribaCompPlug.Linea != plugAbajoCompPlug.Linea) - Conectado - Debido a que son lineas diferentes el foco se quema.");
                        }
                    }
                    else
                    {
                        EliminarMaterial();
                        if (DebugMode)
                        {
                            Debug.LogError(padreTotalComponente.name + ") " + this.name + " - FOCO AMARILLO - NO DEBERIA ENTRAR AQUI - (plugArribaCompPlug.TipoConexion == 1 && plugAbajoCompPlug.TipoConexion == 1)");
                        }
                    }

                }
                else if (plugArribaCompPlug.TipoConexion == 2 && plugAbajoCompPlug.TipoConexion == 2) // Correcto - Dos neutros conectados, no pasa nada
                {
                    focoAveriado = false;
                    ApagarFoco();
                    if (DebugMode)
                    {
                        Debug.Log(padreTotalComponente.name + ") " + this.name + " - FOCO AMARILLO - (plugIzquierdoCompPlug.TipoConexion == 2 && plugDerechoCompPlug.TipoConexion == 2) - Conectado");
                    }
                }
                else
                {
                    //EliminarMaterial();
                    focoAveriado = false;
                    ApagarFoco();
                    if (DebugMode)
                    {
                        Debug.LogError(padreTotalComponente.name + ") " + this.name + " - FOCO AMARILLO - Este caso de uso todavia no esta programado - No entro a ningun caso");
                    }
                }
            }
            else
            {
                focoAveriado = false;
                ApagarFoco();
                if (DebugMode)
                {
                    Debug.Log(padreTotalComponente.name + ") " + this.name + " - FOCO AMARILLO - if (plugIzquierdoCompPlug.Conectado && plugDerechoCompPlug.Conectado) - NO esta conectados");
                }
            }
        }
        else
        {
            focoAveriado = false;
            ApagarFoco();
            if (DebugMode)
            {
                Debug.LogError(padreTotalComponente.name + ") " + this.name + " - FOCO AMARILLO - if(plugArribaCompPlug != null && plugAbajoCompPlug != null) - Alguno de los dos es nulo, plugArribaCompPlug: " + plugArribaCompPlug + ", plugAbajoCompPlug: " + plugAbajoCompPlug);
            }
        }
    }

    void EliminarMaterial()
    {
        Renderer focoAmarillo = transform.GetComponent<Renderer>();
        focoAmarillo.material = null;
    }

    public void EncenderFoco()
    {
        Renderer focoAmarillo = transform.GetComponent<Renderer>();
        focoAmarillo.material = plasticoAmarilloEncendido;
        FocoAmarilloEncendido = true;
        ComprobarEstadoAveria();
    }

    public void ApagarFoco()
    {
        Renderer focoCircularAzul = transform.GetComponent<Renderer>();
        focoCircularAzul.material = plasticoAmarilloApagado;
        FocoAmarilloEncendido = false;
        ComprobarEstadoAveria();
    }

    void ComprobarEstadoAveria()
    {
        if (focoAveriado)
        {
            if (currentParticle == null)
            {
                CrearAveria();
            }
        }
        else
        {
            if (currentParticle != null)
            {
                QuitarAveria();
            }
        }
    }

    public void CrearAveria()
    {
        currentParticle = particleError.CrearParticulasError(currentTypeParticleError, transform.position, transform.rotation.eulerAngles, new Vector3(0.5f, 0.5f, 0.5f));
        currentParticle.transform.parent = this.gameObject.transform;
        focoAveriado = true;
    }

    public void QuitarAveria()
    {
        particleError.DestruirParticulasError(currentParticle);
        focoAveriado = false;
    }
    #endregion

    private void OnMouseDown()
    {
        Renderer focoVerdeRen = transform.GetComponent<Renderer>();
        if (pruebaDeFocoAmarillo)
        {
            focoVerdeRen.material = plasticoAmarilloEncendido;
            Debug.Log("Cambio de material Luminoso - Foco Amarillo");
            pruebaDeFocoAmarillo = false;
            currentParticle = particleError.CrearParticulasError(currentTypeParticleError, transform.position, transform.rotation.eulerAngles);
            currentParticle.transform.parent = this.gameObject.transform;
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
