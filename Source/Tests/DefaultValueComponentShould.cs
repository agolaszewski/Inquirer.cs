using System;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Traits;
using Should;
using Xunit;

namespace Tests
{
    public class DefaultValueComponentShould : IClassFixture<DefaultValueComponentFixture>
    {
        private DefaultValueComponentFixture _fixture;

        public DefaultValueComponentShould(DefaultValueComponentFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Be_Type_DefaultListValueComponent()
        {
            _fixture.Default.ShouldBeType<DefaultValueComponent<ConsoleColor>>();
        }

        [Fact]
        public void Has_Default_Value()
        {
            _fixture.Default.HasDefault.ShouldBeTrue();
            _fixture.Default.Value.ShouldEqual(ConsoleColor.Red);
        }
    }

    public class DefaultValueComponentFixture : IDefaultTrait<ConsoleColor>
    {
        public DefaultValueComponentFixture()
        {
            this.Default(ConsoleColor.Red);
        }

        public AssertConsole Console { get; set; } = new AssertConsole();

        public IDefaultValueComponent<ConsoleColor> Default { get; set; }
    }
}