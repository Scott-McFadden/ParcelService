using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParcelService;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.File;
using System.Threading.Tasks;

namespace ParcelServiceTests
{
    [TestClass]
    public class UnitTest1
    {
        private Logger log;
        private string SecurityToken = "test"; 
        public UnitTest1()
        {
            log = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("/logs/ParcelService.Tests.txt", rollingInterval: RollingInterval.Day) 
                .CreateLogger();

            log.Information("Starting Unit Test 1");
        }
        [TestMethod("test0")]
        public void TestLogging()
        {
            log.Information(  "logging works");

        }

        [TestMethod("Test GetParcelByCoordinatesAsync Fake LAT/LON")]
        public async Task  TestGetParcelByCoordinatesAsync1()
        {
            var TestLon = 1.1f;
            var TestLat = 1.1f;
            ParcelService.ParcelService ps = new ParcelService.ParcelService(log, SecurityToken);

            string ret = await ps.GetParcelByCoordinatesAsync(TestLon, TestLat);

            log.Information(ret);
            Assert.IsTrue(ret.Contains("success"));
            
        }
        [TestMethod("Test GetParcelByCoordinatesAsync Good LAT/LON")]
        public async Task TestGetParcelByCoordinatesAsync2()
        {
            var TestLon = 1.1f;
            var TestLat = 1.1f;
            ParcelService.ParcelService ps = new ParcelService.ParcelService(log, SecurityToken);

            string ret = await ps.GetParcelByCoordinatesAsync(TestLon, TestLat);

            log.Information(ret);
            Assert.IsTrue(ret.Contains("success"));

        }
        [TestMethod("Test GetParcelByCoordinatesAsync bad token")]
        public async Task TestGetParcelByCoordinatesAsync3()
        {
            var TestLon = 1.1f;
            var TestLat = 1.1f;
            ParcelService.ParcelService ps = new ParcelService.ParcelService(log, SecurityToken);

            string ret = await ps.GetParcelByCoordinatesAsync(TestLon, TestLat);

            log.Information(ret);
            Assert.IsTrue(ret.Contains("success"));

        }

        [TestMethod("Test GetParcelByNumberAsync pad parcel id")]
        public async Task GetParcelByNumberAsync()
        {
            var TestParcelID = "testparcel"; 
            ParcelService.ParcelService ps = new ParcelService.ParcelService(log, SecurityToken);

            string ret = await ps.GetParcelByNumberAsync(TestParcelID);

            log.Information(ret);
            Assert.IsTrue(ret.Contains("success"));

        }

        [TestMethod("Test GetParcelByNumberAsync good parcel id")]
        public async Task GetParcelByNumberAsync2()
        {
            var TestParcelID = "testparcel";
            ParcelService.ParcelService ps = new ParcelService.ParcelService(log, SecurityToken);

            string ret = await ps.GetParcelByNumberAsync(TestParcelID);

            log.Information(ret);
            Assert.IsTrue(ret.Contains("success"));

        }

        //GetParcelByAddressAsync

        [TestMethod("Test GetParcelByAddressAsync good address, no context")]
        public async Task GetParcelByAddressAsync()
        {
            var TestAdddress = "testparcel";
            ParcelService.ParcelService ps = new ParcelService.ParcelService(log, SecurityToken);

            string ret = await ps.GetParcelByAddressAsync(TestAdddress, null);

            log.Information(ret);
            Assert.IsTrue(ret.Contains("success"));

        }

        [TestMethod("Test GetParcelByAddressAsync good address, Context")]
        public async Task GetParcelByAddressAsync2()
        {
            var TestAdddress = "testparcel";
            var Context = "us/tn/antioch";
            ParcelService.ParcelService ps = new ParcelService.ParcelService(log, SecurityToken);

            string ret = await ps.GetParcelByAddressAsync(TestAdddress, Context);

            log.Information(ret);
            Assert.IsTrue(ret.Contains("success"));

        }


        [TestMethod("Test GetParcelByAddressAsync bad address, no context")]
        public async Task GetParcelByAddressAsync3()
        {
            var TestAdddress = "testparcel";
            ParcelService.ParcelService ps = new ParcelService.ParcelService(log, SecurityToken);

            string ret = await ps.GetParcelByAddressAsync(TestAdddress, null);

            log.Information(ret);
            Assert.IsTrue(ret.Contains("success"));

        }

        // GetParcelByPath

        [TestMethod("Test GetParcelByPath good parcel path")]
        public async Task GetParcelByPath()
        {
            var TestAdddress = "1234";
            ParcelService.ParcelService ps = new ParcelService.ParcelService(log, SecurityToken);

            string ret = await ps.GetParcelByAddressAsync(TestAdddress, null);

            log.Information(ret);
            Assert.IsTrue(ret.Contains("success"));

        }

        [TestMethod("Test GetParcelByPath bad parcel path")]
        public async Task GetParcelByPath2()
        {
            var TestAdddress = "testparcel";
            ParcelService.ParcelService ps = new ParcelService.ParcelService(log, SecurityToken);

            string ret = await ps.GetParcelByAddressAsync(TestAdddress, null);

            log.Information(ret);
            Assert.IsTrue(ret.Contains("success"));

        }
    }
}
