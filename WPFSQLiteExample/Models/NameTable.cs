using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFSQLiteExample.Models
{
    public class NameTable
    {
        [PrimaryKey,NotNull,AutoIncrement]
        public int NameId { get; set; }

        [NotNull]
        public string Name { get; set; }
    }
}
