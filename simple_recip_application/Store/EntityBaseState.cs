namespace simple_recip_application.Store;

public abstract record class EntityBaseState<T> : BaseState<T>
{
    public bool FormModalVisibility { get; set; } = false;
}