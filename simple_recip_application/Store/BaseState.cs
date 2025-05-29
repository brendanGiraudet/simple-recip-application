namespace simple_recip_application.Store;

public abstract record class BaseState<T>
{
    public bool IsLoading { get; set; } = false;
    public int Take { get; set; } = 10;
    public int Skip { get; set; } = 0;
    public IEnumerable<T> Items { get; set; } = [];
    public IEnumerable<T> SelectedItems { get; set; } = [];
    public T Item { get; set; } = default!;
}