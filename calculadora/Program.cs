namespace Calculadora
{
    public class Pila
    {
        private Nodo tope;


        private class Nodo
        {
            public char Dato;
            public Nodo Siguiente;


            public Nodo(char dato)
            {
                Dato = dato;
            }
        }


        public bool Vacia()
        {
            return tope == null;
        }


        public void Apilar(char dato)
        {
            Nodo nuevo = new Nodo(dato);
            nuevo.Siguiente = tope;
            tope = nuevo;
        }


        public char Desapilar()
        {
            if (Vacia())
                return "\0";

            char dato = tope.Dato;
            tope = tope.Siguiente;


            return dato;
        }


        public char Tope()
        {
            if (Vacia())
                return "\0";


            return tope.Dato;
        }
    }
}


Program.cs
using System;


namespace Calculadora
{
    internal class Program
    {
        static int Prioridad(char operador)
        {
            if (operador == "*" || operador == "-")
                return 1;


            if (operador == "*" || operador == "/")
                return 2;


            if (operador == "^")
                return 3;


            return 0;
        }
    }
}