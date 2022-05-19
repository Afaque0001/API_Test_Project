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
        //URI
        private static Uri _baseUri = new Uri("https://gorest.co.in/");
        
        //Auth_Token
        private static string _authToken = "Bearer 886b7f96c07a7348d64502105d5da86c7b3b33add1843a60ceeaae82627cad90";

        public static Task<RestResponse> CreateTodo(Todo todo)
        {
            //RestClient Object
            var restClient = new RestClient();
            var url = new Uri(_baseUri, "/public/v1/users/" + todo.user_id + "/todos");

            //API Request and Method
            var request = new RestRequest(url, Method.Post);
            
            //Request Headers
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization",_authToken);
            
            //Request Body
            request.AddJsonBody(todo);

            //Response
            var response = restClient.ExecuteAsync(request);
            return response;
        }

        public static Task<RestResponse> GetAllTodos()
        {
            //RestClient Object
            var restClient = new RestClient();
            var url = new Uri(_baseUri, "/public/v1/todos");

            //API Request and Method
            var request = new RestRequest(url, Method.Get);
            
            //Request Headers
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", _authToken);
             
            //Response
            var response = restClient.ExecuteAsync(request);
            return response;
        }

        public static Task<RestResponse> GetAllTodosByUserId(int userId)
        {
            //RestClient Object
            var restClient = new RestClient();
            var url = new Uri(_baseUri, "/public/v1/users/" + userId + "/todos");

            //API Request and Method
            var request = new RestRequest(url, Method.Get);
            
            //Request Headers
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", _authToken);

            //Response
            var response = restClient.ExecuteAsync(request);
            return response;
        }

        public static Task<RestResponse> GetTodo(int todoId)
        {
            //RestClient Object
            var restClient = new RestClient();
            var url = new Uri(_baseUri, "/public/v1/todos/" + todoId);

            //API Request and Method
            var request = new RestRequest(url, Method.Get);
            
            //Request Headers
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", _authToken);

            //Response
            var response = restClient.ExecuteAsync(request);
            return response;
        }
    }
}
