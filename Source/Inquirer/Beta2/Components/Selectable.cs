namespace InquirerCS.Beta2.Components
{
    public class Selectable<T>
    {
        public Selectable(bool isSelected, T item)
        {
            Item = item;
            IsSelected = isSelected;
        }

        public bool IsSelected { get; set; }

        public T Item { get; }
    }
}