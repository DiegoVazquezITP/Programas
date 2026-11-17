using System;
using System.Xml.Linq;

namespaces Restaurante
{
    public class Cliente
{
    public string Nombre { get; set; }
    public int Personas { get; set; }


    public Cliete(string nombre, int personas)
    {
        Nombre = nombre;
        Personas = personas;
    }


    public override string ToString()
    {
        return $("Nombre)-(Personas)personas";
    }
}
}