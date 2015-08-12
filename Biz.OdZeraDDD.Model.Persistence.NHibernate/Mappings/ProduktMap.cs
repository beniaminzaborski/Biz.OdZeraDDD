using Biz.OdZeraDDD.Model.DomainModel;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.OdZeraDDD.Model.Persistence.NHibernate.Mappings
{
  public class ProduktMap : ClassMap<Produkt>
  {
    public ProduktMap()
    {
      Table("Produkty");

      Id(e => e.Id).GeneratedBy.Assigned();

      Map(e => e.Nazwa);
      Map(e => e.Symbol);
      Map(e => e.CzyAktywny);
      Map(e => e.CenaNetto);
      Map(e => e.StawkaVAT);
    }
  }
}
