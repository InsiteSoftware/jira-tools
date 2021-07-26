using System;

namespace JiraTools
{
    class Program
    {
        static void Main(string[] args)
        {
            var jiraService = new JiraService();
            
            // TODO let them pick boardId
            string boardId = "112";
            
            Console.WriteLine("Pick a sprint:");
            var x = 1;
            foreach (var sprint in jiraService.GetActiveSprints(boardId))
            {
                Console.WriteLine($"{PadToLength(x + ":", 2)} " + sprint.Name);
                x++;
            }

            var picked = int.Parse(Console.ReadLine());
            var sprintId = jiraService.GetActiveSprints(boardId)[picked].Id;

            Console.WriteLine($"{PadToLength("Issue", 12)}{PadToLength("Assignee", 25)}Status");
            foreach (var issue in jiraService.GetIssues(boardId, sprintId))
            {
                Console.WriteLine($"{PadToLength(issue.Key, 12)}{PadToLength(issue.Fields.Assignee?.DisplayName, 25)}{issue.Fields.Status?.Name}");                
            }
        }

        static string PadToLength(string value, int length)
        {
            value ??= "";
            while (value.Length < length)
            {
                value += " ";
            }

            return value;
        }
    }
}
