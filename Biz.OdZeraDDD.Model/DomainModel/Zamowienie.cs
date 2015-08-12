using Biz.OdZeraDDD.Model.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.OdZeraDDD.Model.DomainModel
{
  public class Zamowienie : Entity
  {
    public virtual string Numer { get; set; }

    public virtual DateTime DataZlozenia { get; set; }

    public virtual Kontrahent Kontrahent { get; set; }

    public virtual IEnumerable<PozycjaZamowienia> Pozycje { get; set; }

    public virtual bool CzyZafakturowane { get; set; }
  }
}
