using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.OdZeraDDD.Model.Infrastructure
{
  public interface IRepository<TEntity>
      where TEntity : Entity
  {
    /// <summary>
    /// Pobiera wszystkie obiekty.
    /// </summary>
    /// <returns>Lista encji domenowych</returns>
    IEnumerable<TEntity> GetAll();

    /// <summary>
    /// Pobiera obiekty według zadanego filtra z ewentualnym sortowaniem elementów.
    /// </summary>
    /// <param name="filter">Kryteria</param>
    /// <param name="orderBy">Sortowanie</param>
    /// <returns>Lista encji domenowych</returns>
    IEnumerable<TEntity> GetFiltered(
      System.Linq.Expressions.Expression<Func<TEntity, bool>> filter = null,
      System.Linq.Expressions.Expression<Func<TEntity, object>> orderBy = null
    );

    /// <summary>
    /// Pobiera encję domenową po wskazanym identyfikatorze wykonując pełne załadowanie z warstwy danych.
    /// </summary>
    /// <param name="id">Identyfikator obiektu</param>
    /// <returns>Encja domenowa</returns>
    TEntity Get(Guid id);

    /// <summary>
    /// Pobiera referencję (obiekt proxy) do encji domenowej po wskazanym identyfikatorze.
    /// </summary>
    /// <param name="id">Identyfikator obiektu</param>
    /// <returns>Encja domenowa</returns>
    TEntity Load(Guid id);

    /// <summary>
    /// Dodaje encję domenową.
    /// </summary>
    /// <param name="entity">Encja domenowa</param>
    /// <returns>Identyfikator dodanej encji domenowej</returns>
    Guid Add(TEntity entity);

    /// <summary>
    /// Zapisuje zamiany na istniejącej encji domenowej.
    /// </summary>
    /// <param name="entity">Encja domenowa</param>
    void Update(TEntity entity);

    /// <summary>
    /// Usuwa encję domenową po wskazanym identyfikatorze.
    /// </summary>
    /// <param name="id">Identyfikator obiektu</param>
    void Delete(Guid id);

    /// <summary>
    /// Usuwa przekazaną encję domenową.
    /// </summary>
    /// <param name="entity">Encja domenowa</param>
    void Delete(TEntity entity);
  }
}
