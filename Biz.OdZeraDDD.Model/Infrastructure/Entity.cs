using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.OdZeraDDD.Model.Infrastructure
{
  public abstract class Entity
  {
    public virtual Guid Id { get; set; }

    public override int GetHashCode()
    {
      return Id.GetHashCode();
    }

    public override bool Equals(object obj)
    {
      Entity other = obj as Entity;
      if (other != null)
        return Id.Equals(other.Id);
      return base.Equals(obj);
    }
  }
}
