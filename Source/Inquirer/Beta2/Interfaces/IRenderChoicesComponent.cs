namespace InquirerCS.Beta2.Interfaces
{
    public interface IRenderChoicesComponent<TResult>
    {
        void Render();

        void Select(int index);
    }
}
