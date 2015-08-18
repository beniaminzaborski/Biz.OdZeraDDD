using Biz.OdZeraDDD.Model.DomainModel;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.OdZeraDDD.Model.Persistence.NHibernate.Mappings
{
  public class ZamowienieMap : ClassMap<Zamowienie>
  {
    public ZamowienieMap()
    {
      Table("Zamowienia");
      LazyLoad();

      Id(e => e.Id).GeneratedBy.Assigned();

      Map(e => e.CzyZafakturowane);
      Map(e => e.DataZlozenia);
      Map(e => e.Numer);

      References(e => e.Kontrahent)
        .Class<Kontrahent>()
        .Column("IdKontrahenta");

      HasMany<PozycjaZamowienia>(e => e.Pozycje)
        .KeyColumn("IdZamowienia")
        .AsList(i => i.Column("Lp"))
        //.Not.LazyLoad()       
        .Cascade.AllDeleteOrphan();
    }
  }
}
