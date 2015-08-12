using Biz.OdZeraDDD.Model.DomainModel;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.OdZeraDDD.Model.Persistence.NHibernate.Mappings
{
  public class PozycjaZamowieniaMap : ClassMap<PozycjaZamowienia>
  {
    public PozycjaZamowieniaMap()
    {
      Table("PozycjeZamowien");

      Id(e => e.Id).GeneratedBy.Assigned();

      Map(e => e.Lp);
      Map(e => e.Ilosc);
      Map(e => e.StawkaVat);
      Map(e => e.CenaNetto);

      References(e => e.Produkt)
        .Class<Produkt>()
        .Column("IdProduktu");
    }
  }
}
