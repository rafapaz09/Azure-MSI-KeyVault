using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Azure.Services.AppAuthentication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace msi_function.DataAccess
{
    public class KeyVault
    {

        private KeyVaultClient keyVaultClient;
        private string KeyVaultUri;

        public KeyVault()
        {
            Init();
        }

        public void Init()
        {
            var serviceTokenProvider = new AzureServiceTokenProvider();
            keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(serviceTokenProvider.KeyVaultTokenCallback));
            KeyVaultUri = System.Environment.GetEnvironmentVariable("KeyVaultUri");
        }

        public async Task<SecretBundle> GetSecret(string secret)
        {
            try
            {
                //Building keyvault uri
                string uri = $"{KeyVaultUri}Secrets/{secret}";

                //Returning keyvault secret
                return await keyVaultClient.GetSecretAsync(uri);
            }
            catch (KeyVaultErrorException kex)
            {
                throw new Exception(kex.Message);
            }
        }


    }
}
