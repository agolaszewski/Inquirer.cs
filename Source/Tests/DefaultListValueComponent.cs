using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using InquirerCS.Components;
using InquirerCS.Interfaces;
using InquirerCS.Traits;
using Xunit;

namespace Tests
{
    public class DefaultListValueComponentFixture<TResult> : IDefaultTrait<TResult>, IConvertToStringTrait<TResult>
    {
        public List<ConsoleColor> Colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();

        public DefaultListValueComponentFixture()
        {
        }

        public IConfirmComponent<List<TResult>> Confirm { get; set; }

        public AssertConsole Console { get; set; } = new AssertConsole();

        public IConvertToStringComponent<TResult> Convert { get; set; }

        public IDefaultValueComponent<TResult> Default { get; set; }
    }

    public class DefaultListValueComponentShould : IClassFixture<DefaultListValueComponentFixture<ConsoleColor>>
    {
        private DefaultListValueComponentFixture<ConsoleColor> _fixture;

        public DefaultListValueComponentShould(DefaultListValueComponentFixture<ConsoleColor> fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Be_Type_DefaultListValueComponent()
        {
            _fixture.Default(_fixture.Colors, ConsoleColor.Red);
            _fixture.Default.Should().BeOfType<DefaultListValueComponent<ConsoleColor>>();
        }

        [Fact]
        public void Has_Default_Value()
        {
            _fixture.Default(_fixture.Colors, ConsoleColor.Red);

            _fixture.Default.HasDefault.Should().BeTrue();
            _fixture.Default.Value.Should().Be(ConsoleColor.Red);
            _fixture.Colors.First().Should().Be(ConsoleColor.Red);
        }

        [Fact]
        public void Throws_Exception_When_No_Default_Value_In_Collection()
        {
            try
            {
                _fixture.Default(new List<ConsoleColor>() { ConsoleColor.Green }, ConsoleColor.Red);
            }
            catch (Exception ex)
            {
                ex.Should().BeOfType<ArgumentNullException>();
            }
        }
    }
}
