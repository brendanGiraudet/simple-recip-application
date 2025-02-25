namespace simple_recip_application.Store.Actions;

public record LoadItemsAction<T>();
public record LoadItemsSuccessAction<T>(IEnumerable<T> Items);
public record LoadItemsFailureAction<T>();
