using AventStack.ExtentReports.MarkupUtils;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using Serilog;
using Store.HelperClass;
using Store.Utllities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.TestScripts
{
    internal class StoreTests:CoreCodes
    {
        [Test]
        [Order(1)]
        [Category("Get")]
        public void GetOrder()
        {
            test = extent.CreateTest("Get Order");
            Log.Information("Get single user");
            var request = new RestRequest("/store/order/19", Method.Get);
            request.AddHeader("Content-Type", "application/json");
            var response=client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                Log.Information($"Api Response:{response.Content}");
                var data = JsonConvert.DeserializeObject<Details>(response.Content);
                Assert.NotNull(data);
                Log.Information("Order details returned");
                Assert.That(data.PetId, Is.EqualTo(7323));
                test.Pass("Get order details test passed");

            }
            catch (AssertionException)
            {
                test.Fail("Get order details test failed");
            }
        }
        [Test]
        [Order(2)]
        [Category("post")]
        public void PostOrder()
        {
            test = extent.CreateTest("Create Order");
            Log.Information("Create Order Test");
            var request = new RestRequest("/store/order",Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new
            {
                petId = 54,
                quantity = 0,
                shipDate = "2023-12-18T16:38:41.323Z",
                status = "placed",
                complete=true
            });
            var response=client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                var data=JsonConvert.DeserializeObject<Details>(response.Content);
                Assert.NotNull(data);
                Log.Information("Order created");
                Assert.That(data.PetId, Is.EqualTo(54));
                test.Pass("Create test Passed");

            }
            catch(AssertionException) 
            {
                test.Fail("Crreate Test failed");
            }
        }
        [Test]
        [Order(3)]
        [Category("Delete")]
        public void DeleteOrder()
        {
            test = extent.CreateTest("Create Order");
            Log.Information("Create Order Test");
            var request = new RestRequest("/store/order/19", Method.Delete);
            request.AddHeader("Content-Type", "application/json");
            var response = client.Execute(request);
            try
            {
                Assert.That(response.StatusCode, Is.EqualTo(System.Net.HttpStatusCode.OK));
                test.Pass("Delete test passed");
            }
            catch(AssertionException)
            {
                test.Fail("Delete test failed");
            }
        }
        
    }
}
