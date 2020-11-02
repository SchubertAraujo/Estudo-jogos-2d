
namespace UInterface
{
    class BuscadorFactory
    {
        public static  string ESP = "ESP";
        public static string TIM = "TIM";

        public IBuscador getBuscador(string tipo)
        {
            IBuscador procura = null;

            if (tipo.Equals(ESP))
            {
                procura = new Buscador();
            }
            else if (tipo.Equals(TIM))
            {
                procura = new BuscadorTimes();
            }
            return procura;
        }
            
    }
}
