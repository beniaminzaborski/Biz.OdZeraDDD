using Biz.OdZeraDDD.Model.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.OdZeraDDD.Model.DomainModel
{
  public class PozycjaZamowienia : Entity
  {
    public virtual int Lp { get; set; }

    public virtual Produkt Produkt { get; set; }

    public virtual int Ilosc { get; set; }

    public virtual decimal CenaNetto { get; set; }

    public virtual decimal StawkaVat { get; set; }
  }
}
