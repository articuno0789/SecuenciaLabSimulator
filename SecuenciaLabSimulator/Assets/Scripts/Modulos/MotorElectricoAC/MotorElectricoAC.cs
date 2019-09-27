﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorElectricoAC : MonoBehaviour
{
    #region Atributos
    [Header("Encendido")]
    public bool motorEncendido = false;
    public bool moduloExistente = false;
    [Header("Módulo controlador")]
    public GameObject moduloControlador;
    [Header("Listas de elementos")]
    public GameObject ejeMotor;
    public GameObject cajaElectrica;
    [Header("Parametros Motor")]
    public float velocidadRotacionActual = 0;
    public float velocidadMaximaRotacion = 1750;
    public float velocidadMinimaRotacion = 0;
    public int direccionRotacion = 0;
    public float voltajeMaximo = 250; //250v
    public float voltajeMinimo = 40; //0v
    public float voltajeActual = 0; //velocidad actual
    public bool rotaMotorPrueba = true;
    public bool motorAveriado = false;
    [Header("Panel Informativo")]
    public GameObject panelInformativo;
    [Header("Particulas")]
    public GameObject currentParticle;
    private ParticlesError particleError;
    public int currentTypeParticleError = (int)AuxiliarModulos.ParticlesErrorTypes.SmokeEffect;
    //Variables de debug
    [Header("Debug")]
    public bool mostrarDiccionarioConexiones = false; // Variable
    public bool mostrarPlugAnaranjados = false; // Variable
    public bool mostrarPlugNegros = false; // Variable
    #endregion

    #region Propiedades
    public int DireccionRotacion
    {
        get => direccionRotacion;
        set
        {
            if (value < (int)AuxiliarModulos.DireccionRotacion.SinRotar || value > (int)AuxiliarModulos.DireccionRotacion.Antihorario)
            {
                throw new System.ArgumentOutOfRangeException(
                      $"{nameof(value)} debe ser un valor entre 0 y 2. 0) sin rotación, 1) Horario, 2) Antihorario");
            }
            direccionRotacion = value;
        }
    }

    public float VelocidadRotacionActual
    {
        get => velocidadRotacionActual;
        set
        {
            if (value < velocidadMinimaRotacion || value > velocidadMaximaRotacion)
            {
                throw new System.ArgumentOutOfRangeException(
                      $"{nameof(value)}  valuedebe ser un valor entre: " + velocidadMinimaRotacion + " y " 
                      + velocidadMaximaRotacion + ", El valor actual es: " + value);
            }
            velocidadRotacionActual = value;
        }
    }

    public float VoltajeActual
    {
        get => voltajeActual;
        set
        {
            if(value == 0)
            {
                voltajeActual = value;
            }
            else
            if (value < voltajeMinimo || value > voltajeMaximo)
            {
                /*throw new System.ArgumentOutOfRangeException(
                      $"{nameof(value)} debe ser un valor entre: " + voltajeMinimo + " y "
                      + voltajeMaximo);*/
                Debug.LogError($"{nameof(value)} debe ser un valor entre: " + voltajeMinimo + " y "
                      + voltajeMaximo);
            }
            voltajeActual = value;
        }
    }
    #endregion

    #region Inicializacion
    private void Awake()
    {
        particleError = new ParticlesError();

        InicializarComponentes(gameObject);
        ActualizarPanelInfo();
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
            }else if (child.name.Contains("caja_electrica"))
            {
                cajaElectrica = child;
            }
            InicializarComponentes(child);
        }
    }
    #endregion

    #region Comportamiento Modulo
    // Update is called once per frame
    void Update()
    {
        if (motorEncendido)
        {
            //Hacer algo si el modulo esta encendido.
            if (rotaMotorPrueba)
            {
                RotarEjeMotor();
                //ejeMotor.transform.Rotate(Vector3.right, -velocidadRotacionActual * Time.deltaTime);
            }
        }
        else
        {
            //Hacer algo si el modulo esta apagado.
        }
    }

    public void RotarEjeMotor()
    {
        if (ejeMotor != null)
        {
            if(voltajeActual > voltajeMinimo && voltajeActual < voltajeMaximo)
            {
                float velo = velocidadRotacionActual/2;
                if (direccionRotacion == (int)AuxiliarModulos.DireccionRotacion.SinRotar) //Sin rotacion
                {
                    //Sin rotacion
                }
                else
                if (direccionRotacion == (int)AuxiliarModulos.DireccionRotacion.Horario) //Rotacion derecha - Sentido horario
                {
                    ejeMotor.transform.Rotate(Vector3.right, velo * Time.deltaTime);
                }
                else if (direccionRotacion == (int)AuxiliarModulos.DireccionRotacion.Antihorario)// Rotacion Sentido antiorario
                {
                    ejeMotor.transform.Rotate(Vector3.left, velo * Time.deltaTime);
                }
                else
                {
                    Debug.LogError("Error." + name + ": Se establecio un tipo de rotacion del motor no permitida.");
                }
                QuitarAveria();
            }
            else
            {
                CrearAveria();
            }
            /*if (valorActualPerilla >= valorMinimoPerilla && valorActualPerilla <= valorMaximoPerilla)
            {
                //ejeMotor.transform.rotation = originalRotationKnob;
                //float valorRotacionGrados = (limiteGiroSuperiorPerilla * valorActualPerilla) / valorMaximoPerilla;
                //Debug.Log("Modulo 6: valorRotacionGrados: " + valorRotacionGrados + ", valorActualPerilla: " + valorActualPerilla + ", limiteGiroSuperiorPerilla: " + limiteGiroSuperiorPerilla + ", valorMaximoPerilla: " + valorMaximoPerilla);
                ejeMotor.transform.Rotate(0, 0, velocidadRotacionActual);
            }
            else
            {
                Debug.LogError("Error." + name + " Modulo 6: rotarPerilla(float valorActual): El valor actual recibido sobrepasa los limites establecidos");
            }*/
        }
        else
        {
            Debug.LogError(this.name + ", void RotarEjeMotor() - ejeMotor es nulo.");
        }
    }

    public void EncenderMotor()
    {
        motorEncendido = true;
        ActualizarPanelInfo();
    }

    public void ApagarMotor()
    {
        motorEncendido = false;
        ActualizarPanelInfo();
    }

    public void ReiniciarMotor()
    {
        ApagarMotor();
        DireccionRotacion = 0;
        VelocidadRotacionActual = 0;
        VoltajeActual = 0;
        ActualizarPanelInfo();
    }

    public void EstablecerParametrosMotor(bool encenderMotor, int directRotacion, float voltaje)
    {
        if (encenderMotor)
        {
            EncenderMotor();
        }
        else
        {
            ApagarMotor();
        }
        DireccionRotacion = directRotacion;
        VelocidadRotacionActual = CalcularVelocidadDeReotacion();
        VoltajeActual = voltaje;
        ActualizarPanelInfo();
    }

    public float CalcularVelocidadDeReotacion()
    {
        float veloRotacion2;
        if (VoltajeActual != 0)
        {
            veloRotacion2 = ((velocidadMaximaRotacion) / (voltajeMaximo - voltajeMinimo)) * (VoltajeActual - voltajeMinimo);
        }
        else
        {
            veloRotacion2 = 0;
        }
        return veloRotacion2;
    }

    public void ActualizarPanelInfo()
    {
        if(panelInformativo != null)
        {
            MotorStatePanel motorState = panelInformativo.GetComponent<MotorStatePanel>();
            if (motorState != null)
            {
                motorState.EstablecerTextoVoltajeMotor(voltajeActual);
                motorState.EstablecerTextoVeloMotor(velocidadRotacionActual);
                motorState.EstablecerTextoDireccionGiroMotor(direccionRotacion);
                if (!motorAveriado)
                {
                    motorState.EstablecerTextoEstadoMotor(!motorAveriado);
                }
                else
                {
                    motorState.EstablecerTextoEstadoMotor(motorAveriado, "Descripcion de falla de pueba");
                }
            }
            else
            {
                Debug.LogError(this.name + ", void ActualizarPanelInfo() - motorState es nulo.");
            }
        }
        else
        {
            Debug.LogError(this.name + ", void ActualizarPanelInfo() - panelInformativo es nulo.");
        }
    }

    public void CrearAveria()
    {
        if (!motorAveriado)
        {
            currentParticle = particleError.CrearParticulasError(currentTypeParticleError, cajaElectrica.transform.position, 
                cajaElectrica.transform.rotation.eulerAngles, new Vector3(5.5f, 5.5f, 5.5f));
            currentParticle.transform.parent = this.gameObject.transform;
            motorAveriado = true;
        }
        ActualizarPanelInfo();
    }

    public void QuitarAveria()
    {
        if (currentParticle != null)
        {
            particleError.DestruirParticulasError(currentParticle);
            motorAveriado = false;
        }
        ActualizarPanelInfo();
    }
    #endregion
}
