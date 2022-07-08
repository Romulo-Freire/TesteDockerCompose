using Domain.Context;
using Domain.RepositoryModel;
using Microsoft.AspNetCore.Mvc;

namespace Domain.DomainUser
{
    public class DomainUser : IDomainUser
    {
        IRepository<UserEntity> _repository;

        public DomainUser(IRepository<UserEntity> repository)
        {            
            _repository = repository;
        }

        public async Task<ActionResult<long>> Apagar()
        {
            try
            {
                var entidade = new UserEntity
                {
                    Id = 3
                };

                _repository.Apagar(entidade);
                _repository.Context.SaveChanges();

                return 1;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public async Task<ActionResult<long>> Criar()
        {
            var entidade = new UserEntity
            {
                Name = "Romulo",
                FullName = "Vieira2",
            };

            _repository.Salvar(entidade);
            _repository.Context.SaveChanges();
            return entidade.Id;
        }

        public async Task<ActionResult<List<string>>> Listar()
        {
            var result = await _repository.Listar(x => x.Id > 0);
            return result.Select(x => x.Name).ToList();
        }



        public string RetornoTeste()
        {
            return "ok";
        }
    }

    public class User
    {
        public string? Nome { get; set; }
    }
}
