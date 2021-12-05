using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using MQTT.Api.Contracts.v1.Request.DeviceController;
using MQTT.Api.Options;
using MQTT.Data;
using MQTT.Shared.DBO;
using Xunit;

namespace MQTT.IntegrationTests
{
    public class DeviceControllerTests : IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutAnyDevices_ReturnsEmptyResponse()
        {
            await AuthenticateAsync();

            var response = await GetAllDevicesAsync();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await response.Content.ReadAsAsync<List<DeviceViewModel>>();
            content.Should().BeOfType<List<DeviceViewModel>>();
        }

        [Fact]
        public async Task GetById_WithExistsInDatabase()
        {
            await AuthenticateAsync();
            var responseAllDevices = await GetAllDevicesAsync();
            var allDevices = await responseAllDevices.Content.ReadAsAsync <List<DeviceViewModel>>();
            (allDevices.Count > 0).Should().BeTrue();
            var device = allDevices.First();
            var responseGetDeviceById = await GetDeviceByIdAsync(device.Id);
            responseGetDeviceById.StatusCode.Should().Be(HttpStatusCode.OK);
            var contentGetDeviceById = await responseGetDeviceById.Content.ReadAsAsync<DeviceViewModel>();
            contentGetDeviceById.Id.Should().Be(device.Id);
        }

        [Fact]
        public async Task CreateAndDeleteDevice()
        {
            await AuthenticateAsync();
            var responseCreating = await CreateDeviceAsync(new CreateDeviceRequest()
            {
                Name = "UnitTesting",
                Description = "TestDescr",
                Geo = "TestGeo",
                IsPublic = true,
                PrivateIp = "172.26.105.1",
                PublicIp = "178.54.86.113"
            });
            responseCreating.StatusCode.Should().Be(HttpStatusCode.OK);
            var contentAfterCreating = await responseCreating.Content.ReadAsAsync<DeviceViewModel>();

            var responseDeleting = await DeleteDeviceAsync(contentAfterCreating.Id);
            responseCreating.StatusCode.Should().Be(HttpStatusCode.OK);
            var contentAfterDeleting = await responseDeleting.Content.ReadAsStringAsync();
            contentAfterDeleting.Should().Be("Success delete");

            var responseDeletingElementById = await GetDeviceByIdAsync(contentAfterCreating.Id);
            responseDeletingElementById.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}