using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Domain.Contracts
{
    public interface IIssue
    {
        int? Id { get; set; }

        string Title { get; set; }

        decimal Estimate { get; set; }

        string Description { get; set; }

        string Status { get; set; }

        List<string> PastStates { get; set; }
    }
}
