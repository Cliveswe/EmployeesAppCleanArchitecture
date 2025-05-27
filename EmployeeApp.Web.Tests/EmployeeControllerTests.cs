using EmployeesApp.Application.Employees.Interfaces;
using EmployeesApp.Domain.Entities;
using EmployeesApp.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace EmployeeApp.Web.Tests
{
    public class EmployeeControllerTests
    {
        [Fact]
        public void Index_Returns_View_Model()
        {
            // Arrange
            var employeeService = new Mock<IEmployeeService>();
            employeeService
                .Setup(e => e.GetAll())
            .Returns([
                new Employee { Id = 10, Name = "test", Email = "test@asdasd"},
                new Employee { Id = 20, Name = "test2", Email = "test2@asdasd" }
                ]);
            var controller = new EmployeesController(employeeService.Object);
            // Act
            var result = controller.Index();

            // Assert

            Assert.IsType<ViewResult>(result);


        }
    }
}
