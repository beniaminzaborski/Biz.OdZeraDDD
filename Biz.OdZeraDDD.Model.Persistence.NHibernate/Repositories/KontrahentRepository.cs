using Biz.OdZeraDDD.Model.DomainModel;
using Biz.OdZeraDDD.Model.Persistence.NHibernate.Infrastructure;
using Biz.OdZeraDDD.Model.Repositories;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.OdZeraDDD.Model.Persistence.NHibernate.Repositories
{
  public class KontrahentRepository : NHRepository<Kontrahent>, IKontrahentRepository
  {
    public KontrahentRepository(ISession session) : base(session) { }
  }
}
