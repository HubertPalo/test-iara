using TestIARA.Application.Common.Interfaces;
using System;

namespace TestIARA.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
