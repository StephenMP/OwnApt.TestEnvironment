using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TestEnvironment.TestResource.Objects
{
    [Table("TestTable")]
    public class TestEntity
    {
        [Key, Column("Key")]
        public int Key { get; set; }

        [Column("Value")]
        public string Value { get; set; }
    }
}
