using Biz.OdZeraDDD.Model.DomainModel;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.OdZeraDDD.Model.Persistence.NHibernate.Mappings
{
  public class KontrahentMap : ClassMap<Kontrahent>
  {
    public KontrahentMap()
    {
      Table("Kontrahenci");

      Id(e => e.Id).GeneratedBy.Assigned();

      Map(e => e.Nazwa);
      Map(e => e.Symbol);
      Map(e => e.NIP);
      Map(e => e.CzyAktywny);

      References(e => e.Adres)
        .Class<DaneAdresowe>()
        .Column("IdAdresu")
        .Cascade.All();
    }
  }
}
