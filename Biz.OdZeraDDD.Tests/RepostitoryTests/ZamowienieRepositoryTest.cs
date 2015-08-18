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
  public class ZamowienieRepositoryTest : BaseInMemoryRepositoryTest
  {
    private Produkt UtworzProdukt1()
    {
      Produkt produkt = new Produkt
      {
        Id = new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6"),
        Nazwa = "Produkt 1",
        StawkaVAT = 0.23m,
        Symbol = "PR1",
        CzyAktywny = true,
        CenaNetto = 12.99m
      };

      using (var tx = Session.BeginTransaction())
      {
        Session.Save(produkt);
        tx.Commit();
      }

      Session.Flush();
      Session.Clear();

      return produkt;
    }

    private Produkt UtworzProdukt2()
    {
      Produkt produkt = new Produkt
      {
        Id = new Guid("7bc79ed5-a97d-4d7e-8d36-a4e8f235b59f"),
        Nazwa = "Produkt 2",
        StawkaVAT = 0.08m,
        Symbol = "PR2",
        CzyAktywny = true,
        CenaNetto = 22.79m
      };

      using (var tx = Session.BeginTransaction())
      {
        Session.Save(produkt);
        tx.Commit();
      }

      Session.Flush();
      Session.Clear();

      return produkt;
    }

    private Kontrahent UtworzKontrahenta()
    {
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

      using (var tx = Session.BeginTransaction())
      {
        Session.Save(kontrahent);
        tx.Commit();
      }

      Session.Flush();
      Session.Clear();

      return kontrahent;
    }

    [Fact]
    public void Zamowienie_AddNew_Created()
    {
      // Arrange
      var produkt1 = UtworzProdukt1();
      var produkt2 = UtworzProdukt2();
      var kontrahent = UtworzKontrahenta();

      var pozycjeZamowienia = new List<PozycjaZamowienia>();
      pozycjeZamowienia.Add(new PozycjaZamowienia 
      {
        Id = new Guid("A027A2D3-E78B-4920-A1B6-5C4F58A4D109"),
        CenaNetto = 2.99m,
        Ilosc = 2,
        Lp = 0,
        Produkt = produkt1,
        StawkaVat = 0.23m
      });
      pozycjeZamowienia.Add(new PozycjaZamowienia
      {
        Id = new Guid("4C923725-107F-47B6-852D-2B1E5C7D9664"),
        CenaNetto = 13.79m,
        Ilosc = 1,
        Lp = 1,
        Produkt = produkt2,
        StawkaVat = 0.23m
      });

      Zamowienie zamowienie = new Zamowienie
      {
        Id = new Guid("66748112-6445-4CFF-8EDB-43B785A53B43"),
        CzyZafakturowane = false,
        DataZlozenia = new DateTime(2015, 8, 18),
        Numer = "ZM-001",
        Kontrahent = kontrahent,
        Pozycje = pozycjeZamowienia
      };

      var zamowienieRepository = new ZamowienieRepository(Session);

      // Act
      zamowienieRepository.Add(zamowienie);

      Session.Flush();
      Session.Clear();

      // Assert
      Zamowienie savedZamowienie = Session.Get<Zamowienie>(new Guid("66748112-6445-4CFF-8EDB-43B785A53B43"));
      Assert.NotNull(savedZamowienie);
      Assert.Equal(new Guid("66748112-6445-4CFF-8EDB-43B785A53B43"), savedZamowienie.Id);
      Assert.Equal(false, savedZamowienie.CzyZafakturowane);
      Assert.Equal(new DateTime(2015, 8, 18), savedZamowienie.DataZlozenia);
      Assert.Equal(new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6"), savedZamowienie.Kontrahent.Id);
      Assert.Equal("ZM-001", savedZamowienie.Numer);

      Assert.Equal(2, savedZamowienie.Pozycje.Count());
      var pozycjeEnumerator = savedZamowienie.Pozycje.GetEnumerator();
      pozycjeEnumerator.MoveNext();
      var savedPozycja1 = pozycjeEnumerator.Current;
      Assert.Equal(new Guid("A027A2D3-E78B-4920-A1B6-5C4F58A4D109"), savedPozycja1.Id);
      Assert.Equal(2.99m, savedPozycja1.CenaNetto);
      Assert.Equal(2, savedPozycja1.Ilosc);
      Assert.Equal(0, savedPozycja1.Lp);
      Assert.Equal(new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6"), savedPozycja1.Produkt.Id);
      Assert.Equal(0.23m, savedPozycja1.StawkaVat);

      pozycjeEnumerator.MoveNext();
      var savedPozycja2 = pozycjeEnumerator.Current;
      Assert.Equal(new Guid("4C923725-107F-47B6-852D-2B1E5C7D9664"), savedPozycja2.Id);
      Assert.Equal(13.79m, savedPozycja2.CenaNetto);
      Assert.Equal(1, savedPozycja2.Ilosc);
      Assert.Equal(1, savedPozycja2.Lp);
      Assert.Equal(new Guid("7bc79ed5-a97d-4d7e-8d36-a4e8f235b59f"), savedPozycja2.Produkt.Id);
      Assert.Equal(0.23m, savedPozycja2.StawkaVat);
    }

    
    [Fact]
    public void Zamowienie_Get_Exists()
    {
      // Arrange
      var produkt1 = UtworzProdukt1();
      var produkt2 = UtworzProdukt2();
      var kontrahent = UtworzKontrahenta();

      using (var tx = Session.BeginTransaction())
      {
        var pozycjeZamowienia = new List<PozycjaZamowienia>();
        pozycjeZamowienia.Add(new PozycjaZamowienia
        {
          Id = new Guid("A027A2D3-E78B-4920-A1B6-5C4F58A4D109"),
          CenaNetto = 2.99m,
          Ilosc = 2,
          Lp = 0,
          Produkt = produkt1,
          StawkaVat = 0.23m
        });
        pozycjeZamowienia.Add(new PozycjaZamowienia
        {
          Id = new Guid("4C923725-107F-47B6-852D-2B1E5C7D9664"),
          CenaNetto = 13.79m,
          Ilosc = 1,
          Lp = 1,
          Produkt = produkt2,
          StawkaVat = 0.23m
        });

        Session.Save(new Zamowienie
        {
          Id = new Guid("66748112-6445-4CFF-8EDB-43B785A53B43"),
          CzyZafakturowane = false,
          DataZlozenia = new DateTime(2015, 8, 18),
          Numer = "ZM-001",
          Kontrahent = kontrahent,
          Pozycje = pozycjeZamowienia
        });

        tx.Commit();
      }

      Session.Flush();
      Session.Clear();

      var zamowienietRepository = new ZamowienieRepository(Session);

      // Act
      Zamowienie savedZamowienie = zamowienietRepository.Get(new Guid("66748112-6445-4CFF-8EDB-43B785A53B43"));

      // Assert
      Assert.NotNull(savedZamowienie);
      Assert.Equal(new Guid("66748112-6445-4CFF-8EDB-43B785A53B43"), savedZamowienie.Id);
      Assert.Equal(false, savedZamowienie.CzyZafakturowane);
      Assert.Equal(new DateTime(2015, 8, 18), savedZamowienie.DataZlozenia);
      Assert.Equal(new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6"), savedZamowienie.Kontrahent.Id);
      Assert.Equal("ZM-001", savedZamowienie.Numer);

      Assert.Equal(2, savedZamowienie.Pozycje.Count());
      var pozycjeEnumerator = savedZamowienie.Pozycje.GetEnumerator();
      pozycjeEnumerator.MoveNext();
      var savedPozycja1 = pozycjeEnumerator.Current;
      Assert.Equal(new Guid("A027A2D3-E78B-4920-A1B6-5C4F58A4D109"), savedPozycja1.Id);
      Assert.Equal(2.99m, savedPozycja1.CenaNetto);
      Assert.Equal(2, savedPozycja1.Ilosc);
      Assert.Equal(0, savedPozycja1.Lp);
      Assert.Equal(new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6"), savedPozycja1.Produkt.Id);
      Assert.Equal(0.23m, savedPozycja1.StawkaVat);

      pozycjeEnumerator.MoveNext();
      var savedPozycja2 = pozycjeEnumerator.Current;
      Assert.Equal(new Guid("4C923725-107F-47B6-852D-2B1E5C7D9664"), savedPozycja2.Id);
      Assert.Equal(13.79m, savedPozycja2.CenaNetto);
      Assert.Equal(1, savedPozycja2.Ilosc);
      Assert.Equal(1, savedPozycja2.Lp);
      Assert.Equal(new Guid("7bc79ed5-a97d-4d7e-8d36-a4e8f235b59f"), savedPozycja2.Produkt.Id);
      Assert.Equal(0.23m, savedPozycja2.StawkaVat);
    }

    
    [Fact]
    public void Zamowienie_Get_NotExists()
    {
      // Arrange 
      var zamowienieRepository = new ZamowienieRepository(Session);

      // Act
      Zamowienie savedZamowienie = zamowienieRepository.Get(new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6"));

      // Assert
      Assert.Null(savedZamowienie);
    }

    [Fact]
    public void Zamowienie_GetAll_Exists()
    {
      // Arrange 
      var produkt1 = UtworzProdukt1();
      var produkt2 = UtworzProdukt2();
      var kontrahent = UtworzKontrahenta();

      using (var tx = Session.BeginTransaction())
      {
        var pozycjeZamowienia = new List<PozycjaZamowienia>();
        pozycjeZamowienia.Add(new PozycjaZamowienia
        {
          Id = new Guid("A027A2D3-E78B-4920-A1B6-5C4F58A4D109"),
          CenaNetto = 2.99m,
          Ilosc = 2,
          Lp = 0,
          Produkt = produkt1,
          StawkaVat = 0.23m
        });
        pozycjeZamowienia.Add(new PozycjaZamowienia
        {
          Id = new Guid("4C923725-107F-47B6-852D-2B1E5C7D9664"),
          CenaNetto = 13.79m,
          Ilosc = 1,
          Lp = 1,
          Produkt = produkt2,
          StawkaVat = 0.23m
        });

        Session.Save(new Zamowienie
        {
          Id = new Guid("66748112-6445-4CFF-8EDB-43B785A53B43"),
          CzyZafakturowane = false,
          DataZlozenia = new DateTime(2015, 8, 18),
          Numer = "ZM-001",
          Kontrahent = kontrahent,
          Pozycje = pozycjeZamowienia
        });

        tx.Commit();
      }

      Session.Flush();
      Session.Clear();

      var zamowienieRepository = new ZamowienieRepository(Session);

      // Act
      IEnumerable<Zamowienie> savedZamowienieList = zamowienieRepository.GetAll();

      // Assert
      Assert.NotNull(savedZamowienieList);
      Assert.Equal(1, savedZamowienieList.Count());
    }

    [Fact]
    public void Zamowienie_GetAll_EmptyList()
    {
      // Arrange 
      var zamowienieRepository = new ZamowienieRepository(Session);

      // Act
      IEnumerable<Zamowienie> savedZamowienieList = zamowienieRepository.GetAll();

      // Assert
      Assert.NotNull(savedZamowienieList);
      Assert.Equal(0, savedZamowienieList.Count());
    }

    [Fact]
    public void Zamowienie_RemoveExistedById_Removed()
    {
      // Arrange
      var produkt1 = UtworzProdukt1();
      var produkt2 = UtworzProdukt2();
      var kontrahent = UtworzKontrahenta();

      using (var tx = Session.BeginTransaction())
      {
        var pozycjeZamowienia = new List<PozycjaZamowienia>();
        pozycjeZamowienia.Add(new PozycjaZamowienia
        {
          Id = new Guid("A027A2D3-E78B-4920-A1B6-5C4F58A4D109"),
          CenaNetto = 2.99m,
          Ilosc = 2,
          Lp = 0,
          Produkt = produkt1,
          StawkaVat = 0.23m
        });
        pozycjeZamowienia.Add(new PozycjaZamowienia
        {
          Id = new Guid("4C923725-107F-47B6-852D-2B1E5C7D9664"),
          CenaNetto = 13.79m,
          Ilosc = 1,
          Lp = 1,
          Produkt = produkt2,
          StawkaVat = 0.23m
        });

        Session.Save(new Zamowienie
        {
          Id = new Guid("66748112-6445-4CFF-8EDB-43B785A53B43"),
          CzyZafakturowane = false,
          DataZlozenia = new DateTime(2015, 8, 18),
          Numer = "ZM-001",
          Kontrahent = kontrahent,
          Pozycje = pozycjeZamowienia
        });

        tx.Commit();
      }

      Session.Flush();
      Session.Clear();

      var zamowienieRepository = new ZamowienieRepository(Session);

      // Act
      zamowienieRepository.Delete(new Guid("66748112-6445-4CFF-8EDB-43B785A53B43"));

      // Assert
      Zamowienie deletedZamowienie = Session.Get<Zamowienie>(new Guid("66748112-6445-4CFF-8EDB-43B785A53B43"));
      Assert.Null(deletedZamowienie);
    }

    [Fact]
    public void Zamowienie_RemoveNonExistedById_Fail()
    {
      // Arrange 
      var zamowienieRepository = new ZamowienieRepository(Session);

      // Act
      Assert.Throws<ObjectNotFoundException>(() => zamowienieRepository.Delete(new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6")));
    }
    
  }
}
