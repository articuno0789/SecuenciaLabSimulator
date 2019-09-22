﻿using System.Text.RegularExpressions;
using UnityEngine;

public class Plugs : MonoBehaviour
{
    #region Atributos
    public GameObject padreTotalComponente;
    public bool estaConectado;
    public int tipoConexion = 0; //1 - Linea, 0 - Sin conexion, 2 - Neutro
    public float voltaje = 0;
    public int numeroDeLinea = 0; //0 - sin linea, 1 - primera linea, 2 - segunda linea, 3 - tercera linea
    public int tipoNodo = 1; // 0 - poder, 1 - intermedio, 2 - final
    GameObject padreTotalBuscado;
    public GameObject plugRelacionado = null;
    public bool relacionCerrada = false;

    //Particulas de error
    public GameObject currentParticle;
    private ParticlesError particleError;
    public int currentTypeParticleError = 2;
    public bool plugEncendido = false;
    public bool plugAveriado = false;
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

    public bool PlugAveriado
    {
        get => plugAveriado;
        set => plugAveriado = value;
    }

    public bool PlugEncendido
    {
        get => plugEncendido;
        set => plugEncendido = value;
    }

    public int Linea
    {
        get => numeroDeLinea;
        set => numeroDeLinea = value;
    }

    public bool Conectado
    {
        get => estaConectado;
        set {
            estaConectado = value;
        }
    }

    public int TipoConexion
    {
        get => tipoConexion;
        set
        {
            if (value < 0 || value > 2)
            {
                throw new System.ArgumentOutOfRangeException(
                      $"{nameof(value)} debe ser un valor entre 0 y 2. 0 sin conexión, 1 - Línea, 2 - Neutro.");
            }
            /*if (value == 1 || value == 2)
            {
                estaConectado = true;
            }
            else*/ /*if (value == 0)
            {
                estaConectado = false;
            }*/
            tipoConexion = value;
        }
    }

    public float Voltaje
    {
        get => voltaje;
        set => voltaje = value;
    }
    #endregion

    public void EstablecerRelacionCerrado(bool cerrada)
    {
        if (plugRelacionado != null)
        {
            Plugs plugRela = plugRelacionado.GetComponent<Plugs>();
            if (cerrada)
            {
                plugRela.EstablecerPropiedadesConexionesEntrantesPrueba();
            }
            else
            {
                plugRela.EliminarPropiedadesConexionesEntradaPrueba();
            }
            relacionCerrada = cerrada;
            if (plugRela.relacionCerrada != relacionCerrada)
            {
                plugRela.relacionCerrada = relacionCerrada;
            }
        }
    }

