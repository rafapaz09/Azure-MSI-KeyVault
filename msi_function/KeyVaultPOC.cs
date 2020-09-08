using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Azure.KeyVault;
using msi_function.Models;
using Microsoft.Azure.KeyVault.Models;
using System.Configuration;
using msi_function.DataAccess;

namespace msi_function
{
    public static class KeyVaultPOC
    {
        [FunctionName("KeyVaultPOC")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            //Getting secret from query
            string secret = req.Query["secret"];

            //Validating if secret is empty
            if (string.IsNullOrEmpty(secret))
                return new BadRequestObjectResult("Request does not contain a valid Secret");

            SecretRequest secretRequest = new SecretRequest(secret);

            log.LogInformation($"GetKeyVaultSecret request received for secret { secretRequest.Secret}");

            //Creating keyvault object
            var keyvault = new KeyVault();

            log.LogInformation("Secret Value retrieved from KeyVault.");

            //Getting secret information
            var secretBundle = await keyvault.GetSecret(secret);

            //Returning secret data
            var secretResponse = new SecretResponse { Secret = secretRequest.Secret, Value = secretBundle.Value };

            return new OkObjectResult(secretResponse);

        }

    }
}
