﻿using System.Threading.Tasks;

namespace IHelloGrain
{
    public interface IHello : Orleans.IGrainWithIntegerKey
    {
        Task<string> SayHello(string greeting);

        Task<double> CalculaCotacao(double valor, double cotacao);
    }
}