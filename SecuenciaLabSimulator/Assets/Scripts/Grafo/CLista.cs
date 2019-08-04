using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CLista : MonoBehaviour
{
    //----------------ATRIBUTOS-------------------------
    private CVertice aElemento;
    private CLista aSubLista;
    private int aPeso;
    public double ponderacionMombo;
    private double ponderacionPirolo;
    private double ponderacionLucas;
    private double montanaMombo;
    private double aguaMombo;
    private double barrancoMombo;
    private double planoMombo;
    private double montanaPirolo;
    private double aguaPirolo;
    private double barrancoPirolo;
    private double planoPirolo;
    private double montanaLucas;
    private double aguaLucas;
    private double barrancoLucas;
    private double planoLucas;

    //----------------CONSTRUCTORES---------------------
    public CLista()
    {
        aElemento = null;
        aSubLista = null;
        aPeso = 0;
        ponderacionMombo = int.MaxValue / 2;
        ponderacionPirolo = int.MaxValue / 2;
        ponderacionLucas = int.MaxValue / 2;

        montanaMombo = 2.5;
        aguaMombo = 0.3;
        barrancoMombo = 1.5;
        planoMombo = 1.0;

        montanaPirolo = 0.3;
        aguaPirolo = 2.5;
        barrancoPirolo = 1.0;
        planoPirolo = 1.5;

        montanaLucas = 1.5;
        aguaLucas = 1.0;
        barrancoLucas = 2.5;
        planoLucas = 0.3;
    }
    public CLista(CLista pLista)
    {
        if (pLista != null)
        {
            aElemento = pLista.aElemento;
            aSubLista = pLista.aSubLista;
            aPeso = pLista.aPeso;
        }
    }
    public const int TIPO_MURO = 9;
    public const int TIPO_MONTANA = 10;
    public const int TIPO_BARRANCO = 11;
    public const int TIPO_AGUA = 12;
    public const int TIPO_PLANO = 13;

    public double dameEsfuerzo(int personaje)
    {
        double esfuerzo = 0;
        //Console.WriteLine("public double dameEsfuerzo(int personaje)");
        switch (personaje)
        {
            case 1:
                //Console.WriteLine("MOMBO. Dentro dameEsfuerzo: aElemento: " + aElemento.nombre + ", Tipo: " + aElemento.tipoTerreno);
                switch (aElemento.tipoTerreno)
                {
                    case 9:
                        esfuerzo = -1;
                        break;
                    case 10:
                        esfuerzo = montanaMombo;
                        break;
                    case 11:
                        esfuerzo = barrancoMombo;
                        break;
                    case 12:
                        esfuerzo = aguaMombo;
                        break;
                    case 13:
                        esfuerzo = planoMombo;
                        break;
                    default:
                        esfuerzo = -2;
                        break;
                }
                break;
            case 2:
                //Console.WriteLine("PIROLO. Dentro dameEsfuerzo: aElemento: " + aElemento.nombre + ", Tipo: " + aElemento.tipoTerreno);
                switch (aElemento.tipoTerreno)
                {
                    case 9:
                        esfuerzo = -1;
                        break;
                    case 10:
                        esfuerzo = montanaPirolo;
                        break;
                    case 11:
                        esfuerzo = barrancoPirolo;
                        break;
                    case 12:
                        esfuerzo = aguaPirolo;
                        break;
                    case 13:
                        esfuerzo = planoPirolo;
                        break;
                    default:
                        esfuerzo = -2;
                        break;
                }
                break;
            case 3:
                //Console.WriteLine("LUCAS. Dentro dameEsfuerzo: aElemento: " + aElemento.nombre + ", Tipo: " + aElemento.tipoTerreno);
                switch (aElemento.tipoTerreno)
                {
                    case 9:
                        esfuerzo = -1;
                        break;
                    case 10:
                        esfuerzo = montanaLucas;
                        break;
                    case 11:
                        esfuerzo = barrancoLucas;
                        break;
                    case 12:
                        esfuerzo = aguaLucas;
                        break;
                    case 13:
                        esfuerzo = planoLucas;
                        break;
                    default:
                        esfuerzo = -2;
                        break;
                }
                break;
            default:
                break;
        }
        return esfuerzo;
    }

    public void ajustarPoderacion(int personaje, double ajuste)
    {
        switch (personaje)
        {
            case 1:
                ponderacionMombo = ponderacionMombo + ajuste;
                break;
            case 2:
                ponderacionPirolo = ponderacionPirolo + ajuste;
                break;
            case 3:
                ponderacionLucas = ponderacionLucas + ajuste;
                break;
            default:
                break;
        }
    }

    public CLista(CVertice pElemento, CLista pSubLista, int pPeso)
    {
        aElemento = pElemento;
        aSubLista = pSubLista;
        aPeso = pPeso;
    }
    //---------------PROPIEDADES------------------------
    public CVertice Elemento
    {
        get
        { return aElemento; }
        set
        { aElemento = value; }
    }
    public CLista SubLista
    {
        get
        { return aSubLista; }
        set
        { aSubLista = value; }
    }
    public int Peso
    {
        get
        { return aPeso; }
        set
        { aPeso = value; }
    }
    //------------------------METODOS-------------------
    public bool EsVacia()
    {
        return aElemento == null;
    }
    public void Agregar(CVertice pElemento, int pPeso)
    {
        if (pElemento != null)
        {
            if (aElemento == null)
            {
                aElemento = new CVertice(pElemento.nombre, pElemento.tipoTerreno);
                aPeso = pPeso;
                aSubLista = new CLista();
            }
            else
            {
                if (!ExisteElemento(pElemento))
                    aSubLista.Agregar(pElemento, pPeso);
            }
        }
    }
    public void Eliminar(CVertice pElemento)
    {
        if (aElemento != null)
        {
            if (aElemento.Equals(pElemento))
            {
                aElemento = aSubLista.aElemento;
                aSubLista = aSubLista.SubLista;
            }
            else
                aSubLista.Eliminar(pElemento);
        }
    }
    public int NroElementos()
    {
        if (aElemento != null)
            return 1 + aSubLista.NroElementos();
        else
            return 0;
    }
    public object IesimoElemento(int posicion)
    {
        if ((posicion > 0) && (posicion <= NroElementos()))
            if (posicion == 1)
                return aElemento;
            else
                return aSubLista.IesimoElemento(posicion - 1);
        else
            return null;
    }

    public object IesimoElementoPeso(int posicion)
    {
        if ((posicion > 0) && (posicion <= NroElementos()))
            if (posicion == 1)
                return aPeso;
            else
                return aSubLista.IesimoElementoPeso(posicion - 1);
        else
            return 0;
    }

    public object IesimoElementoPeso(int posicion, int personajeEntero)
    {
        double ponderacion = 0;
        if ((posicion > 0) && (posicion <= NroElementos()))
        {
            if (posicion == 1)
            {
                switch (personajeEntero)
                {
                    case 1:
                        ponderacion = ponderacionMombo;
                        break;
                    case 2:
                        ponderacion = ponderacionPirolo;
                        break;
                    case 3:
                        ponderacion = ponderacionLucas;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                return aSubLista.IesimoElementoPeso(posicion - 1, personajeEntero);
            }

        }
        return ponderacion;
    }

    public bool ExisteElemento(CVertice pElemento)
    {
        if ((aElemento != null) && (pElemento != null))
        {
            return (aElemento.Equals(pElemento) ||
          (aSubLista.ExisteElemento(pElemento)));
        }
        else
            return false;
    }

    public int PosicionElemento(CVertice pElemento)
    {
        if ((aElemento != null) || (ExisteElemento(pElemento)))
            if (aElemento.Equals(pElemento))
                return 1;
            else
                return 1 + aSubLista.PosicionElemento(pElemento);
        else
            return 0;
    }
    public void Mostrar1()
    {
        if (aElemento != null)
        {
            Debug.Log(aElemento.nombre + ":");
            aSubLista.Mostrar1();
        }
    }
    public void Mostrar()
    {
        if (aElemento != null)
        {
            Debug.Log(aElemento.nombre + " " + aPeso);
            aSubLista.Mostrar();
        }
    }

}
