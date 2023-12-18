
using Example_GoRest.Utilities;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example_GoRest.TestScript
{
    public class GoRestTest:CoreCodes
    {
        [Test]
        [Order(1)]
        public void GetUserDetails()
        {
            test = extent.CreateTest("Get All User");
            Log.Information("Get all user test");
            //string bearerToken = "8b126f288e94e7a4cc8e75bad0dd0a3eb63a7f2a100c4677b7799d3b1922198c";
            var request = new RestRequest("", Method.Get);
            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"API Response: {response.Content}");

                List<UserData> users = JsonConvert.DeserializeObject<List<UserData>>(response.Content);

                Assert.NotNull(users);
                Log.Information("Get All User test passed");

                test.Pass("Get All User test passed");
            }
            catch (AssertionException)
            {
                test.Fail("Get All User test failed");
            }

        }
        [Test]
        [Order(2)]
        public void CreateUserTest()
        {
            test = extent.CreateTest("Create User");
            Log.Information("Create User Test Started");

            string bearerToken = "8b126f288e94e7a4cc8e75bad0dd0a3eb63a7f2a100c4677b7799d3b1922198c";
            var request = new RestRequest("", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {bearerToken}");
            request.AddJsonBody(new
            {
                name = "abcdef",
                email = "abcdef@gmail.com",
                gender = "female",
                status = "active"
            });
            var response = client.Execute(request);

            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.Created));
                Log.Information($"API Response: {response.Content}");

                var user = JsonConvert.DeserializeObject<UserData>(response.Content);
                Assert.NotNull(user);
                Log.Information("User created and returned");
                Assert.That(user.Name, Is.EqualTo("abcdef"));
                Log.Information($"User Name matches with fetch {user.Name}");
                Assert.That(user.Email, Is.EqualTo("abcdef@gmail.com"));
                Log.Information($"User Email matches with fetch {user.Email}");
                Assert.That(user.Gender, Is.EqualTo("female"));
                Log.Information($"User Gender matches with fetch {user.Gender}");
                Assert.That(user.Status, Is.EqualTo("active"));
                Log.Information($"User Status matches with fetch {user.Status}");

                Log.Information("Create User test passed");

                test.Pass("Create User Test passed");
            }
            catch (AssertionException)
            {
                test.Fail("Create User test failed");
            }
        }
        [Test]
        [Order(3)]
        public void UpdateUser()
        {
            test = extent.CreateTest("Update User Test");
            Log.Information("Update User Test");
            string bearerToken = "8b126f288e94e7a4cc8e75bad0dd0a3eb63a7f2a100c4677b7799d3b1922198c";
            var request = new RestRequest("/5837961", Method.Put);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {bearerToken}"); 
            request.AddJsonBody(new
            {
                name = "Allu",
                email = "allu@gmail.com",
                gender = "male",
                status = "active"
            });
            var response=client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"Api Response:{response.Content}");
                var details = JsonConvert.DeserializeObject<UserData>(response.Content);
                Assert.NotNull(details);
                Log.Information("User Details Updated");
                Assert.That(details.Name, Is.EqualTo("Allu"));
                Log.Information("Update User Test Passed");
                test.Pass("Update User Test Passed");


            }
            catch(AssertionException)
            {
                test.Fail("Update User Test Failed");
            }

        }
        [Test]
        [Order(4)]
        public void DeleteUser()
        {
            test = extent.CreateTest("Delete User Test");
            Log.Information("Delete User Test");
            string bearerToken = "8b126f288e94e7a4cc8e75bad0dd0a3eb63a7f2a100c4677b7799d3b1922198c";
            var request = new RestRequest("/5837961", Method.Delete);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {bearerToken}");

            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.NoContent));
                Log.Information($"Api Error:{response.Content}");
                var data=JsonConvert.DeserializeObject<UserData>(response.Content);
                Assert.IsNull(data);
                Log.Information("Delet Test Passed");
                test.Pass("Delete User Test Passed");

            }
            catch(AssertionException) 
            {
                test.Fail("Delete user Test");
            }
        }
       
    }
}
