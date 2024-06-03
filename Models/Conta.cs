namespace TransacaoFinanceira.Models;

public class Conta
{
    public long Id { get; set; }
    public decimal Saldo { get; set; }
    
    public Conta(long id, decimal saldo)
    {
        Id = id;
        Saldo = saldo;
    }
}