using Newtonsoft.Json;
using RestSharp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UserExample.HelperClass;
using UserExample.Utilities;

namespace UserExample.TestScripts
{
    internal class UserTests:CoreCodes
    {
        [Test]
        [Order(1)]
        [Category("Get")]
        [TestCase("string")]
        public void GetSingleUser(string username)
        {
            test = extent.CreateTest("Get Single User Test");
            Log.Information("Get Single User Test Started");
            var request = new RestRequest("/user/" + username, Method.Get);
            request.AddHeader("Content-Type", "application/json");
            var response=client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                var details = JsonConvert.DeserializeObject<Response>(response.Content);
                Assert.NotNull(details);
                Log.Information("User details returned");
                Assert.That(details.Username,Is.EqualTo("string"));
                test.Pass("Get Single User Test Passed");
                Log.Information("Get Single User Test Passed");
            }
            catch(AssertionException)
            {
                test.Fail("Get Single User Test failed");
            }
        }
        [Test]
        [Order(2)]
        [Category("Post")]
        public void CreateUser()
        {
            test = extent.CreateTest("Create User Test");
            Log.Information("Create User Test");
            var request = new RestRequest("/user", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new
            {
                username= "anakha",
                firstName= "anakha",
                lastName= "anakha",
                email="anakha",
                password= "string",
                phone= "string",
                userStatus= 1
            });
            var response=client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"Api Response:{response.Content}");
                var data = JsonConvert.DeserializeObject<ResponseDetails>(response.Content);
                Assert.NotNull(data);
                Assert.That(data.Code, Is.EqualTo(200));
                test.Pass("Create User Test Passed");
                Log.Information("Create User Test Passed");

            }
            catch (AssertionException)
            {
                test.Fail("Create User Test Failed");
            }
        }
        [Test]
        [Order(3)]
        [Category("Put")]
        public void UpdateUser()
        {

            test = extent.CreateTest("Update User Test");
            Log.Information("Update User Test");
            var request = new RestRequest("/user/string", Method.Put);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new
            {
                username = "anakhaaaa",
                firstName = "anakhaaa",
                lastName = "anakha",
                email = "anakha",
                password = "string",
                phone = "string",
                userStatus = 1
            });
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"Api Response:{response.Content}");
                var data = JsonConvert.DeserializeObject<ResponseDetails>(response.Content);
                Assert.NotNull(data);
                Assert.That(data.Code, Is.EqualTo(200));
                test.Pass("Update User Test Passed");
                Log.Information("Update User Test Passed");

            }
            catch (AssertionException)
            {
                test.Fail("Update User Test Failed");
            }
        }
        [Test]
        [Order(4)]
        [Category("Delete")]
        public void DeleteUser()
        {
            test = extent.CreateTest("Delete User Test");
            Log.Information("Delete User Test");
            
            var request = new RestRequest("/user/string", Method.Delete);
            request.AddHeader("Content-Type", "application/json");
            

            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"Api Error:{response.Content}");
                var data = JsonConvert.DeserializeObject<ResponseDetails>(response.Content);
                Assert.NotNull (data);
                Log.Information("Delet Test Passed");
                test.Pass("Delete User Test Passed");

            }
            catch (AssertionException)
            {
                test.Fail("Delete user Test");
            }
        }
    }
}
