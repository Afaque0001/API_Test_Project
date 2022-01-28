using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Threading.Tasks;
using UnitTestProject.Helpers;
using UnitTestProject.Models;

namespace UnitTestProject.UnitTests
{
    [TestClass]
    public class TodoUnitTest
    {
        private User _user;
        private Todo _todo;
         
        [TestMethod]
        public async Task Create_A_New_Todo_Returns_Status_Code_201_Created()
        {
            InitializeUser();
            var existingUser = _user;
            var todo = new Todo
            {
                title = "this is title",
                status = "completed",
                user_id = existingUser.id
            };

            var response = await TodoApiHelper.CreateTodo(todo);
            Console.WriteLine(response.Content);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }

        [TestMethod]
        public async Task Create_A_New_Todo_Without_Required_Fields_Title_And_Status_Returns_Status_Code_422_Unprocessable_Entity()
        {
            InitializeUser();
            var existingUser = _user;
            Todo todo = new Todo
            {
                user_id = existingUser.id
            };

            var response = await TodoApiHelper.CreateTodo(todo);
            Console.WriteLine(response.Content);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.UnprocessableEntity);
        }

        [TestMethod]
        public async Task Get_Existing_Todo_Returns_200_OK_And_Verify_Existing_Data()
        {
            InitializeUser();
            InitializeTodo();
            var existingTodo = _todo;
            var response = await TodoApiHelper.GetTodo(existingTodo.id);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

            var responseModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseModel>(response.Content);
            var todoString = Newtonsoft.Json.JsonConvert.SerializeObject(responseModel.data);
            var todo = Newtonsoft.Json.JsonConvert.DeserializeObject<Todo>(todoString);

            Assert.AreEqual(todo.id, existingTodo.id);
            Assert.AreEqual(todo.status, existingTodo.status);
            Assert.AreEqual(todo.title, existingTodo.title);
            Assert.AreEqual(todo.user_id, existingTodo.user_id);
            Assert.AreEqual(todo.due_on, existingTodo.due_on);
            Console.Write($"Todo ID: {todo.id},\nTodo title: {todo.title}, \nTodo UserID: {todo.user_id}, \nTodo Status: {todo.status}");
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

        private void InitializeTodo()
        {
            _todo = new Todo
            {
                title = "this is title",
                status = "completed",
                user_id = _user.id
            };

            var todoResponse = TodoApiHelper.CreateTodo(_todo).Result;

            if (todoResponse.IsSuccessful)
            {
                var todoResponseModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseModel>(todoResponse.Content);
                var todoString = Newtonsoft.Json.JsonConvert.SerializeObject(todoResponseModel.data);
                var todo = Newtonsoft.Json.JsonConvert.DeserializeObject<Todo>(todoString);

                _todo.id = todo.id;
            }
        }
    }
}
