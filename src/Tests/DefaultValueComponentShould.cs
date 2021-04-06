using System;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Traits;
using Xunit;

namespace Tests
{
    public class DefaultValueComponentFixture : IDefaultTrait<ConsoleColor>
    {
        public DefaultValueComponentFixture()
        {
            this.Default(ConsoleColor.Red);
        }

        public AssertConsole Console { get; set; } = new AssertConsole();

        public IDefaultValueComponent<ConsoleColor> Default { get; set; }
    }

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
            Assert.IsType<DefaultValueComponent<ConsoleColor>>(_fixture.Default);
        }

        [Fact]
        public void Has_Default_Value()
        {
            Assert.True(_fixture.Default.HasDefault);
            Assert.Equal(ConsoleColor.Red, _fixture.Default.Value);
        }
    }
}
