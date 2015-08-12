using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Biz.OdZeraDDD.Tests
{
  public class HelloWorldTest
  {
    [Fact]
    public void HelloByNameTest()
    {
      Assert.Equal("Cześć progamista wita Cię xUnit!!!", Powitaj("progamista"));
    }

    string Powitaj(string imie)
    {
      return String.Format("Cześć {0} wita Cię xUnit!!!", imie);
    }
  }
}
