using CarRental.Common.Interfaces;
using System.Net.Http.Json;

namespace CarRental.Data.Classes
{
    public class LoadData
    {
        HttpClient _httpClient;

		

		public LoadData(HttpClient Http)
        {
            _httpClient = Http;
        }
        public async Task<List<IBase>> ReadFile<T>(string fileName) where T : class,  IBase
		{
            var result = await _httpClient.GetFromJsonAsync<List<T>>(fileName);
			return result is null ? new List<IBase>() : result.Cast<IBase>().ToList();
		}
    }
    }

