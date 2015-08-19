using Biz.OdZeraDDD.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.OdZeraDDD.Model.Services
{
  public interface IZamowieniaService
  {
    string UtworzZamowienie(ZamowienieDTO zamowienieDTO);

    void AnulujZamowienie(Guid idZamowienia);
  }
}