    public bool ComprobarEstado(Plugs plugArriba, Plugs plugAbajo, bool plugAbierto)
    {
        bool estaCorrectaConexion = true;
        Plugs plugArribaCompPlug = plugArriba;
        Plugs plugAbajoCompPlug = plugAbajo;
        if (!plugAbierto)
        {
            if (plugArribaCompPlug != null && plugAbajoCompPlug != null)
            {
                plugArribaCompPlug.EstablecerPropiedadesConexionesEntrantes();
                plugAbajoCompPlug.EstablecerPropiedadesConexionesEntrantes();

                if (plugArribaCompPlug.Conectado && plugAbajoCompPlug.Conectado)
                {
                    if (plugArribaCompPlug.TipoConexion == 1 && plugAbajoCompPlug.TipoConexion == 1 && plugArribaCompPlug.Linea != plugAbajoCompPlug.Linea)// Correcto - Linea y linea, las dos lineas son diferentes
                    {
                        //El mismo plug que llama y el primer plug
                        plugArribaCompPlug.PlugAveriado = true;

                        //El segundo plug involucrado
                        plugAbajoCompPlug.PlugAveriado = true;
                        plugAbajoCompPlug.ComprobarEstadoAveria();

                        estaCorrectaConexion = false;
                        PlugAveriado = true;
                        if (DebugMode)
                        {
                            Debug.Log(padreTotalComponente.name + ") " + this.name + " - Plug - plugArribaCompPlug.TipoConexion == 1 && plugAbajoCompPlug.TipoConexion == 1 && plugArribaCompPlug.Linea != plugAbajoCompPlug.Linea");
                        }
                    }
                    else
                    {
                        //El mismo plug que llama y el primer plug
                        plugArribaCompPlug.PlugAveriado = false;

                        //El segundo plug involucrado
                        plugAbajoCompPlug.PlugAveriado = false;
                        plugAbajoCompPlug.ComprobarEstadoAveria();

                        estaCorrectaConexion = true;
                        PlugAveriado = false;
                        if (DebugMode)
                        {
                            Debug.Log(padreTotalComponente.name + ") " + this.name + " - Plug - plugArribaCompPlug.TipoConexion == 1 && plugAbajoCompPlug.TipoConexion == 1 && plugArribaCompPlug.Linea != plugAbajoCompPlug.Linea - ELSE TODO BIEN");
                        }
                    }
                }
                else
                {
                    //El mismo plug que llama y el primer plug
                    plugArribaCompPlug.PlugAveriado = false;

                    //El segundo plug involucrado
                    plugAbajoCompPlug.PlugAveriado = false;
                    plugAbajoCompPlug.ComprobarEstadoAveria();

                    estaCorrectaConexion = false;
                    PlugAveriado = false;
                    //ApagarFoco();
                    if (DebugMode)
                    {
                        Debug.Log(padreTotalComponente.name + ") " + this.name + " - PLUG - if (plugIzquierdoCompPlug.Conectado && plugDerechoCompPlug.Conectado) - NO esta conectados");
                    }
                }
            }
            else
            {
                estaCorrectaConexion = false;
                Debug.LogError(padreTotalComponente.name + ") " + this.name + " - PLUG - if(plugArribaCompPlug != null && plugAbajoCompPlug != null) - Alguno de los dos es nulo, plugArribaCompPlug: " + plugArribaCompPlug + ", plugAbajoCompPlug: " + plugAbajoCompPlug);
            }
        }
        else
        {
            //El mismo plug que llama y el primer plug
            plugArribaCompPlug.PlugAveriado = false;

            //El segundo plug involucrado
            plugAbajoCompPlug.PlugAveriado = false;
            plugAbajoCompPlug.ComprobarEstadoAveria();

            estaCorrectaConexion = false;
            PlugAveriado = false;
            //ApagarFoco();
            if (DebugMode)
            {
                Debug.Log(padreTotalComponente.name + ") " + this.name + " - PLUG - Los plug estan abiertos, es decir, no conectados.");
            }
        }
        ComprobarEstadoAveria();
        return estaCorrectaConexion;
    }

