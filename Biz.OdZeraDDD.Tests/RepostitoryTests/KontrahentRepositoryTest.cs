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
  public class KontrahentRepositoryTest : BaseInMemoryRepositoryTest //BaseRepositoryTest
  {
    //public KontrahentRepositoryTest(DatabaseFixture fixture) : base(fixture) { }

    [Fact]
    public void Kontrahent_AddNew_Created()
    {
      // Arrange
      Kontrahent kontrahent = new Kontrahent
      {
        Id = new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6"),
        Nazwa = "Kontrahent 1",
        NIP = "222-222-22-222",
        Symbol = "KTH1",
        CzyAktywny = true,
        Adres = new DaneAdresowe 
        { 
          Id = Guid.NewGuid(),
          KodPocztowy = "25-001",
          Miejscowosc = "Kielce",
          NumerDomu = "1",
          NumerLokalu = "2",
          Poczta = "Kielce",
          Ulica = "Deweloperska"
        }
      };

      var kontrahentRepository = new KontrahentRepository(Session);

      // Act
      kontrahentRepository.Add(kontrahent);

      Session.Flush();
      Session.Clear();

      // Assert
      Kontrahent savedKontrahent = Session.Get<Kontrahent>(new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6"));
      Assert.NotNull(savedKontrahent);
      Assert.Equal(new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6"), savedKontrahent.Id);
      Assert.Equal("Kontrahent 1", savedKontrahent.Nazwa);
      Assert.Equal("222-222-22-222", savedKontrahent.NIP);
      Assert.Equal("KTH1", savedKontrahent.Symbol);
      Assert.Equal(true, savedKontrahent.CzyAktywny);
      Assert.NotNull(savedKontrahent.Adres);
      Assert.Equal("25-001", savedKontrahent.Adres.KodPocztowy);
      Assert.Equal("Kielce", savedKontrahent.Adres.Miejscowosc);
      Assert.Equal("1", savedKontrahent.Adres.NumerDomu);
      Assert.Equal("2", savedKontrahent.Adres.NumerLokalu);
      Assert.Equal("Kielce", savedKontrahent.Adres.Poczta);
      Assert.Equal("Deweloperska", savedKontrahent.Adres.Ulica);
    }

    [Fact]
    public void Kontrahent_Get_Exists()
    {
      // Arrange 
      using (var tx = Session.BeginTransaction())
      {
        Session.Save(new Kontrahent
        {
          Id = new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6"),
          Nazwa = "Kontrahent 1",
          NIP = "222-222-22-222",
          Symbol = "KTH1",
          CzyAktywny = true,
          Adres = new DaneAdresowe
          {
            Id = Guid.NewGuid(),
            KodPocztowy = "25-001",
            Miejscowosc = "Kielce",
            NumerDomu = "1",
            NumerLokalu = "2",
            Poczta = "Kielce",
            Ulica = "Deweloperska"
          }
        });

        tx.Commit();
      }

      Session.Flush();
      Session.Clear();

      var kontrahentRepository = new KontrahentRepository(Session);

      // Act
      Kontrahent savedKontrahent = kontrahentRepository.Get(new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6"));

      // Assert
      Assert.NotNull(savedKontrahent);
      Assert.Equal(new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6"), savedKontrahent.Id);
      Assert.Equal("Kontrahent 1", savedKontrahent.Nazwa);
      Assert.Equal("222-222-22-222", savedKontrahent.NIP);
      Assert.Equal("KTH1", savedKontrahent.Symbol);
      Assert.Equal(true, savedKontrahent.CzyAktywny);
      Assert.NotNull(savedKontrahent.Adres);
      Assert.Equal("25-001", savedKontrahent.Adres.KodPocztowy);
      Assert.Equal("Kielce", savedKontrahent.Adres.Miejscowosc);
      Assert.Equal("1", savedKontrahent.Adres.NumerDomu);
      Assert.Equal("2", savedKontrahent.Adres.NumerLokalu);
      Assert.Equal("Kielce", savedKontrahent.Adres.Poczta);
      Assert.Equal("Deweloperska", savedKontrahent.Adres.Ulica);
    }

    [Fact]
    public void Kontrahent_Get_NotExists()
    {
      // Arrange 
      var kontrahentRepository = new KontrahentRepository(Session);

      // Act
      Kontrahent savedKontrahent = kontrahentRepository.Get(new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6"));

      // Assert
      Assert.Null(savedKontrahent);
    }

    [Fact]
    public void Kontrahent_GetAll_Exists()
    {
      // Arrange 
      using (var tx = Session.BeginTransaction())
      {
        Session.Save(new Kontrahent
        {
          Id = new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6"),
          Nazwa = "Kontrahent 1",
          NIP = "222-222-22-222",
          Symbol = "KTH1",
          CzyAktywny = true,
          Adres = new DaneAdresowe
          {
            Id = Guid.NewGuid(),
            KodPocztowy = "25-001",
            Miejscowosc = "Kielce",
            NumerDomu = "1",
            NumerLokalu = "2",
            Poczta = "Kielce",
            Ulica = "Deweloperska"
          }
        });

        Session.Save(new Kontrahent
        {
          Id = new Guid("91D21289-63AD-478B-9833-ED22F1AE7656"),
          Nazwa = "Kontrahent 2",
          NIP = "333-333-33-333",
          Symbol = "KTH2",
          CzyAktywny = false,
          Adres = new DaneAdresowe
          {
            Id = Guid.NewGuid(),
            KodPocztowy = "00-002",
            Miejscowosc = "Warszawa",
            NumerDomu = "300",
            NumerLokalu = "7",
            Poczta = "Warszawa",
            Ulica = "Testowa"
          }
        });

        tx.Commit();
      }

      Session.Flush();
      Session.Clear();

      var kontrahentRepository = new KontrahentRepository(Session);

      // Act
      IEnumerable<Kontrahent> savedKontrahentList = kontrahentRepository.GetAll();

      // Assert
      Assert.NotNull(savedKontrahentList);
      Assert.Equal(2, savedKontrahentList.Count());
    }

    [Fact]
    public void Kontrahent_GetAll_EmptyList()
    {
      // Arrange 
      var kontrahentRepository = new KontrahentRepository(Session);

      // Act
      IEnumerable<Kontrahent> savedKontrahentList = kontrahentRepository.GetAll();

      // Assert
      Assert.NotNull(savedKontrahentList);
      Assert.Equal(0, savedKontrahentList.Count());
    }

    [Fact]
    public void Kontrahent_RemoveExistedById_Removed()
    {
      // Arrange 
      using (var tx = Session.BeginTransaction())
      {
        Session.Save(new Kontrahent
        {
          Id = new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6"),
          Nazwa = "Kontrahent 1",
          NIP = "222-222-22-222",
          Symbol = "KTH1",
          CzyAktywny = true,
          Adres = new DaneAdresowe
          {
            Id = Guid.NewGuid(),
            KodPocztowy = "25-001",
            Miejscowosc = "Kielce",
            NumerDomu = "1",
            NumerLokalu = "2",
            Poczta = "Kielce",
            Ulica = "Deweloperska"
          }
        });

        tx.Commit();
      }

      Session.Flush();
      Session.Clear();

      var kontrahentRepository = new KontrahentRepository(Session);

      // Act
      kontrahentRepository.Delete(new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6"));

      // Assert
      Kontrahent savedKontrahent = Session.Get<Kontrahent>(new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6"));
      Assert.Null(savedKontrahent);
    }

    [Fact]
    public void Kontrahent_RemoveNonExistedById_Fail()
    {
      // Arrange 
      var kontrahentRepository = new KontrahentRepository(Session);

      // Act
      Assert.Throws<ObjectNotFoundException>(() => kontrahentRepository.Delete(new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6")));
    }
  }
}
