namespace testAutofac.Data.Entities
{
    public abstract class BaseEntity: BaseEntity<Guid>
    {
        protected BaseEntity() => Id = Guid.NewGuid();
    }
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; set; } = default!;
        public DateTime CreateAt { get; set; } = default;
    }
}