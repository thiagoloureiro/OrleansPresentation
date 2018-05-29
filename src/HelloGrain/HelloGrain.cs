using System;
using System.Diagnostics;
using System.Threading;
using IHelloGrain;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace HelloGrain
{
    public class HelloGrain : Orleans.Grain, IHello
    {
        private readonly ILogger logger;

        public HelloGrain(ILogger<HelloGrain> logger)
        {
            this.logger = logger;
        }

        Task<string> IHello.SayHello(string greeting)
        {
            logger.LogInformation($"SayHello message received: greeting = '{greeting}'");
            return Task.FromResult($"You said: '{greeting}', I say: Hello! Essa mensagem veio do Grain!");
        }
        Task<double> IHello.CalculaCotacao(double valor, double cotacao)
        {
            var resultado = valor * cotacao;
            logger.LogInformation($"Cotação realizada com sucesso: {resultado}");

            return Task.FromResult(resultado);
        }
    }
}
