namespace InquirerCS.Beta2.Interfaces
{
    public interface IRenderchoices<TResult>
    {
        void Render();

        void Select(int index);
    }
}
