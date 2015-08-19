using Biz.OdZeraDDD.Model.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.OdZeraDDD.Model.DTOs
{
  [Serializable]
  public class PozycjaZamowieniaDTO : DataTransferObject
  {
    public int Lp { get; set; }

    public Guid IdProduktu { get; set; }

    public int Ilosc { get; set; }

    public decimal CenaNetto { get; set; }

    public decimal StawkaVat { get; set; }
  }
}
