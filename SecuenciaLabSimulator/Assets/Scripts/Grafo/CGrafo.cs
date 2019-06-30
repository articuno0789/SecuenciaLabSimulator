using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGrafo : MonoBehaviour
{
    public int numeroArchos = 0;
    //==============================Atributos=========================
    protected CVertice aVertice;
    protected CLista aLista;
    protected CGrafo aSiguiente;

    //========================Constructores============================

    public CGrafo()
    {
        aVertice = null;
        aLista = null;
        aSiguiente = null;
    }
    public CGrafo(CVertice pVertice, CLista pLista, CGrafo pSiguiente)
    {
        aVertice = pVertice;
        aLista = pLista;
        aSiguiente = pSiguiente;
    }
    //==========================Propiedades============================
    public CVertice Vertice
    {
        get
        { return aVertice; }
        set
        { aVertice = value; }
    }
    public CLista Lista
    {
        get
        { return aLista; }
        set
        { aLista = value; }
    }
    public CGrafo Siguiente
    {
        get
        { return aSiguiente; }
        set
        { aSiguiente = value; }
    }
    //=====================Operaciones Básicas=================================
    public bool EstaVacio()
    {
        return (aVertice == null);
    }

    public int NumerodeVertices()
    {
        if (aVertice == null)
            return 0;
        else
            return 1 + aSiguiente.NumerodeVertices();
    }

    public bool ExisteVertice(CVertice vertice)
    {
        if ((aVertice == null) || (vertice == null))
        {
            //Console.WriteLine("if ((aVertice == null) || (vertice == null))");
            return false;
        }
        else if (aVertice.nombre.Equals(vertice.nombre))
        {
            // Console.WriteLine("else if (aVertice.nombre.Equals(vertice.nombre))");
            return true;
        }
        else
        {
            // Console.WriteLine("else");
            return aSiguiente.ExisteVertice(vertice);
        }

    }

    public void AgregarVertice(CVertice vertice)
    {
        if ((vertice != null) && (!ExisteVertice(vertice)))
        {
            if (aVertice != null)
            {
                if (aSiguiente == null)
                {
                    CGrafo aux = new CGrafo(aVertice, aLista, aSiguiente);
                    aVertice = new CVertice(vertice.nombre, vertice.tipoTerreno);
                    aSiguiente = aux;
                }
                else
                {
                    aSiguiente.AgregarVertice(vertice);
                }

                /* if (vertice.nombre.CompareTo(aVertice.nombre) < 0)
                 {
                     CGrafo aux = new CGrafo(aVertice, aLista, aSiguiente);
                     aVertice = new CVertice(vertice.nombre, vertice.tipoTerreno);
                     aSiguiente = aux;
                 }
                 else
                 { //Agregar
                     aSiguiente.AgregarVertice(vertice);
                 }*/
            }
            else
            {
                aVertice = new CVertice(vertice.nombre, vertice.tipoTerreno);
                aLista = new CLista();
                aSiguiente = new CGrafo();
            }
        }
    }
    public void AgregarArco(CVertice pVerticeOrigen, CVertice pVerticeDestino, int pDistancia)
    { // Posicionarse en el elemento donde se agragara el arco
        if (ExisteVertice(pVerticeOrigen) && ExisteVertice(pVerticeDestino))
            agregarArco(pVerticeOrigen, pVerticeDestino, pDistancia);
        else
            Debug.Log("Error......No se agregó arco");
    }
    private void agregarArco(CVertice pVerticeOrigen, CVertice pVerticeDestino, int pDistancia)
    { // Posicionarse en el elemento donde se agragara el arco
        if (ExisteVertice(pVerticeOrigen))
        {
            if (aVertice.Equals(pVerticeOrigen))
            { //Agregar Arco
                if ((!aLista.ExisteElemento(pVerticeDestino)))
                {
                    aLista.Agregar(pVerticeDestino, pDistancia);
                    numeroArchos++;
                }
            }
            else
           if (aSiguiente != null)
            {
                aSiguiente.agregarArco(pVerticeOrigen, pVerticeDestino, pDistancia);
                numeroArchos++;
            }

        }
    }
    public void MostrarVertices()
    {
        if (aVertice != null)
        {
            Debug.Log("Nombre: " + aVertice.nombre + ", Tipo Terreno: " + aVertice.tipoTerreno);
            aSiguiente.MostrarVertices();
        }
    }

    public CVertice regresarVertice(CVertice verticeBuscar)
    {
        CGrafo auxiliar = aSiguiente;
        CVertice encontrado = null;
        // Console.WriteLine("aVertice.nombre: " + aVertice.nombre + " " + aVertice.tipoTerreno + ", Buscado: " + verticeBuscar.nombre + " " + verticeBuscar.tipoTerreno);
        if (aVertice.nombre == verticeBuscar.nombre && aVertice.tipoTerreno == verticeBuscar.tipoTerreno)
        {
            encontrado = aVertice;
        }
        else
        {
            while (auxiliar != null)
            {
                //Console.WriteLine("aSiguiente.aVertice.nombre: " + auxiliar.aVertice.nombre + " " + auxiliar.aVertice.tipoTerreno + ", Buscado: " + verticeBuscar.nombre + " " + verticeBuscar.tipoTerreno);
                if (auxiliar.aVertice.nombre == verticeBuscar.nombre)
                {
                    encontrado = auxiliar.aVertice;
                    //Console.WriteLine("Lo encontro VERTICE");
                    break;
                }
                auxiliar = auxiliar.aSiguiente;
                if (auxiliar.aVertice == null)
                {
                    break;
                }
            }
        }
        return encontrado;


        /*aSiguiente.aSiguiente
        if (aVertice != null)
        {
            if(aVertice.nombre== verticeBuscar.nombre && aVertice.tipoTerreno == verticeBuscar.tipoTerreno)
            {
                return aVertice;
            }

            Console.WriteLine(aVertice.nombre);
            aSiguiente.MostrarVertices();
        }*/
    }

    public CGrafo regresarGrafo(CVertice verticeBuscar)
    {
        CGrafo auxiliar = aSiguiente;
        CGrafo encontrado = null;
        // Console.WriteLine("aVertice.nombre: " + aVertice.nombre + " " + aVertice.tipoTerreno + ", Buscado: " + verticeBuscar.nombre + " " + verticeBuscar.tipoTerreno);
        if (aVertice.nombre == verticeBuscar.nombre && aVertice.tipoTerreno == verticeBuscar.tipoTerreno)
        {
            encontrado = auxiliar;
        }
        else
        {
            while (auxiliar != null)
            {
                //Console.WriteLine("aSiguiente.aVertice.nombre: " + auxiliar.aVertice.nombre + " " + auxiliar.aVertice.tipoTerreno + ", Buscado: " + verticeBuscar.nombre + " " + verticeBuscar.tipoTerreno);
                if (auxiliar.aVertice.nombre == verticeBuscar.nombre)
                {
                    encontrado = auxiliar;
                    //Console.WriteLine("Lo encontro GRAFO");
                    break;
                }
                auxiliar = auxiliar.aSiguiente;
                if (auxiliar.aVertice == null)
                {
                    break;
                }
            }
        }
        return encontrado;


        /*aSiguiente.aSiguiente
        if (aVertice != null)
        {
            if(aVertice.nombre== verticeBuscar.nombre && aVertice.tipoTerreno == verticeBuscar.tipoTerreno)
            {
                return aVertice;
            }

            Console.WriteLine(aVertice.nombre);
            aSiguiente.MostrarVertices();
        }*/
    }

    public void MostrarGrafo()
    {
        if (aVertice != null)
        {
            Debug.Log("ENTRO " + aVertice.nombre + " " + aLista.NroElementos());
            for (int i = 1; i <= aLista.NroElementos(); i++)
                Debug.Log(aVertice.nombre + " ==> " + aLista.IesimoElemento(i) +
                " Con peso>>>>(" + aLista.IesimoElementoPeso(i) + " ) ");
            aSiguiente.MostrarGrafo();
        }
    }

    public void MostrarConexionesVertice(CVertice vertice)
    {
        if ((vertice != null) && (ExisteVertice(vertice)))
        {
            if (aVertice != null)
            {
                if (aVertice.nombre == vertice.nombre)
                {
                    for (int i = 1; i <= aLista.NroElementos(); i++)
                        Debug.Log(aVertice.nombre + " ==> " + aLista.IesimoElemento(i));
                }
                else
                {
                    aSiguiente.MostrarConexionesVertice(vertice);
                }
            }
            else
            {
                //   Console.WriteLine("EL GRAFO ESTA VACIO, NO SE PUEDE ENCONTRAR NINGUN VERTICE");
            }
        }
        else
        {
            //Console.WriteLine("if ((vertice != null) && (!ExisteVertice(vertice)))");
        }
    }

    public List<Pair<int, long>>[] ady2;

    public long[,] construirTablaDeAdyacencia(int personajeEntero)
    {
        ady2 = new List<Pair<int, long>>[10005];
        for (int i = 0; i < 10005; i++)
        {
            ady2[i] = new List<Pair<int, long>>();
        }

        int numeroVertices = NumerodeVertices();
        //Console.WriteLine("Numero vertices: " + numeroVertices);
        long[,] matrizAdyacencia = new long[numeroVertices, numeroVertices];
        for (int i = 0; i < numeroVertices; i++)
        {
            for (int j = 0; j < numeroVertices; j++)
            {
                matrizAdyacencia[i, j] = -1;
            }
        }

        CVertice encontrado = null;
        // Console.WriteLine("aVertice.nombre: " + aVertice.nombre + " " + aVertice.tipoTerreno + ", Buscado: " + verticeBuscar.nombre + " " + verticeBuscar.tipoTerreno);


        for (int i = 1; i <= aLista.NroElementos(); i++)
        {
            Debug.Log(aVertice.nombre + " ==> " + aLista.IesimoElemento(i));
            //conexiones.Add((CVertice)aLista.IesimoElemento(i));
            matrizAdyacencia[Convert.ToInt32(aVertice.nombre), Convert.ToInt32(((CVertice)aLista.IesimoElemento(i)).nombre)] = Convert.ToInt64((double)aLista.IesimoElementoPeso(i, personajeEntero));
            ady2[Convert.ToInt32(aVertice.nombre)].Add(new Pair<int, long>(Convert.ToInt32(((CVertice)aLista.IesimoElemento(i)).nombre), Convert.ToInt64((double)aLista.IesimoElementoPeso(i, personajeEntero)))); //consideremos grafo dirigido

        }
        CGrafo auxiliar = aSiguiente;
        while (auxiliar != null)
        {
            for (int i = 1; i <= auxiliar.aLista.NroElementos(); i++)
            {
                //Console.WriteLine(Convert.ToInt32(auxiliar.aVertice.nombre) + "," + Convert.ToInt32(((CVertice)auxiliar.aLista.IesimoElemento(i)).nombre));
                matrizAdyacencia[Convert.ToInt32(auxiliar.aVertice.nombre), Convert.ToInt32(((CVertice)auxiliar.aLista.IesimoElemento(i)).nombre)] = Convert.ToInt64((double)auxiliar.aLista.IesimoElementoPeso(i, personajeEntero));

                ady2[Convert.ToInt32(auxiliar.aVertice.nombre)].Add(new Pair<int, long>(Convert.ToInt32(((CVertice)auxiliar.aLista.IesimoElemento(i)).nombre), Convert.ToInt64((double)auxiliar.aLista.IesimoElementoPeso(i, personajeEntero)))); //consideremos grafo dirigido
            }
            auxiliar = auxiliar.aSiguiente;
            if (auxiliar.aVertice == null)
            {
                break;
            }
        }
        /*Console.WriteLine("TERMINO CREAR MATRIZ");
        for (int i = 0; i < numeroVertices; i++)
        {
            for (int j = 0; j < numeroVertices; j++)
            {
                Console.Write(matrizAdyacencia[i, j] + " ");
            }
            Console.WriteLine("");
        }*/
        return matrizAdyacencia;


    }

    public List<CVertice> regresarConexionesVertice(CVertice vertice)
    {
        List<CVertice> conexiones = new List<CVertice>();

        if ((vertice != null) && (ExisteVertice(vertice)))
        {
            if (aVertice != null)
            {
                if (aVertice.nombre == vertice.nombre)
                {
                    for (int i = 1; i <= aLista.NroElementos(); i++)
                    {
                        Debug.Log(aVertice.nombre + " ==> " + aLista.IesimoElemento(i));
                        conexiones.Add((CVertice)aLista.IesimoElemento(i));
                    }

                }
                else
                {
                    aSiguiente.MostrarConexionesVertice(vertice);
                }
            }
            else
            {
                // Console.WriteLine("EL GRAFO ESTA VACIO, NO SE PUEDE ENCONTRAR NINGUN VERTICE");
            }
        }
        else
        {
            //Console.WriteLine("if ((vertice != null) && (!ExisteVertice(vertice)))");
        }
        return conexiones;
    }

    public CLista regresarArista(CVertice verticeOrigen, CVertice verticeDestino)
    {
        CGrafo auxiliar = aSiguiente;
        CLista encontrado = null;
        if (aVertice.nombre == verticeOrigen.nombre)
        {
            //Console.WriteLine("EN EL IF: aVertice.nombre: " + aVertice.nombre + ", vERTICE ORIGEN: " + verticeOrigen.nombre);
            CLista auxiliar2 = aLista;
            while (auxiliar2 != null)
            {
                //Console.WriteLine("EN EL IF: auxiliar2.Elemento.nombre: " + auxiliar2.Elemento.nombre + ", vERTICE destino: " + verticeDestino.nombre);
                if (auxiliar2.Elemento.nombre == verticeDestino.nombre)
                {
                    //Console.WriteLine("Lo encontro ARISTA");
                    encontrado = auxiliar2;
                    break;
                }
                auxiliar2 = auxiliar2.SubLista;
                if (auxiliar2.Elemento == null)
                {
                    break;
                }
            }

        }
        else
        {
            // Console.WriteLine("ENTRO ELSE");
            while (auxiliar != null)
            {
                //Console.WriteLine("EN EL else: aVertice.nombre: " + aVertice.nombre + ", vERTICE ORIGEN: " + verticeOrigen.nombre);
                if (auxiliar.aVertice.nombre == verticeOrigen.nombre)
                {
                    //Console.WriteLine("EN EL else: aVertice.nombre: " + aVertice.nombre + ", vERTICE ORIGEN: " + verticeOrigen.nombre);
                    CLista auxiliar2 = auxiliar.aLista;
                    while (auxiliar2 != null)
                    {
                        //Console.WriteLine("EN EL else: auxiliar2.Elemento.nombre: " + auxiliar2.Elemento.nombre + ", vERTICE destino: " + verticeDestino.nombre);

                        if (auxiliar2.Elemento.nombre == verticeDestino.nombre)
                        {
                            encontrado = auxiliar2;
                            //Console.WriteLine("Lo encontro ARISTA");
                            break;
                        }
                        auxiliar2 = auxiliar2.SubLista;
                        if (auxiliar2.Elemento == null)
                        {
                            break;
                        }
                    }
                }
                auxiliar = auxiliar.aSiguiente;
                if (auxiliar.aVertice == null)
                {
                    break;
                }
            }
        }
        return encontrado;
    }

    // Eliminar Vértice
    public void SuprimirVertice(CVertice pVertice)
    {
        if (ExisteVertice(pVertice))
        {
            EliminarDeLLegadas(pVertice);
            suprimirVertice(pVertice);
        }
        else
        {
            Debug.Log("Error al querer eliminar. El vertice {0} no existe", pVertice);
        }
    }

    private void EliminarDeLLegadas(CVertice pVertice)
    {
        if (aVertice != null)
        {
            aLista.Eliminar(pVertice);
            aSiguiente.EliminarDeLLegadas(pVertice);
        }
    }

    private void suprimirVertice(CVertice pVertice)
    {
        if (aVertice != null)
        {
            if (aVertice.Equals(pVertice))
            {
                if (aSiguiente.aSiguiente != null)
                {
                    aVertice.nombre = aSiguiente.aVertice.nombre;
                    aLista = new CLista(aSiguiente.aLista);
                    aSiguiente = aSiguiente.aSiguiente;
                }
                else
                {
                    aVertice = null;
                    aLista = null;
                    aSiguiente = null;
                }
            }
            else
                aSiguiente.SuprimirVertice(pVertice);
        }
    }
    // Eliminar Arco
    public void SuprimirArco(CVertice pVerticeOrigen, CVertice pVerticeDestino)
    { // Posicionarse en el elemento donde se agregará el arco
        if (ExisteVertice(pVerticeOrigen) && ExisteVertice(pVerticeDestino))
        {
            if (aVertice.nombre.CompareTo(pVerticeOrigen.nombre) == 0)
            { //Suprimir Arco
                if (aLista.ExisteElemento(pVerticeDestino))
                    aLista.Eliminar(pVerticeDestino);
            }
            else
            {
                if (aSiguiente != null)
                    aSiguiente.SuprimirArco(pVerticeOrigen, pVerticeDestino);
            }
        }
    }
    // Recorrido en Profundidad
    public void RecorridoEnProfundidad()
    {
        int NroVertices = NumerodeVertices();
        string[] ArreglosVisitados = new string[NroVertices];
        for (int i = 0; i <= NroVertices - 1; i++)
        {
            ArreglosVisitados[i] = "F";
        }
        for (int i = 0; i <= NroVertices - 1; i++)
        {
            if (ArreglosVisitados[i] == "F")
                RecorrerVerticeProfundidad(IesimoVertice(i + 1), ArreglosVisitados);
        }
    }

    private void RecorrerVerticeProfundidad(CVertice pVertice, string[] ArreglosVisitados)
    {
        int Posicion = PosicionVertice(pVertice);
        ArreglosVisitados[Posicion - 1] = "T";
        Debug.Log(" " + pVertice.nombre + " , ");
        for (int i = 0; i <= GradoSaliente(pVertice) - 1; i++)
        {
            CVertice NuevoVertice = IesimoSucesor(pVertice, i + 1);
            int j = PosicionVertice(NuevoVertice);
            if (ArreglosVisitados[j - 1] == "F")
                RecorrerVerticeProfundidad(NuevoVertice, ArreglosVisitados);
        }
    }
    public CVertice IesimoVertice(int posicion)
    {
        if (posicion > 0)
        {
            if (posicion == 1)
                return aVertice;
            else
                return aSiguiente.IesimoVertice(posicion - 1);
        }
        else
            return null;
    }
    public int PosicionVertice(CVertice pVertice)
    {
        if ((aVertice == null) || (pVertice == null))
            return 0;
        else
      if (!ExisteVertice(pVertice))
            return 0;
        else
      if (aVertice.nombre.Equals(pVertice.nombre))
            return 1;
        else
            return 1 + aSiguiente.PosicionVertice(pVertice);
    }
    public CVertice IesimoSucesor(CVertice pVertice, int posicion)
    {
        if (ExisteVertice(pVertice))
        {
            if (aVertice.Equals(pVertice))
                return ((posicion > 0) && (posicion <= aLista.NroElementos()) ?
                (aLista.IesimoElemento(posicion) as CVertice) : null);
            else
                return aSiguiente.IesimoSucesor(pVertice, posicion);
        }
        else
            return null;
    }

    public int GradoSaliente(CVertice pVertice)
    {
        if (!ExisteVertice(pVertice))
            return 0;
        else
        {
            if (aVertice.Equals(pVertice))
                return aLista.NroElementos();
            else
                return aSiguiente.GradoSaliente(pVertice);
        }
    }
    // Recorrido en Anchura
    public void RecorridoAncho()
    {
        int NroVertices = NumerodeVertices();
        string[] ArreglosVisitados = new string[NroVertices];
        for (int i = 0; i <= NroVertices - 1; i++)
        {
            ArreglosVisitados[i] = "F";
        }
        for (int i = 0; i <= NroVertices - 1; i++)
        {
            if (ArreglosVisitados[i] == "F")
                RecorrerVerticeAncho(IesimoVertice(i + 1), ArreglosVisitados);
        }
    }
    private void RecorrerVerticeAncho(CVertice pVertice, string[] ArreglosVisitados)
    {
        int Posicion = PosicionVertice(pVertice);
        ArreglosVisitados[Posicion - 1] = "T";
        Debug.Log(" " + pVertice.nombre + " , ");
        for (int i = 0; i <= GradoSaliente(pVertice) - 1; i++)
        {
            CVertice NuevoVertice = IesimoSucesor(pVertice, i + 1);
            int j = PosicionVertice(NuevoVertice);
            if (ArreglosVisitados[j - 1] == "F")
            {
                Debug.Log(NuevoVertice.nombre + "'");
                ArreglosVisitados[j - 1] = "T";
            }
        }
    }

    public void dikstra(CLista verticeOrigen, CLista verticeDestino)
    {
        bool verticeAcualExisteListaDeVisitados, rutaNoEncontrada = false;
        int costoActual = 0;
        CLista verticeActual, destinoActual;
        CLista aristaAuxiliar;
        List<Pair<CLista, int>> listaCosto = new List<Pair<CLista, int>>();
        List<Pair<CLista, int>> listaOrdenada = new List<Pair<CLista, int>>();
        Stack<Pair<CLista, CLista>> pila = new Stack<Pair<CLista, CLista>>();
        listaCosto.Add(new Pair<CLista, int>(verticeOrigen, 0));
        listaOrdenada.Add(new Pair<CLista, int>(verticeOrigen, 0));
        
        while (listaOrdenada.Count != 0)
        {
            Debug.Log("while (!listaOrdenada.Any()), primer while");
            verticeActual = listaOrdenada[0].First;
            costoActual = listaOrdenada[0].Second;
            listaOrdenada.RemoveAt(0);
            if (verticeActual == verticeDestino)
            {
                Debug.Log("if (verticeActual == verticeDestino)");
                rutaNoEncontrada = true;
                destinoActual = verticeDestino;
                while (pila.Count != 0)
                {
                    Debug.Log("while (!pila.Any()), PRIMERO");
                    Debug.Log(destinoActual.Elemento.nombre + "<--");
                    //cout << destinoActual->etiquetaVertice << "<--";
                    while (pila.Count != 0 && pila.Peek().Second != destinoActual)
                    {
                        pila.Pop();
                        Debug.Log("while (!pila.Any() && pila.Peek().Second != destinoActual), PRIMERO");
                    }
                    if (pila.Count != 0)
                    {
                        Debug.Log("if (!pila.Any()), PRIMERO");
                        destinoActual = pila.Peek().First;
                    }
                }
                break;
            }
            if (verticeActual != null)
            {
                aristaAuxiliar = verticeActual.SubLista;
            }
            else
            {
                aristaAuxiliar = null;
            }

            while (aristaAuxiliar != null)
            {
                verticeAcualExisteListaDeVisitados = false;
                costoActual = costoActual + aristaAuxiliar.Peso;//Cual peso?
                for (int i = 0; i < listaCosto.Count; i++)
                {
                    if (aristaAuxiliar.SubLista == listaCosto[i].First)
                    {
                        verticeAcualExisteListaDeVisitados = true;
                        if (costoActual < listaCosto[i].Second)
                        {
                            listaCosto[i].Second = costoActual;
                            for (int j = 0; j < listaOrdenada.Count; j++)
                            {
                                if (listaOrdenada[j].First == aristaAuxiliar.SubLista)
                                {
                                    listaOrdenada[j].Second = costoActual;
                                }
                            }
                            //listaOrdenada.sort(comparacionDinamica);
                            listaOrdenada.Sort(delegate (Pair<CLista, int> x, Pair<CLista, int> y)
                            {
                                return Convert.ToInt32((x.Second < y.Second));
                            });

                            pila.Push(new Pair<CLista, CLista>(verticeActual, aristaAuxiliar.SubLista));
                            costoActual = costoActual - aristaAuxiliar.Peso;//Cual peso?
                            break;
                        }
                        costoActual = costoActual - aristaAuxiliar.Peso;//Cual peso?
                    }
                }
                if (verticeAcualExisteListaDeVisitados == false)
                {
                    listaCosto.Add(new Pair<CLista, int>(aristaAuxiliar.SubLista, costoActual));
                    listaOrdenada.Add(new Pair<CLista, int>(aristaAuxiliar.SubLista, costoActual));
                    //listaOrdenada.sort(comparacionDinamica);
                    listaOrdenada.Sort(delegate (Pair<CLista, int> x, Pair<CLista, int> y)
                    {
                        return Convert.ToInt32((x.Second < y.Second));
                    });
                    for (int pk = 0; pk < listaOrdenada.Count; pk++)
                    {
                        // Console.WriteLine(listaOrdenada[pk].First.Elemento.nombre);
                    }
                    pila.Push(new Pair<CLista, CLista>(verticeActual, aristaAuxiliar.SubLista));
                    costoActual = costoActual - aristaAuxiliar.Peso;//Cual peso?
                }
                aristaAuxiliar = aristaAuxiliar.SubLista;
            }
        }
        if (rutaNoEncontrada == false)
        {
            Debug.Log("No existe una ruta entre el vertice origen. " + costoActual);
            // cout << "No existe una ruta entre el vertice origen \"" << verticeOrigen->etiquetaVertice
            //    << "\" y el vertice destino \"" << verticeDestino->etiquetaVertice << "\"." << endl;
        }
        else
        {
            //cout << endl;
            Debug.Log("El costo de la mejor ruta es: " + costoActual);
        }
    }

    List<Pair<int, int>>[] ady;

    public int[,] construirTablaDeAdyacencia2(int personajeEntero)
    {
        int numeroVertices = NumerodeVertices();
        ady = new List<Pair<int, int>>[numeroVertices];

        int[,] matrizAdyacencia = new int[numeroVertices, numeroVertices];
        for (int i = 0; i < numeroVertices; i++)
        {
            for (int j = 0; j < numeroVertices; j++)
            {
                matrizAdyacencia[i, j] = -1;
            }
        }

        CVertice encontrado = null;
        // Console.WriteLine("aVertice.nombre: " + aVertice.nombre + " " + aVertice.tipoTerreno + ", Buscado: " + verticeBuscar.nombre + " " + verticeBuscar.tipoTerreno);


        for (int i = 1; i <= aLista.NroElementos(); i++)
        {
            Debug.Log(aVertice.nombre + " ==> " + aLista.IesimoElemento(i));
            //conexiones.Add((CVertice)aLista.IesimoElemento(i));
            ady[Convert.ToInt32(aVertice.nombre)].Add(new Pair<int, int>(Convert.ToInt32(((CVertice)aLista.IesimoElemento(i)).nombre), (int)aLista.IesimoElementoPeso(i, personajeEntero))); //consideremos grafo dirigido
            matrizAdyacencia[Convert.ToInt32(aVertice.nombre), Convert.ToInt32(((CVertice)aLista.IesimoElemento(i)).nombre)] = (int)aLista.IesimoElementoPeso(i, personajeEntero);
        }
        CGrafo auxiliar = aSiguiente;
        while (auxiliar != null)
        {
            for (int i = 1; i <= auxiliar.aLista.NroElementos(); i++)
            {
                ady[Convert.ToInt32(auxiliar.aVertice.nombre)].Add(new Pair<int, int>(Convert.ToInt32(((CVertice)auxiliar.aLista.IesimoElemento(i)).nombre), (int)auxiliar.aLista.IesimoElementoPeso(i, personajeEntero))); //consideremos grafo dirigido
                matrizAdyacencia[Convert.ToInt32(auxiliar.aVertice.nombre), Convert.ToInt32(((CVertice)auxiliar.aLista.IesimoElemento(i)).nombre)] = (int)auxiliar.aLista.IesimoElementoPeso(i, personajeEntero);
            }
            auxiliar = auxiliar.aSiguiente;
            if (auxiliar.aVertice == null)
            {
                break;
            }
        }
        Debug.Log("TERMINO CREAR MATRIZ");
        for (int i = 0; i < numeroVertices; i++)
        {
            for (int j = 0; j < numeroVertices; j++)
            {
                Debug.Log(matrizAdyacencia[i, j] + " ");
            }
            Debug.Log("");
        }
        return matrizAdyacencia;


    }


    private static int MinimumDistance(int[] distance, bool[]
    shortestPathTreeSet, int verticesCount)
    {
        int min = int.MaxValue;
        int minIndex = 0;

        for (int v = 0; v < verticesCount; ++v)
        {
            if (shortestPathTreeSet[v] == false && distance[v] <= min)
            {
                min = distance[v];
                minIndex = v;
            }
        }

        return minIndex;
    }

    private static void Print(int[] distance, int verticesCount)
    {
        Debug.Log("Vertex    Distance from source");

        for (int i = 0; i < verticesCount; ++i)
            Console.WriteLine("{0}\t  {1}", i, distance[i]);
    }

    public void Dijkstra(int[,] graph, int source, int verticesCount)
    {
        int[] distance = new int[verticesCount];
        bool[] shortestPathTreeSet = new bool[verticesCount];

        for (int i = 0; i < verticesCount; ++i)
        {
            distance[i] = int.MaxValue;
            shortestPathTreeSet[i] = false;
        }

        distance[source] = 0;

        for (int count = 0; count < verticesCount - 1; ++count)
        {
            int u = MinimumDistance(distance, shortestPathTreeSet, verticesCount);
            shortestPathTreeSet[u] = true;

            for (int v = 0; v < verticesCount; ++v)
            {
                if (!shortestPathTreeSet[v] && Convert.ToBoolean(graph[u, v]) && distance[u] != int.MaxValue && distance[u] + graph[u, v] < distance[v])
                {
                    distance[v] = distance[u] + graph[u, v];
                    Console.WriteLine("V: " + v + "  " + u);
                }
            }

        }

        Print(distance, verticesCount);
    }

    public List<int> primeroElMejor(int verticeOrigen, int verticeDestino, int personaje)
    {
        List<int> recorrido = new List<int>();
        bool verticeAcualExisteListaDeVisitados, rutaNoEncontrada = false;
        //List<List<string>> matrizAdyecenciaVerticesSinEspacios = crearMatrizAuxiliarVertices();
        //List<List<int>> matrizAdyecenciaAristasSinEspacios = crearMatrizAuxiliarAristas();
        long verticeActual, destinoActual;
        long aristaAuxiliar;
        List<int> conexionesAUnVertice;
        //verticeOrigen = convertirVerticeRecividoAVerticeDeMatrizSinEspacios(nombreVerticeOrigen, matrizAdyecenciaVerticesSinEspacios);
        //verticeDestino = convertirVerticeRecividoAVerticeDeMatrizSinEspacios(nombreVerticeDestino, matrizAdyecenciaVerticesSinEspacios);
        long[,] matrizAdyacencia = construirTablaDeAdyacencia(personaje);
        long costoActual = 0;
        List<Pair<long, long>> listaCosto = new List<Pair<long, long>>();
        List<Pair<long, long>> listaOrdenada = new List<Pair<long, long>>();
        Stack<Pair<long, long>> pila = new Stack<Pair<long, long>>();
        listaCosto.Add(new Pair<long, long>(verticeOrigen, 0));
        listaOrdenada.Add(new Pair<long, long>(verticeOrigen, 0));
        while (listaOrdenada.Count != 0)
        {
            verticeActual = listaOrdenada[0].First;
            costoActual = listaOrdenada[0].Second;
            listaOrdenada.RemoveAt(0);
            if (verticeActual == verticeDestino)
            {
                rutaNoEncontrada = true;
                destinoActual = verticeDestino;
                while (pila.Count != 0)
                {
                    recorrido.Add((int)destinoActual);
                    Console.Write(destinoActual + "<---");
                    //cout << matrizAdyecenciaVerticesSinEspacios[destinoActual][1] << "<--";
                    while (pila.Count != 0 && pila.Peek().Second != destinoActual)
                    {
                        pila.Pop();
                    }
                    if (pila.Count != 0)
                    {
                        destinoActual = pila.Peek().First;
                    }
                }
                break;
            }
            int numeroDeConexionesVerticeActual = calcularNumeroDeConexionesDeUnaArista(verticeActual, matrizAdyacencia);
            int k = 0;
            conexionesAUnVertice = calcularVerticeALosQueEsConexoUnVertice(verticeActual, matrizAdyacencia);

            aristaAuxiliar = conexionesAUnVertice[k];//primera conexion vertice actual
                                                     // i numero de conexiones que tiene el vertice;
            while (k < numeroDeConexionesVerticeActual)
            {
                aristaAuxiliar = conexionesAUnVertice[k];//siguiente conexion
                verticeAcualExisteListaDeVisitados = false;
                costoActual = costoActual + matrizAdyacencia[verticeActual, aristaAuxiliar];
                for (int i = 0; i != listaCosto.Count; i++)
                {
                    if (aristaAuxiliar == listaCosto[i].First)
                    {
                        verticeAcualExisteListaDeVisitados = true;
                        if (costoActual < listaCosto[i].Second)
                        {
                            listaCosto[i].Second = costoActual;
                            for (int j = 0; j != listaOrdenada.Count; j++)
                            {
                                if (listaOrdenada[j].First == aristaAuxiliar)
                                {
                                    listaOrdenada[j].Second = costoActual;
                                }
                            }
                            listaOrdenada.Sort(delegate (Pair<long, long> x, Pair<long, long> y)
                            {
                                if (x.Second == null && y.Second == null) return 0;
                                else if (x.Second == null) return -1;
                                else if (y.Second == null) return 1;
                                else return x.Second.CompareTo(y.Second);//Checar si es en este orden
                                });

                            pila.Push(new Pair<long, long>(verticeActual, aristaAuxiliar));
                            costoActual = costoActual - matrizAdyacencia[verticeActual, aristaAuxiliar];
                            break;
                        }
                        costoActual = costoActual - matrizAdyacencia[verticeActual, aristaAuxiliar];
                    }
                }
                if (verticeAcualExisteListaDeVisitados == false)
                {
                    listaCosto.Add(new Pair<long, long>(aristaAuxiliar, costoActual));
                    listaOrdenada.Add(new Pair<long, long>(aristaAuxiliar, costoActual));
                    listaOrdenada.Sort(delegate (Pair<long, long> x, Pair<long, long> y)
                    {
                        if (x.Second == null && y.Second == null) return 0;
                        else if (x.Second == null) return -1;
                        else if (y.Second == null) return 1;
                        else return x.Second.CompareTo(y.Second);//Checar si es en este orden
                        });
                    pila.Push(new Pair<long, long>(verticeActual, aristaAuxiliar));
                    costoActual = costoActual - matrizAdyacencia[verticeActual, aristaAuxiliar];
                }
                k++;

            }
        }
        if (rutaNoEncontrada == false)
        {
            Console.WriteLine("No existe una ruta entre el vertice origen \"");
        }
        else
        {
            Console.WriteLine("El costo de la mejor ruta es: " + costoActual);
        }
        recorrido.Reverse();
        return recorrido;
    }

    int calcularNumeroDeConexionesDeUnaArista(long verticeActual, long[,] matrizAdyacencia)
    {
        int numeroConexiones = 0;
        int numeroVertices = NumerodeVertices();
        for (int i = 0; i < numeroVertices; i++)
        {
            if (matrizAdyacencia[verticeActual, i] != -1)
            {
                numeroConexiones++;
            }
        }
        return numeroConexiones;
    }

    List<int> calcularVerticeALosQueEsConexoUnVertice(long verticeActual, long[,] matrizAdyacencia)
    {
        List<int> listaConexos = new List<int>();
        int numeroVertices = NumerodeVertices();
        for (int i = 0; i < numeroVertices; i++)
        {
            if (matrizAdyacencia[verticeActual, i] != -1)
            {
                listaConexos.Add(i);
            }
        }
        return listaConexos;
    }
}
