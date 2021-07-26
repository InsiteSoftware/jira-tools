using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using MissionControl.Domain;
using Newtonsoft.Json;

namespace JiraTools
{
    public class JiraService
    {
        public string UserName => EnvironmentVariables.JIRA_USERNAME;
        public string Password => EnvironmentVariables.JIRA_PASSWORD;
        public string JiraUrl => EnvironmentVariables.JIRA_URL;

        public List<Sprint> GetActiveSprints(string boardId)
        {
            using var httpClient = new HttpClient();

            var authHeader = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{UserName}:{Password}")));
            httpClient.DefaultRequestHeaders.Authorization = authHeader;

            httpClient.BaseAddress = new Uri(JiraUrl);
            httpClient.Timeout = new TimeSpan(2, 0, 0);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var url = $"/rest/agile/1.0/board/{boardId}/sprint?state=active,future";
            return JsonConvert.DeserializeObject<SprintsResult>(httpClient.GetAsync(url).Result.Content.ReadAsStringAsync().Result).Values;
        }
        
        public List<Issue> GetIssues(string boardId, string sprintId)
        {
            using var httpClient = new HttpClient();

                var authHeader = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{UserName}:{Password}")));
                httpClient.DefaultRequestHeaders.Authorization = authHeader;

                httpClient.BaseAddress = new Uri(JiraUrl);
                httpClient.Timeout = new TimeSpan(2, 0, 0);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var url = $"/rest/agile/1.0/board/{boardId}/sprint/{sprintId}/issue";
            return JsonConvert.DeserializeObject<IssuesResult>(httpClient.GetAsync(url).Result.Content.ReadAsStringAsync().Result).Issues;
        }
    }

    public class SprintsResult
    {
        public int StartAt { get; set; }
        public int MaxResults { get; set; }
        public bool IsLast { get; set; }
        public List<Sprint> Values { get; set; }
    }

    public class Sprint
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
    }

    public class IssuesResult
    {
        public int StartAt { get; set; }
        public int MaxResults { get; set; }
        public int Total { get; set; }
        public List<Issue> Issues { get; set; }
    }

    public class Issue
    {
        public string Key { get; set; }
        public string Id { get; set; }
        public IssueFields Fields { get; set; }
    }

    public class IssueFields
    {
        public Status Status { get; set; }
        public Assignee Assignee { get; set; }
    }

    public class Assignee
    {
        public string DisplayName { get; set; }
    }

    public class Status
    {
        public string Name { get; set; }
    }
}