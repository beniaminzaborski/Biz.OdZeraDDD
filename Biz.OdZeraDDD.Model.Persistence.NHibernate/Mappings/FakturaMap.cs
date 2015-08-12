using Biz.OdZeraDDD.Model.DomainModel;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.OdZeraDDD.Model.Persistence.NHibernate.Mappings
{
  public class FakturaMap : ClassMap<Faktura>
  {
    public FakturaMap()
    {
      Table("Faktury");

      Id(e => e.Id).GeneratedBy.Assigned();

      Map(e => e.DataSprzedazy);
      Map(e => e.DataWystawienia);
      Map(e => e.Numer);

      References(e => e.Kontrahent)
        .Class<Kontrahent>()
        .Column("IdKontrahenta");

      HasMany<PozycjaFaktury>(e => e.Pozycje)
        .KeyColumn("IdFaktury")
        .AsList(i => i.Column("Lp"))
        //.Not.LazyLoad()       
        .Cascade.AllDeleteOrphan();
    }
  }
}
