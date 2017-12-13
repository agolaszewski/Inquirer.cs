using System;
using System.ComponentModel;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Traits;
using Should;
using Xunit;

namespace Tests
{
    [Category("Confirm")]
    public class ConfirmComponentShould : IClassFixture<ConfirmFixture<string>>
    {
        private ConfirmFixture<string> _fixture;

        public ConfirmComponentShould(ConfirmFixture<string> fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Always_Return_False_When_NoConfirmationComponent()
        {
            _fixture.Confirm();
            _fixture.Confirm.Confirm("Test").ShouldBeFalse();
        }

        [Fact]
        public void Be_Type_ConfirmComponent()
        {
            _fixture.Confirm(_fixture, _fixture.Console);
            _fixture.Confirm.ShouldBeType(typeof(ConfirmComponent<string>));
        }

        [Fact]
        public void Be_Type_NoConfirmationComponent()
        {
            _fixture.Confirm();
            _fixture.Confirm.ShouldBeType(typeof(NoConfirmationComponent<string>));
        }

        [Theory]
        [InlineData(ConsoleKey.Y)]
        [InlineData(ConsoleKey.N)]
        public void Display(ConsoleKey key)
        {
            _fixture.Confirm(_fixture, _fixture.Console);
            _fixture.ConvertToString();

            _fixture.Console.ReadKeyValue.Enqueue(new ConsoleKeyInfo((char)key, key, false, false, false));

            _fixture.Confirm.Confirm("Test");
            _fixture.Console.ExceptedResult.ShouldEqual("Are you sure? [y/n] : Test");
        }

        [Theory]
        [InlineData(ConsoleKey.Y)]
        [InlineData(ConsoleKey.Enter)]
        public void Returns_False_On_Key(ConsoleKey key)
        {
            _fixture.Confirm(_fixture, _fixture.Console);
            _fixture.ConvertToString();

            _fixture.Console.ReadKeyValue.Enqueue(new ConsoleKeyInfo((char)key, key, false, false, false));

            _fixture.Confirm.Confirm("Test").ShouldBeFalse();
        }

        [Theory]
        [InlineData(ConsoleKey.N)]
        [InlineData(ConsoleKey.Escape)]
        public void Returns_True_On_Key(ConsoleKey key)
        {
            _fixture.Confirm(_fixture, _fixture.Console);
            _fixture.ConvertToString();

            _fixture.Console.ReadKeyValue.Enqueue(new ConsoleKeyInfo((char)key, key, false, false, false));

            _fixture.Confirm.Confirm("Test").ShouldBeTrue();
        }

        [Fact]
        public void Wait_Until_Interrupt_Key_Pressed()
        {
            _fixture.Confirm(_fixture, _fixture.Console);
            _fixture.ConvertToString();

            _fixture.Console.ReadKeyValue.Enqueue(new ConsoleKeyInfo((char)ConsoleKey.E, ConsoleKey.E, false, false, false));
            _fixture.Console.ReadKeyValue.Enqueue(new ConsoleKeyInfo((char)ConsoleKey.D, ConsoleKey.D, false, false, false));
            _fixture.Console.ReadKeyValue.Enqueue(new ConsoleKeyInfo((char)ConsoleKey.A, ConsoleKey.A, false, false, false));
            _fixture.Console.ReadKeyValue.Enqueue(new ConsoleKeyInfo((char)ConsoleKey.A, ConsoleKey.F, false, false, false));
            _fixture.Console.ReadKeyValue.Enqueue(new ConsoleKeyInfo((char)ConsoleKey.Enter, ConsoleKey.Enter, false, false, false));
            _fixture.Console.ReadKeyValue.Enqueue(new ConsoleKeyInfo((char)ConsoleKey.B, ConsoleKey.B, false, false, false));

            _fixture.Confirm.Confirm("Test");

            _fixture.Console.ReadKeyValue.Count.ShouldEqual(1);
            _fixture.Console.ReadKeyValue.Peek().Key.ShouldEqual(ConsoleKey.B);
        }
    }

    public class ConfirmFixture<TResult> : IConfirmTrait<TResult>, IConvertToStringTrait<TResult>
    {
        public ConfirmFixture()
        {
        }

        public IConfirmComponent<TResult> Confirm { get; set; }

        public AssertConsole Console { get; set; } = new AssertConsole();

        public IConvertToStringComponent<TResult> Convert { get; set; }
    }
}
