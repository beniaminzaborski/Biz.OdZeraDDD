using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.OdZeraDDD.Model.DTOs
{
  [Serializable]
  public class ProduktDTO
  {
    public Guid? Id { get; set; }

    public string Nazwa { get; set; }

    public string Symbol { get; set; }

    public bool CzyAktywny { get; set; }

    public decimal CenaNetto { get; set; }

    public decimal StawkaVAT { get; set; }
  }
}
