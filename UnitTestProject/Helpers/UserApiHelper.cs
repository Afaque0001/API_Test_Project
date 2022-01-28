using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnitTestProject.Models;

namespace UnitTestProject.Helpers
{
    static class UserApiHelper
    {
        private static Uri _baseUri = new Uri("https://gorest.co.in");
        private static string _authToken = "Bearer 886b7f96c07a7348d64502105d5da86c7b3b33add1843a60ceeaae82627cad90";

        public static Task<RestResponse> CreateUser(User user)
        {
            var restClient = new RestClient();
            var url = new Uri(_baseUri, "public/v1/users");

            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization",_authToken);

            request.AddJsonBody(user);

            var response = restClient.ExecuteAsync(request);
            return response;
        }

        public static Task<RestResponse> GetAllUsers()
        {
            var restClient = new RestClient();
            var url = new Uri(_baseUri, "public/v1/users");

            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", _authToken);
             
            var response = restClient.ExecuteAsync(request);
            return response;
        }

        public static Task<RestResponse> GetUser(int userId)
        {
            var restClient = new RestClient();
            var url = new Uri(_baseUri, "public/v1/users/" + userId);

            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", _authToken);

            var response = restClient.ExecuteAsync(request);
            return response;
        }
    }
}