    public bool ComprobarEstado1Y15(Plugs plugArriba, Plugs plugAbajo, bool plugAbierto)
    {
        bool estaCorrectaConexion = true;
        Plugs plugArribaCompPlug = plugArriba;
        Plugs plugAbajoCompPlug = plugAbajo;
        if (!plugAbierto)
        {
            if (plugArribaCompPlug != null && plugAbajoCompPlug != null)
            {
                /*if(plugArribaCompPlug.padreTotalComponente.tag != "1" || plugArribaCompPlug.padreTotalComponente.tag != "15")
                {
                    plugArribaCompPlug.EstablecerPropiedadesConexionesEntrantes();
                }
                if (plugAbajoCompPlug.padreTotalComponente.tag != "1" || plugAbajoCompPlug.padreTotalComponente.tag != "15")
                {
                    plugAbajoCompPlug.EstablecerPropiedadesConexionesEntrantes();
                }*/
                if (plugArribaCompPlug.Conectado && plugAbajoCompPlug.Conectado)
                {
                    if (plugArribaCompPlug.TipoConexion == 1 && plugAbajoCompPlug.TipoConexion == 1 
                        && plugArribaCompPlug.Linea != plugAbajoCompPlug.Linea)// Correcto - Linea y linea, las dos lineas son diferentes
                    {
                        //El mismo plug que llama y el primer plug
                        plugArribaCompPlug.PlugAveriado = true;

                        //El segundo plug involucrado
                        plugAbajoCompPlug.PlugAveriado = true;
                        plugAbajoCompPlug.ComprobarEstadoAveria();

                        estaCorrectaConexion = false;
                        PlugAveriado = true;
                        if (DebugMode)
                        {
                            Debug.Log(padreTotalComponente.name + ") " + this.name + " - Plug - plugArribaCompPlug.TipoConexion == 1 && plugAbajoCompPlug.TipoConexion == 1 && plugArribaCompPlug.Linea != plugAbajoCompPlug.Linea");
                        }
                    }
                    else
                    {
                        //El mismo plug que llama y el primer plug
                        plugArribaCompPlug.PlugAveriado = false;

                        //El segundo plug involucrado
                        plugAbajoCompPlug.PlugAveriado = false;
                        plugAbajoCompPlug.ComprobarEstadoAveria();

                        estaCorrectaConexion = true;
                        PlugAveriado = false;
                        if (DebugMode)
                        {
                            Debug.Log(padreTotalComponente.name + ") " + this.name + " - Plug - plugArribaCompPlug.TipoConexion == 1 &&" +
                                " plugAbajoCompPlug.TipoConexion == 1 && " +
                                "plugArribaCompPlug.Linea != plugAbajoCompPlug.Linea" +
                                " - ELSE TODO BIEN, plugArribaCompPlug.Linea: " + plugArribaCompPlug.Linea + ", plugAbajoCompPlug.Linea: " + plugAbajoCompPlug.Linea);
                        }
                    }
                }
                else
                {
                    //El mismo plug que llama y el primer plug
                    plugArribaCompPlug.PlugAveriado = false;

                    //El segundo plug involucrado
                    plugAbajoCompPlug.PlugAveriado = false;
                    plugAbajoCompPlug.ComprobarEstadoAveria();

                    estaCorrectaConexion = false;
                    PlugAveriado = false;
                    //ApagarFoco();
                    if (DebugMode)
                    {
                        Debug.Log(padreTotalComponente.name + ") " + this.name + " - PLUG - if (plugIzquierdoCompPlug.Conectado && plugDerechoCompPlug.Conectado) - NO esta conectados");
                    }
                }
            }
            else
            {
                estaCorrectaConexion = false;
                Debug.LogError(padreTotalComponente.name + ") " + this.name + " - PLUG - if(plugArribaCompPlug != null && plugAbajoCompPlug != null) - Alguno de los dos es nulo, plugArribaCompPlug: " + plugArribaCompPlug + ", plugAbajoCompPlug: " + plugAbajoCompPlug);
            }
        }
        else
        {
            //El mismo plug que llama y el primer plug
            plugArribaCompPlug.PlugAveriado = false;

            //El segundo plug involucrado
            plugAbajoCompPlug.PlugAveriado = false;
            plugAbajoCompPlug.ComprobarEstadoAveria();

            estaCorrectaConexion = false;
            PlugAveriado = false;
            //ApagarFoco();
            if (DebugMode)
            {
                Debug.Log(padreTotalComponente.name + ") " + this.name + " - PLUG - Los plug estan abiertos, es decir, no conectados.");
            }
        }
        ComprobarEstadoAveria();
        return estaCorrectaConexion;
    }


    void ComprobarEstadoAveria()
    {
        if (plugAveriado)
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
        if(currentParticle == null)
        {
            currentParticle = particleError.CrearParticulasError(currentTypeParticleError, transform.position, transform.rotation.eulerAngles, new Vector3(0.5f, 0.5f, 0.5f));
            currentParticle.transform.parent = this.gameObject.transform;
            PlugAveriado = true;
        }
    }

    public void QuitarAveria()
    {
        if(currentParticle != null)
        {
            particleError.DestruirParticulasError(currentParticle);
            PlugAveriado = false;
        }
    }

