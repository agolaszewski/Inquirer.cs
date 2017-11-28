using System.Collections.Generic;
using InquirerCS.Beta2.Components;
using InquirerCS.Beta2.Questions;

namespace InquirerCS.Beta2
{
    public static class Question
    {
        public static Checkbox<List<TResult>, TResult> Checkbox<TResult>(string message, IEnumerable<TResult> choices)
        {
            var messageComponent = new MessageComponent(message);
            var convertToStringComponent = new ConvertToStringComponent<TResult>();
            var defaultValueComponent = new DefaultValueComponent<List<TResult>>();

            var displayQuestionComponent = new DisplayListQuestion<List<TResult>, TResult>(messageComponent, convertToStringComponent, defaultValueComponent);
            var choicesComponent = new SelectableListChoices<TResult>(choices);

            var confirmComponent = new ConfirmListComponent<List<TResult>, TResult>(convertToStringComponent);
            var inputComponent = new ReadConsoleKey();
            var parseComponent = new ParseSelectableListComponent<List<TResult>, TResult>(choicesComponent);
            var renderChoicesComponent = new DisplaySelectableChoices<TResult>(choicesComponent, convertToStringComponent);
            var validateComponent = new ValidationComponent<List<TResult>>();
            var errorComponent = new DisplayErrorCompnent();

            return new Checkbox<List<TResult>, TResult>(choicesComponent, confirmComponent, displayQuestionComponent, inputComponent, parseComponent, renderChoicesComponent, validateComponent, errorComponent);
        }
    }
}
