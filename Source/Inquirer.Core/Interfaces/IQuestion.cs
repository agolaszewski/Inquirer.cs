using System;
using Inquirer.Core.Components;

namespace Inquirer.Core.Interfaces
{
    public interface IQuestion<TResult>
    {
        void Default(Func<string> defaultFn);

        void Validate(Func<TResult, bool> validateFn);

        void Input(Func<char> inputFn);

        void Prompt();
    }

    public interface Input
    {
        void Message(Func<string> messageFn);
    }

    public interface IMessage
    {
        void Message(MessageComponent messageComponent);
    }

    public class Input<TStruct> : IMessage, IQuestion<TStruct> where TStruct : struct
    {
        MessageComponent _messageComponent;
        InputComponent _inputComponent;

        public Input()
        {
            _inputComponent = new InputComponent();
        }

        public void Default(Func<string> defaultFn)
        {
            throw new NotImplementedException();
        }

        public void Message(MessageComponent messageComponent)
        {
            _messageComponent = messageComponent;
        }

        public void Prompt()
        {
            _messageComponent.Draw();
        }

        public void Validate(Func<TStruct, bool> validateFn)
        {
            throw new NotImplementedException();
        }

        void IQuestion<TStruct>.Input(Func<char> inputFn)
        {
            throw new NotImplementedException();
        }
    }

    public static class Extensions
    {
        public static TQuestion Message<TQuestion>(this TQuestion question, string message) where TQuestion : IMessage
        {
            question.Message(new MessageComponent(() => message));
            return question;
        }
    }
}