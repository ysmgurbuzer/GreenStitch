using APIGreenStitch.Controllers;
using BusinessLayer.Abstract;
using Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using System.Security.Claims;
using System.Text;

namespace GreenStitchTest
{
    public class UnitTestFavorites : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        public UnitTestFavorites(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }
        [Fact]

        public async Task TestAddFavoritesEndpoint()
        {
            _client.DefaultRequestHeaders.Add("Authorization", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJlbWFpbCI6InlzbTEyQGdtYWlsLmNvbSIsInJvbGUiOiJNZW1iZXIiLCJuYW1laWQiOiIxIiwibmJmIjoxNzA1MjU0NDc5LCJleHAiOjE3MDUyNTgwNzksImlhdCI6MTcwNTI1NDQ3OSwiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NTAwMSIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjUwMDEifQ.vNJpglwER5IIOKkZT4ZwjSB7me9fh5dl4HyJEWrpFIM");

            var content = new StringContent("{\"favoriteId\": 1, \"advertId\": 1, \"memberId\": 1}", Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/favorites", content);

            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());

            var responseString = await response.Content.ReadAsStringAsync();
        
        }
    }
}