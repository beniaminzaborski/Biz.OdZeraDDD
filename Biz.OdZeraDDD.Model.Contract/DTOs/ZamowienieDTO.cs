using Biz.OdZeraDDD.Model.Enums;
using Biz.OdZeraDDD.Model.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.OdZeraDDD.Model.DTOs
{
  [Serializable]
  public class ZamowienieDTO : DataTransferObject
  {
    public string Numer { get; set; }

    public DateTime DataZlozenia { get; set; }

    public Guid IdKontrahenta { get; set; }

    public IEnumerable<PozycjaZamowieniaDTO> Pozycje { get; set; }

    public StatusZamowienia Status { get; set; }
  }
}
