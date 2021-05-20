using Microsoft.Toolkit.Uwp.UI.Controls;
using StudentSystem.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Services
{
    public interface ISorterService<out T> where T : Observable
    {
        void Sort(IEnumerable<Observable> collectionGetter, DataGridColumn actionSort);
    }
}
