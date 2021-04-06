using System.Collections.Generic;

namespace InquirerCS
{
    public class NavigationList<T> where T : class
    {
        private int _currentIndex = 0;

        private List<T> _list = new List<T>();

        public T Current
        {
            get
            {
                if (_currentIndex < _list.Count)
                {
                    return _list[_currentIndex];
                }

                return null;
            }
        }

        public T Next
        {
            get
            {
                if (_currentIndex + 1 < _list.Count)
                {
                    _currentIndex++;
                    return _list[_currentIndex];
                }

                return null;
            }
        }

        public T Previous
        {
            get
            {
                if (_currentIndex - 1 >= 0)
                {
                    _currentIndex--;
                    return _list[_currentIndex];
                }

                return null;
            }
        }

        public void Add(T item)
        {
            _list.Add(item);
        }
    }
}