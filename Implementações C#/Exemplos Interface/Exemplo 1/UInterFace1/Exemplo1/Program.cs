using System;

namespace Exemplo1
{
    class Program
    {
        static void Main(string[] args)
        {
            Dicionario dicionario = new Dicionario("Palmeiras");
            var lista = dicionario.Procurar();
            foreach (string l in lista) 
            {
                Console.WriteLine(l);
            }
            
        }
    }
}
