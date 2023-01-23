using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandAPI.Tests
{
    public class CommanTests : IDisposable
    {
        Command testCommand;

        public CommanTests()
        {
            testCommand = new Command()
            {
                HowTo = "Do Something",
                Platform = "xUnit",
                CommandLine = "dotnet test"
            };
        }
        [Fact]
        public void CanChangeHowTo()
        {

            testCommand.HowTo = "Execute Unit Tests";

            Assert.Equal("Execute Unit Tests", testCommand.HowTo);
        }

        public void Dispose()
        {
            testCommand = null;
        }
    }
}
