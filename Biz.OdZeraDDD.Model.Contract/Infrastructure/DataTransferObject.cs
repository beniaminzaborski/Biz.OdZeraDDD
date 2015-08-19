using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biz.OdZeraDDD.Model.Infrastructure
{
  [Serializable]
  public abstract class DataTransferObject
  {
    public Guid Id { get; set; }
  }
}
