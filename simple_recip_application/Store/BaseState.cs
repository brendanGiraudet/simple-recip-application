namespace simple_recip_application.Store;

public abstract record class BaseState
{
    public bool IsLoading { get; set; } = false;
}