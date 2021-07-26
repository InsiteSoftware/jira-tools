using System;

namespace MissionControl.Domain
{
    public static class EnvironmentVariables
    {
        public static string JIRA_URL = GetEnvironmentVariable(nameof(JIRA_URL));
        
        public static string JIRA_USERNAME = GetEnvironmentVariable(nameof(JIRA_USERNAME));
        
        public static string JIRA_PASSWORD = GetEnvironmentVariable(nameof(JIRA_PASSWORD));

        private static string GetEnvironmentVariable(string key) =>
            Environment.GetEnvironmentVariable(key);
    }
}
