using Moq;
using Product.Core.Interfaces;
using Product.Controllers;
using Microsoft.AspNetCore.Mvc;
using Product.Core.Dtos.Book;


namespace Product.Test.controllers
{
    public class BookControllerTest
    {
        [Fact]
        public async Task Delete_WithValidId_ReturnsOkResult()
        {
            // Arrange
            var deletedBookId = Guid.NewGuid();
            var expectedResult = new ResponseDto { IsSucceed = true };

            var mockBookService = new Mock<IBookService>();
            mockBookService.Setup(service => service.DeleteAsync(deletedBookId))
                           .ReturnsAsync(expectedResult);

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var bookController = new BookController(mockBookService.Object, null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            // Act
            var result = await bookController.Delete(deletedBookId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResult, result.Value);
            Assert.Equal(200, result.StatusCode);
        }


        [Fact]
        public async Task GetTotalProduct_ReturnsOkResult()
        {
            double expectedResult = 10000;

            var mockBookService = new Mock<IBookService>();
            mockBookService.Setup(service => service.GetTotalProduct())
                           .ReturnsAsync(expectedResult);

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var bookController = new BookController(mockBookService.Object, null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            // Act
            var result = await bookController.GetTotalProduct() as OkObjectResult;


            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedResult, result.Value);
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task GetDetail_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var invalidBookId = Guid.Empty;

            var mockBookService = new Mock<IBookService>();
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var bookController = new BookController(mockBookService.Object, null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            // Act
            var result = await bookController.GetDetail(invalidBookId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            Assert.Equal("Product not found", (result as NotFoundObjectResult).Value);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        [Fact]
        public async Task Update_WithValidData_ReturnsOkResult()
        {
            // Arrange
            var bookId = Guid.NewGuid();
            var updateBookDto = new UpdateBookDto
            {
                Name = "Updated Book",
                Brand = "Updated Brand",
                Thumbnail = "Updated Thumbnail",
                UpdateAt = DateTime.UtcNow
            };

            var mockBookService = new Mock<IBookService>();
            var expectedUpdatedResult = new ResponseDto { IsSucceed = true };
            mockBookService.Setup(service => service.UpdateAsync(bookId, updateBookDto))
                           .ReturnsAsync(expectedUpdatedResult);

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var bookController = new BookController(mockBookService.Object, null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

            // Act
            var result = await bookController.Update(bookId, updateBookDto) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUpdatedResult, result.Value);
            Assert.Equal(200, result.StatusCode);
        }

    }
}