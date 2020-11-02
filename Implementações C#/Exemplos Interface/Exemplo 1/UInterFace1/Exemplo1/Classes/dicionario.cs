using System;
using System.Collections.Generic;

public class Dicionario
{
	private string criterio = null;
	IBuscador procura = null;

	public List<String> Procurar()
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

	public Dicionario(string criterio)
    {
		this.criterio = criterio;
    }
	
}
