using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using WeTravel.Domain;

namespace WeTravel.DataAccessInterface
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Get();
    }
}

