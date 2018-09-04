using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BaseProject.Models
{
    public class IssueViewModel : Issue
    {
        public IssueViewModel() : this(null)
        {

        }

        public IssueViewModel(Issue issue)
        {
            AvailableStatuses = Enum.GetValues(typeof(IssueStatus))
                .Cast<IssueStatus>()
                .Select(v => new SelectListItem(v.ToString(), ((int)v).ToString()))
                .ToList();
            if (issue == null)
                return;
            Id = issue.Id;
            Title = issue.Title;
            Description = issue.Description;
            Status = issue.Status;
        }

        public List<SelectListItem> AvailableStatuses { get; private set; }
    }
}
