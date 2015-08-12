using Biz.OdZeraDDD.Model.DomainModel;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.OdZeraDDD.Model.Persistence.NHibernate.Mappings
{
  public class PozycjaFakturyMap : ClassMap<PozycjaFaktury>
  {
    public PozycjaFakturyMap()
    {
      Table("PozycjeFaktur");

      Id(e => e.Id).GeneratedBy.Assigned();

      Map(e => e.CenaNetto);
      Map(e => e.Ilosc);
      Map(e => e.Lp);
      Map(e => e.StawkaVat);
      Map(e => e.WartoscBrutto);
      Map(e => e.WartoscNetto);
      Map(e => e.WartoscVAT);

      References(e => e.PozycjaZamowienia)
        .Class<PozycjaZamowienia>()
        .Column("IdPozycjiZamowienia");

      References(e => e.Produkt)
        .Class<Produkt>()
        .Column("IdProduktu");
    }
  }
}
