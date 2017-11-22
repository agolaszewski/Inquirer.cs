namespace InquirerCS.Beta
{
    public interface IUIComponent
    {
        void Render();

        void Render<T>(IValidateComponent<T> validateInput);
    }
}
