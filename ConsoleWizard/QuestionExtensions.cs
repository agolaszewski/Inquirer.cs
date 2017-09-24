namespace ConsoleWizard
{
    public static class QuestionExtensions
    {
        public static QuestionBase<T> WithConfirmation<T>(this QuestionBase<T> question)
        {
            question.HasConfirmation = true;
            return question;
        }

        public static QuestionBase<T> WithDefaultValue<T>(this QuestionBase<T> question, T defaultValue)
        {
            question.DefaultValue = defaultValue;
            question.HasDefaultValue = true;
            return question;
        }
    }
}