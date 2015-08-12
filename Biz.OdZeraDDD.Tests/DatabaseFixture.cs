using Biz.OdZeraDDD.Model.Persistence.NHibernate.SessionFactoryProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Biz.OdZeraDDD.Tests
{
  public class DatabaseFixture : IDisposable
  {
    public DatabaseFixture()
    {
      InFileDatabaseSessionFactoryProvider.DatabaseFileName = "testdatabase.db";
      InFileDatabaseSessionFactoryProvider.MappingAssembly = Assembly.LoadFrom("Biz.OdZeraDDD.Model.Persistence.NHibernate.dll");
      InFileDatabaseSessionFactoryProvider.Instance.Initialize();
    }

    public void Dispose()
    {
      InFileDatabaseSessionFactoryProvider.Instance.Dispose();
    }
  }
}
