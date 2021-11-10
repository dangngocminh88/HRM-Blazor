using BlazorClient.Services;
using DB_CSharp.Entities;
using DB_CSharp.Models.Commons;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorClient.Pages
{
    public partial class FetchData
    {
        [Inject] private IClient Client { get; set; }
        private WeatherForecast[] forecasts;

        protected override async Task OnInitializedAsync()
        {
            ApiResult<List<Project>> result = await Client.GetListAsync<Project>("/api/ProjectList/Search");
            if (result.IsSuccessed)
            {
                List<Project> projectList = result.ResultObj;
                string a = projectList[0].ProjectName;
                short i = projectList[0].Id;
            }
        }

        public class WeatherForecast
        {
            public DateTime Date { get; set; }

            public int TemperatureC { get; set; }

            public string Summary { get; set; }

            public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        }
    }
}
