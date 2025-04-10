namespace simple_recip_application.Store.Actions;

public record DeleteItemsAction<T>(IEnumerable<T> Items);
public record DeleteItemsSuccessAction<T>(IEnumerable<T> Items);
public record DeleteItemsFailureAction<T>(IEnumerable<T> Items);
