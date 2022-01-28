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
        private static Uri _baseUri = new Uri("https://gorest.co.in/");
        private static string _authToken = "Bearer 886b7f96c07a7348d64502105d5da86c7b3b33add1843a60ceeaae82627cad90";

        public static Task<RestResponse> CreateComment(Comment comment)
        {
            var restClient = new RestClient();
            var url = new Uri(_baseUri, "/public/v1/posts/" + comment.post_id + "/comments");

            var request = new RestRequest(url, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", _authToken);

            request.AddJsonBody(comment);

            var response = restClient.ExecuteAsync(request);
            return response;
        }

        public static Task<RestResponse> GetAllComments()
        {
            var restClient = new RestClient();
            var url = new Uri(_baseUri, "/public/v1/comments");

            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", _authToken);

            var response = restClient.ExecuteAsync(request);
            return response;
        }

        public static Task<RestResponse> GetAllCommentsByPostId(int postId)
        {
            var restClient = new RestClient();
            var url = new Uri(_baseUri, "/public/v1/posts/" + postId + "/comments");

            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", _authToken);

            var response = restClient.ExecuteAsync(request);
            return response;
        }

        public static Task<RestResponse> GetComment(int commentId)
        {
            var restClient = new RestClient();
            var url = new Uri(_baseUri, "/public/v1/comments/" + commentId);

            var request = new RestRequest(url, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", _authToken);

            var response = restClient.ExecuteAsync(request);
            return response;
        }




    }
}
