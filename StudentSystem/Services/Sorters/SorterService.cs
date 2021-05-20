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
    public abstract class SorterService <T> : ISorterService<T> where T : Observable
    {
        public void Sort(IEnumerable<Observable> collection, DataGridColumn column)
        {
            SortData(collection as ObservableCollection<T>, column);
        }
        private void SortData(ObservableCollection<T> collection, DataGridColumn column)
        {
            var key = column.Header as string;
            if(Comparisons.ContainsKey(key))
            {
                int orderType = 1;
                if(column.SortDirection == null || column.SortDirection == DataGridSortDirection.Descending)
                {
                    column.SortDirection = DataGridSortDirection.Ascending;
                } else
                {
                    orderType = -1;
                    column.SortDirection = DataGridSortDirection.Descending;
                }
                var arr = new T[collection.Count];
                collection.CopyTo(arr, 0);
                Array.Sort(arr, (o1, o2) => orderType * Comparisons[key](o1, o2));
                collection.Clear();
                arr.ToList().ForEach(o => collection.Add(o));
            }
        }

        protected abstract Dictionary<string, Comparison<T>> Comparisons
        {
            get;
            set;
        }
    }
}
