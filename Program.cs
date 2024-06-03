using System;
using System.Collections.Generic;
using System.Threading.Tasks;


//Obs: Voce é livre para implementar na linguagem de sua preferência, desde que respeite as funcionalidades e saídas existentes, além de aplicar os conceitos solicitados.

namespace TransacaoFinanceira
{
    class Program
    {

        static void Main(string[] args)
        {
            var TRANSACOES = new[] { 

            };
            executarTransacaoFinanceira executor = new executarTransacaoFinanceira();
            Parallel.ForEach(TRANSACOES, item =>
            {
                executor.transferir(item.correlation_id, item.conta_origem, item.conta_destino, item.VALOR);
            });

        }
    }

    class executarTransacaoFinanceira: acessoDados
    {
        public void transferir(int correlation_id, int conta_origem, int conta_destino, decimal valor)
        {
            contas_saldo conta_saldo_origem = getSaldo<contas_saldo>(conta_origem) ;
            if (conta_saldo_origem.saldo < valor)
            {
                Console.WriteLine("Transacao numero {0 } foi cancelada por falta de saldo", correlation_id);

            }
            else
            {
                contas_saldo conta_saldo_destino = getSaldo<contas_saldo>(conta_destino);
                conta_saldo_origem.saldo -= valor;
                conta_saldo_destino.saldo += valor;
                Console.WriteLine("Transacao numero {0} foi efetivada com sucesso! Novos saldos: Conta Origem:{1} | Conta Destino: {3}", correlation_id, conta_saldo_origem.saldo, conta_saldo_destino.saldo);
            }
        }
    }
    class contas_saldo
    {
        public contas_saldo(int conta, decimal valor)
        {
            this.conta = conta;
            this.saldo = valor;
        }
        public int conta { get; set; }
        public decimal saldo { get; set; }
    }
    class acessoDados
    {
        Dictionary<int, decimal> SALDOS { get; set; }
        private List<contas_saldo> TABELA_SALDOS;
        public acessoDados()
        {
            TABELA_SALDOS = new List<contas_saldo>();
            TABELA_SALDOS.Add(new contas_saldo(938485762, 180));
            TABELA_SALDOS.Add(new contas_saldo(347586970, 1200));
            TABELA_SALDOS.Add(new contas_saldo(2147483649, 0));
            TABELA_SALDOS.Add(new contas_saldo(675869708, 4900));
            TABELA_SALDOS.Add(new contas_saldo(238596054, 478));
            TABELA_SALDOS.Add(new contas_saldo(573659065, 787));
            TABELA_SALDOS.Add(new contas_saldo(210385733, 10));
            TABELA_SALDOS.Add(new contas_saldo(674038564, 400));
            TABELA_SALDOS.Add(new contas_saldo(563856300, 1200));


            SALDOS = new Dictionary<int, decimal>();
            this.SALDOS.Add(938485762, 180);
           
        }
        public T getSaldo<T>(int id)
        {          
            return (T)Convert.ChangeType(TABELA_SALDOS.Find(x => x.conta == id), typeof(T));
        }
        public bool atualizar<T>(T  dado)
        {
            try
            {
                contas_saldo item = (dado as contas_saldo);
                TABELA_SALDOS.RemoveAll(x => x.conta == item.conta);
                TABELA_SALDOS.Add(dado as contas_saldo);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            
        }

    }
}
