using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using RestApi.Client;
using RestApi.Client.Authentication;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCore_31_Github
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();

			services.AddRestClient(builder =>
			{
				builder.SetBaseAddress(new Uri("https://api.github.com"))
					.SetDefaultRequestHeaders(
						new RestHttpHeaders
						{
							{HeaderNames.Accept, "application/vnd.github.v3+json"},
							{HeaderNames.UserAgent, "Sample App"},
							//{HeaderNames.Authorization, "token <YOUR TOKEN>"}	// https://help.github.com/en/github/authenticating-to-github/creating-a-personal-access-token-for-the-command-line
						}
					)

					// You can either user the above Authorization header or below AddBearerAuthentication<> depending on your requirement.
					.AddBearerAuthentication<BearerAuthenticationProvider>();
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Repository/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Repository}/{action=Index}/{id?}");
			});
		}
	}

	public class BearerAuthenticationProvider : IBearerAuthenticationProvider
	{
		public Task<BearerAuthentication> ProvideAsync(CancellationToken cancellationToken)
		{
			var scheme = "token"; // default is Bearer but since GitHub takes token as scheme
			var token = //"<YOUR GITHUB TOKEN>";	// https://help.github.com/en/github/authenticating-to-github/creating-a-personal-access-token-for-the-command-line
			var auth = new BearerAuthentication(scheme, token);
			return Task.FromResult(auth);
		}
	}
}
