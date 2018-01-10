using System.Collections.Generic;

namespace InquirerCS
{
    public class NavigationList<T> : List<T>
    {
        private int _currentIndex = 0;

        public int CurrnetIndex
        {
            get
            {
                if (_currentIndex > Count - 1)
                {
                    _currentIndex = Count - 1;
                }

                if (_currentIndex < 0)
                {
                    _currentIndex = 0;
                }

                return _currentIndex;
            }

            set
            {
                _currentIndex = value;
            }
        }

        public T MoveNext
        {
            get
            {
                _currentIndex++;
                return this[CurrnetIndex];
            }
        }

        public T MovePrevious
        {
            get
            {
                _currentIndex--;
                return this[CurrnetIndex];
            }
        }

        public T Current
        {
            get
            {
                return this[CurrnetIndex];
            }
        }
    }
}