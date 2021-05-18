using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Services
{
    using Helpers;
    public interface IFilterService<out T> where T : Observable
    {

        IEnumerable<T> SearchParent(string param);

        ObservableCollection<string> Suggestions
        {
            get; set;
        }
    }
    
}
