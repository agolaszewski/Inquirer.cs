using System.Collections.Generic;
using System.Linq;
using InquirerCS.Interfaces;

namespace InquirerCS.Components
{
    internal class PagingComponent<TResult> : IPagingComponent<TResult>
    {
        private int _page;

        public PagingComponent(List<TResult> choices, int pageSize, int page = 0)
        {
            _page = page;

            while (page * pageSize < choices.Count)
            {
                PagedChoices.Add(page, choices.Skip(page * pageSize).Take(pageSize).ToList());
                page += 1;
            }
        }

        public List<TResult> CurrentPage
        {
            get
            {
                return PagedChoices[CurrentPageNumber];
            }
        }

        public int CurrentPageNumber
        {
            get
            {
                return _page;
            }
        }

        public Dictionary<int, List<TResult>> PagedChoices { get; } = new Dictionary<int, List<TResult>>();

        public bool Next()
        {
            if (PagedChoices.ContainsKey(_page + 1))
            {
                _page += 1;
                return true;
            }

            return false;
        }

        public bool Previous()
        {
            if (_page - 1 >= 0)
            {
                _page -= 1;
                return true;
            }

            return false;
        }
    }
}
