using HC.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HC.Shared.Application.Interfaces
{
    public interface IPostgreDbContext : IDbContextBase
    {
        DbSet<Movie> Movies { get; set; }
        DbSet<MovieComments> MovieComments { get; set; }
    }
}
