using Biz.OdZeraDDD.Model.Services;
using Biz.OdZeraDDD.Model.Persistence.NHibernate.Repositories;
using Biz.OdZeraDDD.Model.Repositories;
using Biz.OdZeraDDD.Tests.RepostitoryTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Biz.OdZeraDDD.Tests.IntegrationTests
{
  public class ZamowieniaServiceTest : BaseInMemoryRepositoryTest
  {
    private IZamowieniaService zamowieniaService;
    public IZamowieniaService ZamowieniaService { get { return zamowieniaService; } }

    public ZamowieniaServiceTest()
      : base() 
    {
      IZamowienieRepository zamowienieRepository = new ZamowienieRepository(Session);
      IKontrahentRepository kontrahentRepository = new KontrahentRepository(Session);
      IProduktRepository produktRepository = new ProduktRepository(Session);
      zamowieniaService = new ZamowieniaService(Session, zamowienieRepository, kontrahentRepository, produktRepository);
    }

    [Fact]
    public void ZamowieniaService_UtworzZamowienie_Utworzone()
    { 
      // Arrange

      // Act

      // Assert

    }
  }
}
