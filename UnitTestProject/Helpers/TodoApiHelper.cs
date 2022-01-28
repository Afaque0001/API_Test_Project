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
    static class TodoApiHelper
    {
        private static Uri _baseUri = new Uri("https://gorest.co.in/");
        private static string _authToken = "Bearer 886b7f96c07a7348d64502105d5da86c7b3b33add1843a60ceeaae82627cad90";

        public static Task<RestResponse> CreateTodo(Todo todo)
        {
            var restClient = new RestClient();
            var url = new Uri(_baseUri, "/public/v1/users/" + todo.user_id + "/todos");

            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization",_authToken);

            request.AddJsonBody(todo);

            var response = restClient.ExecuteAsync(request);
            return response;
        }

        public static Task<RestResponse> GetAllTodos()
        {
            var restClient = new RestClient();
            var url = new Uri(_baseUri, "/public/v1/todos");

            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", _authToken);
             
            var response = restClient.ExecuteAsync(request);
            return response;
        }

        public static Task<RestResponse> GetAllTodosByUserId(int userId)
        {
            var restClient = new RestClient();
            var url = new Uri(_baseUri, "/public/v1/users/" + userId + "/todos");

            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", _authToken);

            var response = restClient.ExecuteAsync(request);
            return response;
        }

        public static Task<RestResponse> GetTodo(int todoId)
        {
            var restClient = new RestClient();
            var url = new Uri(_baseUri, "/public/v1/todos/" + todoId);

            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", _authToken);

            var response = restClient.ExecuteAsync(request);
            return response;
        }
    }
}
