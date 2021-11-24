using BlazorClient.Services;
using Blazored.FluentValidation;
using DB_CSharp.Entities;
using DB_CSharp.Models.Commons;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorClient.Pages
{
    public partial class ProjectDetail
    {
        [Parameter]
        public string id { get; set; }
        [Inject] private IApiClient ApiClient { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        private FluentValidationValidator _fluentValidationValidator;
        private Project project = new();
        private bool loading = false;
        protected override async Task OnParametersSetAsync()
        {
            if (!string.IsNullOrEmpty(id))
            {
                loading = true;
                ApiResult<Project> response = await ApiClient.GetAsync<Project>($"/api/ProjectDetail/Init/{id}");
                if (response.IsSuccessed)
                {
                    project = response.ResultObj;
                }
                loading = false;
            }
        }
        private async Task Save()
        {
            loading = true;
            ApiResult<int> response = await ApiClient.PostAsync<int, Project>("/api/ProjectDetail/Save", project);
            if (response.IsSuccessed)
            {
                NavigateToProjectList();
            }
            loading = false;
        }
        private void NavigateToProjectList()
        {
            NavigationManager.NavigateTo("ProjectList");
        }
    }
}
