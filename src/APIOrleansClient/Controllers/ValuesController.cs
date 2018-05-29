using IHelloGrain;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace APIOrleansClient.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public async Task<string> Get()
        {
            var grain = Program._client.GetGrain<IHello>(0);
            var response = await grain.SayHello("Good morning, my friend!");
            return response;
        }


        [HttpPost]
        public async Task<double> Post(double valor, double cotacao)
        {
            var grain = Program._client.GetGrain<IHello>(0);
            var response = await grain.CalculaCotacao(valor, cotacao);
            return response;
        }

      
    }
}
