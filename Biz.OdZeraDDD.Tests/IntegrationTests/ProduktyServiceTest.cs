using Biz.OdZeraDDD.Model.DomainModel;
using Biz.OdZeraDDD.Model.DTOs;
using Biz.OdZeraDDD.Model.Persistence.NHibernate.Repositories;
using Biz.OdZeraDDD.Model.Repositories;
using Biz.OdZeraDDD.Model.Services;
using Biz.OdZeraDDD.Tests.RepostitoryTests;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Biz.OdZeraDDD.Tests.InterationTests
{
  public class ProduktyServiceTest : BaseInMemoryRepositoryTest
  {
    private IProduktyService produktyService;
    public IProduktyService ProduktyService { get { return produktyService; } }

    public ProduktyServiceTest() : base() 
    {
      IProduktRepository produktRepository = new ProduktRepository(Session);
      produktyService = new ProduktyService(Session, produktRepository);
    }

    [Fact]
    public void ProduktyService_DodajNowy_Dodany()
    {
      // Arrange

      // Act
      ProduktDTO produktDTO = new ProduktDTO
      {
        Id = new Guid("BC2B8B02-6069-40AD-BD59-2D243E6B0213"),
        CenaNetto = 0.99m,
        CzyAktywny = true,
        Nazwa = "Produkt z DTO 1",
        StawkaVAT = 0.23m,
        Symbol = "PD1"
      };
      produktyService.DodajProdukt(produktDTO);

      Session.Flush();
      Session.Clear();

      // Assert
      Produkt produkt = Session.Get<Produkt>(new Guid("BC2B8B02-6069-40AD-BD59-2D243E6B0213"));
      Assert.NotNull(produkt);
      Assert.Equal(new Guid("BC2B8B02-6069-40AD-BD59-2D243E6B0213"), produkt.Id);
      Assert.Equal(0.99m, produkt.CenaNetto);
      Assert.Equal(true, produkt.CzyAktywny);
      Assert.Equal("Produkt z DTO 1", produkt.Nazwa);
      Assert.Equal(0.23m, produkt.StawkaVAT);
      Assert.Equal("PD1", produkt.Symbol);
    }

    [Fact]
    public void ProduktyService_EdytujIstniejacy_Zmieniony()
    { 
      // Arrange
      using (var tx = Session.BeginTransaction())
      {
        Session.Save(new Produkt
        {
          Id = new Guid("BC2B8B02-6069-40AD-BD59-2D243E6B0213"),
          CenaNetto = 0.99m,
          CzyAktywny = true,
          Nazwa = "Produkt z DTO 1",
          StawkaVAT = 0.23m,
          Symbol = "PD1"
        });

        tx.Commit();
      }

      Session.Flush();
      Session.Clear();

      // Act
      ProduktDTO produktDTO = new ProduktDTO
      {
        Id = new Guid("BC2B8B02-6069-40AD-BD59-2D243E6B0213"),
        CenaNetto = 1.05m,
        CzyAktywny = false,
        Nazwa = "Produkt z DTO 1.1",
        StawkaVAT = 0.23m,
        Symbol = "PD1.1"
      };
      produktyService.EdytujProdukt(produktDTO);

      Session.Flush();
      Session.Clear();

      // Assert
      Produkt produkt = Session.Get<Produkt>(new Guid("BC2B8B02-6069-40AD-BD59-2D243E6B0213"));
      Assert.NotNull(produkt);
      Assert.Equal(new Guid("BC2B8B02-6069-40AD-BD59-2D243E6B0213"), produkt.Id);
      Assert.Equal(1.05m, produkt.CenaNetto);
      Assert.Equal(false, produkt.CzyAktywny);
      Assert.Equal("Produkt z DTO 1.1", produkt.Nazwa);
      Assert.Equal(0.23m, produkt.StawkaVAT);
      Assert.Equal("PD1.1", produkt.Symbol);
    }

    [Fact]
    public void ProduktyService_PobierzIstniejacy_Pobrany()
    {
      // Arrange
      using (var tx = Session.BeginTransaction())
      {
        Session.Save(new Produkt
        {
          Id = new Guid("BC2B8B02-6069-40AD-BD59-2D243E6B0213"),
          CenaNetto = 0.99m,
          CzyAktywny = true,
          Nazwa = "Produkt z DTO 1",
          StawkaVAT = 0.23m,
          Symbol = "PD1"
        });

        tx.Commit();
      }

      Session.Flush();
      Session.Clear();

      // Act
      ProduktDTO produktDTO = produktyService.PobierzProdukt(new Guid("BC2B8B02-6069-40AD-BD59-2D243E6B0213"));

      // Assert
      Assert.NotNull(produktDTO);
      Assert.Equal(new Guid("BC2B8B02-6069-40AD-BD59-2D243E6B0213"), produktDTO.Id);
      Assert.Equal(0.99m, produktDTO.CenaNetto);
      Assert.Equal(true, produktDTO.CzyAktywny);
      Assert.Equal("Produkt z DTO 1", produktDTO.Nazwa);
      Assert.Equal(0.23m, produktDTO.StawkaVAT);
      Assert.Equal("PD1", produktDTO.Symbol);
    }

    [Fact]
    public void ProduktyService_PobierzListe_Pobrana()
    {
      // Arrange
      using (var tx = Session.BeginTransaction())
      {
        Session.Save(new Produkt
        {
          Id = new Guid("BC2B8B02-6069-40AD-BD59-2D243E6B0213"),
          CenaNetto = 0.99m,
          CzyAktywny = true,
          Nazwa = "Produkt z DTO 1",
          StawkaVAT = 0.23m,
          Symbol = "PD1"
        });

        tx.Commit();
      }

      Session.Flush();
      Session.Clear();

      // Act
      IList<ProduktDTO> listaProduktow = produktyService.PobierzListeProduktow();

      // Assert
      Assert.NotNull(listaProduktow);
      Assert.Equal(1, listaProduktow.Count);
    }

    [Fact]
    public void ProduktyService_UsunIstniejacy_Usuniety()
    {
      // Arrange
      using (var tx = Session.BeginTransaction())
      {
        Session.Save(new Produkt
        {
          Id = new Guid("BC2B8B02-6069-40AD-BD59-2D243E6B0213"),
          CenaNetto = 0.99m,
          CzyAktywny = true,
          Nazwa = "Produkt z DTO 1",
          StawkaVAT = 0.23m,
          Symbol = "PD1"
        });

        tx.Commit();
      }

      Session.Flush();
      Session.Clear();

      // Act
      produktyService.UsunProdukt(new Guid("BC2B8B02-6069-40AD-BD59-2D243E6B0213"));

      // Assert
      Produkt produkt = Session.Get<Produkt>(new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6"));
      Assert.Null(produkt);
    }
    
    [Fact]
    public void ProduktyService_UsunNieistniejacy_Blad()
    {
      // Arrange

      // Act
      Assert.Throws<ObjectNotFoundException>(() => produktyService.UsunProdukt(new Guid("BC2B8B02-6069-40AD-BD59-2D243E6B0213")));

      // Assert
    }
  }
}
