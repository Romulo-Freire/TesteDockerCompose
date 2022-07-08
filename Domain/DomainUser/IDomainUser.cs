using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainUser
{
    public interface IDomainUser
    {
        public string RetornoTeste();
        public Task<ActionResult<List<string>>> Listar();
        public Task<ActionResult<long>> Criar();
        public Task<ActionResult<long>> Apagar();
    }
}
