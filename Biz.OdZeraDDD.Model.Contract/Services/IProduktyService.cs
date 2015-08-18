using Biz.OdZeraDDD.Model.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.OdZeraDDD.Model.Services
{
  public interface IProduktyService
  {
    IList<ProduktDTO> PobierzListeProduktow();

    ProduktDTO PobierzProdukt(Guid idProduktu);

    void DodajProdukt(ProduktDTO produktDTO);

    void EdytujProdukt(ProduktDTO produktDTO);

    void UsunProdukt(Guid idProduktu);
  }
}
