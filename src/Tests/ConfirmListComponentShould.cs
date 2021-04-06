using System;
using System.Collections.Generic;
using System.ComponentModel;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Traits;
using Xunit;

namespace Tests
{
    [Category("Confirm")]
    public class ConfirmListComponentShould : IClassFixture<ConfirmListFixture<string>>
    {
        private ConfirmListFixture<string> _fixture;

        public ConfirmListComponentShould(ConfirmListFixture<string> fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Always_Return_False_When_NoConfirmationComponent()
        {
            _fixture.Confirm();
            Assert.False(_fixture.Confirm.Confirm(_fixture.TestValues));
        }

        [Fact]
        public void Be_Type_ConfirmListComponent()
        {
            _fixture.Confirm(_fixture, _fixture.Console);
            Assert.IsType<ConfirmListComponent<List<string>, string>>(_fixture.Confirm);
        }

        [Theory]
        [InlineData(ConsoleKey.Y)]
        [InlineData(ConsoleKey.N)]
        public void Display(ConsoleKey key)
        {
            _fixture.Confirm(_fixture, _fixture.Console);
            _fixture.ConvertToString();

            _fixture.Console.ReadKeyValue.Enqueue(new ConsoleKeyInfo((char)key, key, false, false, false));

            _fixture.Confirm.Confirm(_fixture.TestValues);
            Assert.Equal("Are you sure? [y/n] : [Yes, No, Maybe, Not Sure]", _fixture.Console.ExceptedResult);
        }

        [Theory]
        [InlineData(ConsoleKey.Y)]
        [InlineData(ConsoleKey.Enter)]
        public void Returns_False_On_Key(ConsoleKey key)
        {
            _fixture.Confirm(_fixture, _fixture.Console);
            _fixture.ConvertToString();

            _fixture.Console.ReadKeyValue.Enqueue(new ConsoleKeyInfo((char)key, key, false, false, false));

            Assert.False(_fixture.Confirm.Confirm(_fixture.TestValues));
        }

        [Theory]
        [InlineData(ConsoleKey.N)]
        [InlineData(ConsoleKey.Escape)]
        public void Returns_True_On_Key(ConsoleKey key)
        {
            _fixture.Confirm(_fixture, _fixture.Console);
            _fixture.ConvertToString();

            _fixture.Console.ReadKeyValue.Enqueue(new ConsoleKeyInfo((char)key, key, false, false, false));

            Assert.True(_fixture.Confirm.Confirm(_fixture.TestValues));
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

            _fixture.Confirm.Confirm(_fixture.TestValues);

            Assert.Single(_fixture.Console.ReadKeyValue);
            Assert.Equal(ConsoleKey.B, _fixture.Console.ReadKeyValue.Peek().Key);
        }
    }

    public class ConfirmListFixture<TResult> : IConfirmTrait<List<TResult>>, IConvertToStringTrait<TResult>
    {
        public List<string> TestValues = new List<string>() { "Yes", "No", "Maybe", "Not Sure" };

        public ConfirmListFixture()
        {
        }

        public IConfirmComponent<List<TResult>> Confirm { get; set; }

        public AssertConsole Console { get; set; } = new AssertConsole();

        public IConvertToStringComponent<TResult> Convert { get; set; }
    }
}
