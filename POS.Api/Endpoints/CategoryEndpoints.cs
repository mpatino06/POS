using Microsoft.AspNetCore.Mvc;
using POS.Api.Filters;
using POS.Application.Dtos.Request;
using POS.Application.Interfaces;
using POS.Infrastructure.Commons.Bases.Request;

namespace POS.Api.Endpoints;

public static class CategoryEndpoints
{
    public static IEndpointRouteBuilder MapCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/categories")
            .WithTags("Category");

        // POST: /api/Category
        group.MapPost("/", async (
            [FromBody] BaseFiltersRequest filters,
            ICategoryApplication categoryApplication) =>
        {
            var response = await categoryApplication.ListCategories(filters);

            return Results.Ok(response);
        })
        .WithName("ListCategories");

        // GET: /api/Category/select
        group.MapGet("/select", async (
            ICategoryApplication categoryApplication) =>
        {
            var response = await categoryApplication.ListSelectCategories();

            return Results.Ok(response);
        })
        .WithName("ListSelectCategories");

        // GET: /api/Category/{categoryId}
        group.MapGet("/{categoryId:int}", async (
            int categoryId,
            ICategoryApplication categoryApplication) =>
        {
            var response = await categoryApplication.CategoryById(categoryId);

            return Results.Ok(response);
        })
        .WithName("GetCategory");

        // POST: /api/Category/register
        group.MapPost("/register", async (
            [FromBody] CategoryRequestDto request,
            ICategoryApplication categoryApplication) =>
        {
            var response = await categoryApplication.RegisterCategory(request);

            return Results.Ok(response);
        })
        .WithName("RegisterCategory")
        .AddRequestValidation<CategoryRequestDto>(); // Add request validation for CategoryRequestDto

        // PUT: /api/Category/edit/{categoryId}
        group.MapPut("/edit/{categoryId:int}", async (
            int categoryId,
            [FromBody] CategoryRequestDto request,
            ICategoryApplication categoryApplication) =>
        {
            var response = await categoryApplication.EditCategory(
                categoryId,
                request);

            return Results.Ok(response);
        })
        .WithName("EditCategory")
        .AddRequestValidation<CategoryRequestDto>(); // Add request validation for CategoryRequestDto

        // PUT: /api/Category/remove/{categoryId}
        group.MapPut("/remove/{categoryId:int}", async (
            int categoryId,
            ICategoryApplication categoryApplication) =>
        {
            var response = await categoryApplication.RemoveCategory(categoryId);

            return Results.Ok(response);
        })
        .WithName("RemoveCategory");

        return app;
    }
}
