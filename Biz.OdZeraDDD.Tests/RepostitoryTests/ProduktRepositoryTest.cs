using Biz.OdZeraDDD.Model.DomainModel;
using Biz.OdZeraDDD.Model.Persistence.NHibernate.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Biz.OdZeraDDD.Tests.RepostitoryTests
{
  public class ProduktRepositoryTest : BaseRepositoryTest
  {
    public ProduktRepositoryTest(DatabaseFixture fixture) : base(fixture) { }

    [Fact]
    public void Produkt_Add_New()
    {
      // Arrange
      Produkt produkt = new Produkt
      {
        Id = new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6"),
        Nazwa = "Produkt 1",
        StawkaVAT = 0.23m,
        Symbol = "PR1",
        CzyAktywny = true,
        CenaNetto = 12.99m
      };

      // Act
      var produktRepository = new ProduktRepository(Session);
      produktRepository.Add(produkt);

      Session.Flush();

      // Assert
      Produkt savedProdukt = produktRepository.Get(new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6"));
      Assert.NotNull(savedProdukt);
      Assert.Equal(new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6"), savedProdukt.Id);
      Assert.Equal("Produkt 1", savedProdukt.Nazwa);
      Assert.Equal(0.23m, savedProdukt.StawkaVAT);
      Assert.Equal("PR1", savedProdukt.Symbol);
      Assert.Equal(true, savedProdukt.CzyAktywny);
      Assert.Equal(12.99m, savedProdukt.CenaNetto);
    }
  }
}
