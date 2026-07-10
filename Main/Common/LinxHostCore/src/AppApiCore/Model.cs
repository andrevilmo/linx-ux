using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AppApiCore
{
    public class CLIENTE
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }

    public class VendasContext
    {
        public IQueryable<CLIENTE> CLIENTE {

            get
            {
                return (new CLIENTE[] {
                    new CLIENTE
                    {
                        Id = 1,
                        Nome = "Alessandro"
                    },
                    new CLIENTE
                    {
                        Id = 2,
                        Nome = "Nadja"
                    },
                    new CLIENTE
                    {
                        Id = 3,
                        Nome = "Anderson"
                    }
                }).AsQueryable();

            }

        }
    }
}
