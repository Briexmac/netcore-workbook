using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IssueTracker.DataAccess;
using IssueTracker.DataAccess.Contracts;
using IssueTracker.DataAccess.Translators;
using IssueTracker.Domain.Contracts;
using IssueTracker.Domain.Internal.Contracts;

namespace IssueTracker.Data.Repositories
{
    public class IssueRepository : IIssueRepository
    {
        private readonly IssueTranslator translator;
        private readonly Func<Task<int>> keyGenerator;
        private readonly static ConcurrentDictionary<int, DataAccess.Contracts.Issue> issues = new ConcurrentDictionary<int, DataAccess.Contracts.Issue>();

        public IssueRepository()
        {
            translator = new IssueTranslator();
            keyGenerator = () => Task.Run(() => (issues.Any() ? issues.Max(kv => kv.Key) : 0) + 1);
        }

        public Task<List<IIssue>> GetIssues()
        {
            return Task.Run(() => issues.Values.Select(i => translator.ToDomain(i)).ToList());
        }

        public async Task<IIssue> GetIssue(int id)
        {
            return translator.ToDomain(await GetIssueInternal(id).ConfigureAwait(false));
        }

        private async Task<DataAccess.Contracts.Issue> GetIssueInternal(int id)
        {
            DataAccess.Contracts.Issue value = null;
            await Task.Run(() => issues.TryGetValue(id, out value)).ConfigureAwait(false);
            return value;
        }

        public async Task<IIssue> SaveIssue(IIssue issue)
        {
            var current = issue.IsPersisted()
                ? await GetIssueInternal(issue.Id.Value).ConfigureAwait(false)
                : new DataAccess.Contracts.Issue() { Id = await keyGenerator().ConfigureAwait(false) };

            var translated = translator.ToModel(issue);
            current.Title = translated.Title;
            current.Description = translated.Description;
            current.Estimate = translated.Estimate;
            current.Type = translated.Type;

            // Capture the history of states
            //if ((current.Type & translated.Type) != translated.Type)
            //{
            //    current.Type |= translated.Type;
            //}

            issues.AddOrUpdate(current.Id.Value, current, (id, existing) => current);

            return issue;
        }

        public Task<int> NextKeyGuess()
        {
            return keyGenerator();
        }
    }
}
