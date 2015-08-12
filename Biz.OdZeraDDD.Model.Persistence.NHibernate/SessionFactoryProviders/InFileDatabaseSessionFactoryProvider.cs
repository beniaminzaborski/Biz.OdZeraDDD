using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Biz.OdZeraDDD.Model.Persistence.NHibernate.SessionFactoryProviders
{
  public class InFileDatabaseSessionFactoryProvider
  {
    public static string DatabaseFileName = "testdatabase.db";
    public static Assembly MappingAssembly = Assembly.GetExecutingAssembly();

    private static InFileDatabaseSessionFactoryProvider instance;
    public static InFileDatabaseSessionFactoryProvider Instance
    {
      get { return instance ?? (instance = new InFileDatabaseSessionFactoryProvider()); }
    }

    private ISessionFactory sessionFactory;
    private Configuration configuration;

    private InFileDatabaseSessionFactoryProvider() { }

    public void Initialize()
    {
      sessionFactory = CreateSessionFactory();
    }

    private ISessionFactory CreateSessionFactory()
    {
      if (File.Exists(DatabaseFileName))
        File.Delete(DatabaseFileName);

      return Fluently.Configure()
              .Database(SQLiteConfiguration.Standard.UsingFile(DatabaseFileName))
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

      //if (File.Exists(databaseFileName))
      //  File.Delete(databaseFileName);
    }
  }
}
