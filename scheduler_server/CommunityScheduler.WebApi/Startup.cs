﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CommunityScheduler.WebApi
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup ( IConfiguration configuration )
		{
			Configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices ( IServiceCollection services )
		{
			services.AddMvc ( ).SetCompatibilityVersion ( CompatibilityVersion.Version_2_2 );
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure ( IApplicationBuilder app, IHostingEnvironment env )
		{
			if ( env.IsDevelopment ( ) )
				app.UseDeveloperExceptionPage ( );
			else
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts ( );


			app.UseDefaultFiles ( );
			app.UseStaticFiles ( );


			app.UseHttpsRedirection ( );
			app.UseMvc ( );
		}
	}
}