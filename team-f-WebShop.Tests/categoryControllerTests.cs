using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using team_f_WebShop.API.Controllers;
using Xunit;

namespace team_f_WebShop.Tests
{
    public class categoryControllerTests
    {
        [Fact]
        public void GetAll_ShouldReturnStatusCode200_WhenDataExists()
        {
            // Arrange
            categoryController controller = new categoryController();

            // Act
            var result = controller.GetAll();

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

    }
}
