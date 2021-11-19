using LetsLike.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetsLike.Configurations
{
    public class UsuarioLikeProjetoConfiguration : IEntityTypeConfiguration<UsuarioLikeProjeto>
    {
        public void Configure(EntityTypeBuilder<UsuarioLikeProjeto> builder)
        {
            // TODO cria a primary key + indexes cluesterizados
            builder.HasKey(x => x.Id);

            // TODO criando a FK vinculando o objeto virtual que foi criado na tabela de usuário
            builder.HasOne(fk => fk.UsuarioLike)
                .WithMany(fk => fk.UsuarioLikeProjeto)
                .HasForeignKey(fk => fk.IdUsuarioLike)
                // todo FORÇA CRIAR O NOME COM BASE NO QUE VC ESTA PASSANDO
                .HasConstraintName("FK_USUARIO_USUARIO_LIKE_PROJETO");

            // TODO criando a FK vinculando o objeto virtual que foi criado na tabela de usuário
            builder.HasOne(fk => fk.ProjetoLike)
                .WithMany(fk => fk.ProjetoLikeUsuario)
                .HasForeignKey(fk => fk.IdProjetoLike)
                // todo FORÇA CRIAR O NOME COM BASE NO QUE VC ESTA PASSANDO
                .HasConstraintName("FK_PROJETO_USUARIO_LIKE_PROJETO");
        }
    }
}
