using System;
using System.ComponentModel;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Traits;
using Xunit;

namespace Tests
{
    [Category("Confirm.Password")]
    public class ConfirmPasswordComponentShould : IClassFixture<ConfirmPasswordFixture<string>>
    {
        private ConfirmPasswordFixture<string> _fixture;

        public ConfirmPasswordComponentShould(ConfirmPasswordFixture<string> fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Be_Type_ConfirmPasswordComponent()
        {
            Assert.IsType<ConfirmPasswordComponent>(_fixture.Confirm);
        }

        [Fact]
        public void Returns_False_When_Strings_Match()
        {
            _fixture.Console.ReadValue = "1234567";

            Assert.False(_fixture.Confirm.Confirm("1234567"));
        }

        [Fact]
        public void Returns_True_When_Strings_Doesnt_Match()
        {
            _fixture.Console.ReadValue = "12345678";
            _fixture.Console.ReadKeyValue.Enqueue(new ConsoleKeyInfo('A', ConsoleKey.A, false, false, false));

            Assert.True(_fixture.Confirm.Confirm("1234567"));
        }
    }

    public class ConfirmPasswordFixture<TResult> : IConfirmTrait<string>, IConvertToStringTrait<TResult>, IWaitForInputTrait<StringOrKey>
    {
        public ConfirmPasswordFixture()
        {
            Input = new StringOrKeyInputComponent(Console);
            Confirm = new ConfirmPasswordComponent(Console, this);
            this.ConvertToString();
        }

        public IConfirmComponent<string> Confirm { get; set; }

        public AssertConsole Console { get; set; } = new AssertConsole();

        public IConvertToStringComponent<TResult> Convert { get; set; }

        public IWaitForInputComponent<StringOrKey> Input { get; set; }
        

      
    }
}