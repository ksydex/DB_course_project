using System;
using System.ComponentModel.DataAnnotations.Schema;
using ContractAndProjectManager.Infrastructure.Interfaces;
using ContractAndProjectManager.Models;

namespace ContractAndProjectManager.Infrastructure.AbstractClasses
{
    public abstract class StatusHistory<T, T2> : IWithDateCreated
        where T : Status
        where T2: class
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int StatusId { get; set; }
        public virtual T Status { get; set; }

        public int EntityId { get; set; }
        public virtual T2 Entity { get; set; }

        public DateTime DateCreated { get; set; }

        protected StatusHistory()
        {
            DateCreated = DateTime.UtcNow;
        }
    }
    
    // CREATE OR REPLACE FUNCTION add_status_to_entity_trigger()
    // RETURNS TRIGGER
    // AS $$
    // BEGIN
    // EXECUTE 'INSERT INTO "' || TG_ARGV[0] || 
    // 'StatusHistories"("StatusId", "EntityId", "DateCreated") VALUES 
    // ('|| TG_ARGV[1] || ', ' || NEW."Id" || ', CURRENT_TIMESTAMP);';
    // RETURN NEW;
    // END;$$
    // LANGUAGE plpgsql;
}