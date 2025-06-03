using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkshopHub.Domain.Entities
{
    public class Interest : Entity
    {
        public string Name { get; private set; }

        public Interest(
            Guid id,
            string name
        ) : base(id)
        {
            Name = name;
        }

        public void SetName( string name ) { Name = name; }
    }
}
