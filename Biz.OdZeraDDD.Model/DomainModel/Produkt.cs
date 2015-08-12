using Biz.OdZeraDDD.Model.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.OdZeraDDD.Model.DomainModel
{
  public class Produkt : Entity
  {
    public virtual string Nazwa { get; set; }

    public virtual string Symbol { get; set; }

    public virtual bool CzyAktywny { get; set; }

    public virtual decimal CenaNetto { get; set; }

    public virtual decimal StawkaVAT { get; set; }
  }
}
