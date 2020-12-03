using AnimalShelter.Web.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelter.Services.Data
{
    public interface IGetCountService
    {
        IndexViewModel GetIndexCounts();

        int GetAdoptDogCount();

        int GetAdoptCatCount();

        int GetAdoptOtherCount();

        int GetAllAnimalCount();
    }
}
