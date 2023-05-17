using System;
using BookStore.Application.Interfaces;
using BookStore.Application.Services;
using BookStore.Interface.Repositories;
using BookStore.Repository.Repositories;

namespace BookStore.API
{
	public class DependencyInjection
	{
		public static void Register(IServiceCollection serviceProvider)
		{
			RepositoryDependence(serviceProvider);

        }

		private static void RepositoryDependence(IServiceCollection serviceProvider)
		{
            serviceProvider.AddScoped<IBookStoreService, BookStoreService>();
            serviceProvider.AddScoped<IBookStoreRepository, BookStoreRepository>();
        }
    }
}

