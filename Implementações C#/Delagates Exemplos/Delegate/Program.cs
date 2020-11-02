using System;
using System.Collections.Generic;

namespace Delegate
{
    class Program
    {
  
        static void Main(string[] args)
        {
             
            //Variavel action para apontar para funçoes
            Action meuDelegate = EscreveMensagem; //Aqui é apontado a area de memoria da função e não a função
            meuDelegate(); // Uma forma de invocar o delegate 
            meuDelegate.Invoke(); // Outra forma de invocar o delegate


            //Chamando a funçao que popula
            //Veficar projetos novos para verificar se toda vez q é chamada ele gera um Dictionary novo.
            Mensagens("Msg4");
            Mensagens("Msg1");

            AcoesComRetorno("Acao1", "Zé");
            AcoesComRetorno("Acao2", "Pedro");


            Console.ReadKey();
        }

  

        //Para popular um delegate usar o dictonary como abaixo
        //Usado melhor para reduzir o numero de ifs
        public static void Mensagens(string Msg)
        {
            // O Action pode ser altarado para "Func" e passar variaveis da função depois o retorno
            Dictionary<string, Action> msgs = new Dictionary<string, Action>
            {
                {"Msg1", msg1},
                {"Msg2", msg2},
                {"Msg3", msg3},
                {"Msg4", msg4},
            };

            msgs[Msg].Invoke();
            
        }


        public static void AcoesComRetorno(string Acao, string nome)
        {
            // O Action pode ser altarado para "Func" e passar variaveis da função depois a do retorno
            Dictionary<string, Func<string, int>> act = new Dictionary<string, Func<string, int>>
            {
                {"Acao1", Acao1},
                {"Acao2", Acao2},

            };

            int retorno = act[Acao].Invoke(nome);
            Console.WriteLine(retorno);

        }


        public static void EscreveMensagem()
        {

            Console.WriteLine("Exibe");
        }
        public static void msg1()
        {
            Console.WriteLine("Mensagem 1");            
        }

        public static void msg2()
        {
            Console.WriteLine("Mensagem 2");
        }

        public static void msg3()
        {
            Console.WriteLine("Mensagem 3");
        }
        public static void msg4()
        {
            Console.WriteLine("Mensagem 4");
        }

        //Mais formas de trabalhar com o delegate
        //Funcoes com parametros e retornos
        public static int Acao1(string nome)
        {
            
            Console.WriteLine("Acao 1 " + nome);
            return 1;

        }

        public static int Acao2(string nome)
        {
            Console.WriteLine("Acao 2 " + nome);
            return 2;
        }
    }
}
