using Biz.OdZeraDDD.Model.DomainModel;
using Biz.OdZeraDDD.Model.Persistence.NHibernate.Repositories;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Biz.OdZeraDDD.Tests.RepostitoryTests
{
  //[Collection("Testy repozytoriów")]
  public class ProduktRepositoryTest : BaseInMemoryRepositoryTest //BaseRepositoryTest
  {
    //public ProduktRepositoryTest(DatabaseFixture fixture) : base(fixture) { }
    public ProduktRepositoryTest() : base() { }

    [Fact]
    public void Produkt_AddNew_Created()
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
      Session.Clear();

      // Assert
      Produkt savedProdukt = Session.Get<Produkt>(new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6"));
      Assert.NotNull(savedProdukt);
      Assert.Equal(new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6"), savedProdukt.Id);
      Assert.Equal("Produkt 1", savedProdukt.Nazwa);
      Assert.Equal(0.23m, savedProdukt.StawkaVAT);
      Assert.Equal("PR1", savedProdukt.Symbol);
      Assert.Equal(true, savedProdukt.CzyAktywny);
      Assert.Equal(12.99m, savedProdukt.CenaNetto);
    }

    [Fact]
    public void Produkt_Get_Exists()
    {
      // Arrange 
      using (var tx = Session.BeginTransaction())
      {
        Session.Save(new Produkt
        {
          Id = new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6"),
          Nazwa = "Produkt 1",
          StawkaVAT = 0.23m,
          Symbol = "PR1",
          CzyAktywny = true,
          CenaNetto = 12.99m
        });

        tx.Commit();
      }

      Session.Flush();
      Session.Clear();

      var produktRepository = new ProduktRepository(Session);

      // Act
      Produkt savedProdukt = produktRepository.Get(new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6"));

      // Assert
      Assert.NotNull(savedProdukt);
      Assert.Equal(new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6"), savedProdukt.Id);
      Assert.Equal("Produkt 1", savedProdukt.Nazwa);
      Assert.Equal(0.23m, savedProdukt.StawkaVAT);
      Assert.Equal("PR1", savedProdukt.Symbol);
      Assert.Equal(true, savedProdukt.CzyAktywny);
      Assert.Equal(12.99m, savedProdukt.CenaNetto);
    }

    [Fact]
    public void Produkt_Get_NotExists()
    {
      // Arrange 
      var produktRepository = new ProduktRepository(Session);

      // Act
      Produkt savedProdukt = produktRepository.Get(new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6"));

      // Assert
      Assert.Null(savedProdukt);
    }

    [Fact]
    public void Produkt_GetAll_Exists()
    {
      // Arrange 
      using (var tx = Session.BeginTransaction())
      {
        Session.Save(new Produkt
        {
          Id = new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6"),
          Nazwa = "Produkt 1",
          StawkaVAT = 0.23m,
          Symbol = "PR1",
          CzyAktywny = true,
          CenaNetto = 12.99m
        });

        Session.Save(new Produkt
        {
          Id = new Guid("7bc79ed5-a97d-4d7e-8d36-a4e8f235b59f"),
          Nazwa = "Produkt 2",
          StawkaVAT = 0.08m,
          Symbol = "PR2",
          CzyAktywny = true,
          CenaNetto = 22.79m
        });

        tx.Commit();
      }

      Session.Flush();
      Session.Clear();

      var produktRepository = new ProduktRepository(Session);

      // Act
      IEnumerable<Produkt> savedProduktList = produktRepository.GetAll();

      // Assert
      Assert.NotNull(savedProduktList);
      Assert.Equal(2, savedProduktList.Count());
    }

    [Fact]
    public void Produkt_GetAll_EmptyList()
    {
      // Arrange 
      var produktRepository = new ProduktRepository(Session);

      // Act
      IEnumerable<Produkt> savedProduktList = produktRepository.GetAll();

      // Assert
      Assert.NotNull(savedProduktList);
      Assert.Equal(0, savedProduktList.Count());
    }

    [Fact]
    public void Produkt_RemoveExistedById_Removed()
    {
      // Arrange 
      using (var tx = Session.BeginTransaction())
      {
        Session.Save(new Produkt
        {
          Id = new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6"),
          Nazwa = "Produkt 1",
          StawkaVAT = 0.23m,
          Symbol = "PR1",
          CzyAktywny = true,
          CenaNetto = 12.99m
        });

        tx.Commit();
      }

      Session.Flush();
      Session.Clear();

      var produktRepository = new ProduktRepository(Session);

      // Act
      produktRepository.Delete(new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6"));

      // Assert
      Produkt savedProdukt = Session.Get<Produkt>(new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6"));
      Assert.Null(savedProdukt);
    }

    [Fact]
    public void Produkt_RemoveNonExistedById_Fail()
    {
      // Arrange 
      var produktRepository = new ProduktRepository(Session);

      // Act
      Assert.Throws<ObjectNotFoundException>(() => produktRepository.Delete(new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6")));
    }
  }
}
