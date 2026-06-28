using System.Net;
using System.Text.Json;
using HRManagementSystem.Application.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HRManagementSystem.Api.Middlewares
{
	public class ExceptionHandlingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionHandlingMiddleware> _logger;
		public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}
		public async Task InvokeAsync(HttpContext context)
		{
			bool flage = false;
			context.Response.ContentType = "application/json";
			var response = new
			{
				title = "",
				status = 0,
				message = ""
			};
			try
			{
				await _next(context);
			}
			catch (NotFoundException ex)
			{
				flage = true;
				_logger.LogError(ex, ex.Message);
				context.Response.StatusCode = (int)HttpStatusCode.NotFound;

				response = new
				{
					title = "Not Found Error",
					status = 404,
					message = ex.Message
				};

			}
			catch (DbUpdateException ex)
			{
				flage = true;
				_logger.LogError(ex, ex.Message);
				if (ex.InnerException?.Message.Contains("unique index") == true)
				{
					context.Response.StatusCode = (int)HttpStatusCode.Conflict;
					response = new
					{
						title = "Conflict Error",
						status = 409,
						message = "This data is already in use (e.g. email or phone number)."
					};
				}
				else if (ex.InnerException?.Message.Contains("CHECK constraint") == true)
				{
					context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
					response = new
					{
						title = "Validation Error.",
						status = 400,
						message = "The submitted data is invalid, please check the values."
					};
				}
				else
				{
					context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
					response = new
					{
						title = "Internal Server Error.",
						status = 500,
						message = ex.Message,
					};
				}

			}
			catch (Exception ex)
			{
				flage = true;
				_logger.LogError(ex, ex.Message);
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
				response = new
				{
					title = "Internal Server Error",
					status = 500,
					message = "An unexpected error occurred."
				};
			}
			if (flage)
			{
				var json = JsonSerializer.Serialize(response);

				await context.Response.WriteAsync(json);
			}
		}
	}
}
