using System;
using System.Linq;
using Octokit;

namespace purge_repos
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new GitHubClient(new ProductHeaderValue("purge-repos"))
            {
                Credentials = new Credentials("username", "password")
            };

            var allForCurrent = client.Repository.GetAllForCurrent().Result;

            var repos = allForCurrent
                .Where(current => current.Name.Equals("repository-name"))
                .ToList();

            foreach (var repo in repos)
            {
                Console.WriteLine("Attempting to delete: {0}", repo.FullName);

                client.Repository.Delete("owner", repo.Name).Wait();
                Console.WriteLine("Deleted: {0}", repo.Name);
            }

            Console.WriteLine("Done!");
        }
    }
}
