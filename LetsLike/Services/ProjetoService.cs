using LetsLike.Data;
using LetsLike.Interfaces;
using LetsLike.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsLike.Services
{
    public class ProjetoService : IProjetoService
    {
        private readonly LetsLikeContext _contexto;

        public ProjetoService(LetsLikeContext contexto)
        {
            _contexto = contexto;
        }

        public Projeto LikeProketo(UsuarioLikeProjeto like)
        {
            


            /*
             TODO
            verificar se o projeto recebido existe
            se existir, adicionar +1 no likeContador
            disparar o saveOrUpdate do Projeto
            após isso
            inserir a injeção de dependencia da ProjetoLikeUsuario
            disparar o SaveOrUpdate da ProjetoLikeUsuario passando o parâmetro like
            
             
             */
        }

        public Projeto SaveOrUpdate(Projeto projeto)
        {
            // TODO primeira coisa a se fazer, validar a existencia
            // do id do usuário que está sendo passado na varíavel projeto

            if (_contexto.Usuarios.Any(e => e.Id.Equals(projeto.IdUsuarioCadastro)))
            {
                // TODO
                var estado = projeto.Id == 0 ? EntityState.Added : EntityState.Modified;
                _contexto.Entry(projeto).State = estado;
                _contexto.SaveChanges();
                return projeto;
            }
            else
            {
                return null;
            }
        }
    }
}
