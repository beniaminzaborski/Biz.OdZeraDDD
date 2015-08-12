using Biz.OdZeraDDD.Model.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.OdZeraDDD.Model.DomainModel
{
  public class Faktura : Entity
  {
    public virtual string Numer { get; set; }

    public virtual Kontrahent Kontrahent { get; set; }

    public virtual DateTime DataWystawienia { get; set; }

    public virtual DateTime DataSprzedazy { get; set; }

    public virtual IEnumerable<PozycjaFaktury> Pozycje { get; set; }

    public virtual decimal WartoscNetto { get { return this.Pozycje.Sum(p => p.WartoscNetto); } }

    public virtual decimal WartoscVAT { get { return this.Pozycje.Sum(p => p.WartoscVAT); } }

    public virtual decimal WartoscBrutto { get { return this.Pozycje.Sum(p => p.WartoscBrutto); } }
  }
}
