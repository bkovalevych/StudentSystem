using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSystem.Services
{
    using Helpers;
    using System.Collections.ObjectModel;
    using System.Text.RegularExpressions;

    public abstract class FilterService<TParent> : IFilterService<TParent>
        where TParent: Observable
    {
        protected static bool eq<T>(T one, string param)
        {
            return Regex.IsMatch(one.ToString().ToLower(), param.ToLower());
        }
        public virtual IEnumerable<TParent> SearchParent(string param)
        {
            var result = new List<TParent>();
            var allParents = DbGet();
            Suggestions.Clear();
            for(int index = 0; index < ParentPredicates.Count; ++index)
            {
                var predicate = ParentPredicates[index];
                var selector = Selects[index];
                var finded = allParents.Where(o => predicate(o, param ?? ""));
                var new_suggestions = finded.Select(selector);
                Add(Suggestions, new_suggestions);
                result = result.Union(finded).ToList();
            }
            return result;
        }

        public ObservableCollection<string> Suggestions
        {
            get; set;
        } = new ObservableCollection<string>();
        public abstract List<Func<TParent, string, bool>> ParentPredicates
        {
            get; set;
        }
        public abstract List<Func<TParent, string>> Selects
        {
            get; set;
        }
        public abstract IEnumerable<TParent> DbGet();
        protected void Add<T>(ObservableCollection<T> collection, IEnumerable<T> addedValues)
        {
            foreach(T val in addedValues)
            {
                collection.Add(val);
            }
        }
    }
}
