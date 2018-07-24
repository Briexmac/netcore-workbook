using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.DataAccess.Contracts
{
    [Flags]
    internal enum IssueType
    {
        NotStarted = 0x00,
        InProgress = 0x01,
        InReview = 0x02,
        Done = 0x04,
        NewState = 0b111111,
    }
}
