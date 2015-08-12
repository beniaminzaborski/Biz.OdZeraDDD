using Biz.OdZeraDDD.Model.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.OdZeraDDD.Model.DomainModel
{
  public class DaneAdresowe : Entity
  {
    public virtual string KodPocztowy { get; set; }

    public virtual string Poczta { get; set; }
            
    public virtual string Miejscowosc { get; set; }
            
    public virtual string Ulica { get; set; }
            
    public virtual string NumerDomu { get; set; }
            
    public virtual string NumerLokalu { get; set; }
  }
}
