using Coboss.Persistance.Entities.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coboss.Persistance.Configuration.Abstracts
{
    public class BaseEntityTypeConfiguration<T> : IEntityTypeConfiguration<T>
        where T : BaseEntitiy
    {
        private readonly DatabaseConfiguration _databaseConfiguration;

        public BaseEntityTypeConfiguration(DatabaseConfiguration databaseConfiguration)
        {
            _databaseConfiguration = databaseConfiguration;
        }

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.ToTable(GetTableName(), _databaseConfiguration.Schema);
        }

        protected virtual string GetTableName()
        {
            return $"{nameof(T)}s";
        }
    }
}
