namespace Domain.Contracts
{
    public abstract class AuditableEntity<TId> : IAuditableEntity<TId>
    {
        public TId Id { get; set; } = default!;
        public string? CreatedBy { get; set; } = string.Empty;

        public string? LastModifiedBy { get; set; } = string.Empty;
        public DateTime? LastModifiedOn { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}