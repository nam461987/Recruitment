using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cotoiday_admin.Dtos
{
    public class CreatingUserChartDto
    {
        public CreatingUserChartDto()
        {
            Labels = new List<string>();
            Seriers = new List<List<int>>();
        }
        public List<string> Labels { get; set; }

        public List<List<int>> Seriers { get; set; }
    }
}