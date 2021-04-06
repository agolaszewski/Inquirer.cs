namespace InquirerCS.Components
{
    public class Selectable<T>
    {
        internal Selectable(bool isSelected, T item)
        {
            Item = item;
            IsSelected = isSelected;
        }

        public bool IsSelected { get; set; }

        public T Item { get; }
    }
}
