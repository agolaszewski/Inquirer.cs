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
    public class DefaultSelectedValueComponentFixture<TResult> : IDefaultTrait<List<ConsoleColor>>
    {
        public List<Selectable<ConsoleColor>> Colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().Select(x => new Selectable<ConsoleColor>(false, x)).ToList();

        public DefaultSelectedValueComponentFixture()
        {
            this.Default(Colors, new List<ConsoleColor>() { ConsoleColor.Red, ConsoleColor.Yellow });
        }

        public AssertConsole Console { get; set; } = new AssertConsole();

        public IDefaultValueComponent<List<ConsoleColor>> Default { get; set; }
    }

    public class DefaultSelectedValueComponentShould : IClassFixture<DefaultSelectedValueComponentFixture<ConsoleColor>>
    {
        private DefaultSelectedValueComponentFixture<ConsoleColor> _fixture;

        public DefaultSelectedValueComponentShould(DefaultSelectedValueComponentFixture<ConsoleColor> fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void Be_Type_DefaultListValueComponent()
        {
            _fixture.Default.Should().BeOfType<DefaultSelectedValueComponent<ConsoleColor>>();
        }

        [Fact]
        public void Has_Default_Value()
        {
            _fixture.Default.HasDefault.Should().BeTrue();
            _fixture.Colors.Where(x => x.Item == ConsoleColor.Red).First().IsSelected.Should().BeTrue();
            _fixture.Colors.Where(x => x.Item == ConsoleColor.Yellow).First().IsSelected.Should().BeTrue();
            _fixture.Colors.Where(x => x.Item != ConsoleColor.Red || x.Item != ConsoleColor.Yellow).All(x => x.IsSelected == false);
            _fixture.Colors.Where(x => x.IsSelected).Count().Should().Be(2);
        }

        [Fact]
        public void Throws_Exception_When_No_Default_Value_In_Collection()
        {
            try
            {
                _fixture.Default(
                    new List<ConsoleColor>() { ConsoleColor.Yellow }.Select(x => new Selectable<ConsoleColor>(false, x)).ToList()
                    , new List<ConsoleColor>() { ConsoleColor.Red, ConsoleColor.Yellow });
            }
            catch (Exception ex)
            {
                ex.Should().BeOfType<ArgumentNullException>();
            }
        }
    }
}
