using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class Plugs : MonoBehaviour
{
    #region Atributos
    public GameObject padreTotalComponente;
    public bool estaConectado;
    public int tipoConexion = 0; //1 - Linea, 0 - Sin conexion, 2 - Neutro
    public float voltaje = 0;
    public int numeroDeLinea = 0; //0 - sin linea, 1 - primera linea, 2 - segunda linea, 3 - tercera linea
    GameObject padreTotalBuscado;
    #endregion

    #region Propiedades

    public int Linea
    {
        get => numeroDeLinea;
        set => numeroDeLinea = value;
    }

    public bool Conectado
    {
        get => estaConectado;
        set => estaConectado = value;
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
            else*/ if (value == 0)
            {
                estaConectado = false;
            }
            tipoConexion = value;
        }
    }

    public float Voltaje
    {
        get => voltaje;
        set => voltaje = value;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        padreTotalBuscado = null;
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
                if (plugConexionEntrante != null && padreTotalBuscado.tag != "1" && padreTotalBuscado.tag != "15")
                {
                    Debug.Log(padreTotalBuscado.tag);
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

    public void EstablecerPropiedadesConexionesEntrantes()
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
        if (cableCompStart.endPoint != null)//Si este valor es diferente a nulo, kquiere decir que este plug ya tiene una conexión
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
        }
        return eliminarCable;
    }

    private void OnDestroy()
    {
        CrearConexionPlugs(true);
    }
}
