namespace InquirerCS.Interfaces
{
    public interface IRenderchoices<TResult>
    {
        void Render();

        void Select(int index);
    }
}
