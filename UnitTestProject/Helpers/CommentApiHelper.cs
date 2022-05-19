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
    static class CommentApiHelper
    {
        //URI
        private static Uri _baseUri = new Uri("https://gorest.co.in/");
        //Auth_Token
        private static string _authToken = "Bearer 886b7f96c07a7348d64502105d5da86c7b3b33add1843a60ceeaae82627cad90";

        public static Task<RestResponse> CreateComment(Comment comment)
        {
            //RestClient Object
            var restClient = new RestClient();
            var url = new Uri(_baseUri, "/public/v1/posts/" + comment.post_id + "/comments");
            
            //API Request and Method
            var request = new RestRequest(url, Method.Post);
            
            //Request Headers
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", _authToken);

            //Request Body
            request.AddJsonBody(comment);
            
            //Response
            var response = restClient.ExecuteAsync(request);
            return response;
        }

        public static Task<RestResponse> GetAllComments()
        {
            //RestClient Object
            var restClient = new RestClient();
            var url = new Uri(_baseUri, "/public/v1/comments");

             //API Request and Method
            var request = new RestRequest(url, Method.Get);
            
            //Request Headers
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", _authToken);

             //Response
            var response = restClient.ExecuteAsync(request);
            return response;
        }

        public static Task<RestResponse> GetAllCommentsByPostId(int postId)
        {   
            //RestClient Object
            var restClient = new RestClient();
            var url = new Uri(_baseUri, "/public/v1/posts/" + postId + "/comments");

            //API Request and Method
            var request = new RestRequest(url, Method.Get);
            
            //Request Headers
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", _authToken);

            //Response
            var response = restClient.ExecuteAsync(request);
            return response;
        }

        public static Task<RestResponse> GetComment(int commentId)
        {
            //RestClient Object
            var restClient = new RestClient();
            var url = new Uri(_baseUri, "/public/v1/comments/" + commentId);

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
