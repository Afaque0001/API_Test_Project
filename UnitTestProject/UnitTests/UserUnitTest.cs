using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Threading.Tasks;
using UnitTestProject.Helpers;
using UnitTestProject.Models;

namespace UnitTestProject.UnitTests
{
    [TestClass]
    public class UserUnitTest
    {
        private User _user;

        [TestMethod]
        public async Task Create_A_New_User_Returns_Status_Code_201_Created()
        {
            var newUser = new User
            {
                name = "Harness",
                email = string.Format("qa_{0}@test.com", Guid.NewGuid()),
                gender = "female",
                status = "active"
            };

            var response = await UserApiHelper.CreateUser(newUser);
            var RespCont = response.Content;
            Console.WriteLine(RespCont);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }

        [TestMethod]
        public async Task Create_A_New_User_With_Existing_Email_Returns_Status_Code_422_Unprocessable_Entity()
        {
            InitializeUser();
            var existingUser = _user;
            var user = new User
            {
                email = existingUser.email,
                name = "Tom",
                gender = "male",
                status = "active"
            };

            var response = await UserApiHelper.CreateUser(user);
            Console.WriteLine(response.Content);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.UnprocessableEntity);
            }

        [TestMethod]
        public async Task Create_A_New_User_With_Invalid_Email_Format_Returns_Status_Code_422_Unprocessable_Entity()
        {
            var user = new User 
            { 
                email = "this is email",
                name = "John",
                gender = "male",
                status = "active"
            };
            var response = await UserApiHelper.CreateUser(user);
            Console.WriteLine(response.Content);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.UnprocessableEntity);
        }

        [TestMethod]
        public async Task Create_A_New_User_Without_Required_Fields_Name_And_Email_Returns_Status_Code_422_Unprocessable_Entity()
        {
            var user = new User
            {              
                gender = "male",
                status = "active"
            };
            var response = await UserApiHelper.CreateUser(user);
            Console.WriteLine(response.Content);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.UnprocessableEntity);
        }

        [TestMethod]
        public async Task Get_Existing_User_Returns_200_OK_And_Verify_Existing_Data()
        {
        
            InitializeUser();
            var existingUser = _user;

            var response = await UserApiHelper.GetUser(existingUser.id);          
            
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

            var responseModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseModel>(response.Content);
            var userString = Newtonsoft.Json.JsonConvert.SerializeObject(responseModel.data);
            var user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(userString);
            
            Assert.AreEqual(user.email, existingUser.email);
            Assert.AreEqual(user.id, existingUser.id);
            Assert.AreEqual(user.name, existingUser.name);
            Assert.AreEqual(user.status, existingUser.status);
            Assert.AreEqual(user.gender, existingUser.gender);
            Console.Write($"User ID: {user.id},\nUser Email: {user.email}, \nUser Name: {user.name}, \nUser Status: {user.status}, \nUser gender: {user.gender}");
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
     }
}
