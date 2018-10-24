using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    interface IRepository<T, K>
    {
        IList<T> GetItems(int pageNo, int noPerPage, string orderParameterName);
        IList<T> GetItemsWhere(string value, string columnName);
        T GetItem(K idx);
        bool EditItem(K idx, T newValue);
        bool DeleteItem(K idx);
        K InsertItem(T item);
    }
}
