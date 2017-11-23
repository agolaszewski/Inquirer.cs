namespace InquirerCS.Beta
{
    public class UISystem : IUISystem
    {
        public UISystem()
        {
        }

        public void Render(IConfirmEntity confirmComponent)
        {
            if (confirmComponent != null)
            {
                confirmComponent.Run();
            }
        }

        public virtual void Render(IDisplayQuestionComponent displayQuestionComponent)
        {
            if (displayQuestionComponent != null)
            {
                displayQuestionComponent.Run();
            }
        }

        public void Render(IValidationResultComponent validationComponent)
        {
            if (validationComponent != null)
            {
                validationComponent.Run();
            }
        }
    }
}