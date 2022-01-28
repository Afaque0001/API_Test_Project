using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Threading.Tasks;
using UnitTestProject.Helpers;
using UnitTestProject.Models;

namespace UnitTestProject.UnitTests
{
    [TestClass]
    public class PostUnitTest
    {
        private User _user;
        private Post _post;

        [TestMethod]
        public async Task Create_A_New_Post_Returns_Status_Code_201_Created()
        {
            InitializeUser();
            var existingUser = _user;
            var newPost = new Post
            {
                title = "this is title",
                body = "this is body",
                user_id = existingUser.id
            };

            var response = await PostApiHelper.CreatePost(newPost);
            Console.WriteLine(response.Content);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }

        [TestMethod]
        public async Task Create_A_New_Post_Without_Required_Fields_Title_And_Body_Returns_Status_Code_422_Unprocessable_Entity()
        {
            InitializeUser();

            var existingUser = _user;
            var post = new Post
            {
               user_id = existingUser.id
            };

            var response = await PostApiHelper.CreatePost(post);
            Console.WriteLine(response.Content);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.UnprocessableEntity);
        }

        [TestMethod]
        public async Task Get_Existing_Post_Returns_200_OK_And_Verify_Existing_Data()
        {
            InitializeUser();
            InitializePost();

            var existingPost = _post;
            var response = await PostApiHelper.GetPost(existingPost.id);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

            var responseModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseModel>(response.Content);
            var postString = Newtonsoft.Json.JsonConvert.SerializeObject(responseModel.data);
            var post = Newtonsoft.Json.JsonConvert.DeserializeObject<Post>(postString);

            Assert.AreEqual(post.body, existingPost.body);
            Assert.AreEqual(post.id, existingPost.id);
            Assert.AreEqual(post.title, existingPost.title);
            Assert.AreEqual(post.user_id, existingPost.user_id);
            Console.Write($"Post ID: {post.id},\nPost Body: {post.body}, \nTitle: {post.title}, \nUser ID: {post.user_id}");

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

    }
}
