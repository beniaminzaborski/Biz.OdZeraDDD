using Biz.OdZeraDDD.Model.Infrastructure;
using NHibernate;
using Seterlund.CodeGuard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.OdZeraDDD.Model.Persistence.NHibernate.Infrastructure
{
  public abstract class NHRepository<TEntity> : IRepository<TEntity>
      where TEntity : Entity
  {
    protected ISession session;

    public NHRepository(ISession session)
    {
      Guard.That(session).IsNotNull();

      this.session = session;
    }

    public IEnumerable<TEntity> GetAll()
    {
      return session.QueryOver<TEntity>().List();
    }

    public IEnumerable<TEntity> GetFiltered(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter = null, System.Linq.Expressions.Expression<Func<TEntity, object>> orderBy = null)
    {
      Guard.That(filter).IsNotNull();

      if (orderBy == null)
        return session.QueryOver<TEntity>()
        .Where(filter)
        .List();

      return session.QueryOver<TEntity>()
        .Where(filter)
        .OrderBy(orderBy)
        .Asc
        .List();
    }

    public TEntity Get(Guid id)
    {
      return session.Get<TEntity>(id);
    }

    public TEntity Load(Guid id)
    {
      return session.Load<TEntity>(id);
    }

    public Guid Add(TEntity entity)
    {
      return (Guid)session.Save(entity);
    }

    public void Update(TEntity entity)
    {
      session.Update(entity);
    }

    public void Delete(Guid id)
    {
      var entity = session.Load<TEntity>(id);
      session.Delete(entity);
    }

    public void Delete(TEntity entity)
    {
      session.Delete(entity);
    }
  }
}
