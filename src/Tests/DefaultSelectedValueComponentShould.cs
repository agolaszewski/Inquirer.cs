using System;
using System.Collections.Generic;
using System.Linq;
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
            Assert.IsType<DefaultSelectedValueComponent<ConsoleColor>>(_fixture.Default);
        }

        [Fact]
        public void Has_Default_Value()
        {
            Assert.True(_fixture.Default.HasDefault);
            Assert.True(_fixture.Colors.Where(x => x.Item == ConsoleColor.Red).First().IsSelected);
            Assert.True(_fixture.Colors.Where(x => x.Item == ConsoleColor.Yellow).First().IsSelected);
            Assert.All(
                _fixture.Colors
                    .Where(x => x.Item != ConsoleColor.Red && x.Item != ConsoleColor.Yellow),
                i => Assert.False(i.IsSelected));
            Assert.Equal(2, _fixture.Colors.Where(x => x.IsSelected).Count());
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
                Assert.IsType<ArgumentNullException>(ex);
            }
        }
    }
}
