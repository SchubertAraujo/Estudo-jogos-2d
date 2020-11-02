using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;

public class Buscador : IBuscador
{
    public List<string> GetResultado(string criterio)
    {
        var itens = new List<string> { "Prato", "Garfo", "Colher", "Tolha de mesa" };
        var resultado = itens.Where(e => e.Contains(criterio)).ToList();
        return resultado;
    }
}

public class BuscadorTimes : IBuscador
{
    public List<string> GetResultado(string criterio)
    {
        var times = new List<string> {"Palmeras", "Goias"};
        var resultados = times.Where(e => e.Contains(criterio)).ToList();
        return resultados;


    } 
}



