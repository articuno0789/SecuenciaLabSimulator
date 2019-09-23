using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public static class AuxiliarModulos
{
    //Tipos de conexiones
    //1 - Linea, 0 - Sin conexion, 2 - Neutro
    public enum TiposConexiones
    {
        SinConexion = 0,
        Linea = 1,
        Neutro = 2
    }
    //Numeros de lineas
    //0 - sin linea, 1 - primera linea, 2 - segunda linea, 3 - tercera linea
    public enum NumeroLinea
    {
        SinLinea = 0,
        PrimeraLinea = 1,
        SegundaLinea = 2,
        TerceraLinea = 3
    }
    //Tipo Nodo
    // 0 - poder, 1 - intermedio, 2 - final
    public enum TipoNodo
    {
        Poder = 0,
        Intermedio = 1,
        Final = 2
    }

    //Tags
    public const string tagMod1 = "1";
    public const string tagMod2 = "2";
    public const string tagMod3 = "3";
    public const string tagMod4 = "4";
    public const string tagMod5 = "5";
    public const string tagMod6 = "6";
    public const string tagMod7 = "7";
    public const string tagtagMod8_11 = "8, 11";
    public const string tagMod9 = "9";
    public const string tagMod10_17_18_19 = "10, 17, 18, 19";
    public const string tagMod13 = "13";
    public const string tagMod14_16 = "14, 16";
    public const string tagMod15 = "15";
    public const string tagMod20 = "20";
    public const string tagMod21 = "21";
    public const string tagMod22 = "22";
    public const string tagMod23 = "23";
    public const string tagMod22_23 = "22, 23";
    public const string tagModMotorElectricoAC = "MotorElectricoAC";
    public const string tagModMulticonector = "Multiconector";
    public const string tagModPotenciometro = "Potenciometro";
    public const string tagPlugAnaranjado = "EntradaPlugAnaranjado";
    public const string tagPlugNegro = "EntradaPlugNegro";

    //Expresiones regulares
    public const string expreRegMod1 = @"^1_\d*$";
    public const string expreRegMod2 = @"^2_\d*$";
    public const string expreRegMod3 = @"^3_\d*$";
    public const string expreRegMod4 = @"^4_\d*$";
    public const string expreRegMod5 = @"^5_\d*$";
    public const string expreRegMod6 = @"^6_\d*$";
    public const string expreRegMod7 = @"^7_\d*$";
    public const string expreRegMod8_11 = @"^8, 11_\d*$";
    public const string expreRegMod9 = @"^9_\d*$";
    public const string expreRegMod10_17_18_19 = @"^10, 17, 18, 19_\d*$";
    public const string expreRegMod13 = @"^13_\d*$";
    public const string expreRegMod14_16 = @"^14, 16_\d*$";
    public const string expreRegMod15 = @"^15_\d*$";
    public const string expreRegMod20 = @"^20_\d*$";
    public const string expreRegMod21 = @"^21_\d*$";
    public const string expreRegMod22 = @"^22_\d*$";
    public const string expreRegMod23 = @"^23_\d*$";
    public const string expreRegMod22_23 = @"^22, 23_\d*$";
    public const string expreRegModMotorElectricoAC = @"^MotorElectricoAC_\d*$";
    public const string expreRegModMulticonector = @"^Multiconector_\d*$";
    public const string expreRegModPotenciometro = @"^Potenciometro_\d*$";
    public const string expreRegPlugAnaranjado = @"^EntradaPlugAnaranjado\d*$";
    public const string expreRegPlugNegro = @"^EntradaPlugNegro\d*$";

    public const string expreRegNumerosReales = "^[0-9]*([.][0-9]+)?$";
    

    //Particulas
    public enum ParticlesErrorTypes
    {
        BigExplosion = 0,
        DrippingFlames = 1,
        ElectricalSparksEffect = 2,
        SmallExplosionEffect = 3,
        SmokeEffect = 4,
        SparksEffect = 5,
        RibbonSmoke = 6,
        PlasmaExplosionEffect = 7
    }

    /*En este método se asigna la lógica de funcionamiento a un determinado módulo, de acuerdo a su tipo.*/
    public static GameObject AsignarLogicaModulo(GameObject modulo, string tipoModulo)
    {
        if (modulo != null)
        {
            if (tipoModulo == tagMod1)
            {
                modulo.AddComponent<Modulo1>();
            }
            else
            if (tipoModulo == tagMod2)
            {
                modulo.AddComponent<Modulo2>();
            }
            else
            if (tipoModulo == tagMod3)
            {
                modulo.AddComponent<Modulo3>();
            }
            else
            if (tipoModulo == tagMod4)
            {
                modulo.AddComponent<Modulo4>();
            }
            else
            if (tipoModulo == tagMod5)
            {
                modulo.AddComponent<Modulo5>();
            }
            else
            if (tipoModulo == tagMod6)
            {
                modulo.AddComponent<Modulo6>();
            }
            else
            if (tipoModulo == tagMod7)
            {
                modulo.AddComponent<Modulo7>();
            }
            else
            if (tipoModulo == tagtagMod8_11)
            {
                modulo.AddComponent<Modulo8_11>();
            }
            else
            if (tipoModulo == tagMod9)
            {
                modulo.AddComponent<Modulo9>();
            }
            else
            if (tipoModulo == tagMod10_17_18_19)
            {
                modulo.AddComponent<Modulo10_17_18_19>();
            }
            else
            if (tipoModulo == tagMod13)
            {
                modulo.AddComponent<Modulo13>();
            }
            else
            if (tipoModulo == tagMod14_16)
            {
                modulo.AddComponent<Modulo14_16>();
            }
            else
            if (tipoModulo == tagMod15)
            {
                modulo.AddComponent<Modulo15>();
            }
            else
            if (tipoModulo == tagMod20)
            {
                modulo.AddComponent<Modulo20>();
            }
            else
            if (tipoModulo == tagMod21)
            {
                modulo.AddComponent<Modulo21>();
            }
            else
            if (tipoModulo == tagMod22_23)
            {
                modulo.AddComponent<Modulo22_23>();
            }
            else
            if (tipoModulo == tagMod22)
            {
                modulo.AddComponent<Modulo22_23>();
            }
            else
            if (tipoModulo == tagMod23)
            {
                modulo.AddComponent<Modulo22_23>();
            }
            else
            if (tipoModulo == tagModPotenciometro)
            {
                modulo.AddComponent<Potenciometro>();
            }
            else
            if (tipoModulo == tagModMulticonector)
            {
                modulo.AddComponent<Multiconector>();
            }
            else
            if (tipoModulo == tagModMotorElectricoAC)
            {
                modulo.AddComponent<MotorElectricoAC>();
            }
            else
            {
                Debug.LogError("GameObject AsignarLogicaModulo(GameObject module, string tipo) - Tipo de modulo desconocido - " + tipoModulo + ".");
            }
        }
        else
        {
            Debug.LogError("GameObject AsignarLogicaModulo(GameObject module, string tipo) - Modulo no asignado, es decir, null.");
        }
        return modulo;
    }

    public static void EliminarMaterial(GameObject objeto)
    {
        Renderer objetoRender = objeto.transform.GetComponent<Renderer>();
        objetoRender.material = null;
    }

    /*Este método se encarga de regresar el GameObject de un plug determinado, apartir del nombre del
     * plug y el módulo padre (Nodo raíz).*/
    public static GameObject BuscarPlugConexion(string tipoModulo, GameObject modulo, string nombrePlug)
    {
        GameObject plugEncontrado = null;
        if (tipoModulo == tagMod1)
        {
            Modulo1 mod1 = modulo.GetComponent<Modulo1>();
            if (Regex.IsMatch(nombrePlug, expreRegPlugAnaranjado))
            {
                plugEncontrado = mod1.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, expreRegPlugNegro))
            {
                plugEncontrado = mod1.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == tagMod2)
        {
            Modulo2 mod2 = modulo.GetComponent<Modulo2>();
            if (Regex.IsMatch(nombrePlug, expreRegPlugAnaranjado))
            {
                plugEncontrado = mod2.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, expreRegPlugNegro))
            {
                plugEncontrado = mod2.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == tagMod3)
        {
            Modulo3 mod3 = modulo.GetComponent<Modulo3>();
            if (Regex.IsMatch(nombrePlug, expreRegPlugAnaranjado))
            {
                plugEncontrado = mod3.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, expreRegPlugNegro))
            {
                plugEncontrado = mod3.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == tagMod4)
        {
            Modulo4 mod4 = modulo.GetComponent<Modulo4>();
            if (Regex.IsMatch(nombrePlug, expreRegPlugAnaranjado))
            {
                plugEncontrado = mod4.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, expreRegPlugNegro))
            {
                plugEncontrado = mod4.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == tagMod5)
        {
            Modulo5 mod5 = modulo.GetComponent<Modulo5>();
            if (Regex.IsMatch(nombrePlug, expreRegPlugAnaranjado))
            {
                plugEncontrado = mod5.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, expreRegPlugNegro))
            {
                plugEncontrado = mod5.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == tagMod6)
        {
            Modulo6 mod6 = modulo.GetComponent<Modulo6>();
            if (Regex.IsMatch(nombrePlug, expreRegPlugAnaranjado))
            {
                plugEncontrado = mod6.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, expreRegPlugNegro))
            {
                plugEncontrado = mod6.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == tagMod7)
        {
            Modulo7 mod7 = modulo.GetComponent<Modulo7>();
            if (Regex.IsMatch(nombrePlug, expreRegPlugAnaranjado))
            {
                plugEncontrado = mod7.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, expreRegPlugNegro))
            {
                plugEncontrado = mod7.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == tagtagMod8_11)
        {
            Modulo8_11 mod8_11 = modulo.GetComponent<Modulo8_11>();
            if (Regex.IsMatch(nombrePlug, expreRegPlugAnaranjado))
            {
                plugEncontrado = mod8_11.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, expreRegPlugNegro))
            {
                plugEncontrado = mod8_11.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == tagMod9)
        {
            Modulo9 mod9 = modulo.GetComponent<Modulo9>();
            if (Regex.IsMatch(nombrePlug, expreRegPlugAnaranjado))
            {
                plugEncontrado = mod9.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, expreRegPlugNegro))
            {
                plugEncontrado = mod9.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == tagMod10_17_18_19)
        {
            Modulo10_17_18_19 mod10_17_18_19 = modulo.GetComponent<Modulo10_17_18_19>();
            if (Regex.IsMatch(nombrePlug, expreRegPlugAnaranjado))
            {
                plugEncontrado = mod10_17_18_19.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, expreRegPlugNegro))
            {
                plugEncontrado = mod10_17_18_19.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == tagMod13)
        {
            Modulo13 mod13 = modulo.GetComponent<Modulo13>();
            if (Regex.IsMatch(nombrePlug, expreRegPlugAnaranjado))
            {
                plugEncontrado = mod13.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, expreRegPlugNegro))
            {
                plugEncontrado = mod13.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == tagMod14_16)
        {
            Modulo14_16 mod14_16 = modulo.GetComponent<Modulo14_16>();
            if (Regex.IsMatch(nombrePlug, expreRegPlugAnaranjado))
            {
                plugEncontrado = mod14_16.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, expreRegPlugNegro))
            {
                plugEncontrado = mod14_16.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == tagMod15)
        {
            Modulo15 mod15 = modulo.GetComponent<Modulo15>();
            if (Regex.IsMatch(nombrePlug, expreRegPlugAnaranjado))
            {
                plugEncontrado = mod15.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, expreRegPlugNegro))
            {
                plugEncontrado = mod15.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == tagMod20)
        {
            Modulo20 mod20 = modulo.GetComponent<Modulo20>();
            if (Regex.IsMatch(nombrePlug, expreRegPlugAnaranjado))
            {
                plugEncontrado = mod20.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, expreRegPlugNegro))
            {
                plugEncontrado = mod20.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == tagMod21)
        {
            Modulo21 mod21 = modulo.GetComponent<Modulo21>();
            if (Regex.IsMatch(nombrePlug, expreRegPlugAnaranjado))
            {
                plugEncontrado = mod21.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, expreRegPlugNegro))
            {
                plugEncontrado = mod21.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == tagMod22_23)
        {
            Modulo22_23 mod22_23 = modulo.GetComponent<Modulo22_23>();
            if (Regex.IsMatch(nombrePlug, expreRegPlugAnaranjado))
            {
                plugEncontrado = mod22_23.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, expreRegPlugNegro))
            {
                plugEncontrado = mod22_23.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == tagMod22)
        {
            Modulo22_23 mod22_23 = modulo.GetComponent<Modulo22_23>();
            if (Regex.IsMatch(nombrePlug, expreRegPlugAnaranjado))
            {
                plugEncontrado = mod22_23.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, expreRegPlugNegro))
            {
                plugEncontrado = mod22_23.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == tagMod23)
        {
            Modulo22_23 mod22_23 = modulo.GetComponent<Modulo22_23>();
            if (Regex.IsMatch(nombrePlug, expreRegPlugAnaranjado))
            {
                plugEncontrado = mod22_23.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, expreRegPlugNegro))
            {
                plugEncontrado = mod22_23.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == tagModPotenciometro)
        {
            Potenciometro modPoten = modulo.GetComponent<Potenciometro>();
            if (Regex.IsMatch(nombrePlug, expreRegPlugAnaranjado))
            {
                plugEncontrado = modPoten.plugAnaranjadosDict[nombrePlug];
            }
            else if (Regex.IsMatch(nombrePlug, expreRegPlugNegro))
            {
                plugEncontrado = modPoten.plugNegrosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == tagModMulticonector)
        {
            Multiconector modMulticonector = modulo.GetComponent<Multiconector>();
            if (Regex.IsMatch(nombrePlug, expreRegPlugAnaranjado))
            {
                plugEncontrado = modMulticonector.plugAnaranjadosDict[nombrePlug];
            }
        }
        else
        if (tipoModulo == tagModMotorElectricoAC)
        {
            Debug.LogError("BuscarPlugConexion - ProgressManager - Motor electrico no tiene plugs, " + tipoModulo);
            //modulo.AddComponent<MotorElectricoAC>();
        }
        else
        {
            Debug.LogError("BuscarPlugConexion - ProgressManager - No entra a ningun tipo, " + tipoModulo);
        }
        return plugEncontrado;
    }

    /*Este método se encarga de regresar el diccionario de conexiones de un respectivo módulo.*/
    public static Dictionary<string, string> ObtenerPlugConnections(string tipoModulo, GameObject modulo)
    {
        Dictionary<string, string> diccionario = null;
        if (tipoModulo == tagMod1)
        {
            Modulo1 mod1 = modulo.GetComponent<Modulo1>();
            diccionario = mod1.plugsConnections;
        }
        else
        if (tipoModulo == tagMod2)
        {
            Modulo2 mod2 = modulo.GetComponent<Modulo2>();
            diccionario = mod2.plugsConnections;
        }
        else
        if (tipoModulo == tagMod3)
        {
            Modulo3 mod3 = modulo.GetComponent<Modulo3>();
            diccionario = mod3.plugsConnections;
        }
        else
        if (tipoModulo == tagMod4)
        {
            Modulo4 mod4 = modulo.GetComponent<Modulo4>();
            diccionario = mod4.plugsConnections;
        }
        else
        if (tipoModulo == tagMod5)
        {
            Modulo5 mod5 = modulo.GetComponent<Modulo5>();
            diccionario = mod5.plugsConnections;
        }
        else
        if (tipoModulo == tagMod6)
        {
            Modulo6 mod6 = modulo.GetComponent<Modulo6>();
            diccionario = mod6.plugsConnections;
        }
        else
        if (tipoModulo == tagMod7)
        {
            Modulo7 mod7 = modulo.GetComponent<Modulo7>();
            diccionario = mod7.plugsConnections;
        }
        else
        if (tipoModulo == tagtagMod8_11)
        {
            Modulo8_11 mod8_11 = modulo.GetComponent<Modulo8_11>();
            diccionario = mod8_11.plugsConnections;
        }
        else
        if (tipoModulo == tagMod9)
        {
            Modulo9 mod9 = modulo.GetComponent<Modulo9>();
            diccionario = mod9.plugsConnections;
        }
        else
        if (tipoModulo == tagMod10_17_18_19)
        {
            Modulo10_17_18_19 mod10_17_18_19 = modulo.GetComponent<Modulo10_17_18_19>();
            diccionario = mod10_17_18_19.plugsConnections;
        }
        else
        if (tipoModulo == tagMod13)
        {
            Modulo13 mod13 = modulo.GetComponent<Modulo13>();
            diccionario = mod13.plugsConnections;
        }
        else
        if (tipoModulo == tagMod14_16)
        {
            Modulo14_16 mod14_16 = modulo.GetComponent<Modulo14_16>();
            diccionario = mod14_16.plugsConnections;
        }
        else
        if (tipoModulo == tagMod15)
        {
            Modulo15 mod15 = modulo.GetComponent<Modulo15>();
            diccionario = mod15.plugsConnections;
        }
        else
        if (tipoModulo == tagMod20)
        {
            Modulo20 mod20 = modulo.GetComponent<Modulo20>();
            diccionario = mod20.plugsConnections;
        }
        else
        if (tipoModulo == tagMod21)
        {
            Modulo21 mod21 = modulo.GetComponent<Modulo21>();
            diccionario = mod21.plugsConnections;
        }
        else
        if (tipoModulo == tagMod22_23)
        {
            Modulo22_23 mod22_23 = modulo.GetComponent<Modulo22_23>();
            diccionario = mod22_23.plugsConnections;
        }
        else
        if (tipoModulo == tagMod22)
        {
            Modulo22_23 mod22_23 = modulo.GetComponent<Modulo22_23>();
            diccionario = mod22_23.plugsConnections;
        }
        else
        if (tipoModulo == tagMod23)
        {
            Modulo22_23 mod22_23 = modulo.GetComponent<Modulo22_23>();
            diccionario = mod22_23.plugsConnections;
        }
        else
        if (tipoModulo == tagModPotenciometro)
        {
            Potenciometro modPoten = modulo.GetComponent<Potenciometro>();
            diccionario = modPoten.plugsConnections;
        }
        else
        if (tipoModulo == tagModMulticonector)
        {
            Multiconector modMultiConect = modulo.GetComponent<Multiconector>();
            diccionario = modMultiConect.plugsConnections;
        }
        else
        if (tipoModulo == tagModMotorElectricoAC)
        {
            Debug.LogError("BuscarPlugConexion - ProgressManager - Motor electrico no tiene plugs, " + tipoModulo);
            //modulo.AddComponent<MotorElectricoAC>();
        }
        else
        {
            Debug.LogError("ObtenerPlugConnections - ProgressManager - No entra a ningun tipo, " + tipoModulo);
        }
        return diccionario;
    }
}
