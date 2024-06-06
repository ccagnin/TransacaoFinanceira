using Moq;
using TransacaoFinanceira.Data;
using TransacaoFinanceira.Exceptions;
using TransacaoFinanceira.Models;
using TransacaoFinanceira.Services;
using Xunit;

namespace TransacaoFinanceira.TransacaoFinanceira.Tests
{
    public class TransacaoServiceTests
    {
        private readonly Mock<IContaRepository> _contaRepositoryMock;
        private readonly Mock<ITransacaoRepository> _transacaoRepositoryMock;
        private readonly TransacaoService _transacaoService;

        public TransacaoServiceTests()
        {
            _contaRepositoryMock = new Mock<IContaRepository>();
            _transacaoRepositoryMock = new Mock<ITransacaoRepository>();
            _transacaoService = new TransacaoService(_contaRepositoryMock.Object, _transacaoRepositoryMock.Object);
        }

        [Fact]
        public void Transferir_ComSaldoSuficiente_DeveEfetivarTransacao()
        {
            var transacao = new Transacao(1, "09/09/2023 14:15:00", 1, 2, 100);
            var contaOrigem = new Conta(1, 200);
            var contaDestino = new Conta(2, 100);

            _contaRepositoryMock.Setup(repo => repo.GetSaldo(1)).Returns(contaOrigem);
            _contaRepositoryMock.Setup(repo => repo.GetSaldo(2)).Returns(contaDestino);

            _transacaoService.Transferir(transacao);

            Assert.Equal(100, contaOrigem.Saldo);
            Assert.Equal(200, contaDestino.Saldo);

            _contaRepositoryMock.Verify(repo => repo.AtualizarSaldo(contaOrigem), Times.Once);
            _contaRepositoryMock.Verify(repo => repo.AtualizarSaldo(contaDestino), Times.Once);
            _transacaoRepositoryMock.Verify(repo => repo.AdicionarTransacao(transacao), Times.Once);
        }

        [Fact]
        public void Transferir_SemSaldoSuficiente_DeveLancarSaldoInsuficienteException()
        {
            var transacao = new Transacao(1, "09/09/2023 14:15:00", 1, 2, 300);
            var contaOrigem = new Conta(1, 200);
            var contaDestino = new Conta(2, 100);

            _contaRepositoryMock.Setup(repo => repo.GetSaldo(1)).Returns(contaOrigem);
            _contaRepositoryMock.Setup(repo => repo.GetSaldo(2)).Returns(contaDestino);

            var ex = Assert.Throws<SaldoInsuficienteException>(() => _transacaoService.Transferir(transacao));
            Assert.Equal("Transação número 1 foi cancelada por falta de saldo.", ex.Message);

            _contaRepositoryMock.Verify(repo => repo.AtualizarSaldo(It.IsAny<Conta>()), Times.Never);
            _transacaoRepositoryMock.Verify(repo => repo.AdicionarTransacao(It.IsAny<Transacao>()), Times.Never);
        }

        [Fact]
        public void Transferir_ContaOrigemNaoEncontrada_DeveLancarContaNaoEncontradaException()
        {
            var transacao = new Transacao(1, "09/09/2023 14:15:00", 1, 2, 100);

            _contaRepositoryMock.Setup(repo => repo.GetSaldo(1)).Returns((Conta)null);
            _contaRepositoryMock.Setup(repo => repo.GetSaldo(2)).Returns(new Conta(2, 100));

            var ex = Assert.Throws<ContaNaoEncontradaException>(() => _transacaoService.Transferir(transacao));
            Assert.Equal("Conta com ID 1 não encontrada.", ex.Message);

            _contaRepositoryMock.Verify(repo => repo.AtualizarSaldo(It.IsAny<Conta>()), Times.Never);
            _transacaoRepositoryMock.Verify(repo => repo.AdicionarTransacao(It.IsAny<Transacao>()), Times.Never);
        }

        [Fact]
        public void Transferir_ContaDestinoNaoEncontrada_DeveLancarContaNaoEncontradaException()
        {
            var transacao = new Transacao(1, "09/09/2023 14:15:00", 1, 2, 100);

            _contaRepositoryMock.Setup(repo => repo.GetSaldo(1)).Returns(new Conta(1, 200));
            _contaRepositoryMock.Setup(repo => repo.GetSaldo(2)).Returns((Conta)null);

            var ex = Assert.Throws<ContaNaoEncontradaException>(() => _transacaoService.Transferir(transacao));
            Assert.Equal("Conta com ID 2 não encontrada.", ex.Message);

            _contaRepositoryMock.Verify(repo => repo.AtualizarSaldo(It.IsAny<Conta>()), Times.Never);
            _transacaoRepositoryMock.Verify(repo => repo.AdicionarTransacao(It.IsAny<Transacao>()), Times.Never);
        }
    }
}
