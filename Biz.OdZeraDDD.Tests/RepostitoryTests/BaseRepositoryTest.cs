using Biz.OdZeraDDD.Model.Persistence.NHibernate.SessionFactoryProviders;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Biz.OdZeraDDD.Tests.RepostitoryTests
{
  public abstract class BaseRepositoryTest : IClassFixture<DatabaseFixture>, IDisposable
  {
    DatabaseFixture fixture;

    private ISession session;
    public ISession Session { get { return session; } }

    // Initialize
    public BaseRepositoryTest(DatabaseFixture fixture)
    {
      this.fixture = fixture;
      session = InFileDatabaseSessionFactoryProvider.Instance.OpenSession();
    }

    // Cleanup
    public void Dispose()
    {
      if (session != null)
        session.Dispose();
    }
  }
}
