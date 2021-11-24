using BlazorClient.Services;
using DB_CSharp.Entities;
using DB_CSharp.Models;
using DB_CSharp.Models.Commons;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorClient.Pages
{
    public partial class ProjectList
    {
        [Inject] private IApiClient ApiClient { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private IJSRuntime JSRuntime { get; set; }
        private List<Project> projects;
        private ProjectListSearchRequest projectListSearchRequest = new() { Active = 2 };
        private bool loading = false;
        private async Task Search()
        {
            loading = true;
            ApiResult<List<Project>> response = 
                await ApiClient.PostAsync<List<Project>, ProjectListSearchRequest>("/api/ProjectList/Search", projectListSearchRequest);
            if (response.IsSuccessed)
            {
                projects = response.ResultObj;
            }
            loading = false;
        }
        private void NavigateToProjectDetail()
        {
            NavigationManager.NavigateTo("ProjectDetail");
        }
        private async Task Delete(short id)
        {
            if (!await JSRuntime.InvokeAsync<bool>("confirm", $"Are you sure you want to delete?"))
            {
                return;
            }

            loading = true;
            ApiResult<int> response = await ApiClient.GetAsync<int>($"/api/ProjectList/Delete/{id}");
            if (response.IsSuccessed)
            {
                await Search();
            }
            loading = false;
        }
    }
}
