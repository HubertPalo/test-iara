using System;
using System.Collections.Generic;
using System.Text;

namespace TestIARA.Application.Common.Models
{
    public class ResultList<UnitClass>
    {
        public List<UnitClass> data;
        public int count;

        public ResultList()
        {
            data = new List<UnitClass>();
            count = 0;
        }
    }
}
