using Biz.OdZeraDDD.Model.Persistence.NHibernate.SessionFactoryProviders;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Biz.OdZeraDDD.Tests.RepostitoryTests
{
  public class BaseInMemoryRepositoryTest : IDisposable
  {
    private ISession session;
    public ISession Session { get { return session; } }

    // Initialize
    public BaseInMemoryRepositoryTest()
    {
      InMemoryDatabaseSessionFactoryProvider.MappingAssembly = Assembly.LoadFrom("Biz.OdZeraDDD.Model.Persistence.NHibernate.dll");
      InMemoryDatabaseSessionFactoryProvider.Instance.Initialize();
      session = InMemoryDatabaseSessionFactoryProvider.Instance.OpenSession();
    }

    // Cleanup
    public void Dispose()
    {
      if (session != null)
        session.Dispose();

      InFileDatabaseSessionFactoryProvider.Instance.Dispose();
    }
  }
}
