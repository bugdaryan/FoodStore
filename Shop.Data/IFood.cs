using Shop.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Data
{
    public interface IFood
    {
        IEnumerable<Food> GetAll();
        IEnumerable<Food> GetPreferred();
        Food GetById(int id);
    }
}