    void Awake()
    {
        padreTotalBuscado = null;

        //Particulas de error
        particleError = new ParticlesError();
        padreTotalComponente = new GameObject();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool VoltajeValido(float voltajeMinimo, float voltajeMaximo)
    {
        if(voltaje >= voltajeMinimo && voltaje <= voltajeMaximo)
        {
            return true;
        }
        return true;
    }

    public bool ComprobaTipoLinea(int tipoLineaComprobar)
    {
        if (tipoLineaComprobar < 1 || tipoLineaComprobar > 3)
        {
            return false;
        }else if(Linea != tipoLineaComprobar)
        {
            return false;
        }
        else //Linea == tipoLineaComprobar
        {
            return true;
        }
    }

    public bool EstaSinConexion()
    {
        if(tipoConexion == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void EstablecerValoresNoConexion()
    {
        Conectado = false;
        TipoConexion = 0;
        Voltaje = 0;
        Linea = 0;
    }

    public void EstablecerValoresNoConexion3(Plugs desconectar)
    {
        Conectado = false;
        TipoConexion = 0;
        Voltaje = 0;
        Linea = 0;
        desconectar.EstablecerValoresNoConexion();
    }

    public void EstablecerValoresNoConexion2()
    {
        Conectado = false;
        TipoConexion = 0;
        Voltaje = 0;
        Linea = 0;
        CableComponent cableComp = this.GetComponent<CableComponent>();
        if (cableComp != null)
        {
            GameObject conexionEntrante = cableComp.EndPoint;
            if (conexionEntrante != null)
            {
                Plugs plugConexionEntrante = conexionEntrante.GetComponent<Plugs>();
                EncontrarPadreTotal(conexionEntrante);
                if (plugConexionEntrante != null && padreTotalBuscado.tag != "1" && padreTotalBuscado.tag != "15")//Se excluyen los modulos que dan energia, ya que estan conectados a una fuente
                {
                    //Debug.Log(padreTotalBuscado.tag);
                    plugConexionEntrante.Conectado = false;
                    plugConexionEntrante.TipoConexion = 0;
                    plugConexionEntrante.Voltaje = 0;
                    plugConexionEntrante.Linea = 0;
                    //plugConexionEntrante.EstablecerValoresNoConexion();
                }
            }
        }
    }

    public bool estoConectado()
    {
        CableComponent cableComp = this.GetComponent<CableComponent>();
        if (cableComp != null)
        {
            GameObject conexionEntrante = cableComp.EndPoint;
            if (conexionEntrante != null)
            {
                Plugs plugConexionEntrante = conexionEntrante.GetComponent<Plugs>();
                if (plugConexionEntrante != null)
                {
                    Conectado = true;
                    return true;
                }
                else
                {
                    Conectado = false;
                    return false;
                }
            }
            else
            {
                Conectado = false;
                return false;
            }
        }
        else
        {
            Conectado = false;
            return false;
        }
    }

    public Plugs RegresarConexionEntrante()
    {
        Plugs conexionEntrantePlug = null;
        CableComponent cableComp = this.GetComponent<CableComponent>();
        if (cableComp != null)
        {
            GameObject conexionEntrante = cableComp.EndPoint;
            if (conexionEntrante != null)
            {
                conexionEntrantePlug = conexionEntrante.GetComponent<Plugs>();
            }
            else
            {
                conexionEntrantePlug = null;
            }
        }
        else
        {
            conexionEntrantePlug = null;
        }
        return conexionEntrantePlug;
    }

    public void EliminarPropiedadesConexionesEntradaPrueba()
    {
        CableComponent cableComp = this.GetComponent<CableComponent>();
        if (tipoNodo != 0) //Diferente a nodo de poder
        {//Si es nodo final o intermedio entra aqui
            if (cableComp != null)
            {
                GameObject conexionEntrante = cableComp.EndPoint;
                if (conexionEntrante != null)
                {
                    Plugs plugConexionEntrante = conexionEntrante.GetComponent<Plugs>();
                    if (plugConexionEntrante != null)//&& !plugConexionEntrante.PlugFinal&& plugConexionEntrante.tipoNodo != 2
                    {
                        //Conectado = false;
                        TipoConexion = 0;
                        Voltaje = 0;
                        Linea = 0;
                        if (relacionCerrada)
                        {
                            if (plugRelacionado != null)
                            {
                                Plugs plugRela = plugRelacionado.GetComponent<Plugs>();
                                CableComponent plugRelaCable = plugRela.GetComponent<CableComponent>();
                                plugRela.TipoConexion = TipoConexion;
                                plugRela.Voltaje = Voltaje;
                                plugRela.Linea = Linea;
                                if (plugRela.Conectado)// && plugRelaCable.EndPoint!= null
                                {
                                    Plugs plugRelaCablePlug = plugRelaCable.EndPoint.GetComponent<Plugs>();
                                    if (ComprobarCorto(plugRela, plugRelaCablePlug))
                                    {
                                        plugRelaCablePlug.EliminarPropiedadesConexionesEntradaPrueba();
                                    }
                                    else
                                    {
                                        Debug.LogError("Desconexion-Hubo un horrible corto - En la prubas");
                                    }
                                }

                            }
                        }
                    }
                    else
                    {
                        EstablecerValoresNoConexion();
                    }
                }
                else
                {
                    EstablecerValoresNoConexion();
                }
            }
            else
            {
                EstablecerValoresNoConexion();
            }
        }
    }

    public void EstablecerPropiedadesConexionesEntrantesPrueba()
    {
        CableComponent cableComp = this.GetComponent<CableComponent>();
        if (tipoNodo != 0) //Diferente a nodo de poder
        {//Si es nodo final o intermedio entra aqui
            if (cableComp != null)
            {
                GameObject conexionEntrante = cableComp.EndPoint;
                if (conexionEntrante != null)
                {
                    Plugs plugConexionEntrante = conexionEntrante.GetComponent<Plugs>();
                    if (plugConexionEntrante != null)//&& !plugConexionEntrante.PlugFinal&& plugConexionEntrante.tipoNodo != 2
                    {
                        //Conectado = true;
                        TipoConexion = plugConexionEntrante.TipoConexion;
                        Voltaje = plugConexionEntrante.Voltaje;
                        Linea = plugConexionEntrante.Linea;
                        if (relacionCerrada)
                        {
                            if (plugRelacionado != null)
                            {
                                Plugs plugRela = plugRelacionado.GetComponent<Plugs>();
                                CableComponent plugRelaCable = plugRela.GetComponent<CableComponent>();
                                plugRela.TipoConexion = TipoConexion;
                                plugRela.Voltaje = Voltaje;
                                plugRela.Linea = Linea;
                                if (plugRela.Conectado)// && plugRelaCable.EndPoint!= null
                                {
                                    Plugs plugRelaCablePlug = plugRelaCable.EndPoint.GetComponent<Plugs>();
                                    if (ComprobarCorto(plugRela, plugRelaCablePlug))
                                    {
                                        plugRelaCablePlug.EstablecerPropiedadesConexionesEntrantesPrueba();
                                    }
                                    else
                                    {
                                        Debug.LogError("Hubo un horrible corto - En la sprubas");
                                    }
                                }
                                
                            }
                        }
                    }
                    else
                    {
                        EstablecerValoresNoConexion();
                    }
                }
                else
                {
                    EstablecerValoresNoConexion();
                }
            }
            else
            {
                EstablecerValoresNoConexion();
            }
        }
    }

    public bool ComprobarCorto(Plugs primerPlug, Plugs segundoPlug)
    {
        bool estaCorrectaConexion = true;
        Plugs primerPlugCompPlug = primerPlug;
        Plugs segundoPlugCompPlug = segundoPlug;
            if (primerPlugCompPlug != null && segundoPlugCompPlug != null)
            {
                //plugArribaCompPlug.EstablecerPropiedadesConexionesEntrantes();
                //plugAbajoCompPlug.EstablecerPropiedadesConexionesEntrantes();
                if (primerPlugCompPlug.Conectado && segundoPlugCompPlug.Conectado)
                {
                    if (primerPlugCompPlug.TipoConexion == 1 && segundoPlugCompPlug.TipoConexion == 1 && primerPlugCompPlug.Linea != segundoPlugCompPlug.Linea)// Correcto - Linea y linea, las dos lineas son diferentes
                    {
                        //El mismo plug que llama y el primer plug
                        primerPlugCompPlug.PlugAveriado = true;

                        //El segundo plug involucrado
                        segundoPlugCompPlug.PlugAveriado = true;
                        segundoPlugCompPlug.ComprobarEstadoAveria();

                        estaCorrectaConexion = false;
                        PlugAveriado = true;
                        if (DebugMode)
                        {
                            Debug.Log(padreTotalComponente.name + ") " + this.name + " - Plug - plugArribaCompPlug.TipoConexion == 1 && plugAbajoCompPlug.TipoConexion == 1 && plugArribaCompPlug.Linea != plugAbajoCompPlug.Linea");
                        }
                    }
                    else
                    {
                        //El mismo plug que llama y el primer plug
                        primerPlugCompPlug.PlugAveriado = false;

                        //El segundo plug involucrado
                        segundoPlugCompPlug.PlugAveriado = false;
                        segundoPlugCompPlug.ComprobarEstadoAveria();

                        estaCorrectaConexion = true;
                        PlugAveriado = false;
                        if (DebugMode)
                        {
                            Debug.Log(padreTotalComponente.name + ") " + this.name + " - Plug - plugArribaCompPlug.TipoConexion == 1 && plugAbajoCompPlug.TipoConexion == 1 && plugArribaCompPlug.Linea != plugAbajoCompPlug.Linea - ELSE TODO BIEN");
                        }
                    }
                }
                else
                {
                    //El mismo plug que llama y el primer plug
                    primerPlugCompPlug.PlugAveriado = false;

                    //El segundo plug involucrado
                    segundoPlugCompPlug.PlugAveriado = false;
                    segundoPlugCompPlug.ComprobarEstadoAveria();

                    estaCorrectaConexion = false;
                    PlugAveriado = false;
                    //ApagarFoco();
                    if (DebugMode)
                    {
                        Debug.Log(padreTotalComponente.name + ") " + this.name + " - PLUG - if (plugIzquierdoCompPlug.Conectado && plugDerechoCompPlug.Conectado) - NO esta conectados");
                    }
                }
            }
            else
            {
                estaCorrectaConexion = false;
                Debug.LogError(padreTotalComponente.name + ") " + this.name + " - PLUG - if(plugArribaCompPlug != null && plugAbajoCompPlug != null) - Alguno de los dos es nulo, plugArribaCompPlug: " + primerPlugCompPlug + ", plugAbajoCompPlug: " + segundoPlugCompPlug);
            }
        ComprobarEstadoAveria();
        return estaCorrectaConexion;
    }


    public void EstablecerPropiedadesConexionesEntrantes()
    {
        CableComponent cableComp = this.GetComponent<CableComponent>();
        if (cableComp != null)
        {
            GameObject conexionEntrante = cableComp.EndPoint;
            if (conexionEntrante != null)
            {
                Plugs plugConexionEntrante = conexionEntrante.GetComponent<Plugs>();
                if (plugConexionEntrante != null)//&& !plugConexionEntrante.PlugFinal&& plugConexionEntrante.tipoNodo != 2
                {
                    Conectado = true;
                    TipoConexion = plugConexionEntrante.TipoConexion;
                    Voltaje = plugConexionEntrante.Voltaje;
                    Linea = plugConexionEntrante.Linea;
                }
                else
                {
                    EstablecerValoresNoConexion();
                }
            }
            else
            {
                EstablecerValoresNoConexion();
            }
        }
        else
        {
            EstablecerValoresNoConexion();
        }
    }

    public void EstablecerPropiedadesConexionesEntrantes(GameObject conexionQueEnergiza)
    {
        CableComponent cableComp = conexionQueEnergiza.GetComponent<CableComponent>();
        if (cableComp != null)
        {
            GameObject conexionEntrante = cableComp.EndPoint;
            if (conexionEntrante != null)
            {
                Plugs plugConexionEntrante = conexionEntrante.GetComponent<Plugs>();
                if (plugConexionEntrante != null)
                {
                    //Conectado = true;
                    TipoConexion = plugConexionEntrante.TipoConexion;
                    Voltaje = plugConexionEntrante.Voltaje;
                    Linea = plugConexionEntrante.Linea;
                }
                else
                {
                    EstablecerValoresNoConexion();
                }
            }
            else
            {
                EstablecerValoresNoConexion();
            }
        }
        else
        {
            EstablecerValoresNoConexion();
        }
    }

    void CrearConexionPlugs(bool enDestruccion = false)
    {
        CableComponent cable = this.GetComponent<CableComponent>();
        if(cable != null)
        {
            if(cable.startPoint != null)
            {
                string startPoint = padreTotalComponente.name + "|" + cable.startPoint.name;
                Plugs endPlug;
                string endPoint;
                if (cable.endPoint != null)
                {
                    endPlug = cable.endPoint.GetComponent<Plugs>();
                    endPoint = endPlug.padreTotalComponente.name + "|" + cable.endPoint.name;
                    if (enDestruccion)
                    {
                        ComprobarEliminarConexion(endPlug.GetComponent<CableComponent>(), cable.endPoint);
                        padreTotalBuscado = null;
                        startPoint = endPoint;
                        endPoint = "";
                        Debug.LogError("Entra a destrucción");
                        CrearConexionPorTipo(startPoint, endPoint, endPlug.padreTotalComponente);
                    }
                }
                else
                {
                    endPoint = "";
                }
                if (!enDestruccion)
                {
                    CrearConexionPorTipo(startPoint, endPoint, padreTotalComponente);
                }
            }
        }
    }

    private void EncontrarPadreTotal(GameObject nodo)
    {
        if (nodo.transform.parent == null)
        {
            padreTotalBuscado = nodo;
        }
        else
        {
            GameObject padre = nodo.transform.parent.gameObject;
            EncontrarPadreTotal(padre);
        }
    }

    private void CrearConexionPorTipo(string startPoint, string endPoint, GameObject padreTotalComp)
    {
        if (Regex.IsMatch(padreTotalComp.name, @"^1_\d*$"))
        {
            Modulo1 mod1 = padreTotalComp.GetComponent<Modulo1>();
            mod1.CrearConexionPlugs(startPoint, endPoint);
        }
        else
if (Regex.IsMatch(padreTotalComp.name, @"^2_\d*$"))
        {
            Modulo2 mod2 = padreTotalComp.GetComponent<Modulo2>();
            mod2.CrearConexionPlugs(startPoint, endPoint);
        }
        else
if (Regex.IsMatch(padreTotalComp.name, @"^3_\d*$"))
        {
            Modulo3 mod3 = padreTotalComp.GetComponent<Modulo3>();
            mod3.CrearConexionPlugs(startPoint, endPoint);
        }
        else
if (Regex.IsMatch(padreTotalComp.name, @"^4_\d*$"))
        {
            Modulo4 mod4 = padreTotalComp.GetComponent<Modulo4>();
            mod4.CrearConexionPlugs(startPoint, endPoint);
        }
        else
if (Regex.IsMatch(padreTotalComp.name, @"^5_\d*$"))
        {
            Modulo5 mod5 = padreTotalComp.GetComponent<Modulo5>();
            mod5.CrearConexionPlugs(startPoint, endPoint);
        }
        else
if (Regex.IsMatch(padreTotalComp.name, @"^6_\d*$"))
        {
            Modulo6 mod6 = padreTotalComp.GetComponent<Modulo6>();
            mod6.CrearConexionPlugs(startPoint, endPoint);
        }
        else
if (Regex.IsMatch(padreTotalComp.name, @"^7_\d*$"))
        {
            Modulo7 mod7 = padreTotalComp.GetComponent<Modulo7>();
            mod7.CrearConexionPlugs(startPoint, endPoint);
        }
        else
if (Regex.IsMatch(padreTotalComp.name, @"^8, 11_\d*$"))
        {
            Modulo8_11 mod8_11 = padreTotalComp.GetComponent<Modulo8_11>();
            mod8_11.CrearConexionPlugs(startPoint, endPoint);
        }
        else
if (Regex.IsMatch(padreTotalComp.name, @"^9_\d*$"))
        {
            Modulo9 mod9 = padreTotalComp.GetComponent<Modulo9>();
            mod9.CrearConexionPlugs(startPoint, endPoint);
        }
        else
if (Regex.IsMatch(padreTotalComp.name, @"^10, 17, 18, 19_\d*$"))
        {
            Modulo10_17_18_19 mod10_17_18_19 = padreTotalComp.GetComponent<Modulo10_17_18_19>();
            mod10_17_18_19.CrearConexionPlugs(startPoint, endPoint);
        }
        else
if (Regex.IsMatch(padreTotalComp.name, @"^13_\d*$"))
        {
            Modulo13 mod13 = padreTotalComp.GetComponent<Modulo13>();
            mod13.CrearConexionPlugs(startPoint, endPoint);
        }
        else
if (Regex.IsMatch(padreTotalComp.name, @"^14, 16_\d*$"))
        {
            Modulo14_16 mod14_16 = padreTotalComp.GetComponent<Modulo14_16>();
            mod14_16.CrearConexionPlugs(startPoint, endPoint);
        }
        else
if (Regex.IsMatch(padreTotalComp.name, @"^15_\d*$"))
        {
            Modulo15 mod15 = padreTotalComp.GetComponent<Modulo15>();
            mod15.CrearConexionPlugs(startPoint, endPoint);
        }
        else
if (Regex.IsMatch(padreTotalComp.name, @"^20_\d*$"))
        {
            Modulo20 mod20 = padreTotalComp.GetComponent<Modulo20>();
            mod20.CrearConexionPlugs(startPoint, endPoint);
        }
        else
if (Regex.IsMatch(padreTotalComp.name, @"^21_\d*$"))
        {
            Modulo21 mod21 = padreTotalComp.GetComponent<Modulo21>();
            mod21.CrearConexionPlugs(startPoint, endPoint);
        }
        else
if (Regex.IsMatch(padreTotalComp.name, @"^22, 23_\d*$"))
        {
            Modulo22_23 mod22_23 = padreTotalComp.GetComponent<Modulo22_23>();
            mod22_23.CrearConexionPlugs(startPoint, endPoint);
        }
        else
if (Regex.IsMatch(padreTotalComp.name, @"^22_\d*$"))
        {
            Modulo22_23 mod22_23 = padreTotalComp.GetComponent<Modulo22_23>();
            mod22_23.CrearConexionPlugs(startPoint, endPoint);
        }
        else
if (Regex.IsMatch(padreTotalComp.name, @"^23_\d*$"))
        {
            Modulo22_23 mod22_23 = padreTotalComp.GetComponent<Modulo22_23>();
            mod22_23.CrearConexionPlugs(startPoint, endPoint);
        }
        else
if (Regex.IsMatch(padreTotalComp.name, @"^Potenciometro__\d*$"))
        {
            Potenciometro modPotenciometro = padreTotalComp.GetComponent<Potenciometro>();
            modPotenciometro.CrearConexionPlugs(startPoint, endPoint);
        }
        else
        {
            Debug.LogError("padreTotalComponente.name DENTRO DE PLUGS NO PERTENECE A NINGUN MODULO CONOCIDO.");
        }
    }

    private bool ComprobarEliminarConexion(CableComponent cableCompStart, GameObject objectStart)
    {
        bool eliminarCable = false;
        if (cableCompStart.endPoint != null)//Si este valor es diferente a nulo, quiere decir que este plug ya tiene una conexión
        {
            eliminarCable = true;
            GameObject endPoint = cableCompStart.endPoint;
            CableComponent cableCompLastEndPointStart = endPoint.GetComponent<CableComponent>();
            Debug.Log("GameObject endPoint = cableCompStart.endPoint;");
            cableCompLastEndPointStart.endPoint = null;
            cableCompStart.endPoint = null;
            cableCompLastEndPointStart.showRender = true;
            cableCompStart.showRender = true;

            //Desturir elementos Line Render
            LineRenderer lineRenderEndPoint = endPoint.GetComponent<LineRenderer>();
            if (lineRenderEndPoint != null)
            {
                Destroy(lineRenderEndPoint);
                endPoint.AddComponent<LineRenderer>();
            }

            LineRenderer lineRenderStartPoint = objectStart.GetComponent<LineRenderer>();
            if (lineRenderStartPoint != null)
            {
                Destroy(lineRenderStartPoint);
                endPoint.AddComponent<LineRenderer>();
            }
            //endPoint.GetComponent<Plugs>().EstablecerValoresNoConexion();
            //objectStart.GetComponent<Plugs>().EstablecerValoresNoConexion();
        }
        return eliminarCable;
    }

    private void OnDestroy()
    {
        CrearConexionPlugs(true);
    }
}
