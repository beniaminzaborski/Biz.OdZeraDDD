using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Biz.OdZeraDDD.Model.Persistence.NHibernate.SessionFactoryProviders
{
  public class InMemoryDatabaseSessionFactoryProvider
  {
    public static Assembly MappingAssembly = Assembly.GetExecutingAssembly();

    private static InMemoryDatabaseSessionFactoryProvider instance;
    public static InMemoryDatabaseSessionFactoryProvider Instance
    {
      get { return instance ?? (instance = new InMemoryDatabaseSessionFactoryProvider()); }
    }

    private ISessionFactory sessionFactory;
    private Configuration configuration;

    private InMemoryDatabaseSessionFactoryProvider() { }

    public void Initialize()
    {
      sessionFactory = CreateSessionFactory();
    }

    private ISessionFactory CreateSessionFactory()
    {
      return Fluently.Configure()
              .Database(SQLiteConfiguration.Standard.InMemory())
              .Mappings(m => m.FluentMappings.AddFromAssembly(MappingAssembly))
              .ExposeConfiguration(cfg => configuration = cfg)
              .BuildSessionFactory();
    }

    public ISession OpenSession()
    {
      ISession session = sessionFactory.OpenSession();

      var export = new SchemaExport(configuration);
      export.Execute(true, true, false, session.Connection, null);

      return session;
    }

    public void Dispose()
    {
      if (sessionFactory != null)
        sessionFactory.Dispose();

      sessionFactory = null;
      configuration = null;
    }
  }
}
