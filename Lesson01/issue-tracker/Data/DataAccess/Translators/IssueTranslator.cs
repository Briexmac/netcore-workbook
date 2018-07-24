using IssueTracker.DataAccess.Contracts;
using IssueTracker.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.DataAccess.Translators
{
    internal class IssueTranslator
    {
        public IIssue ToDomain(Contracts.Issue issue)
        {
            if (issue == null)
                return null;

            var result = new Domain.Contracts.Issue
            {
                Id = issue.Id,
                Title = issue.Title,
                Description = issue.Description,
                Estimate = issue.Estimate,
                PastStates = issue.PastStates()
            };
            if ((int)issue.Type < 1) result.Status = "Not Started";
            else if ((int)issue.Type < 3) result.Status = "In Progress";
            else if ((int)issue.Type < 4) result.Status = "In Review";
            else result.Status = "Done";

            return result;
        }

        public Contracts.Issue ToModel(IIssue issue)
        {
            if (issue == null)
                return null;

            var result = new Contracts.Issue
            {
                Id = issue.Id,
                Title = issue.Title,
                Description = issue.Description,
                Estimate = issue.Estimate
            };

            if (string.IsNullOrEmpty(issue.Status)) result.Type = IssueType.NotStarted;
            else if (issue.Status.Equals("In Progress", StringComparison.CurrentCultureIgnoreCase)) result.Type = IssueType.InProgress;
            else if (issue.Status.Equals("In Review", StringComparison.CurrentCultureIgnoreCase)) result.Type = IssueType.InReview;
            else if (issue.Status.Equals("Done", StringComparison.CurrentCultureIgnoreCase)) result.Type = IssueType.Done;
            else if (issue.Status.Equals("Not Started", StringComparison.CurrentCultureIgnoreCase)) result.Type = IssueType.NotStarted;

            return result;
        }
    }
}
