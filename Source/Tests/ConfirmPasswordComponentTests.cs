using System;
using System.ComponentModel;
using InquirerCS.Interfaces;
using InquirerCS.Traits;
using Should;
using Xunit;

namespace Tests
{
    [Category("Confirm")]
    public class ConfirmPasswordComponentTests : IClassFixture<ConfirmPasswordFixture<string>>
    {
        private ConfirmPasswordFixture<string> _fixture;

        public ConfirmPasswordComponentTests(ConfirmPasswordFixture<string> fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Be_Type_ConfirmListComponent()
        {
            _fixture.Confirm(_fixture, _fixture.Console);
            _fixture.Confirm.ShouldBeType(typeof(ConfirmPasswordFixture<string>));
        }

        [Fact]
        public void Always_Return_False_When_NoConfirmationComponent()
        {
            _fixture.Confirm();
            _fixture.Confirm.Confirm("1234567").ShouldBeFalse();
        }

        [Theory]
        [InlineData(ConsoleKey.Y)]
        [InlineData(ConsoleKey.Enter)]
        public void Returns_False_On_Key(ConsoleKey key)
        {
            _fixture.Confirm(_fixture, _fixture.Console);
            _fixture.ConvertToString();

            _fixture.Console.ReadKeyValue.Enqueue(new ConsoleKeyInfo((char)key, key, false, false, false));

            _fixture.Confirm.Confirm("1234567").ShouldBeFalse();
        }

        [Theory]
        [InlineData(ConsoleKey.N)]
        [InlineData(ConsoleKey.Escape)]
        public void Returns_True_On_Key(ConsoleKey key)
        {
            _fixture.Confirm(_fixture, _fixture.Console);
            _fixture.ConvertToString();

            _fixture.Console.ReadKeyValue.Enqueue(new ConsoleKeyInfo((char)key, key, false, false, false));

            _fixture.Confirm.Confirm("1234567").ShouldBeTrue();
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

            _fixture.Confirm.Confirm("1234567");

            _fixture.Console.ReadKeyValue.Count.ShouldEqual(1);
            _fixture.Console.ReadKeyValue.Peek().Key.ShouldEqual(ConsoleKey.B);
        }
    }

    public class ConfirmPasswordFixture<TResult> : IConfirmTrait<string>, IConvertToStringTrait<TResult>
    {
        public ConfirmPasswordFixture()
        {
        }

        public IConfirmComponent<string> Confirm { get; set; }

        public AssertConsole Console { get; set; } = new AssertConsole();

        public IConvertToStringComponent<TResult> Convert { get; set; }
    }
}