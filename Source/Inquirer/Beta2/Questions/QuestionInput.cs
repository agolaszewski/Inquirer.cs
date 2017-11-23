using InquirerCS.Beta2.Interfaces;

namespace InquirerCS.Beta2.Questions
{
    public class QuestionInput<TResult> : IQuestion<TResult>
    {
        private IRenderComponent _displayQuestion;

        public QuestionInput(IRenderComponent displayQuestion)
        {
            _displayQuestion = displayQuestion;
        }

        public TResult Prompt()
        {
            _displayQuestion.Render();
            return default(TResult);
        }
    }
}