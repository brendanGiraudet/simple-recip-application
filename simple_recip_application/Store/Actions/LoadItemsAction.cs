namespace simple_recip_application.Store.Actions;

public record LoadItemsAction<T>(int Take = 25, int Skip = 0);
public record LoadItemsSuccessAction<T>(IEnumerable<T> Items);
public record LoadItemsFailureAction<T>();
