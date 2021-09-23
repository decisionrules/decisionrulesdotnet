using DecisionRules.Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static DecisionRules.Model.SolverStragiesEnum;

namespace DecisionRules
{
    public class DecisionRulesBase
    {
        protected readonly HttpClient client = new HttpClient();
        protected readonly RequestOption globalOptions;

        public DecisionRulesBase(RequestOption options)
        {
            this.globalOptions = options;
        }

        protected async Task<HttpResponseMessage> ApiCall<T>(string url, T inputData, SolverStrategies solverStrategy)
        {
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.globalOptions.ApiKey);

            if (solverStrategy != SolverStrategies.STANDARD)
            {
                client.DefaultRequestHeaders.Add("X-Strategy", solverStrategy.ToString());
            }

            try
            {
                HttpResponseMessage response;

                if (!(inputData is string))
                {
                    var requestData = new RequestModel<T>(inputData);

                    _ = new JsonSerializerOptions { WriteIndented = false };

                    var request = JsonSerializer.Serialize(requestData);

                    response = await client.PostAsync(url, new StringContent(request, Encoding.UTF8, "application/json"));
                }
                else
                {
                    response = await client.PostAsync(url, new StringContent(inputData.ToString(), Encoding.UTF8, "application/json"));
                }

                ValidateResponse(response);

                return response;
            }
            catch (NoUserException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (TooManyApiCallsException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (NotPublishedException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (ServerErrorException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

        }

        protected async Task<HttpResponseMessage> ApiCall<T>(string url, T inputData)
        {
           this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.globalOptions.PublicApiKey);

            try
            {
                HttpResponseMessage response;

                if (!(inputData is string))
                {
                    var requestData = new RequestModel<T>(inputData);

                    _ = new JsonSerializerOptions { WriteIndented = false };

                    var request = JsonSerializer.Serialize(requestData);

                    response = await client.PostAsync(url, new StringContent(request, Encoding.UTF8, "application/json"));
                }
                else
                {
                    string data = inputData.ToString();

                    response = await client.PostAsync(url, new StringContent(data, Encoding.UTF8, "application/json"));
                }

                ValidateResponse(response);

                return response;
            }
            catch (NoUserException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (TooManyApiCallsException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (NotPublishedException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (ServerErrorException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

        }

        protected async Task<HttpResponseMessage> ApiCallPut<T>(string url, T inputData)
        {
            this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.globalOptions.PublicApiKey);

            try
            {
                HttpResponseMessage response;


                if (!(inputData is string))
                {
                    var requestData = new RequestModel<T>(inputData);

                    _ = new JsonSerializerOptions { WriteIndented = false };

                    var request = JsonSerializer.Serialize(requestData);

                    response = await client.PutAsync(url, new StringContent(request, Encoding.UTF8, "application/json"));
                }
                else
                {
                    response = await client.PutAsync(url, new StringContent(inputData.ToString(), Encoding.UTF8, "application/json"));
                }


                ValidateResponse(response);

                return response;
            }
            catch (NoUserException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (TooManyApiCallsException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (NotPublishedException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (ServerErrorException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        protected async Task<HttpResponseMessage> ApiCallGet(string url)
        {
            try
            {
                this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.globalOptions.PublicApiKey);

                HttpResponseMessage response;

                response = await client.GetAsync(url);

                ValidateResponse(response);

                return response;
            }
            catch (NoUserException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (TooManyApiCallsException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (NotPublishedException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (ServerErrorException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

        }

        protected async Task<HttpResponseMessage> ApiCallDelete(string url)
        {
            try
            {
                this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.globalOptions.PublicApiKey);

                HttpResponseMessage response;

                response = await client.DeleteAsync(url);

                ValidateResponse(response);

                return response;
            }
            catch (NoUserException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (TooManyApiCallsException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (NotPublishedException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (ServerErrorException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

        }

        private void ValidateResponse(HttpResponseMessage response)
        {
            if (response.StatusCode.Equals(HttpStatusCode.BadRequest))
            {
                throw new NotPublishedException();
            }
            else if (response.StatusCode.Equals(HttpStatusCode.UpgradeRequired))
            {
                throw new TooManyApiCallsException();
            }
            else if (response.StatusCode.Equals(HttpStatusCode.Unauthorized))
            {
                throw new NoUserException();
            }
            else if (response.StatusCode.Equals(HttpStatusCode.InternalServerError) || response.StatusCode.Equals(HttpStatusCode.ServiceUnavailable))
            {
                throw new ServerErrorException();
            }
        }
    }
}
