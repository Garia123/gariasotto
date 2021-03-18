using System;
using System.Collections.Generic;
using WeTravel.Model;

namespace WeTravel.ServiceInterface
{
    public interface ICategoryService
    {
        IEnumerable<CategoryModelOut> Get();
    }
}
