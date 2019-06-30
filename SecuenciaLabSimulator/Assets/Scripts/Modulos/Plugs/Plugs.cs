using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class Plugs : MonoBehaviour
{
    public GameObject padreTotalComponente;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void CrearConexionPlugs()
    {
        CableComponent cable = this.GetComponent<CableComponent>();
        string startPoint = padreTotalComponente.name + "|" + cable.startPoint.name;
        Plugs endPlug = cable.endPoint.GetComponent<Plugs>();
        string endPoint = endPlug.padreTotalComponente.name + "|" + cable.endPoint.name;
        if (Regex.IsMatch(padreTotalComponente.name, @"^1_\d*$"))
        {
            Modulo1 mod1 = padreTotalComponente.GetComponent<Modulo1>();
            mod1.CrearConexionPlugs(startPoint, endPoint);
        }
        else
        if (Regex.IsMatch(padreTotalComponente.name, @"^2_\d*$"))
        {
            Modulo2 mod2 = padreTotalComponente.GetComponent<Modulo2>();
            mod2.CrearConexionPlugs(startPoint, endPoint);
        }
        else
        if (Regex.IsMatch(padreTotalComponente.name, @"^3_\d*$"))
        {
            Modulo3 mod3 = padreTotalComponente.GetComponent<Modulo3>();
            mod3.CrearConexionPlugs(startPoint, endPoint);
        }
        else
        if (Regex.IsMatch(padreTotalComponente.name, @"^4_\d*$"))
        {
            Modulo4 mod4 = padreTotalComponente.GetComponent<Modulo4>();
            mod4.CrearConexionPlugs(startPoint, endPoint);
        }
        else
        if (Regex.IsMatch(padreTotalComponente.name, @"^5_\d*$"))
        {
            Modulo5 mod5 = padreTotalComponente.GetComponent<Modulo5>();
            mod5.CrearConexionPlugs(startPoint, endPoint);
        }
        else
        if (Regex.IsMatch(padreTotalComponente.name, @"^6_\d*$"))
        {
            Modulo6 mod6 = padreTotalComponente.GetComponent<Modulo6>();
            mod6.CrearConexionPlugs(startPoint, endPoint);
        }
        else
        if (Regex.IsMatch(padreTotalComponente.name, @"^7_\d*$"))
        {
            Modulo7 mod7 = padreTotalComponente.GetComponent<Modulo7>();
            mod7.CrearConexionPlugs(startPoint, endPoint);
        }
        else
        if (Regex.IsMatch(padreTotalComponente.name, @"^8, 11_\d*$"))
        {
            Modulo8_11 mod8_11 = padreTotalComponente.GetComponent<Modulo8_11>();
            mod8_11.CrearConexionPlugs(startPoint, endPoint);
        }
        else
        if (Regex.IsMatch(padreTotalComponente.name, @"^9_\d*$"))
        {
            Modulo9 mod9 = padreTotalComponente.GetComponent<Modulo9>();
            mod9.CrearConexionPlugs(startPoint, endPoint);
        }
        else
        if (Regex.IsMatch(padreTotalComponente.name, @"^10, 17, 18, 19_\d*$"))
        {
            Modulo10_17_18_19 mod10_17_18_19 = padreTotalComponente.GetComponent<Modulo10_17_18_19>();
            mod10_17_18_19.CrearConexionPlugs(startPoint, endPoint);
        }
        else
        if (Regex.IsMatch(padreTotalComponente.name, @"^13_\d*$"))
        {
            Modulo13 mod13 = padreTotalComponente.GetComponent<Modulo13>();
            mod13.CrearConexionPlugs(startPoint, endPoint);
        }
        else
        if (Regex.IsMatch(padreTotalComponente.name, @"^14, 16_\d*$"))
        {
            Modulo14_16 mod14_16 = padreTotalComponente.GetComponent<Modulo14_16>();
            mod14_16.CrearConexionPlugs(startPoint, endPoint);
        }
        else
        if (Regex.IsMatch(padreTotalComponente.name, @"^15_\d*$"))
        {
            Modulo15 mod15 = padreTotalComponente.GetComponent<Modulo15>();
            mod15.CrearConexionPlugs(startPoint, endPoint);
        }
        else
        if (Regex.IsMatch(padreTotalComponente.name, @"^20_\d*$"))
        {
            Modulo20 mod20 = padreTotalComponente.GetComponent<Modulo20>();
            mod20.CrearConexionPlugs(startPoint, endPoint);
        }
        else
        if (Regex.IsMatch(padreTotalComponente.name, @"^21_\d*$"))
        {
            Modulo21 mod21 = padreTotalComponente.GetComponent<Modulo21>();
            mod21.CrearConexionPlugs(startPoint, endPoint);
        }
        else
        if (Regex.IsMatch(padreTotalComponente.name, @"^22, 23_\d*$"))
        {
            Modulo22_23 mod22_23 = padreTotalComponente.GetComponent<Modulo22_23>();
            mod22_23.CrearConexionPlugs(startPoint, endPoint);
        }
        else
        if (Regex.IsMatch(padreTotalComponente.name, @"^22_\d*$"))
        {
            Modulo22_23 mod22_23 = padreTotalComponente.GetComponent<Modulo22_23>();
            mod22_23.CrearConexionPlugs(startPoint, endPoint);
        }
        else
        if (Regex.IsMatch(padreTotalComponente.name, @"^23_\d*$"))
        {
            Modulo22_23 mod22_23 = padreTotalComponente.GetComponent<Modulo22_23>();
            mod22_23.CrearConexionPlugs(startPoint, endPoint);
        }
        else
        if (Regex.IsMatch(padreTotalComponente.name, @"^Potenciometro__\d*$"))
        {
            Potenciometro modPotenciometro = padreTotalComponente.GetComponent<Potenciometro>();
            modPotenciometro.CrearConexionPlugs(startPoint, endPoint);
        }
        else
        {
            Debug.LogError("padreTotalComponente.name DENTRO DE PLUGS NO PERTENECE A NINGUN MODULO CONOCIDO.");
        }
    }
}
