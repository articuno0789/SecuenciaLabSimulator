using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CVertice : MonoBehaviour
{
        public string nombre;
        public int tipoTerreno;

        public CVertice()
        {
        }
        public CVertice(string Nombre, int TipoTerreno)
        {
            nombre = Nombre;
            tipoTerreno = TipoTerreno;
        }
        public override string ToString()
        {
            return nombre;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            else
                return nombre.Equals(obj.ToString());
        }
    
}
