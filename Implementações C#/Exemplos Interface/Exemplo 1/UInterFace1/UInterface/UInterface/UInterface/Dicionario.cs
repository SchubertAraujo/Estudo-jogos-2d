using System.Collections.Generic;

namespace UInterface
{
    public class Dicionario
    {
        private string criterio = null;
        IBuscador procura = null;

        public List<string> Procurar(List<string> Lista)
        {
            if (criterio.EndsWith(criterio))
            {
                procura = new Buscador();
            }
            else if (criterio.StartsWith(criterio))
            {
                procura = new BuscadorTimes();
            }
            return procura.GetResultado(criterio);
        }

       //implementação usando o factory 
      //public List<string> Procurar(List<string> Lista)
      //{
      //  BuscadorFactory factory = new BuscadorFactory();
      //  if(criterio.EndsWith(criterio))
      //  {
      //      procura = factory.getBuscador(BuscadorFactory.ESP);
      //  }
      //  else if(criterio.StartsWith(criterio))
      //  {
      //      procura = factory.getBuscador(BuscadorFactory.TIM);
      //  }
      //  return procura.GetResultado(criterio);
      //}
      public Dicionario(string _criterio)
      {
          criterio = _criterio;
      }

    }
}
