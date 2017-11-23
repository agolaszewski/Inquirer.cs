namespace InquirerCS.Beta
{
    public interface IUISystem
    {
        void Render(IConfirmEntity confirmComponent);

        void Render(IDisplayQuestionComponent displayQuestionComponent);

        void Render(IValidationResultComponent validationComponent);
    }
}