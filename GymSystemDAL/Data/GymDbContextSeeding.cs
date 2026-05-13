using GymSystemDAL.Data.Contexts;
using GymSystemDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace GymSystemDAL.Data
{
	public static class GymDbContextSeeding
	{
		public static bool DataSeed(GymDbContext dbContext)
		{
			var hasPlans=dbContext.Set<Plan>().Any();
			var hasCategories = dbContext.Set<Category>().Any();
			//because if there are plans , no need for seeding
			try
			{
				if (!hasPlans)
				{
					var plans = LoadDataFromFiles<Plan>("plans.json");
					if (plans.Any())
						dbContext.AddRange(plans);
				}
				if (!hasCategories)
				{
					var categories = LoadDataFromFiles<Category>("categories.json");
					if (categories.Any())
						dbContext.AddRange(categories);
				}

				return dbContext.SaveChanges() > 0;
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Seeding Failed : {ex} ");
				return false;
			}

		}

		private static List<T> LoadDataFromFiles<T>(string fileName)
		{
			//To gett the path of the executing directory (GymSystemPL)
			var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", fileName);
			if (!File.Exists(path))
				throw new FileNotFoundException();
			var data = File.ReadAllText(path); //String

			//Just to ignore case sensitive of property
			var options = new JsonSerializerOptions()
			{
				PropertyNameCaseInsensitive = true,
			};

			return JsonSerializer.Deserialize<List<T>>(data, options) ?? new List<T>();


			
		}
	}

}
