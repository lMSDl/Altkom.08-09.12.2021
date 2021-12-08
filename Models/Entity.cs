using System;
using System.ComponentModel;

namespace Models
{
    public abstract class Entity
    {
        [DisplayName("Identyfikator")]
        public int Id { get; set; }
    }
}
