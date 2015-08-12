using Biz.OdZeraDDD.Model.DomainModel;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.OdZeraDDD.Model.Persistence.NHibernate.Mappings
{
  public class DaneAdresoweMap : ClassMap<DaneAdresowe>
  {
    public DaneAdresoweMap()
    {
      Table("DaneAdresowe");

      Id(e => e.Id).GeneratedBy.Assigned();

      Map(e => e.KodPocztowy);
      Map(e => e.Miejscowosc);
      Map(e => e.NumerDomu);
      Map(e => e.NumerLokalu);
      Map(e => e.Poczta);
      Map(e => e.Ulica);
    }
  }
}
