using Biz.OdZeraDDD.Model.Services;
using Biz.OdZeraDDD.Model.DomainModel;
using Biz.OdZeraDDD.Model.DTOs;
using Biz.OdZeraDDD.Model.Repositories;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Omu.ValueInjecter;
using Biz.OdZeraDDD.Model.Enums;

namespace Biz.OdZeraDDD.Model.Services
{
  public class ZamowieniaService : IZamowieniaService
  {
    private ISession session;
    private IZamowienieRepository zamowienieRepository;
    private IKontrahentRepository kontrahentRepository;
    private IProduktRepository produktRepository;

    public ZamowieniaService(
      ISession session, 
      IZamowienieRepository zamowienieRepository, 
      IKontrahentRepository kontrahentRepository, 
      IProduktRepository produktRepository)
    {
      this.session = session;
      this.zamowienieRepository = zamowienieRepository;
      this.kontrahentRepository = kontrahentRepository;
      this.produktRepository = produktRepository;
    }

    private string GenerujNumer()
    {
      byte[] buffer = Guid.NewGuid().ToByteArray();
      var number = BitConverter.ToInt64(buffer, 0);
      return String.Format("{0:D8}", number);
    }

    public string UtworzZamowienie(ZamowienieDTO zamowienieDTO)
    {
      Zamowienie zamowienie = new Zamowienie();
      zamowienie.Id = zamowienieDTO.Id;
      zamowienie.Kontrahent = kontrahentRepository.Get(zamowienieDTO.IdKontrahenta);
      zamowienie.Numer = GenerujNumer();
      zamowienie.Status = StatusZamowienia.Nowe;
      zamowienie.DataZlozenia = DateTime.Now.Date;
      IList<PozycjaZamowienia> pozycjeZamowien = new List<PozycjaZamowienia>();
      zamowienie.Pozycje = pozycjeZamowien;
      foreach (var pozycjaZamowieniaDTO in zamowienieDTO.Pozycje)
      {
        Produkt produkt = produktRepository.Get(pozycjaZamowieniaDTO.IdProduktu);

        pozycjeZamowien.Add(new PozycjaZamowienia
        {
          Id = pozycjaZamowieniaDTO.Id,
          Ilosc = pozycjaZamowieniaDTO.Ilosc,
          Lp = pozycjaZamowieniaDTO.Lp,
          Produkt = produkt,
          CenaNetto = produkt.CenaNetto,
          StawkaVat = produkt.StawkaVAT
        });
      }

      zamowienieRepository.Add(zamowienie);

      return zamowienie.Numer;
    }

    public void AnulujZamowienie(Guid idZamowienia)
    {
      Zamowienie zamowienie = zamowienieRepository.Get(idZamowienia);
      zamowienie.Status = StatusZamowienia.Anulowane;
      zamowienieRepository.Update(zamowienie);
    }
  }
}
