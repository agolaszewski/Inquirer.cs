using System;
using FluentAssertions;
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
            _fixture.Default.Should().BeOfType<DefaultValueComponent<ConsoleColor>>();
        }

        [Fact]
        public void Has_Default_Value()
        {
            _fixture.Default.HasDefault.Should().BeTrue();
            _fixture.Default.Value.Should().Be(ConsoleColor.Red);
        }
    }
}
