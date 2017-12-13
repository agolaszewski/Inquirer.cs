using System.ComponentModel;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Traits;
using Should;
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
            _fixture.Confirm.ShouldBeType(typeof(ConfirmPasswordComponent));
        }

        [Fact]
        public void Returns_True_When_Strings_Doesnt_Match()
        {
            _fixture.Console.ReadValue = "12345678";

            _fixture.Confirm.Confirm("1234567").ShouldBeTrue();
        }

        [Fact]
        public void Returns_False_When_Strings_Match()
        {
            _fixture.Console.ReadValue = "1234567";

            _fixture.Confirm.Confirm("1234567").ShouldBeFalse();
        }
    }

    public class ConfirmPasswordFixture<TResult> : IConfirmTrait<string>, IConvertToStringTrait<TResult>
    {
        public ConfirmPasswordFixture()
        {
            Confirm = new ConfirmPasswordComponent(Console);
            this.ConvertToString();
        }

        public IConfirmComponent<string> Confirm { get; set; }

        public AssertConsole Console { get; set; } = new AssertConsole();

        public IConvertToStringComponent<TResult> Convert { get; set; }
    }
}