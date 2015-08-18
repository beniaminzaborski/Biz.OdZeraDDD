using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biz.OdZeraDDD.Model.DTOs;
using Biz.OdZeraDDD.Model.Repositories;
using NHibernate;
using Biz.OdZeraDDD.Model.DomainModel;

namespace Biz.OdZeraDDD.Model.Services
{
  public class ProduktyService : IProduktyService
  {
    private ISession session;
    private IProduktRepository produktRepository;

    public ProduktyService(ISession session, IProduktRepository produktRepository)
    {
      this.session = session;
      this.produktRepository = produktRepository;
    }

    public void DodajProdukt(ProduktDTO produktDTO)
    {
      throw new NotImplementedException();
    }

    public void EdytujProdukt(ProduktDTO produktDTO)
    {
      throw new NotImplementedException();
    }

    public IList<ProduktDTO> PobierzListeProduktow()
    {
      IList<ProduktDTO> result = new List<ProduktDTO>();

      var listaProduktow = produktRepository.GetAll();
      listaProduktow.ToList().ForEach(p => result.Add(
        new ProduktDTO
        {
          Id = p.Id,
          CenaNetto = p.CenaNetto,
          Nazwa = p.Nazwa,
          CzyAktywny = p.CzyAktywny,
          StawkaVAT = p.StawkaVAT,
          Symbol = p.Symbol
        }
      ));

      return result;
    }

    public ProduktDTO PobierzProdukt(Guid idProduktu)
    {
      Produkt produkt = produktRepository.Get(idProduktu);

      return new ProduktDTO
      {
        Id = produkt.Id,
        CenaNetto = produkt.CenaNetto,
        Nazwa = produkt.Nazwa,
        CzyAktywny = produkt.CzyAktywny,
        StawkaVAT = produkt.StawkaVAT,
        Symbol = produkt.Symbol
      };
    }

    public void UsunProdukt(Guid idProduktu)
    {
      produktRepository.Delete(idProduktu);
    }
  }
}
