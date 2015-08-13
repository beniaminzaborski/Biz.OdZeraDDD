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
  [Collection("Testy repozytoriów")]
  public class KontrahentRepositoryTest : BaseRepositoryTest
  {
    public KontrahentRepositoryTest(DatabaseFixture fixture) : base(fixture) { }

    [Fact]
    public void Kontrahent_Add_New()
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

      // Act
      var kontrahentRepository = new KontrahentRepository(Session);
      kontrahentRepository.Add(kontrahent);

      Session.Flush();
      Session.Clear();

      // Assert
      Kontrahent savedKontrahent = kontrahentRepository.Get(new Guid("be7bdc8f-c8fa-473a-975e-848d7600aae6"));
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
  }
}
