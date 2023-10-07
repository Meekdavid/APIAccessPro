using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;
using APIAccessPro;
using APIAccessPro.Controllers;
using APIAccessProDependencies.Interfaces;
using static XUnit_Test.RetreivePayload;
using APIAccessProDependencies.Helpers.DTOs;
using APIAccessProDependencies.Helpers.DTOs.Models;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using APIAccessProDependencies.Helpers.DTOs.Models.Preview;
using APIAccessProDependencies.Helpers.Common;

namespace XUnit_Test.TestCases
{
    public class Preview
    {
        private readonly HttpClient _client;
        private readonly IPreview _mockCosmosDbService;

        public Preview()
        {
            _mockCosmosDbService = A.Fake<IPreview>();
        }

        [Fact]
        public async void GetPreview_ReturnsOkStatusCode()
        {
            // Arrange:
            //string sqlCosmosQuery = A.Fake<string>();
            var methodReturnResponse = A.Fake<MethodReturnResponse<List<PreviewDTO>>>();
            A.CallTo(() => _mockCosmosDbService.ApplicationPreviewAsnyc(Utils.SelectQuery)).Returns(methodReturnResponse);
            var controller = new PreviewController(_mockCosmosDbService);

            // Act:
            var actionResult = await controller.ApplicationPreview();

            // Assert:
            actionResult.Should().NotBeNull();
            actionResult.Should().BeOfType(typeof(ActionResult<PreviewResponse>));

            var okResult = actionResult as ActionResult<PreviewResponse>;
            okResult.Result.Should().BeOfType<ObjectResult>();
            var okObjectResult = okResult.Result as ObjectResult;
            okObjectResult.StatusCode.Should().Be((int)HttpStatusCode.OK);

        }
    }
}
