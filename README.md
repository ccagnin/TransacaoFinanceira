# Documentação do Projeto: Sistema de Transações Financeiras

## Sumário

1. [Introdução](#introdução)
2. [Arquitetura do Sistema](#arquitetura-do-sistema)
3. [Princípios e Padrões de Design](#princípios-e-padrões-de-design)
4. [Tratamento de Erros e Logging](#tratamento-de-erros-e-logging)
5. [Concorrência](#concorrência)
6. [Testes Unitários](#testes-unitários)
7. [Instalação e Execução](#instalação-e-execução)

## Introdução

Este projeto é um sistema de transações financeiras desenvolvido em .NET 8.0. Ele permite a transferência de valores entre contas bancárias, assegurando a integridade e consistência dos dados. A aplicação foi projetada seguindo uma arquitetura em camadas e utilizando princípios e padrões de design para garantir manutenibilidade e extensibilidade.

## Arquitetura do Sistema

O sistema foi implementado utilizando uma arquitetura em camadas, que é dividida em:

1. **Camada de Apresentação**:
    - `Program.cs`: Ponto de entrada da aplicação. Configura e inicializa os componentes principais.

2. **Camada de Serviço**:
    - Responsável pela lógica de negócios.
    - `ITransacaoService.cs`: Interface do serviço de transações.
    - `TransacaoService.cs`: Implementação do serviço de transações.

3. **Camada de Dados**:
    - Gerencia o acesso aos dados.
    - `IContaRepository.cs`: Interface para acesso aos dados das contas.
    - `ContaRepository.cs`: Implementação do repositório de contas.
    - `ITransacaoRepository.cs`: Interface para acesso aos dados das transações.
    - `TransacaoRepository.cs`: Implementação do repositório de transações.

4. **Camada de Modelo**:
    - Define as entidades do domínio.
    - `Conta.cs`: Modelo de conta bancária.
    - `Transacao.cs`: Modelo de transação financeira.

5. **Camada de Exceções**:
    - Define exceções personalizadas.
    - `SaldoInsuficienteException.cs`: Exceção para saldo insuficiente.
    - `ContaNaoEncontradaException.cs`: Exceção para conta não encontrada.

6. **Camada de Logging**:
    - Gerencia o registro de logs.
    - `Logger.cs`: Classe para logging utilizando Serilog.


## Princípios e Padrões de Design

O projeto adota os princípios SOLID para garantir um design de software escalável e de fácil manutenção:

### Single Responsibility Principle (SRP)

**Princípio**: Cada classe deve ter uma única responsabilidade.

**Implementação**:
- `TransacaoService.cs`: Esta classe é responsável apenas pela lógica de transferência de transações.
- `ContaRepository.cs` e `TransacaoRepository.cs`: Cada classe é responsável apenas pelo acesso aos dados das contas e das transações, respectivamente.
- `Logger.cs`: Responsável apenas pelo logging da aplicação.

### Open/Closed Principle (OCP)

**Princípio**: Classes devem estar abertas para extensão, mas fechadas para modificação.

**Implementação**:
- Interfaces como `IContaRepository.cs` e `ITransacaoRepository.cs` permitem que novas implementações de repositórios sejam adicionadas sem modificar as classes existentes. 
- `TransacaoService.cs` depende de abstrações (interfaces), permitindo que novas funcionalidades sejam adicionadas sem modificar a classe.

### Liskov Substitution Principle (LSP)

**Princípio**: Subtipos devem ser substituíveis por seus tipos base sem alterar a funcionalidade do programa.

**Implementação**:
- `IContaRepository` e `ITransacaoRepository` são interfaces implementadas por `ContaRepository` e `TransacaoRepository`. Qualquer implementação dessas interfaces pode ser substituída sem alterar a funcionalidade da aplicação.
- O uso de `Mock<IContaRepository>` e `Mock<ITransacaoRepository>` nos testes demonstra a substituição de subtipos por tipos base.

### Interface Segregation Principle (ISP)

**Princípio**: Muitas interfaces específicas são melhores do que uma interface geral.

**Implementação**:
- `IContaRepository.cs` e `ITransacaoRepository.cs` são interfaces específicas que segregam responsabilidades de acesso a dados das contas e transações. 

### Dependency Inversion Principle (DIP)

**Princípio**: Módulos de alto nível não devem depender de módulos de baixo nível. Ambos devem depender de abstrações.

**Implementação**:
- `TransacaoService` depende das interfaces `IContaRepository` e `ITransacaoRepository`, não das implementações concretas. As dependências são injetadas através do construtor, facilitando a substituição de implementações e promovendo a testabilidade.
- O uso de Dependency Injection é demonstrado no construtor de `TransacaoService` e na configuração dos testes com Moq.

### Padrões Implementados

- **Repository Pattern**: Utilizado para abstrair o acesso aos dados e permitir a substituição fácil das implementações de armazenamento.
    - `IContaRepository.cs` e `ContaRepository.cs`
    - `ITransacaoRepository.cs` e `TransacaoRepository.cs`

- **Dependency Injection**: Utilizada para injeção de dependências e facilitar a testabilidade.
    - `TransacaoService` injeta `IContaRepository` e `ITransacaoRepository` através do construtor.
    - Nos testes unitários (`TransacaoServiceTests.cs`), o Moq é utilizado para injetar mocks das dependências.


## Tratamento de Erros e Logging

### Tratamento de Erros

Exceções personalizadas são usadas para lidar com casos específicos de erro:

- `SaldoInsuficienteException`: Lançada quando uma transação é requerida sem saldo suficiente na conta de origem.
- `ContaNaoEncontradaException`: Lançada quando uma conta não é encontrada durante uma transação.

### Logging

O Serilog é utilizado para registrar logs de forma detalhada e configurável, facilitando a detecção de erros. A configuração básica escreve logs no console.

## Concorrência

No contexto do nosso sistema de transações financeiras, a concorrência permite que múltiplas transações sejam processadas simultaneamente de forma segura. Quando isso ocorre, o sistema deve ser capaz de processar essas transações em paralelo, em vez de processá-las sequencialmente. Isso reduz o tempo de espera e aumenta a capacidade de processamento. Garantir que essas operações concorrentes sejam seguras e não causem inconsistências nos dados é essencial para a integridade do software.

## Testes Unitários

### Ferramentas Utilizadas

- xUnit
- Moq

### Como Executar os Testes
```bash
dotnet test
```

## Instalação e Execução

### Requisitos

- .NET SDK 8.0 

### Passos para Instalação

1. **Clone o Repositório**:

    ```sh
    git clone https://github.com/ccagnin/TransacaoFinanceira.git
    ```

2. **Restaure os Pacotes**:

    ```sh
    dotnet restore
    ```

### Execução do Projeto

Para executar o projeto principal:

1. **Navegue até o diretório do projeto principal**:

    ```sh
    cd TransacaoFinanceira
    ```

2. **Execute o Projeto**:

    ```sh
    dotnet run
    ```

### Execução dos Testes

Para executar os testes unitários:

1. **Navegue até o diretório do projeto de testes**:

    ```sh
    cd ../TransacaoFinanceira.Tests
    ```

2. **Execute os Testes**:

    ```sh
    dotnet test
    ```




