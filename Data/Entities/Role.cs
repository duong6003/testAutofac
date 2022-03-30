namespace testAutofac.Data.Entities
{
    public class Role : BaseEntity
    {
        public string? Name { get; set; }
        public virtual List<User>? Users { get; set; }
        public virtual List<RolePermission>? RolePermissions { get; set; }
    }
}