using Api.Data.Context;
using Api.Data.Implementations;
using Api.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Api.Data.Test
{
    public class UsuarioCrudCompleto : BaseTest, IClassFixture<DbTeste>
    {
        private ServiceProvider _serviceProvider;

        public UsuarioCrudCompleto(DbTeste dbTeste)
        {
            _serviceProvider = dbTeste.ServiceProvider;
        }

        [Fact(DisplayName = "CRUD de Usuário")]
        [Trait("CRUD", "UserEntity")]
        public async Task E_Possivel_Realizar_CRUD_Usuario()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                var repositorio = new UserImplementarion(context);
                var entity = new UserEntity
                {
                    Name = Faker.Name.FullName(),
                    Email = Faker.Internet.Email()
                };

                var registroCriado = await repositorio.InsertAsync(entity);
                Assert.NotNull(registroCriado);
                Assert.Equal(entity.Name, registroCriado.Name);
                Assert.Equal(entity.Email, registroCriado.Email);
                Assert.False(registroCriado.Id == Guid.Empty);

                entity.Name = Faker.Name.First();
                var registroAtualizado = await repositorio.UpdateAsync(entity);
                Assert.NotNull(registroAtualizado);
                Assert.Equal(entity.Name, registroAtualizado.Name);
                Assert.Equal(entity.Email, registroAtualizado.Email);

                var registroExiste = await repositorio.ExistAsync(registroAtualizado.Id);
                Assert.True(registroExiste);

                var registroSelecionado = await repositorio.SelectAsync(registroAtualizado.Id);
                Assert.NotNull(registroSelecionado);
                Assert.Equal(registroAtualizado.Name, registroSelecionado.Name);
                Assert.Equal(registroAtualizado.Email, registroSelecionado.Email);

                var todosRegistros = await repositorio.SelectAsync();
                Assert.NotNull(todosRegistros);
                Assert.True(todosRegistros.Count() > 0);

                var registroDeletado = await repositorio.DeleteAsync(registroSelecionado.Id);
                Assert.True(registroDeletado);

                var usuarioPadrao = await repositorio.FindByLogin("admin@example.com");
                Assert.NotNull(usuarioPadrao);
                Assert.Equal("Admin", usuarioPadrao.Name);
                Assert.Equal("admin@example.com", usuarioPadrao.Email);
            }
        }
    }
}
