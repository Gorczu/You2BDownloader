using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    interface IRepository<T>
    {
        IList<T> GetItems(int pageNo, int noPerPage, string orderParameterName);
        T GetItem(int idx);
        bool EditItem(int idx, T newValue);
        bool DeleteItem(int idx);
        bool InsertItem(T item);
    }
}
