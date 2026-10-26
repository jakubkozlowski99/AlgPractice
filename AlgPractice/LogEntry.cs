using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgPractice
{
    public record LogEntry(
    DateTime Timestamp,
    string UserId,
    string Action
);
}
