using AnimalShelter.Web.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimalShelter.Services.Data
{
    public interface IGetCountsService
    {
        IndexViewModel GetIndexCounts();
    }
}
