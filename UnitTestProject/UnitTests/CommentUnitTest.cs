using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Threading.Tasks;
using UnitTestProject.Helpers;
using UnitTestProject.Models;

namespace UnitTestProject.UnitTests
{
    [TestClass]
    public class CommentUnitTest
    {
        private User _user;
        private Post _post;
        private Comment _comment;

        [TestMethod]
        public async Task Create_A_New_PostComment_Returns_Status_Code_201_Created()
        {
            InitializeUser();
            InitializePost();

            var existingPost = _post;

            Comment comment = new Comment
            {
                name = "this is name",
                body = "this is body",
                email = "qa_123@test.com",
                post_id = existingPost.id
            };

            var response = await CommentApiHelper.CreateComment(comment);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }

        [TestMethod]
        public async Task Create_A_New_PostComment_Without_Required_Fields_Name_And_Body_Returns_Status_Code_422_Unprocessable_Entity()
        {
            InitializeUser();
            InitializePost();

            var existingPost = _post;

            Comment comment = new Comment
            {
                post_id = existingPost.id,
                email = "qa_456@test.com"
            };

            var response = await CommentApiHelper.CreateComment(comment);
            Console.WriteLine(response.Content);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.UnprocessableEntity);
            var ExpectedStatusCode = response.StatusCode;
            var ActualStatusCode = HttpStatusCode.UnprocessableEntity;
            Console.WriteLine($"Expected Code: {ExpectedStatusCode}, \nActual Code: {ActualStatusCode}");
        }

        [TestMethod]
        public async Task Get_Existing_Comment_Returns_200_OK__And_Verify_Existing_Data()
        {

            InitializeUser();
            InitializePost();
            InitializeComment();

            var existingComment = _comment;

            var response = await CommentApiHelper.GetComment(existingComment.id);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

            var responseModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseModel>(response.Content);
            var commentString = Newtonsoft.Json.JsonConvert.SerializeObject(responseModel.data);
            var comment = Newtonsoft.Json.JsonConvert.DeserializeObject<Comment>(commentString);

            Assert.AreEqual(comment.body, existingComment.body);
            Assert.AreEqual(comment.id, existingComment.id);
            Assert.AreEqual(comment.name, existingComment.name);
            Assert.AreEqual(comment.email, existingComment.email);
            Assert.AreEqual(comment.post_id, existingComment.post_id);
            Console.Write($"Comment ID: {comment.id},\nEmail: {comment.email},\nName: {comment.name},\nPost ID: {comment.post_id},\nMain Comment: {comment.body}");
        }

        private void InitializeUser()
        {
            _user = new User
            {
                name = "Harness",
                email = string.Format("qa_{0}@test.com", Guid.NewGuid()),
                gender = "female",
                status = "active"
            };

            var response = UserApiHelper.CreateUser(_user).Result;

            if (response.IsSuccessful)
            {
                var responseModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseModel>(response.Content);
                var userString = Newtonsoft.Json.JsonConvert.SerializeObject(responseModel.data);
                var user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(userString);

                _user.id = user.id;
            }
        }

        private void InitializePost()
        {
            _post = new Post
            {
                title = "this is title",
                body = "this is body",
                user_id = _user.id
            };

            var postResponse = PostApiHelper.CreatePost(_post).Result;

            if (postResponse.IsSuccessful)
            {
                var postResponseModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseModel>(postResponse.Content);
                var postString = Newtonsoft.Json.JsonConvert.SerializeObject(postResponseModel.data);
                var post = Newtonsoft.Json.JsonConvert.DeserializeObject<Post>(postString);

                _post.id = post.id;
            }
        }

        private void InitializeComment()
        {
            _comment = new Comment
            {
                name = "this is name",
                body = "this is body",
                email = "qa_123@test.com",
                post_id = _post.id
            };
            var commentResponse = CommentApiHelper.CreateComment(_comment).Result;

            if (commentResponse.IsSuccessful)
            {
                var commentResponseModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseModel>(commentResponse.Content);
                var commentString = Newtonsoft.Json.JsonConvert.SerializeObject(commentResponseModel.data);
                var comment = Newtonsoft.Json.JsonConvert.DeserializeObject<Comment>(commentString);

                _comment.id = comment.id;

            }
        }

    }
}
