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
    static class PostApiHelper
    {
        //URI
        private static Uri _baseUri = new Uri("https://gorest.co.in/");
        
        //Auth_Token
        private static string _authToken = "Bearer 886b7f96c07a7348d64502105d5da86c7b3b33add1843a60ceeaae82627cad90";

        public static Task<RestResponse> CreatePost(Post post)
        {
             //RestClient Object
            var restClient = new RestClient();
            var url = new Uri(_baseUri, "/public/v1/users/" + post.user_id + "/posts");
            
            //API Request and Method
            var request = new RestRequest(url, Method.Post);
            
            //Request Headers
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization",_authToken);

            //Request Body
            request.AddJsonBody(post);
            
            //Response
            var response = restClient.ExecuteAsync(request);
            return response;
        }

        public static Task<RestResponse> GetAllPosts()
        {
            //RestClient Object
            var restClient = new RestClient();
            var url = new Uri(_baseUri, "/public/v1/posts");
            
            //API Request and Method
            var request = new RestRequest(url, Method.Get);
            
            //Request Headers
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", _authToken);
             
            //Response
            var response = restClient.ExecuteAsync(request);
            return response;
        }

        public static Task<RestResponse> GetAllPostsByUserId(int userId)
        {
            //RestClient Object
            var restClient = new RestClient();
            var url = new Uri(_baseUri, "/public/v1/users/" + userId + "/posts");

            //API Request and Method
            var request = new RestRequest(url, Method.Get);
            
            //Request Headers
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", _authToken);

            //Response
            var response = restClient.ExecuteAsync(request);
            return response;
        }

        public static Task<RestResponse> GetPost(int postId)
        {
            //RestClient Object
            var restClient = new RestClient();
            var url = new Uri(_baseUri, "/public/v1/posts/" + postId);

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
